<?php
echo form_open();
echo 'Area : ' . form_dropdown('area', $dropdownData['area']).'<br />'
 .'Rentang Meter-an : ' . form_dropdown('meter', $dropdownData['meter']).'<br />'
 .'Rentang Tgl. Pasang : ' . form_dropdown('tglpasang', $dropdownData['tglpasang']).'<br />'
 .form_submit('', ' lihat data ');
echo form_close();
?>