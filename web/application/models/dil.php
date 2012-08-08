<?php

/**
 * Description of Dil
 *
 * @author spondbob
 */
class Dil extends CI_Model {

    public $tableName = 'DIL';
    
    function __construct() {
        parent::__construct();
        $this->load->database();
    }
    
    public function attributeLabels() {
        return array(
            'JENIS_MK' => 'Jenis Meteran',
            'IDPEL' => 'ID Pelanggan',
            'NAMA' => 'Nama',
            'TARIF' => 'Tarif',
            'DAYA' => 'Daya',
            'TGLPASANG' => 'Tanggal Pasang',
            'MEREK_KWH' => 'Merek KWH',
            'KDGARDU' => 'Kode Gardu',
            'NOTIANG' => 'No. Tiang',
            'KODEAREA' => 'Kode Area',
        );
    }

}

?>
