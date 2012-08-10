<?php
echo form_open('', array('method' => 'get'));
echo 'Area : ' . form_dropdown('area', $dropdownData['area']).'<br />'
 .'Rentang Daya : ' . form_dropdown('daya', $dropdownData['daya']).'<br />'
 .'Rentang Tgl. Pasang : ' . form_dropdown('tglPasang', $dropdownData['tglPasang']).'<br />'
 .form_submit('submit', ' lihat data ');
echo form_close();
?>