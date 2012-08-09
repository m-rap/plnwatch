<?php

/**
 * Description of User
 *
 * @author spondbob
 */
class UserController extends CI_Controller {
    
    private $controller = 'User';
    private $activeUser = null;
    public function __construct() {
        parent::__construct();
        $this->load->library('Layout', array('controller' => strtolower($this->controller), 'activeLayout' => '1column'));
        $this->load->helper(array('url'));
        $this->activeUser = $this->libuser->activeUser;
    }
    
    public function index(){
        $this->login();
    }
    
    public function login(){
        $return = null;
        $data = array(
            'showMessage' => NULL,
        );
        
        // if pressed button login
        if(isset($_POST['login'])){
            $return = $this->libuser->doLogin($_POST['user']);
            $data['showMessage'] = $return;
        }
        
        // if already logged in
        if($this->activeUser['active'] == 1 || $return == 1){
            redirect('user/profile');
        }
        
        $this->layout->render('loginForm', $data);
    }
    
    public function logout(){
        $this->libuser->doLogout();
        redirect('/');
    }
    
    public function _oldPasswordCheck($str){
        $user = $this->activeUser;
        $userData = $this->user->get($user['name']);
        if(md5($str) == $userData->UserPassword){
            return TRUE;
        }
        $this->form_validation->set_message('_oldPasswordCheck', 'Password Lama salah.');
        return FALSE;
    }
    public function _matchPasswordCheck($str){
        if(md5($str) == $userData->UserPassword){
            return TRUE;
        }
        $this->form_validation->set_message('_matchPasswordCheck', 'Password Baru tidak sesuai dengan Password Konfirmasi..');
        return FALSE;
    }
    public function profile(){
        $data = array();
        $data['showMessage'] = null;
        $data['pageTitle'] = 'Beranda';
        $data['label'] = $this->user->attributeLabels();
        if(isset($_POST['update'])){
            $return = $this->libuser->doUpdate($_POST['user']);
            $data['showMessage'] = $return;
            $libuser = new LibUser(); // trigger LibUser to update user session data
            $this->activeUser = $libuser->activeUser;
        }
        $data['user'] = $this->activeUser;
        $data['sidebar'] = $this->activeUser;
        
        $layout = new Layout(array('controller' => strtolower($this->controller)));
        $layout->render('profile', $data);
    }
}

?>
