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
<div style="width:100%;overflow:scroll">
    <table id="menu3" class="display">
        <thead>
            <tr>
                <th rowspan="2"><?php echo $label['IDPEL'] ?></th>
                <th rowspan="2"><?php echo $label['NAMA'] ?></th>
                <?php foreach ($tren as $t) echo '<th colspan="3">' . $t . '</th>'; ?>
            </tr>
            <tr>
            <?php foreach ($tren as $t): ?>
                <th>LWBP</th>
                <th>WBP</th>
                <th>KVARH</th>
            <?php endforeach; ?>
            </tr>
        </thead>
        <tbody>

        </tbody>
    </table>
</div>