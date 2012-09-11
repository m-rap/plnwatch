/*
SQLyog Enterprise - MySQL GUI v8.1 
MySQL - 5.5.8 : Database - plnwatch
*********************************************************************
*/
/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

/*Table structure for table `dil` */

DROP TABLE IF EXISTS `DIL`;

CREATE TABLE IF NOT EXISTS `dil` (
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
  PRIMARY KEY  (`IDPEL`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;