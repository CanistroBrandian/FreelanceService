CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [UserId_Executor] INT NOT NULL, 
    [CategoryId] INT NOT NULL, 
    [Name] VARCHAR(256) NOT NULL, 
    [Description] VARCHAR(1024) NOT NULL, 
    [City] INT NOT NULL, 
    [Status] INT NOT NULL, 
    [StartDateTimeTime] DATETIME NOT NULL, 
    [RegistrationJobDateTime] DATETIME NOT NULL, 
    [FinishedDateTime] DATETIME NOT NULL, 
    [Price] DECIMAL(18, 2) NULL
)
