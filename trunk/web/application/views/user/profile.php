<?php
switch ($showMessage) {
    case 0 :
        echo validation_errors();
        break;
    default :
        NULL;
}
?>
<h2>Profile</h2>
<form method="post">
<table>
    <tr>
        <td><?php echo $label['UserName'] ?></td>
        <td> : <input type="text" value="<?php echo $user['name'] ?>" disabled /><input type="hidden" name="user[UserName]" value="<?php echo $user['name'] ?>" /></td>
    </tr>
    <tr>
        <td><?php echo $label['UserAlias'] ?></td>
        <td> : <input type="text" name="user[UserAlias]" value="<?php echo $user['alias'] ?>" /></td>
    </tr>
    <tr>
        <td>Password Lama</td>
        <td> : <input type="password" name="user[password1]" /></td>
    </tr>
    <tr>
        <td>Password Baru</td>
        <td> : <input type="password" name="user[password2]" /></td>
    </tr>
    <tr>
        <td>Konfirmasi Password Baru</td>
        <td> : <input type="password" name="user[password3]" /></td>
    </tr>
    <tr>
        <td colspan="2"><input type="submit" name="update" value="simpan" /></td>
    </tr>
</table>
</form>