<h2><?php echo $pageTitle ?></h2>
<hr />
<?php
echo form_open();
echo 'Area : ' . form_dropdown('area', $dropdownData['area'])
 . ' ' . form_submit('', ' lihat data ');
echo form_close();
?>