<?php
/**
 * Description of Layout
 * 
 * Main library to generate page on browser and ability to switch to another layout.
 * @author spondbob
 */
class Layout {
    
    private $ci = null;
    public $activeLayout = '2column';
    public $pageTitle = 'PLN Watch | PT PLN (Persero) | Distribusi Jatim';
    public $controller = null;
    private $activeUser = null;
    
    public function __construct($params = array()) {
        $this->ci =& get_instance();
        $this->activeLayout = (array_key_exists('activeLayout', $params) ? $params['activeLayout'] : $this->activeLayout);
        $this->controller = (array_key_exists('controller', $params) ? strtolower($params['controller']) : null);
        
        // LibUser already loaded by AutoLoad when the Controller was called
        $this->activeUser = $this->ci->libuser->activeUser;
    }
    
    public function render($page = null, $attr = null, $returnData = false){
        $start = array();
        $attr = ($attr == null ? array() : $attr);
        $start['pageTitle'] = (array_key_exists('pageTitle', $attr) ? $attr['pageTitle'].' - ' : '').$this->pageTitle;
        $start['controller'] = $this->controller;
        $start['sidebar'] = (array_key_exists('sidebar', $attr) ? $attr['sidebar'] : array());
        $start['user'] = $this->activeUser;
        
        $ret = $this->ci->load->view('layout/header', $start, $returnData);
        $ret .= $this->ci->load->view('layout/'.$this->activeLayout.'/start', $start, $returnData);
        $ret .= $this->ci->load->view(($page == null ? 'error' : ($this->controller == null ? '' : $this->controller.'/').$page), $attr, $returnData);
        $ret .= $this->ci->load->view('layout/'.$this->activeLayout.'/end', null, $returnData);
        $ret .= $this->ci->load->view('layout/footer', null, $returnData);
        
        if($returnData){
            echo $ret;
        }
    }
}

?>
