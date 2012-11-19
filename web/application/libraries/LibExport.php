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
    }

    public function generateX($filter) {
        if (!file_exists($this->wdir)) {
            mkdir($this->wdir);
        }
        $this->extract($this->template, $this->wdir);
        $this->injectDataToSpreadsheet($this->activeSheet, $filter);
        $this->compress(FCPATH . 'static/export/' . $filter['controller'] . '/' . $this->fileName, $this->wdir);
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

        $controller = $filter['controller'];
        unset($filter['controller']);
        if ($controller == 'Menu1') {
            $model = new Dil();
            $label = $model->attributeLabels();
            $select = $filter['select'];
        } else if ($controller == 'Menu2') {
            $dil = new Dil();
            $model = new Sorek();
            $label = array_merge($model->attributeLabels(), $dil->attributeLabels());
            $select = $filter['select'];
        } else if ($controller == 'Menu3') {
            $model = new Sorek();
            $select = $model->getTrenLabels();
            array_unshift($select, 'ID Pelanggan');
        } else if ($controller == 'Menu4') {
            $dph = new Dph();
            $model = new Dil();
            $label = array_merge($model->attributeLabels(), $dph->attributeLabels());
            $select = $filter['select'];
        }

        $newRow = $xml->sheetData->addChild('row');
        foreach ($select as $col) {
            $newCell = $newRow->addChild('c');
            $newCell->addAttribute('t', "inlineStr");
            $newIs = $newCell->addChild('is');
            if (!mb_check_encoding($col, 'utf-8'))
                $col = iconv("cp1250", "utf-8", $col);
            if ($controller != 'Menu3') {
                $explode = explode(' AS ', $col);
                $col = (array_key_exists(1, $explode) ? $explode[1] : $explode[0]);
                $col = $label[$col];
            }
            $newIs->addChild('t', htmlspecialchars($col));
        }

        // count n data without LIMIT
        if ($controller == 'Menu1')
            $n = $model->countFilterMenu1($filter);
        else if ($controller == 'Menu2')
            $n = $model->countFilterMenu2($filter);
        else if ($controller == 'Menu3')
            $n = $model->countFilterMenu3($filter);
        else if ($controller == 'Menu4')
            $n = $model->countFilterMenu4($filter);

        $part = $filter['limit'];
        $filter['limit'] = ($filter['limit'] > $n ? $n : $filter['limit']);
        for ($i = 0; $i <= $n + $part; $i+=$part) {
            $filter['offset'] = ($i < $n || $n == $filter['limit'] ? $i : $n);

            if ($controller == 'Menu1')
                $q = $model->filterMenu1($filter, false);   //return query object
            else if ($controller == 'Menu2')
                $q = $model->filterMenu2($filter, false);   //return query object
            else if ($controller == 'Menu3')
                $q = $model->filterMenu3($filter, false);   //return query object
            else if ($controller == 'Menu4')
                $q = $model->filterMenu4($filter, false);   //return query object

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

    public function generate($filter) {
        $controller = $filter['controller'];
        unset($filter['controller']);
        if ($controller == 'Menu1') {
            $model = new Dil();
            $label = $model->attributeLabels();
            $select = $filter['select'];
        } else if ($controller == 'Menu2') {
            $dil = new Dil();
            $model = new Sorek();
            $label = array_merge($model->attributeLabels(), $dil->attributeLabels());
            $select = $filter['select'];
        } else if ($controller == 'Menu3') {
            $model = new Sorek();
            $select = $model->getTrenLabels();
            array_unshift($select, 'ID Pelanggan');
        } else if ($controller == 'Menu4') {
            $dph = new Dph();
            $model = new Dil();
            $label = array_merge($model->attributeLabels(), $dph->attributeLabels());
            $select = $filter['select'];
        }

        $start = "<html><head></head><body><table border='1'>";
        $header = "<tr><td><strong>No</strong></td>";
        foreach ($select as $col) {
            $header .= "<td><strong>" . $label[$col] . "</strong></td>";
        }
        $header .= "</tr>";
        $body = "";

        //count all data according to filter. used to write partially.
        $countFilterMenu = 'countFilter' . $controller;
        $n = $model->$countFilterMenu($filter);

        $part = $filter['limit'];
        $filter['limit'] = ($filter['limit'] > $n ? $n : $filter['limit']);
        for ($i = 0, $no = 1; $i <= $n + $part; $i+=$part) {
            $filter['offset'] = ($i < $n || $n == $filter['limit'] ? $i : $n);

            //query data
            $filterMenu = 'filter' . $controller;
            $q = $model->$filterMenu($filter, false);

            while ($row = @mysql_fetch_object($q->result_id)) {
                $body .= "<tr><td>" . $no . "</td>";
                foreach ($row as $col) {
                    $body .= "<td>" . $col . "</td>";
                }
                $body .= "</tr>";
                $no++;
            }
        }

        $end = "</table></body></html>";

        file_put_contents('static/export/' . strtolower($controller) . '/' . $this->fileName, $start . $header . $body . $end, FILE_APPEND | LOCK_EX);
        //header('Content-type: application/ms-excel');
        //header('Content-Disposition: attachment; filename=' . $this->fileName);
        //echo $start . $header . $body . $end.die();
    }

}

?>
