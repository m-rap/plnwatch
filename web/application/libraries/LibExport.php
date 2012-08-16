<?php

/**
 * Description of LibExport
 *
 * @author spondbob
 */
class LibExport {

    private $ci = null;
    public $template = 'template.xlsx';
    public $activeSheet = 'sheet1.xml';
    public $wdir = 'wdir';
    public $fileName = 'file.xls';

    function __construct() {
        $this->ci = & get_instance();
        $this->ci->load->library(array('LibMenu1'));
        $this->ci->load->dbutil();
        $this->ci->load->model(array('dil'));

        $this->template = FCPATH . 'static/export/' . $this->template;
        $this->activeSheet = FCPATH . 'static/export/' . $this->wdir . '/xl/worksheets/' . $this->activeSheet;
        $this->wdir = FCPATH . 'static/export/' . $this->wdir;
    }

    public function generate($filter) {
        if (!file_exists($this->wdir)) {
            mkdir($this->wdir);
        }
        $this->extract($this->template, $this->wdir);
        $this->injectDataToSpreadsheet($this->activeSheet, $filter);
        $this->compress($this->fileName, $this->wdir);
        //$this->removeWdir($this->wdir);
    }

    private function extract($spreadsheet, $target) {
        $zip = new ZipArchive;
        $zip->open($spreadsheet);
        $zip->extractTo($target);
    }

    private function compressRecursive(&$zip, $path, $len) {
        $dir = opendir($path);
        while ($file = readdir($dir)) {
            if ($file != "." && $file != "..") {
                if (is_dir($path . '/' . $file)) {
                    $this->compressRecursive($zip, $path . '/' . $file, $len);
                } else {
                    $f = substr($path . '/' . $file, $len);
                    if (substr($f, 0, 1) == "/") {
                        $f = substr($f, 1, strlen($f));
                    }
                    $zip->addFile($path . '/' . $file, $f);
                }
            }
        }
    }

    private function compress($spreadsheet, $source) {
        $zip = new ZipArchive;
        $zip->open($spreadsheet, ZipArchive::CREATE);
        $this->compressRecursive($zip, $source, strlen($source));
        $zip->close();
    }

    private function injectDataToSpreadsheet($ws, $filter) {
        $worksheet = file_get_contents($ws);
        if ($worksheet === false)
            return false;

        $xml = simplexml_load_string($worksheet);
        if (!$xml)
            return false;

        $model = new Dil();
        $label = $model->attributeLabels();
        $newRow = $xml->sheetData->addChild('row');
        foreach ($filter['select'] as $col) {
            $newCell = $newRow->addChild('c');
            $newCell->addAttribute('t', "inlineStr");
            $newIs = $newCell->addChild('is');
            if (!mb_check_encoding($col, 'utf-8'))
                $col = iconv("cp1250", "utf-8", $col);
            $newIs->addChild('t', htmlspecialchars($label[$col]));
        }

        $n = $model->count($filter);
        $part = $filter['limit'];
        $filter['limit'] = ($filter['limit'] > $n ? $n : $filter['limit']);
        for ($i = 0; $i <= $n + $part; $i+=$part) {
            $filter['offset'] = ($i < $n || $n == $filter['limit'] ? $i : $n);
            $q = $model->filterMenu1($filter, false);   //return query object
            while ($row = @mysql_fetch_object($q->result_id)) {
                $newRow = $xml->sheetData->addChild('row');
                foreach ($row as $col) {
                    $newCell = $newRow->addChild('c');
                    $newCell->addAttribute('t', "inlineStr");
                    $newIs = $newCell->addChild('is');
                    if (!mb_check_encoding($col, 'utf-8'))
                        $col = iconv("cp1250", "utf-8", $col);
                    $newIs->addChild('t', htmlspecialchars($col));
                }
            }
        }
        if (file_put_contents($ws, $xml->asXML()) !== false)
            return true;
    }

    private function removeWdirRecursive($path) {
        $dir = opendir($path);
        while ($file = readdir($dir)) {
            if ($file != "." && $file != "..") {
                if (is_dir($path . '/' . $file)) {
                    $this->removeWdirRecursive($path . '/' . $file);
                } else {
                    unlink($path . '/' . $file);
                }
            }
        }
        rmdir($path);
    }

    private function removeWdir($path) {
        $this->removeWdirRecursive($path);
    }

    public function generateHtml($filter) {
        $start = "<html><head></head><body><table>";
        $model = new Dil();
        $label = $model->attributeLabels();
        $header = "<tr>";
        foreach ($filter['select'] as $col) {
            $header .= "<td>" . $label[$col] . "</td>";
        }
        $header .= "</tr>";

        $body = "";
        $n = $model->count($filter);
        $part = $filter['limit'];
        $filter['limit'] = ($filter['limit'] > $n ? $n : $filter['limit']);
        for ($i = 0; $i <= $n + $part; $i+=$part) {
            $filter['offset'] = ($i < $n || $n == $filter['limit'] ? $i : $n);
            $q = $model->filterMenu1($filter, false);   //return object
            while ($row = @mysql_fetch_object($q->result_id)) {
                $body .= "<tr>";
                foreach ($row as $col) {
                    $body .= "<td>" . $col . "</td>";
                }
                $body .= "</tr>";
            }
        }

        $end = "</table></body></html>";

        file_put_contents('static/export/' . strtolower($filter['controller']) . '/' . $this->fileName, $start . $header . $body . $end, FILE_APPEND | LOCK_EX);
        //header('Content-type: application/ms-excel');
        //header('Content-Disposition: attachment; filename=' . $this->fileName);
        //echo $start . $header . $body . $end;
    }

}

?>
