<?php
/**
 * Description of Theme
 *
 * @author spondbob
 */
class Layout {
    
    private $ci = null;
    public $activeLayout = '2column';
    public $pageTitle = 'PLN Watch | PT PLN (Persero) | Distribusi Jatim';
    private $controller = null;
    
    public function __construct($params = array()) {
        $this->ci =& get_instance();
        $this->activeLayout = (array_key_exists('activeLayout', $params) ? $params['activeLayout'] : $this->activeLayout);
        $this->controller = (array_key_exists('controller', $params) ? strtolower($params['controller']) : null);
    }
    
    public function render($page = null, $attr = array()){
        $start = array();
        $start['pageTitle'] = $this->pageTitle.(array_key_exists('pageTitle', $attr) ? ' - '.$attr['pageTitle'] : '');
        $start['controller'] = $this->controller;
        $start['sidebar'] = (array_key_exists('sidebar', $attr) ? $attr['sidebar'] : array());
        
        $this->ci->load->view('layout/header', $start);
        //$this->get('header', $attr);
        $this->ci->load->view('layout/'.$this->activeLayout.'/start', $start);
        $this->ci->load->view(($page == null ? 'error' : ($this->controller == null ? '' : $this->controller.'/').$page), $attr);
        $this->ci->load->view('layout/'.$this->activeLayout.'/end');
        //$this->get('footer');
        $this->ci->load->view('layout/footer');
    }
}

?>
