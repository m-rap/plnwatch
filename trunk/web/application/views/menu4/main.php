<script type="text/javascript" charset="utf-8">
    $(document).ready(function() {
        $("#menu4").dataTable({
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
<hr />
<table id="menu4" class="display">
    <thead>
        <tr>
            <th><?php echo $label['IDPEL'] ?></th>
            <th><?php echo $label['NAMA'] ?></th>
            <th><?php echo $label['TARIF'] ?></th>
            <th><?php echo $label['DAYA'] ?></th>
            <th><?php echo $label['RPTAG'] ?></th>
            <th><?php echo $label['TGLBAYAR'] ?></th>
            <th><?php echo $label['PEMKWH'] ?></th>
        </tr>
    </thead>
    <tbody>

    </tbody>
</table>