<script type="text/javascript" charset="utf-8">
    $(document).ready(function() {
        $("#menu3").dataTable({
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
echo 'Area : ' . form_dropdown('area', $dropdownData['kodearea']['list'], $dropdownData['kodearea']['input'])
 . ' Tren : ' . form_dropdown('tren', $dropdownData['tren']['list'], $dropdownData['tren']['input'])
 . ' ' . form_submit('', 'lihat data', 'class="button"')
 . ' ' . form_submit('download', 'download', 'class="button"');
echo form_close();
?>
<hr />
<table id="menu3" class="display">
    <thead>
        <tr><?php foreach ($label as $l) echo '<th>' . $l . '</th>'; ?></tr>
    </thead>
    <tbody>

    </tbody>
</table>