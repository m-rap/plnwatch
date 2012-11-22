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
        $this->ci->load->model(array('dil', 'sorek', 'option'));
        $this->ci->load->library(array('LibExport'));
    }

    public function getData() {
        $opt = new Option();
        $DilBLTH = $opt->getValue('DilBLTH');
        return $this->ci->dil->filterMenu5('SOREK_' . $DilBLTH);
    }

    public function export($filter) {
        $fileName = $filter['controller'] . $filter['area'] . $filter['jamNyala'] . '.xls';
        if (!file_exists(FCPATH . 'static/export/' . strtolower($filter['controller']) . '/' . $fileName)) {
            $filter = $this->filter($filter);
            $filter['select'] = null;
            $filter['limit'] = 50000;

            $export = new LibExport();
            $export->fileName = $fileName;
            $export->generate($filter);
        }
        redirect('static/export/' . strtolower($filter['controller']) . '/' . $fileName); 
    }

}

?>
