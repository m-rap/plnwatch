<?php

echo form_open('', array('method' => 'get'));
echo 'Area :<br />' . form_dropdown('area', $dropdownData['area']['list'], $dropdownData['area']['input']) . '<br />'
 . 'Kode Mutasi :<br />' . form_dropdown('mutasi', $dropdownData['mutasi']['list'], $dropdownData['mutasi']['input']) . '<br />'
 . form_submit('', 'lihat data', 'class="button"');
echo form_close();

echo anchor('menu4/export/?area=' . $dropdownData['area']['input'], 'download')
 . ' (proses membutuhkan waktu lebih untuk pertama kali)';
?>