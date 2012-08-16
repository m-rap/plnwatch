<?php

echo form_open('', array('method' => 'get'));
echo 'Area : ' . form_dropdown('area', $dropdownData['area']['list'], $dropdownData['area']['input']) . '<br />'
 . form_submit('', ' lihat data ');
echo form_close();

echo anchor('menu4/export/?area=' . $dropdownData['area']['input'], 'download')
 . ' (proses membutuhkan waktu lebih untuk pertama kali)';
?>