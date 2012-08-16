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
        $this->load->helper(array('form', 'file'));
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
        $fArea = (isset($_GET['area']) ? $_GET['area'] : 'A');
        $fDaya = (isset($_GET['daya']) ? $_GET['daya'] : 1);
        $fTglPasang = (isset($_GET['tglPasang']) ? $_GET['tglPasang'] : 1);
        $data = array(
            'pageTitle' => 'Menu 1',
            'label' => $this->dil->attributeLabels(),
            'sAjaxSource' => site_url("menu1/data?area=" . $fArea . "&daya=" . $fDaya . "&tglPasang=" . $fTglPasang),
            'select' => array('IDPEL', 'NAMA', 'JENIS_MK', 'KDGARDU', 'NOTIANG'),
        );

        $data['sidebar']['dropdownData'] = array(
            'area' => array(
                'data' => $this->libarea->getList(),
                'selected' => $fArea,
            ),
            'daya' => array(
                'data' => $this->libmenu1->getListRangeDaya(),
                'selected' => $fDaya,
            ),
            'tglPasang' => array(
                'data' => $this->libmenu1->getListRangeTglPasang(),
                'selected' => $fTglPasang,
            ),
        );
        $this->layout->render('main', $data);
    }

    public function data() {
        if (empty($_GET['area']) || empty($_GET['daya']) || empty($_GET['tglPasang'])) {
            return;
        }
        $select = array('IDPEL', 'NAMA', 'JENIS_MK', 'KDGARDU', 'NOTIANG');
        $filter = array(
            'select' => $select,
            'area' => (isset($_GET['area']) ? $_GET['area'] : -1),
            'daya' => (isset($_GET['daya']) ? $_GET['daya'] : -1),
            'tglPasang' => (isset($_GET['tglPasang']) ? $_GET['tglPasang'] : -1 ),
            'limit' => (isset($_GET['iDisplayLength']) && $_GET['iDisplayLength'] != -1 ? intval($_GET['iDisplayLength']) : 50),
            'offset' => (isset($_GET['iDisplayStart']) ? intval($_GET['iDisplayStart']) : 0),
        );
        $sOrder = "";
        if (isset($_GET['iSortCol_0'])) {
            for ($i = 0; $i < intval($_GET['iSortingCols']); $i++) {
                if ($_GET['bSortable_' . intval($_GET['iSortCol_' . $i])] == "true") {
                    $sOrder .= "`" . $select[intval($_GET['iSortCol_' . $i])] . "` " .
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
                ($d->JENIS_MK == "A" ? "AMR" : ($d->JENIS_MK == "E" ? "Elektronik" : ($d->JENIS_MK == "M" ? "Mekanik" : "Blank"))),
                $d->KDGARDU,
                $d->NOTIANG
            );
        }
        $output = array(
            "sEcho" => (isset($_GET['sEcho']) ? intval($_GET['sEcho']) : 1),
            "iTotalRecords" => $data['num'],
            "iTotalDisplayRecords" => $data['num'],
                //"aaData" => $data['data']
        );
        $output['aaData'] = $aaData;
        echo json_encode($output);
    }

    public function export() {
        $filter = array(
            'area' => (isset($_GET['area']) ? $_GET['area'] : -1),
            'daya' => (isset($_GET['daya']) ? $_GET['daya'] : -1),
            'tglPasang' => (isset($_GET['tglPasang']) ? $_GET['tglPasang'] : -1 ),
            'controller' => $this->controller,
        );
        $this->libmenu1->export($filter);
    }

}

?>
