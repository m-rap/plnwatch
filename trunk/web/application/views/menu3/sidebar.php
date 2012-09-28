<?php

echo form_open('', array('method' => 'get'));
echo 'Area :<br />' . form_dropdown('area', $dropdownData['kodearea']['list'], $dropdownData['kodearea']['input']) . '<br />'
 . ' Tren Pemakaian KWH :<br />' . form_dropdown('tren', $dropdownData['tren']['list'], $dropdownData['tren']['input']) . '<br />'
 . form_submit('', 'lihat data', 'class="button"');
echo form_close();

echo anchor('menu3/export/?area=' . $dropdownData['kodearea']['input'] . '&tren=' . $dropdownData['tren']['input'], 'download')
 . ' (proses membutuhkan waktu lebih untuk pertama kali)';
?>