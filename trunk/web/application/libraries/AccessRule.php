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

        if ($this->ci->uri->segment(1) == NULL) {
            $allow = true;
        } else if ($currentMethod == NULL) {
            $allow = $this->check($this->activeRole, $this->allowedRoles);
            /* if (in_array($this->allowedRoles, array('*', '?', '@'))) {
                if ($this->allowedRoles == "*") {
                    $allow = true;
                } else if ($this->activeRole == 0 && $this->allowedRoles == "?") {
                    $allow = true;
                } else if ($this->activeRole != 0 && $this->allowedRoles == "@") {
                    $allow = true;
                } else {
                    $allow = false;
                }
            } else if (is_array($this->allowedRoles) && in_array($this->activeRole, $this->allowedRoles)) {
                $allow = true;
            } else {
                $allow = false;
            }*/
        } else {
            // defined for some role
            if (array_key_exists($currentMethod, $this->allowedRoles)) {
                $allow = $this->check($this->activeRole, $this->allowedRoles[$currentMethod]);
                /*if (in_array($this->allowedRoles[$currentMethod], array('*', '?', '@'))) {
                    if ($this->allowedRoles[$currentMethod] == "*") {
                        $allow = true;
                    } else if ($this->activeRole == 0 && $this->allowedRoles[$currentMethod] == "?") {
                        $allow = true;
                    } else if ($this->activeRole != 0 && $this->allowedRoles[$currentMethod] == "@") {
                        $allow = true;
                    } else {
                        $allow = false;
                    }
                } else if (is_array($this->allowedRoles) && in_array($this->activeRole, $this->allowedRoles[$currentMethod])) {
                    $allow = true;
                } else {
                    $allow = false;
                }*/
            }
        }

        if (!$allow) {
            $layout = new Layout(array('activeLayout' => '1column', 'controller' => null));
            $layout->render('error', null, true);
            exit;
        }
    }

}

?>
