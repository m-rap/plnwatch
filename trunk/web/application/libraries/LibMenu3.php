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
        $this->ci->load->library(array('LibExport'));
    }

    public function getListTren($value = false) {
        if ($value) {
            return array(
                1 => 'naik',
                2 => 'turun',
                3 => 'flat',
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
        return array(
            'data' => $this->ci->sorek->filterMenu3($filter),
            'num' => $this->ci->sorek->countFilterMenu3($filter),
        );
    }
    
    public function export($filter) {
        $fileName = $filter['controller'] . $filter['kodearea'] . $filter['tren'] . '.xls';
        if (!file_exists(FCPATH . 'static/export/' . $filter['controller'] . '/' . $fileName)) {
            $filter['select'] = null;
            $filter['limit'] = 50000;

            $export = new LibExport();
            $export->fileName = $fileName;
            $export->generate($filter);
        }
        redirect('static/export/' . $filter['controller'] . '/' . $fileName);
    }
}

?>
