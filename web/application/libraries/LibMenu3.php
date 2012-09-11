<?php

/**
 * Description of LibMenu3
 *
 * @author spondbob
 */
class LibMenu3 {
    private $ci;
    
    public function __construct() {
        $this->ci =& get_instance();
    }

    public function getListTren($value = false) {
        if ($value) {
            return array(
                1 => array('min' => 450, 'max' => 5500),
                2 => array('min' => 6600, 'max' => 33000),
                3 => array('min' => 41500),
            );
        } else {
            return array(
                1 => 'Naik (> 25%)',
                2 => 'Turun (< 25%)',
                3 => 'Flat (0 - 5%)',
            );
        }
    }
    
    public function validateInput($input, $list = null) {
        if ($list == null) {
            $list = array(
                'kodearea' => $this->ci->libarea->getList(),
                'tren' => $this->getListTren(),
            );
        }

        $k = array_keys($list['kodearea']);
        $k2 = array_keys($list['tren']);
        $defaultValue = array(
            'kodearea' => $list['kodearea'][$k[0]],
            'tren' => $k2[0],
        );

        foreach (array_keys($input) as $i) {
            if (empty($input[$i]) or !array_key_exists($input[$i], $list[$i])) {
                $input[$i] = $defaultValue[$i];
            }
        }

        return $input;
    }
    
    public function getData($filter) {
        $tren = $filter['tren'];
        unset($filter['tren']);
        switch ($tren) {
            case '1':
                return $this->ci->sorek->getTrenNaik($filter);
            case '2':
                return $this->ci->sorek->getTrenTurun($filter);
            case '3':
                return $this->ci->sorek->getTrenFlat($filter);
        }
    }
    
    public function export($filter) {
        $fileName = $filter['controller'] . $filter['kodearea'] . $filter['tren'] . '.xlsx';
        if (!file_exists(FCPATH . 'static/export/' . $filter['controller'] . '/' . $fileName)) {
            $filter = $this->filter($filter);
            $filter['limit'] = 50000;

            $export = new LibExport();
            $export->fileName = $fileName;
            $export->generate($filter);
        }
        redirect('static/export/' . $filter['controller'] . '/' . $fileName);
    }
}

?>
