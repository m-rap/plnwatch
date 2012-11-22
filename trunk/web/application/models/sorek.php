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
        $this->table = $this->table . '_' . $this->currentBLTH2(); //$this->currentBLTH();
    }

    public function attributeLabels() {
        return array(
            'IDPEL' => 'ID Pelanggan',
            'TGLBACA' => 'Tanggal Baca',
            'PEMKWH' => 'Pemakaian KWH',
            'KODEAREA' => 'Kode Area',
            'JAMNYALA' => 'Jam Nyala',
            'FAKM' => 'Faktor Kali',
            'KWHLWBP' => 'KWHLWBP',
            'KWHWBP' => 'KWHWBP',
            'KWHKVARH' => 'KWHKVARH',
            'TREN' => 'Tren',
        );
    }

    public function getSortedSorekTables() {
        $tables = $this->db->query("SHOW TABLES LIKE 'SOREK_%'")->result_array();
        foreach ($tables as $key => $value) {
            $tables[$key] = array_values($value);
            $tables[$key] = $tables[$key][0];
        }
        sort($tables);
        return $tables;
    }

    public function getSortedLastNSorekTables($n) {
        $tables = $this->getSortedSorekTables();
        return array_values(array_slice($tables, -$n));
    }

    public function currentBLTH() {
        $bl = date('m');
        $th = date('Y');
        $table = explode(',', strtoupper(implode(',', $this->db->list_tables())));
        $i = 0;
        while (!in_array('SOREK_' . $bl . $th, $table)) {
            //while (mysql_query('select 1 from `SOREK_' . $bl . $th . '`') === FALSE) {
            if ($i++ == 3)
                die('no table SOREK found.');
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

    public function currentBLTH2() {
        $tables = $this->getSortedSorekTables();
        return substr($tables[count(($tables)) - 1], -6);
    }

    public function getListArea() {
        $this->db->distinct();
        $this->db->select('KODEAREA');
        $this->db->order_by('KODEAREA');
        return $this->db->get_where($this->table)->result();
    }

    public function export($filter){
        $this->db->select($filter['select']);
        $this->db->from('DIL');
        $this->db->join($this->table, 'DIL.IDPEL = '.$this->table.'.IDPEL', 'LEFT');
        $this->db->where($filter['condition']);
        return $this->db->get();
    }

    /*
     * ---------------------------------------------- Menu 2 ----------------------------------------------
     */

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
        $this->db->select(implode(',', $filter['select']));
        $this->db->join('DIL', $this->table . '.IDPEL = DIL.IDPEL', 'left');
        $this->db->order_by($filter['order']);
        $q = $this->db->get_where($this->table, $this->filterMenu2Condition($filter), $filter['limit'], $filter['offset']);
        if ($returnData) {
            return $q->result();
        }
        return $q;
    }

    public function countFilterMenu2($filter) {
        $where = $this->filterMenu2Condition($filter);
        return $this->db->get_where($this->table, $where)->num_rows();
    }

    /*
     * ---------------------------------------------- Menu 3 ----------------------------------------------
     */

    /*
    public function getTrenNaik($filter, $returnData = true) {
        $tables = $this->getSortedLastNSorekTables(6);

        $select = '';
        $select2 = '';
        $join = '';
        $where = '';
        $alias = 'a';
        $n = count($tables);
        foreach ($tables as $i => $t) {
            $join .= "$t $alias ON dil.idpel = $alias.idpel " . (($i < $n - 1) ? 'JOIN ' : '');
            $select .= "$alias.jamnyala as jamnyala" . ($i + 1) . ', ';
            $select2 .= "jamnyala" . ($i + 1) . ', ';
            $alias++;
            if ($i >= $n - 1)
                continue;
            $where .= 'jamnyala' . ($i + 2) . ' - jamnyala' . ($i + 1) . ' > selisih ' . (($i < $n - 2) ? 'AND ' : '');
        }

        $limit = '';
        if (isset($filter['limit'])) {
            $limit = 'LIMIT ' . ((isset($filter['offset'])) ? $filter['offset'] . ',' : '') . $filter['limit'];
        }
        unset($filter['limit']);
        unset($filter['offset']);

        foreach ($filter as $key => $value) {
            $filter[$key] = "$key = '$value'";
        }
        array_unshift($filter, $where);
        $where = implode(' AND ', $filter);

        $sql = "
SELECT idpel, $select2 selisih FROM (
    SELECT dil.IDPEL as idpel, dil.KODEAREA as kodearea, $select 0.25 * a.jamnyala as selisih
    FROM dil JOIN $join
) s 
WHERE $where $limit";

        $query = $this->db->query($sql);
        if (!$returnData)
            return $query;
        return array('data' => $query->result_array(), 'num' => $query->num_rows());
    }

    public function getTrenFlat($filter, $returnData = true) {
        $tables = $this->getSortedLastNSorekTables(6);

        $select = '';
        $select2 = '';
        $join = '';
        $where = '';
        $alias = 'a';
        $n = count($tables);
        foreach ($tables as $i => $t) {
            $join .= "$t $alias ON dil.idpel = $alias.idpel " . (($i < $n - 1) ? 'JOIN ' : '');
            $select .= "$alias.jamnyala as jamnyala" . ($i + 1) . ', ';
            $select2 .= "jamnyala" . ($i + 1) . ', ';
            $alias++;
            if ($i >= $n - 1)
                continue;
            $where .= 'ABS(jamnyala' . ($i + 1) . ' - jamnyala' . ($i + 2) . ') <= selisih ' . (($i < $n - 2) ? 'AND ' : '');
        }

        $limit = '';
        if (isset($filter['limit'])) {
            $limit = 'LIMIT ' . ((isset($filter['offset'])) ? $filter['offset'] . ',' : '') . $filter['limit'];
        }
        unset($filter['limit']);
        unset($filter['offset']);

        foreach ($filter as $key => $value) {
            $filter[$key] = "$key = '$value'";
        }
        array_unshift($filter, $where);
        $where = implode(' AND ', $filter);

        $sql = "
SELECT idpel, $select2 selisih FROM (
    SELECT dil.IDPEL as idpel, dil.KODEAREA as kodearea, $select 0.05 * a.jamnyala as selisih
    FROM dil JOIN $join
) s 
WHERE $where $limit";

        $query = $this->db->query($sql);
        if (!$returnData)
            return $query;
        return array('data' => $query->result_array(), 'num' => $query->num_rows());
    }

    public function getTrenTurun($filter = array(), $returnData = true) {
        $tables = $this->getSortedLastNSorekTables(6);

        $select = '';
        $select2 = '';
        $join = '';
        $where = '';
        $alias = 'a';
        $n = count($tables);
        foreach ($tables as $i => $t) {
            $join .= "$t $alias ON dil.idpel = $alias.idpel " . (($i < $n - 1) ? 'JOIN ' : '');
            $select .= "$alias.jamnyala as jamnyala" . ($i + 1) . ', ';
            $select2 .= "jamnyala" . ($i + 1) . ', ';
            $alias++;
            if ($i >= $n - 1)
                continue;
            $where .= 'jamnyala' . ($i + 1) . ' - jamnyala' . ($i + 2) . ' > selisih ' . (($i < $n - 2) ? 'AND ' : '');
        }

        $limit = '';
        if (isset($filter['limit'])) {
            $limit = 'LIMIT ' . ((isset($filter['offset'])) ? $filter['offset'] . ',' : '') . $filter['limit'];
        }
        unset($filter['limit']);
        unset($filter['offset']);

        foreach ($filter as $key => $value) {
            $filter[$key] = "$key = '$value'";
        }
        array_unshift($filter, $where);
        $where = implode(' AND ', $filter);

        $sql = "
SELECT idpel, $select2 selisih FROM (
    SELECT dil.IDPEL as idpel, dil.KODEAREA as kodearea, $select 0.25 * a.jamnyala as selisih
    FROM dil JOIN $join
) s 
WHERE $where $limit";

        $query = $this->db->query($sql);
        if (!$returnData)
            return $query;
        return array('data' => $query->result_array(), 'num' => $query->num_rows());
    }
    */

    public function getTrenLabels() {
        $tables = $this->db->query("SHOW TABLES LIKE 'SOREK_%'")->result_array();
        foreach ($tables as $key => $value) {
            $tables[$key] = array_values($value);
            $tables[$key] = substr($tables[$key][0], -6);
        }
        sort($tables);

        return $tables;
    }

    public function filterMenu3Condition($filter) {
        return array('KODEAREA' => $filter['area'], 'TREN' => $filter['tren']);
    }

    public function filterMenu3($filter, $returnData = true) {
        $this->db->select(implode(',', $filter['select']));
        $this->db->join('DIL', 'DIL.IDPEL = ' . $this->table . '.IDPEL', 'LEFT');
        $this->db->order_by($filter['order']);

        $q = $this->db->get_where($this->table, $this->filterMenu3Condition($filter), $filter['limit'], $filter['offset']);
        if ($returnData) {
            return $q->result();
        }
        return $q;
    }

    public function countFilterMenu3($filter) {
        $filter['limit'] = null;
        $filter['offset'] = null;
        return $this->filterMenu3($filter, false)->num_rows();
    }

}

?>
