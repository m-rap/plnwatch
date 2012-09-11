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
<hr />
<table id="menu3" class="display">
    <thead>
        <tr><?php foreach ($label as $l) echo '<th>' . $l . '</th>'; ?></tr>
    </thead>
    <tbody>

    </tbody>
</table>