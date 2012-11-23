<?php

/**
 * Description of Dph
 *
 * @author spondbob
 */
class Dph extends CI_Model {

    public $table = 'DPH';
    
    function __construct() {
        parent::__construct();
        $this->load->database();
    }
    
    public function attributeLabels() {
        return array(
            'IDPEL' => 'ID Pelanggan',
            'JMLBELI' => 'Jumlah Beli',
            'PEMKWH' => 'Pemakaian KWH',
            'RPTAG' => 'Rp. Beli',
            'TGLBAYAR' => 'Tgl. Beli',
        );
    }

}

?>
