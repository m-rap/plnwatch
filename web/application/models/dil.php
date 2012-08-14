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
        $this->load->dbutil();
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

    public function getListArea() {
        $this->db->distinct();
        $this->db->select('KODEAREA');
        return $this->db->get_where($this->table)->result();
    }

    public function filterMenu1Condition($filter) {
        $year = date('Y');
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
        return $where;
    }

    public function filterMenu1($filter, $returnData = true) {
        $where = $this->filterMenu1Condition($filter);
        //die($filter['limit'].'-'.$filter['offset']);
        $this->db->select(implode(',', $filter['select']));
        $this->db->order_by($filter['order']);
        $q = $this->db->get_where($this->table, $where, $filter['limit'], $filter['offset']);
        if($returnData){
            return $q->result();
        }
        return $q;
    }

    public function count($filter) {
        $where = $this->filterMenu1Condition($filter);
        return $this->db->get_where($this->table, $where)->num_rows();
    }
    
    public function export($filter) {
        $where = $this->filterMenu1Condition($filter);
        $n = $this->db->get_where($this->table, $where)->num_rows();
    }

}

?>
