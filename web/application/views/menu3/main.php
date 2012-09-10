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
<?php
echo form_open('', array('method' => 'get'));
echo 'Area : ' . form_dropdown('area', $dropdownData['area'], $this->input->get('area'))
 . ' Tren Pemakaian KWH : ' . form_dropdown('tren', $dropdownData['tren'], $this->input->get('tren'))
 . ' ' . form_submit('', ' lihat data ');
echo form_close();
?>
<table id="menu3" class="display">
    <thead>
        <tr>
            <?php foreach ($label as $l) { ?> 
            <th><?php echo $l; ?></th>
            <?php } ?> 
        </tr>
    </thead>
    <tbody>

    </tbody>
</table>