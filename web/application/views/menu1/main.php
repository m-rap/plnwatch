<script type="text/javascript" charset="utf-8">
    $(document).ready(function() {
        $("#menu1").dataTable({
            "bProcessing": true,
            "bServerSide": true,
            "sAjaxSource": "<?php echo $sAjaxSource ?>",
            "sColumns"   : "<?php echo implode(',', $select) ?>",
            "iDisplayLength" : 25,
            "bFilter"    : false,
            "sPaginationType": "full_numbers"
        });
    } );
</script>

<h2><?php echo $pageTitle ?></h2>

<?php
echo form_open('', array('method' => 'get'));
echo 'BLTH terbaru : ' . form_input('blth', substr($blth, 0, 2) . ' ' . substr($blth, 2), 'disabled size="6"')
 . ' Area : ' . form_dropdown('area', $dropdownData['area']['list'], $dropdownData['area']['input'])
 . ' Rentang Daya : ' . form_dropdown('daya', $dropdownData['daya']['list'], $dropdownData['daya']['input'])
 . ' Rentang Tgl. Pasang : ' . form_dropdown('tglPasang', $dropdownData['tglPasang']['list'], $dropdownData['tglPasang']['input'])
 . ' ' . form_submit('submit', 'lihat data', 'class="button"')
 . ' ' . anchor('menu1/export/?area=' . $dropdownData['area']['input'] . '&daya=' . $dropdownData['daya']['input'] . '&tglPasang=' . $dropdownData['tglPasang']['input'], 'download');
echo form_close();
?>
<hr />
<p>&nbsp;</p>
<table id="menu1" class="display">
    <thead>
        <tr>
            <th><?php echo $label['IDPEL'] ?></th>
            <th><?php echo $label['NAMA'] ?></th>
            <th><?php echo $label['JENIS_MK'] ?></th>
            <th><?php echo $label['DAYA'] ?></th>
            <th><?php echo $label['TGLPASANG_KWH'] ?></th>
            <th><?php echo $label['KDGARDU'] ?></th>
            <th><?php echo $label['NOTIANG'] ?></th>
        </tr>
    </thead>
    <tbody>

    </tbody>
</table>