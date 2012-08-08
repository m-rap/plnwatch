<?php
echo form_open();
echo 'Area : ' . form_dropdown('area', $dropdownData['area']).'<br />'
 . ' Rentang Jam Nyala : ' . form_dropdown('jamNyala', $dropdownData['jamNyala']).'<br />'
 . ' ' . form_submit('', ' lihat data ');
echo form_close();
?>