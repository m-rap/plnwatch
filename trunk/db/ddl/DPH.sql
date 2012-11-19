DROP TABLE IF EXISTS `DPH`;
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
-- Table structure for table `dph`
--

CREATE TABLE IF NOT EXISTS `dph` (
  `IDPEL` varchar(12) NOT NULL,
  `JMLBELI` int(11) default NULL,
  `PEMKWH` double default NULL,
  `RPTAG` double default NULL,
  `TGLBAYAR` datetime default NULL,
  PRIMARY KEY  (`IDPEL`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
