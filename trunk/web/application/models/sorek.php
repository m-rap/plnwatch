<?php

/**
 * Description of Sorek
 *
 * @author spondbob
 */
class Sorek extends CI_Model {

    public $tableName = 'SOREK';
    
    function __construct() {
        parent::__construct();
        $this->load->database();
    }
    
    public function attributeLabels() {
        return array(
            'ID' => 'ID',
            'BLTH' => 'Bulan Tahun',
            'IDPEL' => 'ID Pelanggan',
            'NAMA' => 'Nama',
            'DAYA' => 'Daya',
            'TGLBACA' => 'Tanggal Baca',
            'PEMKWH' => 'Pemakaian KWH',
            'KODEAREA' => 'Kode Area',
        );
    }

}

?>
