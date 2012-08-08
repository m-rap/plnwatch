/*
SQLyog Enterprise - MySQL GUI v8.1 
MySQL - 5.5.8 : Database - plnwatch
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

/*Table structure for table `sorek` */

DROP TABLE IF EXISTS `SOREK`;

CREATE TABLE `SOREK` (
  `BLTH` varchar(6) NOT NULL,
  `IDPEL` varchar(12) NOT NULL,
  `TGLBACA` date DEFAULT NULL,
  `PEMKWH` int(11) DEFAULT NULL,
  `KODEAREA` varchar(5) DEFAULT NULL,
  PRIMARY KEY (`BLTH`,`IDPEL`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;