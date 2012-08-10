<?php
/**
 * Description of Menu1
 *
 * @author spondbob
 */
class Menu1Controller extends CI_Controller {
    private $activeUser = null;
    public function __construct() {
        parent::__construct();
        $this->load->library('layout', array('controller' => 'menu1'));
        $this->load->library(array('LibMenu1','LibArea'));
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
        $this->view();
    }
    
    public function view(){
        $data = array(
            'pageTitle' => 'Menu 1',
            'data' => array(),
            'label' => $this->dil->attributeLabels(),
        );
        $data['dropdownData'] = array(
            'area' => $this->libarea->getList(),
            'daya' => $this->libmenu1->getListRangeDaya(),
            'tglPasang' => $this->libmenu1->getListRangeTglPasang(),
            
        );
        
        if(isset($_GET['submit'])){
            $filter = array(
                'area' => $_GET['area'],
                'daya' => $_GET['daya'],
                'tglPasang' => $_GET['tglPasang'],
                'limit' => 10,
                'offset' => 0,
            );
            $data['data'] = $this->libmenu1->filter($filter);
            //redirect('menu1/view/'.$filter['area'].'/'.$filter['daya'].'/'.$filter['tglPasang']);
        }
        $data['sidebar']['dropdownData'] = $data['dropdownData'];
        $this->layout->render('main', $data);
    }
    
    public function data(){
        $data['data'] = $this->libmenu1->filter($filter);
    }
}

?>
