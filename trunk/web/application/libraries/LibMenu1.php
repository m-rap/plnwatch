<?php

/**
 * Description of LibMenu1
 *
 * @author spondbob
 */
class LibMenu1 {

    private $ci = null;
    public function __construct() {
        $this->ci =& get_instance();
        $this->ci->load->model(array('dil'));
    }

    public function getListRangeDaya($value = false) {
        if ($value) {
            return array(
                0 => array('min' => 450, 'max' => 5500),
                1 => array('min' => 6600, 'max' => 33000),
                2 => array('min' => 41500),
            );
        } else {
            return array(
                0 => '450 - 5.500',
                1 => '6.600 - 33.000',
                2 => '> 41.500',
            );
        }
    }

    public function getListRangeTglPasang($value = false) {
        if ($value) {
            return array(
                0 => array('min' => 0, 'max' => 10),
                1 => array('min' => 10, 'max' => 15),
                2 => array('min' => 15),
            );
        } else {
            return array(
                0 => '0 - 10 thn',
                1 => '10 - 15 thn',
                2 => '> 15 thn',
            );
        }
    }

    public function filter($filter){
        $daya = $this->getListRangeDaya(true);
        $filter['daya'] = $daya[$filter['daya']];
        $tglPasang = $this->getListRangeTglPasang(true);
        $filter['tglPasang'] = $tglPasang[$filter['tglPasang']];
        return $this->ci->dil->filterMenu1($filter, date('Y'));
    }
}

?>
