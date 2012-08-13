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
        $this->ci->load->model(array('dil'));
        $this->ci->load->library(array('PHPExcel'));
    }

    public function getListRangeDaya($value = false) {
        if ($value) {
            return array(
                0 => array('min' => 450, 'max' => 5500),
                1 => array('min' => 6600, 'max' => 33000),
                2 => array('min' => 41500),
            );
        } else {
            return array(
                0 => '450 - 5.500',
                1 => '6.600 - 33.000',
                2 => '> 41.500',
            );
        }
    }

    public function getListRangeTglPasang($value = false) {
        if ($value) {
            return array(
                0 => array('min' => 0, 'max' => 10),
                1 => array('min' => 10, 'max' => 15),
                2 => array('min' => 15),
            );
        } else {
            return array(
                0 => '0 - 10 thn',
                1 => '10 - 15 thn',
                2 => '> 15 thn',
            );
        }
    }

    public function filter($filter) {
        $daya = $this->getListRangeDaya(true);
        $tglPasang = $this->getListRangeTglPasang(true);

        $filter['select'] = (!array_key_exists('select', $filter) ? 'IDPEL,NAMA,JENIS_MK,KDGARDU,NOTIANG' : $filter['select']);
        $filter['daya'] = $daya[$filter['daya']];
        $filter['tglPasang'] = $tglPasang[$filter['tglPasang']];
        return $this->ci->dil->filterMenu1($filter, date('Y'));
    }

    public function export($filter) {
        $label = $this->ci->dil->attributeLabels();

        $daya = $this->getListRangeDaya(true);
        $tglPasang = $this->getListRangeTglPasang(true);

        $filter['limit'] = -1;
        $filter['offset'] = -1;
        $filter['select'] = (!array_key_exists('select', $filter) ? 'IDPEL,NAMA,JENIS_MK,KDGARDU,NOTIANG' : $filter['select']);
        $filter['daya'] = $daya[$filter['daya']];
        $filter['tglPasang'] = $tglPasang[$filter['tglPasang']];

        $objPHPExcel = new PHPExcel();
        $objPHPExcel->getProperties()->setCreator("spondbob")
                ->setLastModifiedBy("spondbob")
                ->setTitle("Office 2007 XLSX")
                ->setSubject("Office 2007 XLSX")
                ->setDescription("Schematics2011")
                ->setKeywords("schematics")
                ->setCategory("file");
        $objPHPExcel->getSheet(0)->setTitle('Menu 1');
        $objPHPExcel->setActiveSheetIndex(0)
                ->setCellValue('A1', $label['IDPEL'])
                ->setCellValue('B1', $label['NAMA'])
                ->setCellValue('C1', $label['JENIS_MK'])
                ->setCellValue('D1', $label['KDGARDU'])
                ->setCellValue('E1', $label['NOTIANG']);
        $i = 2;
        $d = $this->ci->dil->filterMenu1($filter, date('Y'));
        foreach ($d['data'] as $r) {
            $objPHPExcel->setActiveSheetIndex(0)
                    ->setCellValue('A' . $i, $r->IDPEL)
                    ->setCellValue('B' . $i, $r->NAMA)
                    ->setCellValue('C' . $i, ($r->JENIS_MK == "A" ? "AMR" : ($r->JENIS_MK == "E" ? "Elektronik" : ($r->JENIS_MK == "M" ? "Mekanik" : "Blank"))))
                    ->setCellValue('D' . $i, $r->KDGARDU)
                    ->setCellValue('E' . $i, $r->NOTIANG);
            $i++;
        }
        $objPHPExcel->getActiveSheet()->setTitle('Menu 1 - PLN Watch');

        $objPHPExcel->setActiveSheetIndex(0);
        return $objPHPExcel;
    }

}

?>
