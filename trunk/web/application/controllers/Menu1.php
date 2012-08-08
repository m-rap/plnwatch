<?php
/**
 * Description of Menu1
 *
 * @author spondbob
 */
class Menu1 extends CI_Controller {
    function __construct() {
        parent::__construct();
        $this->load->library('layout', array('controller' => 'menu1'));
        $this->load->helper(array('form'));
    }
    
    public function index(){
        $data = array();
        $data['dropdownData'] = array(
            'area' => array(),
            'meter' => array(
                0 => '450 - 5.500',
                1 => '6.600 - 33.000',
                2 => '> 41.500'
            ),
            'tglpasang' => array(
                0 => '0 - 10 thn',
                1 => '10 - 15 thn',
                2 => '> 15 thn'
            )
        );
        $data['sidebar']['dropdownData'] = array(
            'area' => array(),
            'meter' => array(
                0 => '450 - 5.500',
                1 => '6.600 - 33.000',
                2 => '> 41.500'
            ),
            'tglpasang' => array(
                0 => '0 - 10 thn',
                1 => '10 - 15 thn',
                2 => '> 15 thn'
            )
        );
        $this->layout->render('main', $data);
    }
}

?>
