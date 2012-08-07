/*
SQLyog Enterprise - MySQL GUI v8.1 
MySQL - 5.5.8 : Database - plnwatch
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

/*Table structure for table `dil` */

CREATE TABLE `DIL` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `BLTH` varchar(6) DEFAULT NULL,
  `JENIS_MK` varchar(20) DEFAULT NULL,
  `IDPEL` int(11) DEFAULT NULL,
  `NAMA` varchar(25) DEFAULT NULL,
  `TARIF` varchar(4) DEFAULT NULL,
  `DAYA` int(11) DEFAULT NULL,
  `LINGKUNGAN` varchar(10) DEFAULT NULL,
  `TGLPASANG` date DEFAULT NULL,
  `MEREK_KWH` varchar(24) DEFAULT NULL,
  `KDGARDU` varchar(30) DEFAULT NULL,
  `NOTIANG` varchar(35) DEFAULT NULL,
  `KODEAREA` varchar(5) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;