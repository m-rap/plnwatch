<?php

/**
 * Description of Menu1
 *
 * @author spondbob
 */
class Menu1Controller extends CI_Controller {

    private $controller = 'Menu1';
    private $activeUser = null;

    public function __construct() {
        parent::__construct();
        $this->load->library('layout', array('controller' => strtolower($this->controller)));
        $this->load->library(array('LibMenu1', 'LibArea', 'LibExport'));
        $this->load->helper(array('form', 'file', 'url'));
        $this->activeUser = $this->libuser->activeUser;
        $this->_accessRules();
    }

    /*
     * array(1, 2, ...)
     * array('*')
     * array(
     *  'index' => array('*')
     *  'menu'  => array('@')
     * )
     * array(
     *  'index' => array(1, 2, ...)
     *  'menu'  => array(3, 4, ...)
     * )
     */

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
		if($this->input->get('download')){
			$this->export();
			exit;
		}
		
        $lib = new LibMenu1();
        $input = array(
            'area' => $this->input->get('area'),
            'daya' => $this->input->get('daya'),
            'tglPasang' => $this->input->get('tglPasang'),
            'praPasca' => $this->input->get('praPasca'),
        );
        $list = array(
            'area' => $this->libarea->getList(),
            'daya' => $lib->getListRangeDaya(),
            'tglPasang' => $lib->getListRangeTglPasang(),
            'praPasca' => $lib->getListPraPasca(),
        );
        $input = $lib->validateInput($input, $list);

        $data = array(
            'pageTitle' => 'Menu 1 - Analisa Usia APP',
            'label' => $this->dil->attributeLabels(),
            'select' => array('IDPEL', 'NAMA', 'KDPEMBMETER', 'DAYA', 'TGLPASANG_KWH', 'KDGARDU', 'NOTIANG'),
            'sAjaxSource' => site_url("menu1/data?area=" . $input['area'] . "&daya=" . $input['daya'] . "&tglPasang=" . $input['tglPasang'] . "&praPasca=" . $input['praPasca']),
        );

        foreach (array_keys($input) as $k) {
            $data['dropdownData'][$k] = array(
                'input' => $input[$k],
                'list' => $list[$k],
            );
        }
        $data['blth'] = $this->option->getValue('DilBLTH');
        $this->layout->render('main', $data);
    }

    public function data() {
        $filter = array(
            'area' => $this->input->get('area'),
            'daya' => $this->input->get('daya'),
            'tglPasang' => $this->input->get('tglPasang'),
            'praPasca' => $this->input->get('praPasca'),
        );
        $filter = $this->libmenu1->validateInput($filter);
        $filter['select'] = array('IDPEL', 'NAMA', 'KDPEMBMETER', 'DAYA', 'TGLPASANG_KWH', 'KDGARDU', 'NOTIANG');
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
        $data = $this->libmenu1->getData($filter);
        $aaData = array();
        foreach ($data['data'] as $d) {
            $aaData[] = array(
                $d->IDPEL,
                $d->NAMA,
                ($d->KDPEMBMETER == "A" ? "AMR" : ($d->KDPEMBMETER == "E" ? "Elektronik" : ($d->KDPEMBMETER == "M" ? "Mekanik" : "Blank"))),
                $d->DAYA,
                $d->TGLPASANG_KWH,
                $d->KDGARDU,
                $d->NOTIANG
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
        $lib = new LibMenu1();
        $filter = array(
            'area' => $this->input->get('area'),
            'daya' => $this->input->get('daya'),
            'tglPasang' => $this->input->get('tglPasang'),
            'praPasca' => $this->input->get('praPasca'),
        );
        $filter = $lib->validateInput($filter);
        $filter['controller'] = $this->controller;
        $lib->export($filter);
    }

}

?>
