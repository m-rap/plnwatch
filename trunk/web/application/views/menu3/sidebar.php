<?php
echo form_open();
echo 'Area :<br />' . form_dropdown('area', $dropdownData['area']).'<br />'
 . ' Tren Pemakaian KWH :<br />' . form_dropdown('tren', $dropdownData['tren']).'<br />'
 .form_submit('', 'lihat data', 'class="button"');
echo form_close();
?>