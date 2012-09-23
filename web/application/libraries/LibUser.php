<?php

/**
 * Description of LibArea
 *
 * @author spondbob
 */
class LibUser {

    private $ci = null;
    public $activeUser = array(
        'active' => 0,
        'id' => 0,
        'alias' => NULL,
        'name' => NULL,
        'role' => 0);

    public function __construct() {
        $this->ci = & get_instance();
        $this->ci->load->library(array('session', 'form_validation'));
        $this->ci->load->model(array('user'));

        $this->setProperty();
    }

    private function setProperty() {
        if ($this->ci->session->userdata('active') == 1) {
            $this->activeUser = array(
                'active' => $this->ci->session->userdata('active'),
                'id' => $this->ci->session->userdata('id'),
                'alias' => $this->ci->session->userdata('alias'),
                'name' => $this->ci->session->userdata('name'),
                'role' => $this->ci->session->userdata('role'));
        }
    }

    private function inputCheck($mode = 'login', $data = null) {
        if ($mode == 'login') {
            $config = array(
                array(
                    'field' => 'user[UserName]',
                    'label' => 'Username',
                    'rules' => 'required|max_length[32]|xss_clean'
                ),
                array(
                    'field' => 'user[UserPassword]',
                    'label' => 'Password',
                    'rules' => 'required|xss_clean'
                ),
            );
        } else {
            $config = array(
                array(
                    'field' => 'user[UserAlias]',
                    'label' => 'Alias',
                    'rules' => 'required|max_length[32]|xss_clean'
                )
            );
            if (trim($data['password1']) != "" || trim($data['password2']) != "" || trim($data['password3']) != "") {
                $config[] = array(
                    'field' => 'user[password1]',
                    'label' => 'Password Lama',
                    'rules' => 'required|callback__oldPasswordCheck|xss_clean'
                );
                $config[] = array(
                    'field' => 'user[password2]',
                    'label' => 'Password Baru',
                    'rules' => 'required|callback__matchPasswordCheck]|xss_clean'
                );
                $config[] = array(
                    'field' => 'user[password3]',
                    'label' => 'Password Konfirmasi',
                    'rules' => 'required|xss_clean'
                );
            }
        }
        $this->ci->form_validation->set_rules($config);
        if ($this->ci->form_validation->run()) {
            return TRUE;
        }
        return FALSE;
    }

    /*
     * Return value :
     * -1 = wrong value on input form
     * 0  = wrong username or password
     * 1  = everything's okay, go on
     */

    public function doLogin($login) {
        if ($this->inputCheck('login')) {
            $data = array(
                'userName' => $login['UserName'],
                'userPassword' => md5($login['UserPassword']),
                'userLoginTime' => date('Y-m-d H:i:s'),
                'userLoginIp' => $_SERVER['REMOTE_ADDR'],
                'userLoginBrowser' => $_SERVER['HTTP_USER_AGENT'],
            );
            $user = $this->ci->user->login($data);
            if ($user != FALSE) {
                $this->activeUser = array(
                    'active' => 1,
                    'id' => $user->UserId,
                    'alias' => $user->UserAlias,
                    'name' => $user->UserName,
                    'role' => $user->UserRole);
                $this->ci->session->set_userdata($this->activeUser);
                $this->setProperty();
                return 1;
            }
            return -1;
        } else {
            return 0;
        }
    }

    public function doLogout() {
        $this->ci->session->unset_userdata(array(
            'active' => 0,
            'id' => 0,
            'alias' => NULL,
            'name' => NULL,
            'role' => 0));
    }

    public function doUpdate($data) {
        if($this->inputCheck('update', $data)){
            $newData = array();
            $newData['UserAlias'] = $data['UserAlias'];
            $this->ci->session->set_userdata('alias', $newData['UserAlias']);
            if (trim($data['password1']) != "" || trim($data['password2']) != "" || trim($data['password3']) != "") {
                $newData['UserPassword'] = md5($data['password3']);
            }
            $this->ci->user->update($newData, $data['UserName']);
        }
    }

    public function get() {
        $user = $this->activeUser;
        if ($user['active'] == 1) {
            return $this->ci->user->get($user['name']);
        }
        return FALSE;
    }

}

?>
