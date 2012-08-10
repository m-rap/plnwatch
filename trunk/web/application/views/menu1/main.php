<script type="text/javascript" charset="utf-8">
    $(document).ready(function() {
        $("#menu1").dataTable();
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
        <?php foreach ($data as $r) : ?>
            <tr>
                <td><?php echo $r->IDPEL ?></td>
                <td><?php echo $r->NAMA ?></td>
                <td><?php echo $r->JENIS_MK ?></td>
                <td><?php echo $r->KDGARDU ?></td>
                <td><?php echo $r->NOTIANG ?></td>
            </tr>
        <?php endforeach; ?>
    </tbody>
</table>