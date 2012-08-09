<?php

/**
 * Description of AccessRule
 *
 * @author spondbob
 */
class AccessRule {

    private $ci = null;
    public $activeRole = 0;
    public $allowedRoles = 0;

    public function __construct() {
        $this->ci = & get_instance();
        $this->ci->load->helper(array('url'));
    }

    public function validate() {
        $allow = true;
        $currentMethod = $this->ci->uri->segment(2);

        if ($currentMethod != NULL && array_key_exists($currentMethod, $this->allowedRoles)) {
            if (!in_array($this->activeRole, $this->allowedRoles[$currentMethod])) {
                $allow = false;
            }
        }
        if (!in_array($this->activeRole, $this->allowedRoles)) {
            $allow = false;
        }
        
        if(!$allow){
            $layout = new Layout();
            $layout->activeLayout = '1column';
            $layout->controller = null;
            $layout->render('error', null, true);
            exit;
        }
    }

}

?>
