<?php

echo form_open('', array('method' => 'get'));
echo 'Area : ' . form_dropdown('area', $dropdownData['area']['list'], $dropdownData['area']['input']) . '<br />'
 . 'Rentang Daya : ' . form_dropdown('daya', $dropdownData['daya']['list'], $dropdownData['daya']['input']) . '<br />'
 . 'Rentang Tgl. Pasang : ' . form_dropdown('tglPasang', $dropdownData['tglPasang']['list'], $dropdownData['tglPasang']['input']) . '<br />'
 . form_submit('submit', ' lihat data ');
echo form_close();

echo anchor('menu1/export/?area=' . $dropdownData['area']['input'] . '&daya=' . $dropdownData['daya']['input'] . '&tglPasang=' . $dropdownData['tglPasang']['input'], 'download')
 . ' (proses membutuhkan waktu lebih untuk pertama kali)';
?>