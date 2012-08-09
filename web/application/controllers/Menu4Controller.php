<?php
/**
 * Description of Menu4
 *
 * @author spondbob
 */
class Menu4Controller extends CI_Controller {
    private $activeUser = null;
    public function __construct() {
        parent::__construct();
        $this->load->library('layout', array('controller' => 'menu4'));
        $this->load->library(array('LibMenu4','LibArea'));
        $this->load->helper(array('form'));
        $this->activeUser = $this->libuser->activeUser;
        $this->_accessRules();
    }
    
    private function _accessRules(){
        $access = new AccessRule();
        $access->activeRole = $this->activeUser['role'];
        $access->allowedRoles = array(1, 3);
        $access->validate();
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
