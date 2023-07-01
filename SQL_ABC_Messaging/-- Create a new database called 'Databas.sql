-- Create a new database called 'DatabaseName'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT name
        FROM sys.databases
        WHERE name = N'DatabaseName'
)
CREATE DATABASE DatabaseName
GO
CREATE TABLE ABC.message
(
    MessageId INT NOT NULL PRIMARY KEY, -- primary key column
    SenderUsername string foreign key references agent(Username),
    RecieverUsername string foreign key references agent(Username),
    TimeStamp DATETIME NOT NULL DEFAULT (GETDATE())
);
GO
-- Create the table in the specified schema
CREATE TABLE ABC.agent
(
    AgentID INT NOT NULL PRIMARY KEY, -- primary key column
    Name NVARCHAR,
    Lastname NVARCHAR,
    Telephone NVARCHAR,
    Email NVARCHAR,
    Password NVARCHAR,
    Role NVARCHAR,
    Region NVARCHAR,
);
GO

