<?php

/**
 * Description of User
 *
 * @author spondbob
 */
class User extends CI_Model {

    public $table = 'User';
    
    function __construct() {
        parent::__construct();
        $this->load->database();
    }
    
    public function attributeLabels() {
        return array(
            'UserId' => 'ID',
            'UserAlias' => 'Alias',
            'UserName' => 'Username',
            'UserPassword' => 'Password',
            'UserCreated' => 'Date Created',
            'UserLoginTime' => 'Last Login Time',
            'UserLoginIp' => 'Last Login IP',
            'userLoginBrowser' => 'Last Login Browser',
            'UserRole' => 'Role'
        );
    }
    
    public function login($data){
        $q = $this->db->get_where($this->table, array('UserName' => $data['userName'], 'UserPassword' => $data['userPassword']));
        if($q->num_rows() == 1){
            $user = $q->row();
            $this->db->update($this->table
                    , array('UserLoginTime' => $data['userLoginTime'], 'UserLoginIp' => $data['userLoginIp'], 'UserLoginBrowser' => $data['userLoginBrowser'])
                    , array('UserId' => $user->UserId));
            return $user;
        }else{
            return false;
        }
    }
    
    public function update($data, $id){
        $this->db->update($this->table, $data, array('UserName' => $id));
    }

    public function get($userName){
        return $this->db->get_where($this->table, array('UserName' => $userName))->row();
    }
}

?>
