<script type="text/javascript" charset="utf-8">
    $(document).ready(function() {
        $("#menu2").dataTable({
            "bProcessing": true,
            "bServerSide": true,
            "sAjaxSource": "<?php echo $sAjaxSource ?>",
            "iDisplayLength" : 25,
            "bFilter"    : false,
            "sPaginationType": "full_numbers"
        });
    } );
</script>

<h2><?php echo $pageTitle ?></h2>
<?php
echo form_open('', array('method' => 'get'));
echo 'BLTH : ' . form_input('blth', substr($blth, 0, 2).' '.substr($blth, 2), 'disabled size="4"')
 . ' Area : ' . form_dropdown('area', $dropdownData['area']['list'], $dropdownData['area']['input'])
 . ' Jam Nyala : ' . form_dropdown('jamNyala', $dropdownData['jamNyala']['list'], $dropdownData['jamNyala']['input'])
 . ' ' . form_submit('', 'lihat data', 'class="button"')
 . ' ' . form_submit('download', 'download', 'class="button"');
echo form_close();
?>
<hr />
<table id="menu2" class="display">
    <thead>
        <tr>
            <th><?php echo $label['IDPEL'] ?></th>
            <th><?php echo $label['NAMA'] ?></th>
            <th><?php echo $label['TARIF'] ?></th>
            <th><?php echo $label['DAYA'] ?></th>
            <th><?php echo $label['FAKM'] ?></th>
            <th><?php echo $label['JAMNYALA'] ?></th>
        </tr>
    </thead>
    <tbody>

    </tbody>
</table>