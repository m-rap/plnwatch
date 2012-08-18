<?php

echo form_open('', array('method' => 'get'));
echo 'BLTH terbaru :<br />' . form_input('blth', substr($blth, 0, 2).' '.substr($blth, 2), 'disabled size="4"') . '<br />'
 . 'Area :<br />' . form_dropdown('area', $dropdownData['area']['list'], $dropdownData['area']['input']) . '<br />'
 . ' Rentang Jam Nyala :<br />' . form_dropdown('jamNyala', $dropdownData['jamNyala']['list'], $dropdownData['jamNyala']['input']) . '<br />'
 . ' ' . form_submit('', 'lihat data', 'class="button"');
echo form_close();

echo anchor('menu2/export/?area=' . $dropdownData['area']['input'] . '&jamNyala=' . $dropdownData['jamNyala']['input'], 'download')
 . ' (proses membutuhkan waktu lebih untuk pertama kali)';
?>