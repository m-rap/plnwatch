DROP TABLE IF EXISTS `DIL`;

-- phpMyAdmin SQL Dump
-- version 2.11.7
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Nov 19, 2012 at 10:40 PM
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

CREATE TABLE IF NOT EXISTS `DIL` (
  `JENIS_MK` varchar(20) default NULL,
  `IDPEL` varchar(12) NOT NULL,
  `NAMA` varchar(25) default NULL,
  `TARIF` varchar(4) default NULL,
  `DAYA` int(11) default NULL,
  `PNJ` varchar(2) default NULL,
  `NAMAPNJ` varchar(62) default NULL,
  `NOBANG` varchar(3) default NULL,
  `RT` varchar(3) default NULL,
  `RW` varchar(2) default NULL,
  `LINGKUNGAN` varchar(10) default NULL,
  `NOTELP` varchar(20) default NULL,
  `KODEPOS` int(5) default NULL,
  `TGLPASANG_KWH` date default NULL,
  `KDPEMBMETER` varchar(1) default NULL,
  `MEREK_KWH` varchar(24) default NULL,
  `KDGARDU` varchar(30) default NULL,
  `NOTIANG` varchar(35) default NULL,
  `KODEAREA` varchar(5) default NULL,
  `TGLPDL` datetime default NULL,
  `TGLNYALA_PB` date default NULL,
  `TGLRUBAH_MK` date default NULL,
  PRIMARY KEY  (`IDPEL`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
