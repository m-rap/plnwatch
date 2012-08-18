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
        $this->ci->load->model(array('dil','sorek'));
    }

    public function getList($model = 'dil') {
        $list = $this->ci->$model->getListArea();
        $data = array();
        foreach($list as $l){
            $data[$l->KODEAREA] = $l->KODEAREA;
        }
        return $data;
    }
}

?>
