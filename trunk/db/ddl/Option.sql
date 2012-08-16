/*
SQLyog Enterprise - MySQL GUI v8.1 
MySQL - 5.5.8 : Database - plnwatch
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

/*Table structure for table `Option` */

DROP TABLE IF EXISTS `Option`;

CREATE TABLE `Option` (
  `OptionKey` varchar(10) NOT NULL,
  `OptionValue` text,
  PRIMARY KEY (`OptionKey`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*Data for the table `Option` */

insert  into `Option`(OptionKey,OptionValue) values ('DilBLTH','072012');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;