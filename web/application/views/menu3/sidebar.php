<?php
echo form_open();
echo 'Area : ' . form_dropdown('area', $dropdownData['area']).'<br />'
 . ' Tren Pemakaian KWH : ' . form_dropdown('tren', $dropdownData['tren']).'<br />'
 .form_submit('', ' lihat data ');
echo form_close();
?>