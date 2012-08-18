<?php

echo form_open('', array('method' => 'get'));
echo 'BLTH terbaru :<br />' . form_input('blth', substr($blth, 0, 2) . ' ' . substr($blth, 2), 'disabled size="6"') . '<br />'
 . 'Area :<br />' . form_dropdown('area', $dropdownData['area']['list'], $dropdownData['area']['input']) . '<br />'
 . 'Rentang Daya :<br />' . form_dropdown('daya', $dropdownData['daya']['list'], $dropdownData['daya']['input']) . '<br />'
 . 'Rentang Tgl. Pasang :<br />' . form_dropdown('tglPasang', $dropdownData['tglPasang']['list'], $dropdownData['tglPasang']['input']) . '<br />'
 . form_submit('submit', 'lihat data', 'class="button"');
echo form_close();

echo anchor('menu1/export/?area=' . $dropdownData['area']['input'] . '&daya=' . $dropdownData['daya']['input'] . '&tglPasang=' . $dropdownData['tglPasang']['input'], 'download')
 . ' (proses membutuhkan waktu lebih untuk pertama kali)';
?>