<?php

/**
 * Description of Menu2
 *
 * @author spondbob
 */
class Menu2Controller extends CI_Controller {

    private $controller = 'Menu2';
    private $activeUser = null;

    public function __construct() {
        parent::__construct();
        $this->load->library('layout', array('controller' => $this->controller));
        $this->load->library(array('LibMenu2', 'LibArea'));
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
        $lib = new LibMenu2();
        $list = array(
            'area' => $this->libarea->getList('sorek'),
            'jamNyala' => $lib->getListRangeJamNyala(),
        );
        $input = array(
            'area' => $this->input->get('area'),
            'jamNyala' => $this->input->get('jamNyala'),
        );
        $input = $lib->validateInput($input, $list);

        $data = array(
            'pageTitle' => 'Menu 2 - Analisa Jam Nyala',
            'label' => array_merge($this->sorek->attributeLabels(), $this->dil->attributeLabels()),
            'sAjaxSource' => site_url('menu2/data?area=' . $input['area'] . '&jamNyala=' . $input['jamNyala']),
        );
        foreach (array_keys($input) as $k) {
            $data['dropdownData'][$k] = array(
                'input' => $input[$k],
                'list' => $list[$k],
            );
        }
        $data['blth'] = $this->sorek->currentBLTH();

        $this->layout->render('main', $data);
    }

    public function data() {
        $lib = new LibMenu2();
        $filter = array(
            'area' => $this->input->get('area'),
            'jamNyala' => $this->input->get('jamNyala'),
        );
        $filter = $lib->validateInput($filter);
        $filter['select'] = array('DIL.IDPEL AS IDPEL', 'NAMA', 'TARIF', 'DAYA', 'FAKM', 'JAMNYALA');
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
        $data = $lib->getData($filter);
        $aaData = array();
        foreach ($data['data'] as $d) {
            $aaData[] = array(
                $d->IDPEL,
                $d->NAMA,
                $d->TARIF,
                $d->DAYA,
                $d->FAKM,
                $d->JAMNYALA,
            );
        }
        $output = array(
            "sEcho" => (isset($_GET['sEcho']) ? intval($_GET['sEcho']) : 1),
            "iTotalRecords" => $data['num'],
            "iTotalDisplayRecords" => $data['num'],
            "aaData" => $aaData,
        );
        echo json_encode($output);
    }

    public function export() {
        $filter = array(
            'area' => $this->input->get('area'),
            'jamNyala' => $this->input->get('jamNyala'),
        );
        $filter = $this->libmenu2->validateInput($filter);
        $filter['controller'] = $this->controller;
        $this->libmenu2->export($filter);
    }

}

?>
