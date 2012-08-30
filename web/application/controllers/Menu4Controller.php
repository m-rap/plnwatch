<?php

/**
 * Description of Menu4
 *
 * @author spondbob
 */
class Menu4Controller extends CI_Controller {

    private $controller = 'Menu4';
    private $activeUser = null;

    public function __construct() {
        parent::__construct();
        $this->load->library('layout', array('controller' => strtolower($this->controller)));
        $this->load->library(array('LibMenu4', 'LibArea'));
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
        $this->view();
    }

    public function view() {
        $lib = new LibMenu4();
        $list = array(
            'area' => $this->libarea->getList(),
        );
        $input = array(
            'area' => $this->input->get('area'),
        );
        $input = $lib->validateInput($input, $list);
        
        $data = array(
            'pageTitle' => 'Menu 4',
            'label' => array_merge($this->dil->attributeLabels(), $this->dph->attributeLabels()),
            'sAjaxSource' => site_url('menu4/data?area=' . $input['area']),
        );
        $data['sidebar']['dropdownData']['area'] = array(
            'input' => $input['area'],
            'list' => $list['area'],
        );
        $this->layout->render('main', $data);
    }

    public function data() {
        $filter = array(
            'area' => $this->input->get('area'),
        );
        $filter = $this->libmenu4->validateInput($filter);
        $filter['select'] = array('DIL.IDPEL AS IDPEL', 'NAMA', 'JMLBELI', 'KDGARDU', 'NOTIANG');
        $filter['limit'] = (isset($_GET['iDisplayLength']) && $_GET['iDisplayLength'] != -1 ? intval($_GET['iDisplayLength']) : 25);
        $filter['offset'] = (isset($_GET['iDisplayStart']) ? intval($_GET['iDisplayStart']) : 0);
        
        $sOrder = "";
        if (isset($_GET['iSortCol_0'])) {
            for ($i = 0; $i < intval($_GET['iSortingCols']); $i++) {
                if ($_GET['bSortable_' . intval($_GET['iSortCol_' . $i])] == "true") {
                    $sOrder .= "`" . $filter['select'][intval($_GET['iSortCol_' . $i])] . "` " .
                            mysql_real_escape_string($_GET['sSortDir_' . $i]);
                }
                if ($i != 0 && $i + 1 == intval($_GET['iSortingCols']))
                    $sOrder .= ', ';
            }
        }

        $filter['order'] = $sOrder;
        $data = $this->libmenu4->getData($filter);
        $aaData = array();
        foreach ($data['data'] as $d) {
            $aaData[] = array(
                $d->IDPEL,
                $d->NAMA,
                ($d->JMLBELI == null ? '0' : $d->JMLBELI),
                $d->KDGARDU,
                $d->NOTIANG
            );
        }
        $output = array(
            "sEcho" => (isset($_GET['sEcho']) ? intval($_GET['sEcho']) : 1),
            "iTotalRecords" => $data['num'],
            "iTotalDisplayRecords" => $data['num'],
        );
        $output['aaData'] = $aaData;
        echo json_encode($output);
    }

    public function export() {
        $filter = array(
            'area' => $this->input->get('area'),
        );
        $filter = $this->libmenu4->validateInput($filter);
        $filter['controller'] = $this->controller;
        $this->libmenu4->export($filter);
    }

}

?>
