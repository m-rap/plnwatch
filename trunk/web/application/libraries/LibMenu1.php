<?php

/**
 * Description of LibMenu1
 *
 * @author spondbob
 */
class LibMenu1 {

    private $ci = null;

    public function __construct() {
        $this->ci = & get_instance();
        $this->ci->load->model(array('dil', 'option'));
        $this->ci->load->library(array('LibExport'));
    }

    public function getListRangeDaya($value = false) {
        if ($value) {
            return array(
                1 => array('min' => 450, 'max' => 5500),
                2 => array('min' => 6600, 'max' => 33000),
                3 => array('min' => 41500),
            );
        } else {
            return array(
                1 => '450 - 5.500',
                2 => '6.600 - 33.000',
                3 => '> 41.500',
            );
        }
    }

    public function getListRangeTglPasang($value = false) {
        if ($value) {
            return array(
                1 => array('min' => 0, 'max' => 10),
                2 => array('min' => 10, 'max' => 15),
                3 => array('min' => 15),
            );
        } else {
            return array(
                1 => '0 - 10 thn',
                2 => '10 - 15 thn',
                3 => '> 15 thn',
            );
        }
    }

    public function validateInput($input, $list = null) {
        if ($list == null) {
            $list = array(
                'area' => $this->ci->libarea->getList(),
                'daya' => $this->getListRangeDaya(),
                'tglPasang' => $this->getListRangeTglPasang(),
            );
        }
        $k = array_keys($list['area']);
        $defaultValue = array(
            'area' => $list['area'][$k[0]],
            'daya' => 1,
            'tglPasang' => 1,
        );

        foreach (array_keys($input) as $i) {
            if (empty($input[$i]) or !array_key_exists($input[$i], $list[$i])) {
                $input[$i] = $defaultValue[$i];
            }
        }

        return $input;
    }

    private function filter($filter) {
        $daya = $this->getListRangeDaya(true);
        $tglPasang = $this->getListRangeTglPasang(true);
        $filter['select'] = (!array_key_exists('select', $filter) ? array('IDPEL', 'NAMA', 'KDPEMBMETER', 'DAYA', 'TGLPASANG_KWH', 'KDGARDU', 'NOTIANG') : $filter['select']);
        $filter['order'] = (!array_key_exists('order', $filter) || $filter['order'] == "" ? 'IDPEL' : $filter['order']);
        $filter['daya'] = $daya[$filter['daya']];
        $filter['tglPasang'] = $tglPasang[$filter['tglPasang']];

        return $filter;
    }

    public function getData($filter) {
        $filter = $this->filter($filter);
        return array(
            'data' => $this->ci->dil->filterMenu1($filter),
            'num' => $this->ci->dil->countFilterMenu1($filter),
        );
    }

    public function export($filter) {
        $BLTH = $this->ci->option->getValue('DilBLTH');
        $fileName = $filter['controller'] . $BLTH . $filter['area'] . $filter['daya'] . $filter['tglPasang'] . '.xlsx';
        if (!file_exists(FCPATH . 'static/export/' . $filter['controller'] . '/' . $fileName)) {
            $filter = $this->filter($filter);
            $filter['limit'] = 50000;

            $export = new LibExport();
            $export->fileName = $fileName;
            $export->generate($filter);
        }
        redirect('static/export/' . $filter['controller'] . '/' . $fileName);
    }

}

?>
