<script type="text/javascript" charset="utf-8">
    $(document).ready(function() {
        $("#menu1").dataTable({
            "bProcessing": true,
            "bServerSide": true,
            "sAjaxSource": "<?php echo $sAjaxSource ?>",
            "sColumns"   : "IDPEL,NAMA,JENIS_MK,KDGARDU,NOTIANG",
            "iDisplayLength" : 25
        });
    } );
</script>

<h2><?php echo $pageTitle ?></h2>
<hr />
<table id="menu1" class="display">
    <thead>
        <tr>
            <th><?php echo $label['IDPEL'] ?></th>
            <th><?php echo $label['NAMA'] ?></th>
            <th><?php echo $label['JENIS_MK'] ?></th>
            <th><?php echo $label['KDGARDU'] ?></th>
            <th><?php echo $label['NOTIANG'] ?></th>
        </tr>
    </thead>
    <tbody>

    </tbody>
</table>