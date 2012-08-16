<?php
/**
 * Description of Option
 *
 * @author spondbob
 */
class Option extends CI_Model {
    private $table = 'Option';
    public function __construct() {
        parent::__construct();
        $this->load->database();
    }
    
    public function attributeLabels() {
        return array(
            'OptionKey' => 'Key',
            'OptionValue' => 'Value',
        );
    }
    
    public function getValue($OptionKey){
        return $this->db->get_where($this->table, array('OptionKey' => $OptionKey))->row()->OptionValue;
    }
    
    public function update($data){
        return $this->db->update($this->table, array('OptionValue' => $data['OptionValue']), array('OptionKey' => $data['OptionKey']));
    }
}

?>
