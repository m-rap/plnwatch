<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class Welcome extends CI_Controller {
    
    public function __construct() {
        parent::__construct();
        $this->load->library('Layout');
    }

    public function index(){
        $layout = new Layout();
        $layout->activeLayout = '1column';
        $layout->render('main');
    }
}

/* End of file welcome.php */
/* Location: ./application/controllers/welcome.php */