CREATE TABLE [dbo].[Users] (
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
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Email] ASC)
);

