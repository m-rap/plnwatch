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
    
    private function check($activeRole, $allowedRoles) {
        if (in_array($allowedRoles, array('*', '?', '@'))) {
            if ($allowedRoles == "*") {
                return true;
            } else if ($activeRole == 0 && $allowedRoles == "?") {
                return true;
            } else if ($activeRole != 0 && $allowedRoles == "@") {
                return true;
            } else {
                return false;
            }
        } else if (is_array($allowedRoles) && in_array($activeRole, $allowedRoles)) {
            return true;
        } else {
            return false;
        }
    }

    /*
     * *: any user, including both anonymous and authenticated users.
     * ?: anonymous users.
     * @: authenticated users.
     */
    public function validate() {
        $allow = false;
        $currentMethod = $this->ci->uri->segment(2);

        $allowedRolesKeys = array_keys($this->allowedRoles);
        if ($this->ci->uri->segment(1) == NULL) {
            $allow = true;
        } else if ($currentMethod != NULL && array_key_exists($currentMethod, $this->allowedRoles)){
            $allow = $this->check($this->activeRole, $this->allowedRoles[$currentMethod]);
        } else if ($currentMethod == NULL || !array_key_exists($currentMethod, $this->allowedRoles)){
            $allow = $this->check($this->activeRole, $this->allowedRoles);
        } else {
            $allow = false;
        }

        if (!$allow) {
            $layout = new Layout(array('activeLayout' => '1column', 'controller' => null));
            $layout->render('error', null, true);
            exit;
        }
    }

}

?>
