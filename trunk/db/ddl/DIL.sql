DROP TABLE IF EXISTS `DIL`;
-- phpMyAdmin SQL Dump
-- version 2.11.7
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Nov 20, 2012 at 12:14 AM
-- Server version: 5.0.51
-- PHP Version: 5.2.6

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";

--
-- Database: `plnwatch`
--

-- --------------------------------------------------------

--
-- Table structure for table `dil`
--

CREATE TABLE IF NOT EXISTS `dil` (
  `JENIS_MK` varchar(20) DEFAULT NULL,
  `IDPEL` varchar(12) NOT NULL,
  `NAMA` varchar(25) DEFAULT NULL,
  `TARIF` varchar(4) DEFAULT NULL,
  `DAYA` int(11) DEFAULT NULL,
  `ALAMAT` varchar(70) DEFAULT NULL,
  `NOTELP` varchar(20) DEFAULT NULL,
  `KODEPOS` varchar(5) DEFAULT NULL,
  `TGLPASANG_KWH` date DEFAULT NULL,
  `KDPEMBMETER` varchar(1) DEFAULT NULL,
  `MEREK_KWH` varchar(24) DEFAULT NULL,
  `KDGARDU` varchar(30) DEFAULT NULL,
  `NOTIANG` varchar(35) DEFAULT NULL,
  `KODEAREA` varchar(5) DEFAULT NULL,
  `KDDK` varchar(25) DEFAULT NULL,
  `TGLPDL` date DEFAULT NULL,
  `TGLNYALA_PB` date DEFAULT NULL,
  `TGLRUBAH_MK` date DEFAULT NULL,
  PRIMARY KEY (`IDPEL`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
