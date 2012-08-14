<?php

/**
 * Description of LibMenu1
 *
 * @author spondbob
 */
class LibMenu1 {

    private $ci = null;

    public function __construct() {
        $this->ci = & get_instance();
        $this->ci->load->model(array('dil'));
        $this->ci->load->library(array('LibExport'));
    }

    public function getListRangeDaya($value = false) {
        if ($value) {
            return array(
                1 => array('min' => 450, 'max' => 5500),
                2 => array('min' => 6600, 'max' => 33000),
                3 => array('min' => 41500),
            );
        } else {
            return array(
                1 => '450 - 5.500',
                2 => '6.600 - 33.000',
                3 => '> 41.500',
            );
        }
    }

    public function getListRangeTglPasang($value = false) {
        if ($value) {
            return array(
                1 => array('min' => 0, 'max' => 10),
                2 => array('min' => 10, 'max' => 15),
                3 => array('min' => 15),
            );
        } else {
            return array(
                1 => '0 - 10 thn',
                2 => '10 - 15 thn',
                3 => '> 15 thn',
            );
        }
    }

    private function filter($filter) {
        $daya = $this->getListRangeDaya(true);
        $tglPasang = $this->getListRangeTglPasang(true);
        $filter['select'] = (!array_key_exists('select', $filter) ? array('IDPEL', 'NAMA', 'JENIS_MK', 'KDGARDU', 'NOTIANG') : $filter['select']);
        $filter['dayaId'] = $filter['daya'];
        $filter['tglPasangId'] = $filter['tglPasang'];
        $filter['daya'] = $daya[$filter['daya']];
        $filter['tglPasang'] = $tglPasang[$filter['tglPasang']];

        return $filter;
    }

    public function getData($filter) {
        $filter = $this->filter($filter);
        return array(
            'data' => $this->ci->dil->filterMenu1($filter),
            'num' => $this->ci->dil->count($filter),
        );
    }

    public function export($filter) {
        if (file_exists(FCPATH . 'static/export/menu1/Menu1' . $filter['area'] . $filter['daya'] . $filter['tglPasang'] . '.xls')) {
            redirect('static/export/menu1/Menu1' . $filter['area'] . $filter['daya'] . $filter['tglPasang'] . '.xls');
        }else{
            $filter['limit'] = 10000;
            $filter = $this->filter($filter);
            $export = new LibExport();
            $export->fileName = 'Menu1' . $filter['area'] . $filter['dayaId'] . $filter['tglPasangId'] . '.xls';
            $export->generateHtml($filter);
        }
    }

}

?>
