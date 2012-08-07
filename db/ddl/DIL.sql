-- phpMyAdmin SQL Dump
-- version 3.3.9
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Aug 07, 2012 at 05:32 
-- Server version: 5.5.8
-- PHP Version: 5.3.5

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `plnwatch`
--

-- --------------------------------------------------------

--
-- Table structure for table `dil`
--

CREATE TABLE IF NOT EXISTS `DIL` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `JENIS_MK` varchar(20) DEFAULT NULL,
  `IDPEL` int(11) DEFAULT NULL,
  `NAMA` varchar(25) DEFAULT NULL,
  `TARIF` varchar(4) DEFAULT NULL,
  `DAYA` int(11) DEFAULT NULL,
  `LINGKUNGAN` varchar(10) DEFAULT NULL,
  `TGLPASANG` datetime DEFAULT NULL,
  `MEREK_KWH` varchar(24) DEFAULT NULL,
  `KDGARDU` varchar(30) DEFAULT NULL,
  `NOTIANG` varchar(35) DEFAULT NULL,
  `KODEAREA` varchar(5) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;
