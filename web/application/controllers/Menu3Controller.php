<?php

/**
 * Description of Menu3
 *
 * @author spondbob
 */
class Menu3Controller extends CI_Controller {

    private $controller = 'Menu3';
    private $activeUser = null;

    public function __construct() {
        parent::__construct();
        $this->load->library('layout', array('controller' => strtolower($this->controller)));
        $this->load->library(array('LibMenu3', 'LibArea'));
        $this->load->helper(array('form'));
        $this->activeUser = $this->libuser->activeUser;
        $this->_accessRules();
    }

    private function _accessRules() {
        $access = new AccessRule();
        $access->activeRole = $this->activeUser['role'];
        $access->allowedRoles = array(1, 3);
        $access->validate();
    }

    public function index() {
        $data = array(
            'pageTitle' => 'Menu 3',
        );
        $data['dropdownData'] = array(
            'area' => $this->libarea->getList(),
            'tren' => $this->libmenu3->getListTren()
        );
        $data['sidebar']['dropdownData'] = $data['dropdownData'];
        $this->layout->render('main', $data);
    }

}

?>
