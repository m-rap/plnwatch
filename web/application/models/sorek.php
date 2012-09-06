<?php

/**
 * Description of Sorek
 *
 * @author spondbob
 */
class Sorek extends CI_Model {

    public $table = 'SOREK';

    public function __construct() {
        parent::__construct();
        $this->load->database();
        $this->table = $this->table . '_' . $this->currentBLTH();
    }

    public function attributeLabels() {
        return array(
            'IDPEL' => 'ID Pelanggan',
            'TGLBACA' => 'Tanggal Baca',
            'PEMKWH' => 'Pemakaian KWH',
            'KODEAREA' => 'Kode Area',
            'JAMNYALA' => 'Jam Nyala',
        );
    }

    public function currentBLTH() {
        $bl = date('m');
        $th = date('Y');
        $table = explode(',', strtoupper(implode(',', $this->db->list_tables())));
        $i=0;
        while (!in_array('SOREK_' . $bl . $th, $table)) {
        //while (mysql_query('select 1 from `SOREK_' . $bl . $th . '`') === FALSE) {
            if($i++ == 3) die('no table SOREK found.');
            if ($bl == '01') {
                $bl = 12;
                $th--;
            } else {
                $bl--;
                $bl = ($bl < 10 ? '0' . $bl : $bl);
            }
        }
        return $bl . $th;
    }

    public function getListArea() {
        $this->db->distinct();
        $this->db->select('KODEAREA');
        $this->db->order_by('KODEAREA');
        return $this->db->get_where($this->table)->result();
    }

    public function filterMenu2Condition($filter) {
        $where = $this->table . ".KODEAREA = '" . $filter['area'] . "'";
        $where .= " AND " . $this->table . ".JAMNYALA ";
        if (array_key_exists('max', $filter['jamNyala'])) {
            $where .= "BETWEEN " . $filter['jamNyala']['min'] . " AND " . $filter['jamNyala']['max'];
        } else {
            $where .= ">= " . $filter['jamNyala']['min'];
        }
        return $where;
    }

    public function filterMenu2($filter, $returnData = true) {
        $where = $this->filterMenu2Condition($filter);
        $this->db->select(implode(',', $filter['select']));
        $this->db->join('DIL', $this->table . '.IDPEL = DIL.IDPEL', 'left');
        $this->db->order_by($filter['order']);
        $q = $this->db->get_where($this->table, $where, $filter['limit'], $filter['offset']);
        if ($returnData) {
            return $q->result();
        }
        return $q;
    }

    public function countFilterMenu2($filter) {
        $where = $this->filterMenu2Condition($filter);
        return $this->db->get_where($this->table, $where)->num_rows();
    }

}

?>
