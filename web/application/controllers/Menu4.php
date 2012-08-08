<?php
/**
 * Description of Menu4
 *
 * @author spondbob
 */
class Menu4 extends CI_Controller {
    function __construct() {
        parent::__construct();
        $this->load->library('layout', array('controller' => 'menu4'));
        $this->load->library(array('LibMenu4','LibArea'));
        $this->load->helper(array('form'));
    }
    
    public function index(){
        $data = array();
        $data['dropdownData'] = array(
            'area' => $this->libarea->getList()
        );
        $data['sidebar']['dropdownData'] = $data['dropdownData'];
        $data['pageTitle'] = 'Menu 4';
        $this->layout->render('main', $data);
    }
}

?>
