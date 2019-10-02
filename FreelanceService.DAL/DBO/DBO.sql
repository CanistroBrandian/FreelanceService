CREATE TABLE Users (
    [Id]                   INT             IDENTITY (1, 1) NOT NULL,
    [Email]                VARCHAR (50)    NOT NULL,
    [PassHash]             VARCHAR (256)   NOT NULL,
    [FirstName]            VARCHAR (50)    NOT NULL,
    [LastName]             VARCHAR (50)    NOT NULL,
    [Phone]                VARCHAR (16)    NOT NULL,
    [City]                 INT             NOT NULL,
    [DynamicSalt]          VARCHAR (256)   NOT NULL,
    [RegistrationDateTime] DATETIME        NOT NULL,
    [Rating]               DECIMAL (18, 2) NULL,
    [Role]                 INT             NOT NULL,
	[VerifyCodeForResetPass]   VARCHAR(256)             NULL,
    UNIQUE(Email,Phone)
)
GO

ALTER TABLE Users 
ADD CONSTRAINT PK_Users_Id PRIMARY KEY CLUSTERED (Id)
GO

ALTER TABLE Users
ADD CONSTRAINT DF_Users_RegistrationDateTime_Default DEFAULT (getdate()) FOR RegistrationDateTime
GO

CREATE TABLE Responses (
    [Id]               INT         IDENTITY (1, 1)  NOT NULL,
    [UserId_Executor]  INT            NOT NULL,
    [JobId]            INT            NOT NULL,
    [Description]      VARCHAR (1024) NOT NULL,
    [ResponseDateTime] DATETIME       NOT NULL,
    [Status]           INT            NOT NULL,
   
)
GO 

ALTER TABLE Responses
ADD CONSTRAINT DF_Response_ResponseDateTime_Default DEFAULT (getdate()) FOR [ResponseDateTime]
GO

ALTER TABLE Responses 
ADD CONSTRAINT PK_Responses_Id PRIMARY KEY CLUSTERED (Id)
GO


ALTER TABLE Responses
WITH CHECK ADD CONSTRAINT FK_Responses_UserId_Executor_Users FOREIGN KEY (UserId_Executor)
REFERENCES Users(Id) 
ON UPDATE CASCADE 
ON DELETE CASCADE 
GO

CREATE TABLE Jobs (
    [Id]                      INT             IDENTITY(1,1),
    [UserId_Executor]         INT             NULL,
	[UserId_Customer]         INT             NOT NULL,
    [CategoryId]              INT             NOT NULL,
    [Name]                    VARCHAR (256)   NOT NULL,
    [Description]             VARCHAR (1024)  NOT NULL,
    [City]                    INT             NOT NULL,
    [Status]                  INT             NOT NULL,
    [StartDateTime]        DATETIME           NULL,
    [RegistrationDateTime] DATETIME           NOT NULL,
    [FinishedDateTime]        DATETIME        NOT NULL,
    [Price]                   DECIMAL (18, 2) NULL,
)
GO 

ALTER TABLE Jobs 
ADD CONSTRAINT PK_Jobs_Id PRIMARY KEY CLUSTERED (Id)
GO

ALTER TABLE Jobs
ADD CONSTRAINT DF_Jobs_RegistrationDateTime_Default DEFAULT (getdate()) FOR RegistrationDateTime
GO

ALTER TABLE Jobs
ADD CONSTRAINT DF_Jobs_Status_Default DEFAULT (1) FOR Status
GO

ALTER TABLE Jobs
WITH CHECK ADD CONSTRAINT FK_Jobs_UserId_Customer_Users FOREIGN KEY (UserId_Customer)
REFERENCES Users(Id) 
ON UPDATE CASCADE 
ON DELETE CASCADE 
GO

ALTER TABLE Jobs
WITH CHECK ADD CONSTRAINT FK_Jobs_Id_Responses FOREIGN KEY (Id)
REFERENCES Responses(JobId) 
ON UPDATE CASCADE 
ON DELETE CASCADE 
GO

CREATE TABLE Reviews (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [Name]            VARCHAR (128)   NOT NULL,
	[UserId]          INT             NOT NULL,
	[JobId]           INT             NOT NULL,
    [Description]     VARCHAR (1024)    NOT NULL,
    [WritingDateTime] DATETIME        NOT NULL,
    [Feedback]        BIT             NOT NULL,
    [Rating]          DECIMAL (18, 2) NULL,
  
)
GO

ALTER TABLE Reviews 
ADD CONSTRAINT PK_Reviews_Id PRIMARY KEY CLUSTERED (Id)
GO

ALTER TABLE Reviews
ADD CONSTRAINT DF_Reviews_WritingDateTime_Default DEFAULT (getdate()) FOR WritingDateTime
GO

CREATE TABLE Projects (
    [Id]          INT     IDENTITY (1, 1)  NOT NULL,
    [Description] VARCHAR (1024) NOT NULL,
)
GO

ALTER TABLE Projects 
ADD CONSTRAINT PK_Projects_Id PRIMARY KEY CLUSTERED (Id)
GO

CREATE TABLE Categories (
    [Id]   INT       IDENTITY (1, 1)   NOT NULL,
    [Name] VARCHAR (50) NOT NULL,
)
GO

ALTER TABLE Categories 
ADD CONSTRAINT PK_Categories_Id PRIMARY KEY CLUSTERED (Id)
GO