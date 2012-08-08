<?php
echo form_open();
echo 'Area : ' . form_dropdown('area', $dropdownData['area']).'<br />'
 .form_submit('', ' lihat data ');
echo form_close();
?>