<?php
echo form_open('', array('method' => 'get'));
echo 'Area : ' . form_dropdown('area', $dropdownData['area']['data'], $dropdownData['area']['selected']).'<br />'
 .'Rentang Daya : ' . form_dropdown('daya', $dropdownData['daya']['data'], $dropdownData['daya']['selected']).'<br />'
 .'Rentang Tgl. Pasang : ' . form_dropdown('tglPasang', $dropdownData['tglPasang']['data'], $dropdownData['tglPasang']['selected']).'<br />'
 .form_submit('submit', ' lihat data ');
echo form_close();

if(isset($_GET['area']) && isset($_GET['daya']) && isset($_GET['tglPasang'])){
    echo anchor('menu1/export/?area='.$_GET['area'].'&daya='.$_GET['daya'].'&tglPasang='.$_GET['tglPasang'], 'download')
        .' (proses akan berjalan cukup lama untuk pertama kali)';
}
?>