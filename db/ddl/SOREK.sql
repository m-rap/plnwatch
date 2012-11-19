DROP TABLE IF EXISTS `SOREK_062012`;
-- phpMyAdmin SQL Dump
-- version 2.11.7
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Nov 20, 2012 at 12:15 AM
-- Server version: 5.0.51
-- PHP Version: 5.2.6

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";

--
-- Database: `plnwatch`
--

-- --------------------------------------------------------

--
-- Table structure for table `sorek_062012`
--

CREATE TABLE IF NOT EXISTS `sorek_062012` (
  `IDPEL` varchar(12) NOT NULL,
  `TGLBACA` date default NULL,
  `PEMKWH` double default NULL,
  `KODEAREA` varchar(5) default NULL,
  `JAMNYALA` int(11) default NULL,
  `FAKM` varchar(10) default NULL,
  `KWHLWBP` int(11) default NULL,
  `KWHWBP` int(11) default NULL,
  `KWHKVARH` int(11) default NULL,
  `TREN` enum('naik','turun','flat') NOT NULL,
  PRIMARY KEY  (`IDPEL`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
