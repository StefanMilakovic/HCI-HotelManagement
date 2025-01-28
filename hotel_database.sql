-- MySQL dump 10.13  Distrib 8.0.38, for Win64 (x86_64)
--
-- Host: localhost    Database: hotel_database
-- ------------------------------------------------------
-- Server version	8.0.39

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `guest`
--

DROP TABLE IF EXISTS `guest`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `guest` (
  `GuestID` int NOT NULL AUTO_INCREMENT,
  `First_Name` varchar(45) NOT NULL,
  `Last_Name` varchar(45) NOT NULL,
  `Passport_Number` varchar(9) NOT NULL,
  `Email` varchar(45) DEFAULT NULL,
  `Phone_Number` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`GuestID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `guest`
--

LOCK TABLES `guest` WRITE;
/*!40000 ALTER TABLE `guest` DISABLE KEYS */;
INSERT INTO `guest` VALUES (1,'Gost1','Prezime1','123456','gost1@mail.com','065065065'),(2,'Gost2','Prezime2','654321','gost2@mail.com','066066066'),(3,'Gost3','Prezime3','324156','gost3@mail.com','065656565'),(4,'Gost4','Prezime4','213452','gost4@mail.com','064064064'),(5,'Gost5','Prezime5','1351461','gost5@mail.com','034034034'),(6,'Gost6','Prezime6','3125214','gost6@mail.com','064023045'),(7,'fsdfsfs','fsdfs','sdfsdfs','fdsfsd','sdfs');
/*!40000 ALTER TABLE `guest` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `invoice`
--

DROP TABLE IF EXISTS `invoice`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `invoice` (
  `InvoiceID` int NOT NULL AUTO_INCREMENT,
  `Total_Amount` decimal(10,2) NOT NULL,
  `Issued_date` date NOT NULL,
  `GuestID` int NOT NULL,
  `ReservationID` int NOT NULL,
  PRIMARY KEY (`InvoiceID`),
  KEY `fk_Invoices_Guests1_idx` (`GuestID`),
  KEY `fk_Invoices_Reservations1_idx` (`ReservationID`),
  CONSTRAINT `fk_Invoices_Guests1` FOREIGN KEY (`GuestID`) REFERENCES `guest` (`GuestID`),
  CONSTRAINT `fk_Invoices_Reservations1` FOREIGN KEY (`ReservationID`) REFERENCES `reservation` (`ReservationID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `invoice`
--

LOCK TABLES `invoice` WRITE;
/*!40000 ALTER TABLE `invoice` DISABLE KEYS */;
INSERT INTO `invoice` VALUES (1,340.00,'2025-01-09',5,1),(2,490.00,'2025-01-09',3,4),(4,1600.00,'2025-01-24',1,5);
/*!40000 ALTER TABLE `invoice` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `reservation`
--

DROP TABLE IF EXISTS `reservation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `reservation` (
  `ReservationID` int NOT NULL AUTO_INCREMENT,
  `Check_in_date` date NOT NULL,
  `Check_out_date` date NOT NULL,
  `GuestID` int NOT NULL,
  `RoomID` int NOT NULL,
  `UserID` int NOT NULL,
  PRIMARY KEY (`ReservationID`),
  KEY `fk_Reservations_Guests1_idx` (`GuestID`),
  KEY `fk_Reservations_Rooms1_idx` (`RoomID`),
  KEY `fk_Reservation_User1_idx` (`UserID`),
  CONSTRAINT `fk_Reservation_User1` FOREIGN KEY (`UserID`) REFERENCES `user` (`UserID`),
  CONSTRAINT `fk_Reservations_Guests1` FOREIGN KEY (`GuestID`) REFERENCES `guest` (`GuestID`),
  CONSTRAINT `fk_Reservations_Rooms1` FOREIGN KEY (`RoomID`) REFERENCES `room` (`RoomID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reservation`
--

LOCK TABLES `reservation` WRITE;
/*!40000 ALTER TABLE `reservation` DISABLE KEYS */;
INSERT INTO `reservation` VALUES (1,'2025-01-09','2025-01-12',5,1,2),(2,'2025-01-09','2025-01-16',2,6,2),(3,'2025-01-11','2025-01-18',4,4,2),(4,'2025-01-10','2025-01-12',3,5,2),(5,'2025-01-10','2025-01-18',1,3,2);
/*!40000 ALTER TABLE `reservation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `reservation_detailed_view`
--

DROP TABLE IF EXISTS `reservation_detailed_view`;
/*!50001 DROP VIEW IF EXISTS `reservation_detailed_view`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `reservation_detailed_view` AS SELECT 
 1 AS `ReservationID`,
 1 AS `CheckInDate`,
 1 AS `CheckOutDate`,
 1 AS `NumberOfGuests`,
 1 AS `GuestName`,
 1 AS `RoomID`,
 1 AS `ReservationType`,
 1 AS `ReceptionistName`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `reservation_has_service`
--

DROP TABLE IF EXISTS `reservation_has_service`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `reservation_has_service` (
  `ReservationHasServiceId` int NOT NULL AUTO_INCREMENT,
  `Quantity` int NOT NULL,
  `ServiceId` int NOT NULL,
  `ReservationId` int NOT NULL,
  `Total_Price` decimal(10,2) NOT NULL,
  PRIMARY KEY (`ReservationHasServiceId`),
  KEY `fk_Reservations_has_Item_Item1_idx` (`ServiceId`),
  KEY `fk_Reservations_has_Item_Reservations1_idx` (`ReservationId`),
  CONSTRAINT `fk_Reservations_has_Item_Item1` FOREIGN KEY (`ServiceId`) REFERENCES `service` (`ServiceId`),
  CONSTRAINT `fk_Reservations_has_Item_Reservations1` FOREIGN KEY (`ReservationId`) REFERENCES `reservation` (`ReservationID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reservation_has_service`
--

LOCK TABLES `reservation_has_service` WRITE;
/*!40000 ALTER TABLE `reservation_has_service` DISABLE KEYS */;
INSERT INTO `reservation_has_service` VALUES (1,1,1,1,40.00),(2,3,5,1,60.00),(3,5,3,4,50.00),(4,4,2,4,80.00),(5,1,3,2,10.00),(6,1,2,3,20.00),(7,2,4,3,100.00);
/*!40000 ALTER TABLE `reservation_has_service` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `calculate_total_price` BEFORE INSERT ON `reservation_has_service` FOR EACH ROW BEGIN
    DECLARE service_price DECIMAL(10,2);

    SELECT Price INTO service_price
    FROM `Hotel_Database`.`Service`
    WHERE ServiceId = NEW.ServiceId;

    SET NEW.Total_Price = NEW.Quantity * service_price;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `room`
--

DROP TABLE IF EXISTS `room`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `room` (
  `RoomID` int NOT NULL AUTO_INCREMENT,
  `Room_Number` int NOT NULL,
  `Floor` int NOT NULL,
  `RoomTypeId` int NOT NULL,
  PRIMARY KEY (`RoomID`),
  KEY `fk_Room_Room_Type1` (`RoomTypeId`),
  CONSTRAINT `fk_Room_Room_Type1` FOREIGN KEY (`RoomTypeId`) REFERENCES `room_type` (`RoomTypeId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `room`
--

LOCK TABLES `room` WRITE;
/*!40000 ALTER TABLE `room` DISABLE KEYS */;
INSERT INTO `room` VALUES (1,1,1,1),(2,2,1,2),(3,3,1,3),(4,4,1,4),(5,5,1,5),(6,6,1,1);
/*!40000 ALTER TABLE `room` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `room_type`
--

DROP TABLE IF EXISTS `room_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `room_type` (
  `RoomTypeId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  `PricePerNight` decimal(10,2) NOT NULL,
  `MaxNumOfGuests` int NOT NULL,
  PRIMARY KEY (`RoomTypeId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `room_type`
--

LOCK TABLES `room_type` WRITE;
/*!40000 ALTER TABLE `room_type` DISABLE KEYS */;
INSERT INTO `room_type` VALUES (1,'SingleRoom',80.00,1),(2,'DoubleRoom',120.00,2),(3,'Suite',200.00,4),(4,'DeluxeRoom',250.00,4),(5,'FamilyRoom',180.00,6);
/*!40000 ALTER TABLE `room_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `service`
--

DROP TABLE IF EXISTS `service`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `service` (
  `ServiceId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  `Price` decimal(10,2) NOT NULL,
  PRIMARY KEY (`ServiceId`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `service`
--

LOCK TABLES `service` WRITE;
/*!40000 ALTER TABLE `service` DISABLE KEYS */;
INSERT INTO `service` VALUES (1,'Airport Transfer',40.00),(2,'Sauna Access',20.00),(3,'Gym Access',10.00),(4,'City Tour',50.00),(5,'Laundry Service',20.00),(6,'Full-board',50.00),(7,'Half-board',30.00);
/*!40000 ALTER TABLE `service` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `UserID` int NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(45) DEFAULT NULL,
  `LastName` varchar(45) DEFAULT NULL,
  `Username` varchar(45) NOT NULL,
  `PasswordHash` varchar(200) DEFAULT NULL,
  `Role` enum('administrator','receptionist') DEFAULT NULL,
  PRIMARY KEY (`UserID`),
  UNIQUE KEY `Username_UNIQUE` (`Username`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'Stefan','Milakovic','admin','8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918','administrator'),(2,'Marko','Markovic','rec','cb41b38387d6fca01272be03380f63568fccbd724c299ae58e8d548d91b67a58','receptionist'),(3,'Nikola','Nikolic','rec1','c97c121a37e664a6ccbe874f9b0afb22e21661305772e0c2939e61ddfdb74924','receptionist');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'hotel_database'
--

--
-- Dumping routines for database 'hotel_database'
--

--
-- Final view structure for view `reservation_detailed_view`
--

/*!50001 DROP VIEW IF EXISTS `reservation_detailed_view`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `reservation_detailed_view` AS select 1 AS `ReservationID`,1 AS `CheckInDate`,1 AS `CheckOutDate`,1 AS `NumberOfGuests`,1 AS `GuestName`,1 AS `RoomID`,1 AS `ReservationType`,1 AS `ReceptionistName` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-01-28 21:20:37
