<?php

/**
 * Description of LibMenu5
 *
 * @author spondbob
 */
class LibMenu5 {

    private $ci = null;

    public function __construct() {
        $this->ci = & get_instance();
        $this->ci->load->library(array('LibMenu2', 'LibExport'));
    }

    public function getData() {
        $opt = new Option();
        $DilBLTH = $opt->getValue('DilBLTH');
        return $this->ci->dil->filterMenu5('SOREK_' . $DilBLTH);
    }

    public function export($filter) {
        $BLTH = $this->ci->option->getValue('DilBLTH'); //table sorek di ambil dari dil terakhir
        $fileName = $filter['controller'] . $BLTH . $filter['area'] . $filter['jamNyala'] . '.xls';
        $location  = 'static/export/' . strtolower($filter['controller']) . '/' . $BLTH . '/';
        if (!file_exists(FCPATH . $location . $fileName)) {
            $filter = $this->ci->libmenu2->filter($filter);
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
