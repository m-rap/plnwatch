<?php
echo form_open('', array('method' => 'get'));
echo 'Area : ' . form_dropdown('area', $dropdownData['area']['data'], $dropdownData['area']['selected']).'<br />'
 .'Rentang Daya : ' . form_dropdown('daya', $dropdownData['daya']['data'], $dropdownData['daya']['selected']).'<br />'
 .'Rentang Tgl. Pasang : ' . form_dropdown('tglPasang', $dropdownData['tglPasang']['data'], $dropdownData['tglPasang']['selected']).'<br />'
 .form_submit('submit', ' lihat data ');
echo form_close();
?>