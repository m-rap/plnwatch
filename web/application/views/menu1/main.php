<h2>Menu1</h2>
<hr />
<?php
echo form_open();
echo 'Area : ' . form_dropdown('area', $dropdownData['area'])
 . ' Rentang Meter-an : ' . form_dropdown('meter', $dropdownData['meter'])
 . ' Rentang Tgl. Pasang : ' . form_dropdown('tglpasang', $dropdownData['tglpasang'])
 . ' ' . form_submit('', ' lihat data ');
echo form_close();
?>
<p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p>
<p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p>