<?php

/**
 * Description of Menu5
 *
 * @author spondbob
 */
class Menu5Controller extends CI_Controller {

    private $controller = 'Menu5';
    private $activeUser = null;

    public function __construct() {
        parent::__construct();
        $this->load->library('layout', array('controller' => strtolower($this->controller)));
        $this->load->library(array('LibMenu2', 'LibMenu5', 'LibArea', 'LibExport'));
        //$this->load->helper(array('form', 'file'));
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
        $this->view();
    }

    public function view() {
        $data = array(
            'pageTitle' => 'Menu 5 - EIS',
            'sAjaxSource' => site_url("menu5/data"),
            'data' => $this->libmenu5->getData(),
            'jamNyala' => $this->libmenu2->getListRangeJamNyala(),
        );
        $this->layout->render('main', $data);
    }

    public function export() {
        $filter = array(
            'area' => $this->input->get('area'),
            'jamNyala' => $this->input->get('jamNyala'),
        );
        $filter = $this->libmenu5->validateInput($filter);
        $filter['controller'] = $this->controller;
        $this->libmenu5->export($filter);
    }

}

?>
