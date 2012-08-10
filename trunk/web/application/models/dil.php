<?php

/**
 * Description of Dil
 *
 * @author spondbob
 */
class Dil extends CI_Model {

    public $table = 'DIL';
    
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
            'TGLPASANG_KWH' => 'Tanggal Pasang',
            'MEREK_KWH' => 'Merek KWH',
            'KDGARDU' => 'Kode Gardu',
            'NOTIANG' => 'No. Tiang',
            'KODEAREA' => 'Kode Area',
        );
    }

    public function filterMenu1($filter, $year){
        $where = "KODEAREA = '". $filter['area']."'";
        $where .= " AND DAYA ";
        if(array_key_exists('max', $filter['daya'])){
            $where .= "BETWEEN ".$filter['daya']['min']." AND ".$filter['daya']['max'];
        }else{
            $where .= ">= ".$filter['daya']['min'];
        }
        
        $where .= " AND (".$year." - DATE_FORMAT(TGLPASANG_KWH, '%Y')) ";
        if(array_key_exists('max', $filter['tglPasang'])){
            $where .= "BETWEEN ".$filter['tglPasang']['min']." AND ".$filter['tglPasang']['max'];
        }else{
            $where .= ">= ".$filter['tglPasang']['min'];
        }

        return $this->db->get_where($this->table, $where, $filter['limit'], $filter['offset'])->result();
    }
}

?>
