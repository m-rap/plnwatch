/*
SQLyog Enterprise - MySQL GUI v8.1 
MySQL - 5.5.8 : Database - plnwatch
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

/*Table structure for table `sorek` */

CREATE TABLE `SOREK` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `BLTH` varchar(6) NOT NULL,
  `IDPEL` int(11) NOT NULL,
  `NAMA` varchar(25) NOT NULL,
  `DAYA` int(11) NOT NULL,
  `PNJ` varchar(2) DEFAULT NULL,
  `NAMAPNJ` varchar(62) DEFAULT NULL,
  `NOBANG` varchar(3) DEFAULT NULL,
  `RT` varchar(3) DEFAULT NULL,
  `RW` varchar(2) DEFAULT NULL,
  `LINGKUNGAN` varchar(10) DEFAULT NULL,
  `NOTELP` varchar(20) DEFAULT NULL,
  `KODEPOS` int(5) DEFAULT NULL,
  `TGLBACA` date NOT NULL,
  `PEMKWH` int(11) NOT NULL,
  `KODEAREA` varchar(5) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;