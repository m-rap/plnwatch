<h2><?php echo $pageTitle ?></h2>
<hr />
<?php
echo form_open();
echo 'Area : ' . form_dropdown('area', $dropdownData['area'])
 . ' Rentang Jam Nyala : ' . form_dropdown('jamNyala', $dropdownData['jamNyala'])
 . ' ' . form_submit('', ' lihat data ');
echo form_close();
?>