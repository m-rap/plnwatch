<?php

/**
 * Description of LibMenu3
 *
 * @author spondbob
 */
class LibMenu3 {

    public function getListTren($value = false) {
        if ($value) {
            return array(
                1 => array('min' => 450, 'max' => 5500),
                2 => array('min' => 6600, 'max' => 33000),
                3 => array('min' => 41500),
            );
        } else {
            return array(
                1 => 'Naik (> 25%)',
                2 => 'Turun (< 25%)',
                3 => 'Flat (0 - 5%)',
            );
        }
    }

}

?>
