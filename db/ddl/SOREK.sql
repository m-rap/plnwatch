-- phpMyAdmin SQL Dump
-- version 2.11.7
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Aug 10, 2012 at 12:20 PM
-- Server version: 5.0.51
-- PHP Version: 5.2.6

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

CREATE TABLE IF NOT EXISTS `sorek` (
  `BLTH` varchar(6) NOT NULL,
  `IDPEL` varchar(12) NOT NULL,
  `TGLBACA` date default NULL,
  `PEMKWH` double default NULL,
  `KODEAREA` varchar(5) default NULL,
  `JAMNYALA` double default NULL,
  PRIMARY KEY  (`BLTH`,`IDPEL`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `sorek`
--

