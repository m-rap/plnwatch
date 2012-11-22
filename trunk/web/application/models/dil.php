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
            'JENIS_MK' => 'Jenis Mutasi',
            'IDPEL' => 'ID',
            'NAMA' => 'Nama Pelanggan',
            'TARIF' => 'Tarif',
            'DAYA' => 'Daya',
            'ALAMAT' => 'Alamat',
            'NOTELP' => 'No Telp',
            'KODEPOS' => 'Kode Pos',
            'TGLPASANG_KWH' => 'Tgl. Pasang',
            'KDPEMBMETER' => 'Meteran',
            'MEREK_KWH' => 'Merek KWH',
            'KDGARDU' => 'Kode Gardu',
            'NOTIANG' => 'No. Tiang',
            'KODEAREA' => 'Kode Area',
            'KDDK' => 'Kode Kedudukan',
            'TGLPDL' => 'Tgl. PDL',
            'TGLNYALA_PB' => 'Tgl. Nyala',
            'TGLRUBAH_MK' => 'Tgl. Ubah',
        );
    }

    public function getListArea() {
        $this->db->distinct();
        $this->db->select('KODEAREA');
        $this->db->order_by('KODEAREA');
        return $this->db->get_where($this->table)->result();
    }

    public function export($filter){
        $this->db->select($filter['select']);
        $this->db->from($this->table);
        $this->db->join('');
    }

    /*
     * ---------------------------------------------- Menu 1 ----------------------------------------------
     */

    public function filterMenu1Condition($filter) {
        $year = date('Y');
        $where = "KODEAREA = '" . $filter['area'] . "'";

        //array('KODEAREA' => $filter['area'], "SUBSTR(DIL.TARIF, 3, 1) = " => 'T', 'JENIS_MK LIKE ' => "%".$filter['mutasi']."%"),
        $where .= " AND SUBSTR(DIL.TARIF, 3, 1) " . ($filter['praPasca'] == 1 ? '=' : '<>') . " 'T'";

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
        $this->db->select(implode(',', $filter['select']));
        $this->db->order_by($filter['order']);
        $q = $this->db->get_where($this->table, $this->filterMenu1Condition($filter), $filter['limit'], $filter['offset']);
        if ($returnData) {
            return $q->result();
        }
        return $q;
    }

    // count all according to filter without limit
    public function countFilterMenu1($filter) {
        $where = $this->filterMenu1Condition($filter);
        return $this->db->get_where($this->table, $where)->num_rows();
    }

    /*
     * ---------------------------------------------- Menu 4 ----------------------------------------------
     */

    public function filterMenu4Condition($filter){
        return array('KODEAREA' => $filter['area'], "SUBSTR(DIL.TARIF, 3, 1) = " => 'T', 'JENIS_MK LIKE ' => "%" . $filter['mutasi'] . "%");
    }

    public function filterMenu4($filter, $returnData = true) {
        $this->db->select(implode(',', $filter['select']));
        $this->db->join('DPH', 'DIL.IDPEL = DPH.IDPEL', 'LEFT');
        $this->db->order_by($filter['order']);

        $q = $this->db->get_where($this->table, $this->filterMenu4Condition($filter), $filter['limit'], $filter['offset']);
        if ($returnData) {
            return $q->result();
        }
        return $q;
    }

    public function countFilterMenu4($filter) {
        return $this->db->get_where($this->table, array('KODEAREA' => $filter['area'], "SUBSTR(DIL.TARIF, 3, 1) = " => 'T', 'JENIS_MK LIKE ' => "%" . $filter['mutasi'] . "%"))->num_rows();
    }

    /*
     * ---------------------------------------------- Menu 5 ----------------------------------------------
     */

    public function filterMenu5Condition($filter) {
        return array('KODEAREA' => $filter['KODEAREA'], 'JAMNYALA' => $filter['JAMNYALA']);
    }

    public function filterMenu5($sorekTable) {
        $this->db->select("DIL.KODEAREA AS KODEAREA,
	SUM(IF(SUBSTR(DIL.TARIF, 3, 1) = 'T', 1, 0)) AS PRA,
	SUM(IF(SUBSTR(DIL.TARIF, 3, 1) <> 'T', 1, 0)) AS PASCA,
	SUM(IF(" . $sorekTable . ".TREN = 'naik', 1, 0)) AS NAIK,
	SUM(IF(" . $sorekTable . ".TREN = 'turun', 1, 0)) AS TURUN,
	SUM(IF(" . $sorekTable . ".TREN = 'flat', 1, 0)) AS FLAT", FALSE);
        $this->db->from($this->table);
        $this->db->join($sorekTable, 'DIL.IDPEL = ' . $sorekTable . '.IDPEL', 'LEFT');
        return $this->db->get();
    }

}

?>
