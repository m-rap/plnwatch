<?php
/**
 * Description of Theme
 *
 * @author spondbob
 */
class Theme {
    
    public $currentTheme = 'pln';
    public $pageTitle = 'PLNWatch';
    public function get($part){
        $this->load->view('theme/'.$this->currentTheme.'/'.$get);
    }
    
    public function load($page, $attr){
        $attr['pageTitle'] = $pageTitle.($attr['pageTitle'] == "" ? '' : ' - '.$attr['pageTitle']);
        
        $this->get('header');
        $this->load->view($page, $attr);
        $this->get('footer');
    }
}

?>
