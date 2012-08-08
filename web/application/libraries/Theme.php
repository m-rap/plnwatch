<?php
/**
 * Description of Theme
 *
 * @author spondbob
 */
class Theme {
    
    private $ci = null;
    public $currentTheme = 'pln';
    public $pageTitle = 'PLN Watch | PT PLN (Persero) | Distribusi Jatim';
    function __construct() {
        $this->ci =& get_instance();
    }
    public function get($part, $attr = null){
        $this->ci->load->view('theme/'.$this->currentTheme.'/'.$part, $attr);
    }
    
    public function render($page = null, $attr = array()){
        if($page == null) $page = 'error';
        $t = $this->pageTitle.(array_key_exists('pageTitle', $attr) ? ' - '.$attr['pageTitle'] : '');
        $attr['pageTitle'] = $t;
        
        $this->get('header', $attr);
        $this->ci->load->view($page, $attr);
        $this->get('footer');
    }
}

?>
