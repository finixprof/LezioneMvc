-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Creato il: Feb 14, 2023 alle 12:08
-- Versione del server: 10.4.24-MariaDB
-- Versione PHP: 8.1.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `ospedale5d`
--
CREATE DATABASE IF NOT EXISTS `ospedale5d` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `ospedale5d`;

-- --------------------------------------------------------

--
-- Struttura della tabella `paziente`
--

CREATE TABLE `paziente` (
  `ID` int(11) NOT NULL,
  `Nome` varchar(255) COLLATE utf8_bin NOT NULL,
  `DataNascita` date NOT NULL,
  `Provincia` varchar(255) COLLATE utf8_bin NOT NULL,
  `Sesso` char(1) COLLATE utf8_bin NOT NULL,
  `stipendio` float NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Dump dei dati per la tabella `paziente`
--

INSERT INTO `paziente` (`ID`, `Nome`, `DataNascita`, `Provincia`, `Sesso`, `stipendio`) VALUES
(1, 'Anna', '1980-08-12', 'Re', 'F', 0),
(2, 'Gino', '1990-09-15', 'Re', 'M', 0),
(3, 'Fabio', '1998-08-06', 'Mo', 'M', 0),
(4, 'Tobia', '1963-08-05', 'Bo', 'M', 0),
(5, 'Giovanni', '1990-02-03', 'Mo', 'M', 0),
(6, 'Aldo', '1998-08-03', 'Re', 'M', 0),
(7, 'Giacomo', '2000-11-03', 'RC', 'm', 0);

-- --------------------------------------------------------

--
-- Struttura della tabella `personale`
--

CREATE TABLE `personale` (
  `ID` int(11) NOT NULL,
  `Cognome` varchar(255) COLLATE utf8_bin NOT NULL,
  `DataNascita` date NOT NULL,
  `Professione` varchar(255) COLLATE utf8_bin NOT NULL,
  `Reparto` varchar(255) COLLATE utf8_bin NOT NULL,
  `Stipendio` float NOT NULL,
  `Superiore` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Dump dei dati per la tabella `personale`
--

INSERT INTO `personale` (`ID`, `Cognome`, `DataNascita`, `Professione`, `Reparto`, `Stipendio`, `Superiore`) VALUES
(1, 'Rossi', '1960-08-12', 'Medico', 'Chirurgia', 6050.85, 3),
(2, 'Bianchi', '1950-09-08', 'Medico', 'Ginecologia', 5336.03, NULL),
(3, 'Bigi', '1945-07-09', 'Medico', 'Chirurgia', 7260, NULL),
(4, 'Sassi', '1955-07-05', 'Infermiere', 'Chirurgia', 2420, 3),
(5, 'Gatti', '1960-06-06', 'Infermiere', 'Ginecologia', 2420, 2),
(6, 'Pini', '1962-08-04', 'Ostetrico', 'Ginecologia', 3630, 2),
(7, 'Valli', '1970-08-05', 'Infermiere', 'Ginecologia', 2420, 2),
(8, 'Rodolfi', '1970-12-17', 'medico', 'chirurgia', 0, 1),
(20, 'Gianni', '1970-12-17', 'medico', 'ginecologia', 0, 1),
(21, 'Pino', '1970-12-17', 'medico', 'ginecologia', 0, 1);

-- --------------------------------------------------------

--
-- Struttura della tabella `utente`
--

CREATE TABLE `utente` (
  `id` int(11) NOT NULL,
  `datacreazione` datetime NOT NULL DEFAULT current_timestamp(),
  `dataultimamodifica` datetime DEFAULT NULL,
  `username` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `isMailConfermata` bit(1) NOT NULL DEFAULT b'0'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dump dei dati per la tabella `utente`
--

INSERT INTO `utente` (`id`, `datacreazione`, `dataultimamodifica`, `username`, `password`, `email`, `isMailConfermata`) VALUES
(1, '2023-01-24 12:29:05', '2023-02-02 11:42:17', 'finix', '8df3dc034e6388e8ba6915073dbea6440ef7c12c30860c57ca7f2fe444d7502c', 'salvo.finistrella@iispascal.it', b'0'),
(2, '2023-02-13 11:34:10', '2023-02-14 11:56:47', 'finix77', '65fa5e6abf83842d6eccf9bfb286d58767803e59cdbbda85a268855b29673e20', 'finix77@hotmail.com', b'1');

-- --------------------------------------------------------

--
-- Struttura della tabella `visita`
--

CREATE TABLE `visita` (
  `ID` int(11) NOT NULL,
  `DataVisita` date NOT NULL,
  `Peso` int(11) NOT NULL,
  `Altezza` int(11) NOT NULL,
  `PressioneMin` int(11) NOT NULL,
  `PressioneMax` int(11) NOT NULL,
  `IDPaziente` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Dump dei dati per la tabella `visita`
--

INSERT INTO `visita` (`ID`, `DataVisita`, `Peso`, `Altezza`, `PressioneMin`, `PressioneMax`, `IDPaziente`) VALUES
(1, '2005-11-04', 120, 2, 80, 150, 1),
(2, '2005-11-06', 58, 2, 90, 120, 3);

-- --------------------------------------------------------

--
-- Struttura della tabella `visitapersonale`
--

CREATE TABLE `visitapersonale` (
  `ID` int(11) NOT NULL,
  `InQualita` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `IDPersonale` int(11) NOT NULL,
  `IDVisita` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Dump dei dati per la tabella `visitapersonale`
--

INSERT INTO `visitapersonale` (`ID`, `InQualita`, `IDPersonale`, `IDVisita`) VALUES
(1, 'Consulente', 1, 1),
(2, NULL, 2, 1),
(3, NULL, 3, 1),
(4, NULL, 3, 2),
(5, 'Assistente', 4, 1),
(6, NULL, 4, 2),
(7, NULL, 6, 1);

--
-- Indici per le tabelle scaricate
--

--
-- Indici per le tabelle `paziente`
--
ALTER TABLE `paziente`
  ADD PRIMARY KEY (`ID`);

--
-- Indici per le tabelle `personale`
--
ALTER TABLE `personale`
  ADD PRIMARY KEY (`ID`);

--
-- Indici per le tabelle `utente`
--
ALTER TABLE `utente`
  ADD PRIMARY KEY (`id`);

--
-- Indici per le tabelle `visita`
--
ALTER TABLE `visita`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `IDPaziente` (`IDPaziente`);

--
-- Indici per le tabelle `visitapersonale`
--
ALTER TABLE `visitapersonale`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `IDPersonale` (`IDPersonale`),
  ADD KEY `IDVisita` (`IDVisita`);

--
-- AUTO_INCREMENT per le tabelle scaricate
--

--
-- AUTO_INCREMENT per la tabella `paziente`
--
ALTER TABLE `paziente`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT per la tabella `personale`
--
ALTER TABLE `personale`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT per la tabella `utente`
--
ALTER TABLE `utente`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT per la tabella `visita`
--
ALTER TABLE `visita`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT per la tabella `visitapersonale`
--
ALTER TABLE `visitapersonale`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- Limiti per le tabelle scaricate
--

--
-- Limiti per la tabella `visita`
--
ALTER TABLE `visita`
  ADD CONSTRAINT `visita_ibfk_1` FOREIGN KEY (`IDPaziente`) REFERENCES `paziente` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Limiti per la tabella `visitapersonale`
--
ALTER TABLE `visitapersonale`
  ADD CONSTRAINT `visitapersonale_ibfk_1` FOREIGN KEY (`IDPersonale`) REFERENCES `personale` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `visitapersonale_ibfk_2` FOREIGN KEY (`IDVisita`) REFERENCES `visita` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
