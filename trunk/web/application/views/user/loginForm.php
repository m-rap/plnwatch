<?php
    switch($showMessage){
        case 0 :
            echo validation_errors();
            break;
        case -1 :
            echo "Username atau Password anda salah, silahkan cek kembali.";
            break;
        default :
            NULL;
    }
?>
<form method="post" action="<?php echo base_url() ?>user/login">
    <table>
        <tr>
            <td>Username</td>
            <td> : <input type="text" name="user[UserName]" value="<?php echo set_value('user[UserName]') ?>" /></td>
        </tr>
        <tr>
            <td>Password</td>
            <td> : <input type="password" name="user[UserPassword]" /></td>
        </tr>
        <tr>
            <td colspan="2"><input type="submit" name="login" value="Login" /></td>
        </tr>
    </table>
</form>