<?php
/**
 * Description of Menu2
 *
 * @author spondbob
 */
class Menu2Controller extends CI_Controller {
    private $activeUser = null;
    public function __construct() {
        parent::__construct();
        $this->load->library('layout', array('controller' => 'menu2'));
        $this->load->library(array('LibMenu2','LibArea'));
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
            'area' => $this->libarea->getList(),
            'jamNyala' => $this->libmenu2->getListRangeJamNyala()
        );
        $data['sidebar']['dropdownData'] = $data['dropdownData'];
        $data['pageTitle'] = 'Menu 2';
        $this->layout->render('main', $data);
    }
}

?>
