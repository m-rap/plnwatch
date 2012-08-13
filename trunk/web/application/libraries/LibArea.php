<?php
/**
 * Description of LibArea
 *
 * @author spondbob
 */
class LibArea {
    
    private $ci = null;
    public function __construct() {
        $this->ci =& get_instance();
        $this->ci->load->model(array('dil'));
    }

    public function getList() {
        $list = $this->ci->dil->getListArea();
        $data = array();
        foreach($list as $l){
            $data[$l->KODEAREA] = $l->KODEAREA;
        }
        return $data;
        /*
        return array(
            'SBS' => 'SBS',
        );
         * 
         */
    }
}

?>
