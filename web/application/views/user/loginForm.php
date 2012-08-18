<div style="width:300px;margin:0 auto;text-align: center">
    <?php
    switch ($showMessage) {
        case 0 :
            echo validation_errors();
            break;
        case -1 :
            echo "Username atau Password Anda salah, pastikan Anda memiliki akses.";
            break;
        default :
            NULL;
    }
    ?>
</div>
<p>&nbsp;</p>
<div style="width:250px;margin:0 auto;">
    <fieldset style="padding:5px">
        <legend>Autentikasi</legend>
        <form method="post" action="<?php echo base_url() ?>user/login">
            <table>
                <tr>
                    <td>Username</td>
                    <td> : <input type="text" name="user[UserName]" value="<?php echo set_value('user[UserName]') ?>" class="input" /></td>
                </tr>
                <tr>
                    <td>Password</td>
                    <td> : <input type="password" name="user[UserPassword]" class="input" /></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right"><input type="submit" name="login" value="Login" class="button" /></td>
                </tr>
            </table>
        </form>
    </fieldset>
</div>