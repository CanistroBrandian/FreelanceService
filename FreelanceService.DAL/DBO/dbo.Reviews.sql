CREATE TABLE [dbo].[Reviews] (
    [Id]              INT             NOT NULL,
    [Name]            VARCHAR (128)   NOT NULL,
    [Description]     NCHAR (1024)    NOT NULL,
    [WritingDateTime] DATETIME        NOT NULL,
    [Feedback]        BIT             NOT NULL,
    [Rating]          DECIMAL (18, 2) NULL,
    [UserId]          INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

