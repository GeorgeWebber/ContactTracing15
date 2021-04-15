CREATE TABLE [dbo].[Contacts] (
    [ContactID]   INT            IDENTITY (1, 1) NOT NULL,
    [Forename]    NVARCHAR (MAX) NULL,
    [Surname]     NVARCHAR (MAX) NULL,
    [CaseID]      INT            NOT NULL,
    [Email]       NVARCHAR (MAX) NULL,
    [AddedDate]   DATETIME2 (7)  NOT NULL,
    [Phone]       NVARCHAR (MAX) NULL,
    [TracedDate]  DATETIME2 (7)  NULL,
    [RemovedDate] DATETIME2 (7)  NULL,
    CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED ([ContactID] ASC),
    CONSTRAINT [FK_Contacts_Cases_CaseID] FOREIGN KEY ([CaseID]) REFERENCES [dbo].[Cases] ([CaseID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Contacts_CaseID]
    ON [dbo].[Contacts]([CaseID] ASC);

