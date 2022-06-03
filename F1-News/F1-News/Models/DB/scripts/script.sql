drop database f1DB;

create database f1DB collate utf8mb4_general_ci;

use f1DB;

DROP TABLE IF EXISTS `user`;

CREATE TABLE `user` (
  `user_id` int unsigned NOT NULL AUTO_INCREMENT,
  `username` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `password` varchar(300) COLLATE utf8mb4_general_ci NOT NULL,
  `firstname` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `lastname` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `email` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `birthdate` date DEFAULT NULL,
  `gender` int DEFAULT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;


LOCK TABLES `user` WRITE;

INSERT INTO `user` VALUES (10,'fasteinlechner','fa585d89c851dd338a70dcf535aa2a92fee7836dd6aff1226583e88e0996293f16bc009c652826e0fc5c706695a03cddce372f139eff4d13959da6f1f5d3eabe','Fabian','Steinlechner','fasteinlechner@tsn.at','2004-06-21',0),(11,'adminF1','fa585d89c851dd338a70dcf535aa2a92fee7836dd6aff1226583e88e0996293f16bc009c652826e0fc5c706695a03cddce372f139eff4d13959da6f1f5d3eabe','admin','admin','admin@F1.at','2000-01-21',0),(12,'maxipumpfer','fa585d89c851dd338a70dcf535aa2a92fee7836dd6aff1226583e88e0996293f16bc009c652826e0fc5c706695a03cddce372f139eff4d13959da6f1f5d3eabe','Maximilian','Pumpfer','mpumpfer@tsn.at','2004-06-21',0),(13,'luggi','fa585d89c851dd338a70dcf535aa2a92fee7836dd6aff1226583e88e0996293f16bc009c652826e0fc5c706695a03cddce372f139eff4d13959da6f1f5d3eabe','Lucas','Steinlechner','lsteinlechner@gmail.com','2004-06-21',0),(14,'Dominik','f539b5bef0f31912e7b2a959a766c22e44051ad5e4eda437a4811f0e5b1a462f195064962ec764c7beb2a3e8c770217dc3d667c6c4ed48805448c5b139cfeea4','Fabian','Steinlechner','fasteinlechner@tsn.at','2022-04-07',0),(15,'fasdfadf','f539b5bef0f31912e7b2a959a766c22e44051ad5e4eda437a4811f0e5b1a462f195064962ec764c7beb2a3e8c770217dc3d667c6c4ed48805448c5b139cfeea4','fadfafa','asdfasdfas','fasteinlechner@tsn.at','2022-04-07',0),(16,'adfadfa','f539b5bef0f31912e7b2a959a766c22e44051ad5e4eda437a4811f0e5b1a462f195064962ec764c7beb2a3e8c770217dc3d667c6c4ed48805448c5b139cfeea4','fadfadf','sfastsdfsf','fasteinlechner@tsn.at','2022-04-07',0),(17,'Sandra','fa585d89c851dd338a70dcf535aa2a92fee7836dd6aff1226583e88e0996293f16bc009c652826e0fc5c706695a03cddce372f139eff4d13959da6f1f5d3eabe','Sandra','Steinlechner','fasteinlechner@tsn.at','1977-10-31',1),(18,'dolettenbichler','fa585d89c851dd338a70dcf535aa2a92fee7836dd6aff1226583e88e0996293f16bc009c652826e0fc5c706695a03cddce372f139eff4d13959da6f1f5d3eabe','Dominik','Steinlechner','fasteinlechner@tsn.at','2004-06-21',0),(19,'steini','fa585d89c851dd338a70dcf535aa2a92fee7836dd6aff1226583e88e0996293f16bc009c652826e0fc5c706695a03cddce372f139eff4d13959da6f1f5d3eabe','Fabian','Steinlechner','fasteinlechner@tsn.at','2022-04-01',0);

UNLOCK TABLES;

insert into user values(null, "dolettenbichler", sha2("domi123456", 512), "Dominik", "Lettenbichler", "dolettenbichler@tsn.at", "2004-01-01", 0);

DROP TABLE IF EXISTS `article`;

CREATE TABLE `article` (
  `idarticle` int unsigned NOT NULL AUTO_INCREMENT,
  `bezeichnung` varchar(45) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `beschreibung` varchar(500) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `preis` decimal(7,2) DEFAULT NULL,
  `elemente` int DEFAULT NULL,
  PRIMARY KEY (`idarticle`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

LOCK TABLES `article` WRITE;

INSERT INTO `article` VALUES (1,'RedbullRacing Schildkappe','Original RB-Racing Schildkappe von Max',45.50,45),(2,'Mclaren Schlüsselanhänger','von Lando Norris',28.80,20),(3,'Mclaren Schlüsselanhänger','von Lando Norris',28.80,20),(4,'Mclaren Schlüsselanhänger','von Lando Norris',28.80,20),(5,'Mclaren Schlüsselanhänger','von Lando Norris',28.80,20),(6,'RedbullRacing Schildkappe','Original RB-Racing Schildkappe von Max',45.50,45),(7,'Mclaren Schlüsselanhänger','von Lando Norris',28.80,20),(8,'Mclaren Schlüsselanhänger','von Lando Norris',28.80,20),(9,'Mclaren Schlüsselanhänger','von Lando Norris',28.80,20),(10,'Mclaren Schlüsselanhänger','von Lando Norris',28.80,20);

UNLOCK TABLES;

DROP TABLE IF EXISTS `bestellungen`;

CREATE TABLE `bestellungen` (
  `idbestellungen` int unsigned NOT NULL AUTO_INCREMENT,
  `FK_idarticle` int unsigned DEFAULT NULL,
  `FK_user_id` int unsigned DEFAULT NULL,
  PRIMARY KEY (`idbestellungen`),
  KEY `foreignUser_idx` (`FK_user_id`),
  KEY `foreignArticle_idx` (`FK_idarticle`),
  CONSTRAINT `foreignArticle` FOREIGN KEY (`FK_idarticle`) REFERENCES `article` (`idarticle`),
  CONSTRAINT `foreignUser` FOREIGN KEY (`FK_user_id`) REFERENCES `user` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

LOCK TABLES `bestellungen` WRITE;

UNLOCK TABLES;