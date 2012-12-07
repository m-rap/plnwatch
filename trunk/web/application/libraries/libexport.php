<?php

/**
 * Description of LibExport
 *
 * @author spondbob
 */
class LibExport {

    private $ci = null;
    /*public $template = 'template.xlsx';
    public $activeSheet = 'sheet1.xml';
    public $wdir = 'wdir';*/
    public $fileName = 'file.xls';
    public $location = 'static/export/';
    public $BLTH = '012012';

    function __construct() {
        $this->ci = & get_instance();
        $this->ci->load->dbutil();
        $this->ci->load->model(array('dil', 'sorek', 'dph', 'option'));
        $this->ci->load->library(array('libmenu1'));
    }

    /*
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
    */

    private function removeDir($path) {
        $dir = opendir($path);
        while ($file = readdir($dir)) {
            if ($file != "." && $file != "..") {
                if (is_dir($path . '/' . $file)) {
                    $this->removeDir($path . '/' . $file);
                } else {
                    unlink($path . '/' . $file);
                }
            }
        }
        rmdir($path);
    }

    public function generate($filter) {
        $dil = new Dil();
        $sorek = new Sorek();
        $label = array_merge($dil->attributeLabels(), $sorek->attributeLabels());

        $controller = $filter['controller'];
        unset($filter['controller']);
        if ($controller == 'Menu1') {
            $model = $dil;
        } else if ($controller == 'Menu2') {
            $model = $sorek;
        } else if ($controller == 'Menu3') {
            $model = $sorek;
        } else if ($controller == 'Menu4') {
            $model = $dil;
            $dph = new Dph();
            $label = array_merge($label, $dph->attributeLabels());
        } else if ($controller == 'Menu5') {
            $model = $sorek;
        }

        //prepare select filter
        $select = array('DIL.IDPEL AS IDPEL', 'NAMA', 'TARIF', 'DAYA', 'FAKM', 'JAMNYALA', 'KDPEMBMETER', 'ALAMAT', 'KDDK', 'KDGARDU', 'NOTIANG');
        $filter['select'] = ($filter['select'] == null ? $select : array_merge($select, $filter['select']));

        //prepare html format and start labelling table header
        $start = "<html><head><title>".$this->fileName."</title></head><body><table border='1'>";
        $header = "<tr><td><strong>No</strong></td>";
        foreach($filter['select'] as $col){
            $explode = explode(' AS ', $col);
            $col = (array_key_exists(1, $explode) ? $explode[1] : $explode[0]);
            $header .= "<td style='align:center'><strong>" . $label[$col] . "</strong></td>";
        }
        $header .= "</tr>";
        $body = "";

        //condition
        $filterMenuCondition = 'filter' . ($controller == 'Menu5' ? 'Menu2' : $controller) . 'Condition';
        $filter['condition'] = $model->$filterMenuCondition($filter);

        //count all data according to filter. used to write partially.
        $n = $sorek->countExport($filter);

        //part
        $part = (array_key_exists('limit', $filter) ? $filter['limit'] : 50000);
        $filter['limit'] = ($part > $n ? $n : $part);

        //writing partially
        for ($i = 0, $no = 1; $i <= $n + $part; $i+=$part) {
            $filter['offset'] = ($i < $n || $n == $filter['limit'] ? $i : $n);

            //query data
            $q = $sorek->export($filter);
            while ($row = @mysql_fetch_object($q->result_id)) {
                $body .= "<tr><td>" . $no . "</td>";
                foreach ($row as $key => $col) {
					if($key == "KDPEMBMETER")
                        $col = $this->ci->libmenu1->translateKodePembMeter($col);
                    $body .= "<td>" . $col . "</td>";
                }
                $body .= "</tr>";
                $no++;
            }
        }

        $end = "</table></body></html>";

        // create BLTH folder if not exist
        if (!file_exists(FCPATH . $this->location)){
            mkdir(FCPATH . $this->location);
        }

        // start writing file to the specified location
        file_put_contents($this->location . $this->fileName, $start . $header . $body . $end, FILE_APPEND | LOCK_EX);
        
        // remove the old BLTH folder
        $m = substr($this->BLTH, 0, 2) - 1;
        $y = substr($this->BLTH, 2, 4);
        $y = ($m == 0 ? $y-1 : $y);
        $m = ($m == 0 ? 12 : ($m <= 9 ? '0'.$m : $m));
        if (file_exists(FCPATH . 'static/export/' . strtolower($controller) . '/' . $m.$y)){
            $this->removeDir(FCPATH . 'static/export/' . strtolower($controller) . '/' . $m.$y);
        }

        //header('Content-type: application/ms-excel');
        //header('Content-Disposition: attachment; filename=' . $this->fileName);
        //echo $start . $header . $body . $end.die();
    }

}

?>
