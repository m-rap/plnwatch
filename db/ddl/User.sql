/*
SQLyog Enterprise - MySQL GUI v8.1 
MySQL - 5.5.8 : Database - plnwatch
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

/*Table structure for table `User` */

DROP TABLE IF EXISTS `User`;

CREATE TABLE `User` (
  `UserId` int(11) NOT NULL AUTO_INCREMENT,
  `UserAlias` varchar(32) DEFAULT NULL,
  `UserName` varchar(32) NOT NULL,
  `UserPassword` varchar(32) DEFAULT NULL,
  `UserCreated` datetime DEFAULT NULL,
  `UserLoginTime` datetime DEFAULT NULL,
  `UserLoginIp` varchar(15) DEFAULT NULL,
  `UserLoginFrom` tinyint(4) DEFAULT NULL,
  `UserRole` int(11) DEFAULT NULL,
  PRIMARY KEY (`UserId`,`UserName`),
  UNIQUE KEY `UserId` (`UserId`),
  KEY `FK_User` (`UserRole`),
  CONSTRAINT `FK_User` FOREIGN KEY (`UserRole`) REFERENCES `Role` (`RoleId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;