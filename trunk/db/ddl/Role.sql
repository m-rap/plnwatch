/*
SQLyog Enterprise - MySQL GUI v8.1 
MySQL - 5.5.8 : Database - plnwatch
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

/*Table structure for table `Role` */

DROP TABLE IF EXISTS `Role`;

CREATE TABLE `Role` (
  `RoleId` int(11) NOT NULL AUTO_INCREMENT,
  `RoleName` varchar(15) DEFAULT NULL,
  PRIMARY KEY (`RoleId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

/*Data for the table `role` */

insert  into `Role`(RoleId,RoleName) values (1,'Admin');
insert  into `Role`(RoleId,RoleName) values (2,'Operator');
insert  into `Role`(RoleId,RoleName) values (3,'User');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;