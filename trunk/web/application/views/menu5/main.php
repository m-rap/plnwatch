<h2><?php echo $pageTitle ?></h2>

<hr />
<p>&nbsp;</p>
<table id="menu1" class="display">
    <thead>
        <tr>
            <th rowspan="2">Area</th>
            <th colspan="2">Jumlah Pelanggan Total</th>
            <th colspan="3">Jumlah Tren Pelanggan</th>
            <th rowspan="2">Download<br />(berdasarkan jam nyala)</th>
        </tr>
        <tr>
            <th>Pra</th>
            <th>Pasca</th>
            <th>Naik</th>
            <th>Turun</th>
            <th>Flat</th>
        </tr>
    </thead>
    <tbody>
        <?php foreach ($data->result() as $row) : echo form_open('menu5/export', array('method' => 'get', '_target' => 'blank')); ?>
            <tr>
                <td><?php echo $row->KODEAREA ?></td>
                <td><?php echo $row->PRA ?></td>
                <td><?php echo $row->PASCA ?></td>
                <td><?php echo $row->NAIK ?></td>
                <td><?php echo $row->TURUN ?></td>
                <td><?php echo $row->FLAT ?></td>
                <td align="center"><?php echo form_hidden('kodeArea', $row->KODEAREA) . form_dropdown('jamNyala', $jamNyala) . ' ' . form_submit('download', 'download', 'class="button"') ?></td>
            </tr>
            <?php echo form_close();
        endforeach;
        ?>
    </tbody>
</table>