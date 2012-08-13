<?php

/**
 * Description of Dil
 *
 * @author spondbob
 */
class Dil extends CI_Model {

    public $table = 'DIL';

    public function __construct() {
        parent::__construct();
        $this->load->database();
    }

    public function attributeLabels() {
        return array(
            'JENIS_MK' => 'Jenis Meteran',
            'IDPEL' => 'ID',
            'NAMA' => 'Nama Pelanggan',
            'TARIF' => 'Tarif',
            'DAYA' => 'Daya',
            'TGLPASANG_KWH' => 'Tanggal Pasang',
            'MEREK_KWH' => 'Merek KWH',
            'KDGARDU' => 'Kode Gardu',
            'NOTIANG' => 'No. Tiang',
            'KODEAREA' => 'Kode Area',
        );
    }

    public function getListArea(){
        $this->db->distinct();
        $this->db->select('KODEAREA');
        return $this->db->get_where($this->table)->result();
    }

    public function filterMenu1($filter, $year) {
        $where = "KODEAREA = '" . $filter['area'] . "'";
        $where .= " AND DAYA ";
        if (array_key_exists('max', $filter['daya'])) {
            $where .= "BETWEEN " . $filter['daya']['min'] . " AND " . $filter['daya']['max'];
        } else {
            $where .= ">= " . $filter['daya']['min'];
        }

        $where .= " AND (" . $year . " - DATE_FORMAT(TGLPASANG_KWH, '%Y')) ";
        if (array_key_exists('max', $filter['tglPasang'])) {
            $where .= "BETWEEN " . $filter['tglPasang']['min'] . " AND " . $filter['tglPasang']['max'];
        } else {
            $where .= ">= " . $filter['tglPasang']['min'];
        }

        $this->db->select($filter['select']);
        if($filter['limit'] == -1 && $filter['offset'] == -1){
            $d = $this->db->get_where($this->table, $where);
            $num = $d->num_rows();
            $data = $d->result();
        }else{
            $d = $this->db->get_where($this->table, $where, $filter['limit'], $filter['offset']);
            $total = $this->db->get_where($this->table, $where);
            $data = $d->result();
            $num = $total->num_rows();
        }
        
        return array(
            'num' => $num,
            'data' => $data
        );
    }

}

?>
