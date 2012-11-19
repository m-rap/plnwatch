<?php

/**
 * Description of LibMenu2
 *
 * @author spondbob
 */
class LibMenu2 {

    private $ci = null;

    public function __construct() {
        $this->ci = & get_instance();
        $this->ci->load->model(array('sorek', 'option'));
        $this->ci->load->library(array('LibExport'));
    }

    public function getListRangeJamNyala($value = false) {
        if ($value) {
            return array(
                1 => array('min' => 0, 'max' => 40),
                2 => array('min' => 40, 'max' => 60),
                3 => array('min' => 60, 'max' => 300),
                4 => array('min' => 300, 'max' => 720),
                5 => array('min' => 720,),
            );
        } else {
            return array(
                1 => '0 - 40',
                2 => '40 - 60',
                3 => '60 - 300',
                4 => '300 - 720',
                5 => '> 720',
            );
        }
    }

    public function validateInput($input, $list = null) {
        if ($list == null) {
            $list = array(
                'area' => $this->ci->libarea->getList(),
                'jamNyala' => $this->getListRangeJamNyala(),
            );
        }

        $k = array_keys($list['area']);
        $defaultValue = array(
            'area' => $list['area'][$k[0]],
            'jamNyala' => 1,
        );

        foreach (array_keys($input) as $i) {
            if (empty($input[$i]) or !array_key_exists($input[$i], $list[$i])) {
                $input[$i] = $defaultValue[$i];
            }
        }

        return $input;
    }

    private function filter($filter) {
        $jamNyala = $this->getListRangeJamNyala(true);
        $filter['select'] = (!array_key_exists('select', $filter) ? array('DIL.IDPEL AS IDPEL', 'NAMA', 'JAMNYALA', 'KDGARDU', 'NOTIANG') : $filter['select']);
        $filter['order'] = (!array_key_exists('order', $filter) || $filter['order'] == "" ? 'IDPEL' : $filter['order']);
        $explode = explode(' AS ', $filter['order']);
        $filter['order'] = $explode[0];
        $filter['jamNyala'] = $jamNyala[$filter['jamNyala']];

        return $filter;
    }

    public function getData($filter) {
        $filter = $this->filter($filter);
        return array(
            'data' => $this->ci->sorek->filterMenu2($filter),
            'num' => $this->ci->sorek->countFilterMenu2($filter),
        );
    }

    public function export($filter) {
        $currentBLTH = $this->ci->sorek->currentBLTH();
        $fileName = $filter['controller'] . $currentBLTH . $filter['area'] . $filter['jamNyala'] . '.xls';
        if (!file_exists(FCPATH . 'static/export/' . strtolower($filter['controller']) . '/' . $fileName)) {
            $filter = $this->filter($filter);
            $filter['limit'] = 10000;
            $export = new LibExport();
            $export->fileName = $fileName;
            $export->generate($filter);
        }
        redirect('static/export/' . strtolower($filter['controller']) . '/' . $fileName);
    }

}

?>
