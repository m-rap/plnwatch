<?php
echo form_open('', array('method' => 'get'));
echo 'Area : ' . form_dropdown('area', $dropdownData['area']).'<br />'
 .form_submit('', ' lihat data ');
echo form_close();

if(isset($_GET['area'])){
    echo anchor('menu4/export/?area='.$_GET['area'], 'download')
        .' (proses membutuhkan waktu lebih untuk pertama kali)';
}
?>