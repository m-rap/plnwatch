-- phpMyAdmin SQL Dump
-- version 3.3.9
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Aug 07, 2012 at 06:33 
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
-- Table structure for table `sorek`
--

CREATE TABLE IF NOT EXISTS `SOREK` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `BLTH` varchar(6) NOT NULL,
  `IDPEL` int(11) NOT NULL,
  `NAMA` varchar(25) NOT NULL,
  `DAYA` int(11) NOT NULL,
  `TGLBACA` date NOT NULL,
  `PEMKWH` int(11) NOT NULL,
  `KODEAREA` varchar(5) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;