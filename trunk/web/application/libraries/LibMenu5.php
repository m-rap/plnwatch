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
        $this->ci->load->model(array('dil', 'option'));
        $this->ci->load->library(array('LibExport'));
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
        $fileName = $filter['controller'] . $BLTH . $filter['area'] . $filter['daya'] . $filter['tglPasang'] . $filter['praPasca'] . '.xls';
        if (!file_exists(FCPATH . 'static/export/' . strtolower($filter['controller']) . '/' . $fileName)) {
            $filter = $this->filter($filter);
            $filter['limit'] = 50000;

            $export = new LibExport();
            $export->fileName = $fileName;
            $export->generate($filter);
        }
        redirect('static/export/' . strtolower($filter['controller']) . '/' . $fileName);
    }

}

?>
