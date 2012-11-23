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
        $this->load->model(array('dil', 'sorek'));
        $this->activeUser = $this->libuser->activeUser;
        $this->_accessRules();
    }

    private function _accessRules() {
        $access = new AccessRule();
        $access->activeRole = $this->activeUser['role'];
        $access->allowedRoles = array(1, 3);
        $access->validate();
    }

    public function index(){
        $this->view();
    }
    
    public function view() {
		if($this->input->get('download')){
			$this->export();
			exit;
		}
		
        $lib = new LibMenu3();
        $list = array(
            'kodearea' => $this->libarea->getList(),
            'tren' => $this->libmenu3->getListTren(),
        );
        $input = array(
            'kodearea' => $this->input->get('area'),
            'tren' => $this->input->get('tren'),
        );
        $input = $lib->validateInput($input, $list);
        
        $data = array(
            'pageTitle' => 'Menu 3 - Analisa Tren Pemakaian KWH',
            'tren' => $this->sorek->getTrenLabels(),
            'label' => array_merge($this->sorek->attributeLabels(), $this->dil->attributeLabels()),
            'sAjaxSource' => site_url('menu3/data?area='.$input['kodearea'].'&tren='.$input['tren']),
        );
        foreach (array_keys($input) as $k) {
            $data['dropdownData'][$k] = array(
                'input' => $input[$k],
                'list' => $list[$k],
            );
        }
        $this->layout->render('main', $data);
    }
    
    public function data() {
        $lib = new LibMenu3();
        $filter = array(
            'kodearea' => $this->input->get('area'),
            'tren' => $this->input->get('tren'),
        );
        $filter = $lib->validateInput($filter);
        $filter['offset'] = (isset($_GET['iDisplayStart']) ? intval($_GET['iDisplayStart']) : 0);
        $filter['limit'] = (isset($_GET['iDisplayLength']) && $_GET['iDisplayLength'] != -1 ? intval($_GET['iDisplayLength']) : 25);
        //$labels = implode('', $this->sorek->getTrenLabels());
        //$filename = $filter['offset'].'l'.$filter['limit'].$filter['kodearea'].$filter['tren'].$labels.'.php';

        $data = $lib->getData($filter);
        $aaData = array();
        foreach ($data['data'] as $d) {
            $aaData[] = array_values($d);
        }
        $output = array(
            "sEcho" => (isset($_GET['sEcho']) ? intval($_GET['sEcho']) : 1),
            "iTotalRecords" => $data['num'],
            "iTotalDisplayRecords" => $data['num'],
            'aaData' => $aaData,
        );
        echo json_encode($output);
    }
    
    public function export() {
        $lib = new LibMenu3();
        $filter = array(
            'kodearea' => $this->input->get('area'),
            'tren' => $this->input->get('tren'),
        );
        $filter = $lib->validateInput($filter);
        $filter['controller'] = $this->controller;
        $lib->export($filter);
    }

}

?>
