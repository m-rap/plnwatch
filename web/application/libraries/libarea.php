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
    }

    public function getList($model = 'dil') {
        $this->ci->load->model($model);
        $list = $this->ci->$model->getListArea();
        $data = array();
        foreach($list as $l){
            $data[$l->KODEAREA] = $l->KODEAREA;
        }
        return $data;
    }
}

?>
