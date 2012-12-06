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
    
    public function getListPraPasca($value = false) {
        if ($value) {
            return array(
                1 => 'ada T nya',
                2 => 'gak ada T nya',
            );
        } else {
            return array(
                1 => 'Pra',
                2 => 'Pasca',
            );
        }
    }

	public function translateKodePembMeter($code = null){
		$data = array('A' => 'AMR', 'E' => 'Elektronik', 'M' => 'Mekanik');
        if(array_key_exists($code, $data) && $code != null){
            return $data[$code];
        }
        return '-';
	}
	
    public function validateInput($input, $list = null) {
        if ($list == null) {
            $list = array(
                'area' => $this->ci->libarea->getList(),
                'daya' => $this->getListRangeDaya(),
                'tglPasang' => $this->getListRangeTglPasang(),
                'praPasca' => $this->getListPraPasca(),
            );
        }
        $k = array_keys($list['area']);
        $defaultValue = array(
            'area' => $list['area'][$k[0]],
            'daya' => 1,
            'tglPasang' => 1,
            'praPasca' => 1,
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
        return $this->ci->dil->filterMenu1($filter, true);
    }

    public function export($filter) {
        $BLTH = $this->ci->option->getValue('DilBLTH');
        $fileName = $filter['controller'] . $BLTH . $filter['area'] . $filter['daya'] . $filter['tglPasang'] . $filter['praPasca'] . '.xls';
        $location  = 'static/export/' . strtolower($filter['controller']) . '/' . $BLTH . '/';
        if (!file_exists(FCPATH . $location . $fileName)) {
            $filter = $this->filter($filter);
            $filter['select'] = null;

            $export = new LibExport();
            $export->fileName = $fileName;
            $export->location = $location;
            $export->BLTH = $BLTH;
            $export->generate($filter);
        }
        redirect(base_url() . $location . $fileName);
    }

}

?>
