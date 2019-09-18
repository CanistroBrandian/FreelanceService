CREATE TABLE [dbo].[Responses] (
    [Id]               INT            NOT NULL,
    [UserId_Executor]  INT            NOT NULL,
    [JobId]            INT            NOT NULL,
    [Description]      VARCHAR (1024) NOT NULL,
    [ResponseDateTime] DATETIME       NOT NULL,
    [Status]           INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

