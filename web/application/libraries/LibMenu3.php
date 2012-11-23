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

    private function filter($filter) {
        $tren = $this->getListTren(true);
        //$filter['select'] = (!array_key_exists('select', $filter) ? array('IDPEL', 'NAMA', 'KDPEMBMETER', 'DAYA', 'TGLPASANG_KWH', 'KDGARDU', 'NOTIANG') : $filter['select']);
        $filter['order'] = (!array_key_exists('order', $filter) || $filter['order'] == "" ? 'DIL.IDPEL' : $filter['order']);
        $filter['tren'] = $tren[$filter['tren']];

        return $filter;
    }

    public function getData($filter) {
        $filter = $this->filter($filter);
        return $this->ci->sorek->filterMenu3($filter, true);
    }
    
    public function export($filter) {
        $fileName = $filter['controller'] . $filter['kodearea'] . $filter['tren'] . '.xls';
        if (!file_exists(FCPATH . 'static/export/' . $filter['controller'] . '/' . $fileName)) {
            $filter = $this->filter($filter);
            $filter['select'] = null;

            $export = new LibExport();
            $export->fileName = $fileName;
            $export->generate($filter);
        }
        redirect(base_url().'static/export/' . $filter['controller'] . '/' . $fileName);
    }
}

?>
