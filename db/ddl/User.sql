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
  `UserModified` datetime DEFAULT NULL,
  `UserLoginTime` datetime DEFAULT NULL,
  `UserLoginIp` varchar(15) DEFAULT NULL,
  `UserLoginBrowser` varchar(255) DEFAULT NULL,
  `UserRole` int(11) DEFAULT NULL,
  PRIMARY KEY (`UserId`,`UserName`),
  UNIQUE KEY `UserId` (`UserId`),
  KEY `FK_user` (`UserRole`),
  CONSTRAINT `FK_user` FOREIGN KEY (`UserRole`) REFERENCES `Role` (`RoleId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

/*Data for the table `User` */

insert  into `User`(UserId,UserAlias,UserName,UserPassword,UserCreated,UserModified,UserLoginTime,UserLoginIp,UserLoginBrowser,UserRole) values (1,'PLN','admin','46f94c8de14fb36680850768ff1b7f2a',NULL,NULL,'2012-08-16 06:26:09','127.0.0.1','Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.77 Safari/537.1',1);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;