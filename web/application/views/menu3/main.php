<h2><?php echo $pageTitle ?></h2>
<hr />
<?php
echo form_open();
echo 'Area : ' . form_dropdown('area', $dropdownData['area'])
 . ' Tren Pemakaian KWH : ' . form_dropdown('tren', $dropdownData['tren'])
 . ' ' . form_submit('', ' lihat data ');
echo form_close();
?>