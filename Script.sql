USE [master]
GO
CREATE DATABASE Ranking
GO
USE Ranking
GO
CREATE TABLE Alunos(id int IDENTITY(1,1) NOT NULL primary key, Nome varchar(50) NOT NULL, Pontos int NOT NULL)
GO
