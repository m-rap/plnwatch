<?php

echo form_open('', array('method' => 'get'));
echo 'Area :<br />' . form_dropdown('area', $dropdownData['area'], $this->input->get('area')) . '<br />'
 . ' Tren Pemakaian KWH :<br />' . form_dropdown('tren', $dropdownData['tren'], $this->input->get('tren')) . '<br />'
 . form_submit('', 'lihat data', 'class="button"');
echo form_close();

echo anchor('menu3/export/?area=' . $this->input->get('area') . '&tren=' . $this->input->get('tren'), 'download')
 . ' (proses membutuhkan waktu lebih untuk pertama kali)';
?>