CREATE TABLE [dbo].[Cases] (
    [CaseID]      INT            IDENTITY (1, 1) NOT NULL,
    [Forename]    NVARCHAR (MAX) NULL,
    [Surname]     NVARCHAR (MAX) NULL,
    [TesterID]    INT            NOT NULL,
    [Phone]       NVARCHAR (MAX) NULL,
    [TestDate]    DATETIME2 (7)  NOT NULL,
    [AddedDate]   DATETIME2 (7)  NOT NULL,
    [Postcode]    NVARCHAR (MAX) NULL,
    [Traced]      BIT            NOT NULL,
    [Email]       NVARCHAR (MAX) NULL,
    [Phone2]      NVARCHAR (MAX) NULL,
    [TracerID]    INT            NULL,
    [SymptomDate] DATETIME2 (7)  NULL,
    [RemovedDate] DATETIME2 (7)  NULL,
    CONSTRAINT [PK_Cases] PRIMARY KEY CLUSTERED ([CaseID] ASC),
    CONSTRAINT [FK_Cases_Testers_TesterID] FOREIGN KEY ([TesterID]) REFERENCES [dbo].[Testers] ([TesterID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Cases_Tracers_TracerID] FOREIGN KEY ([TracerID]) REFERENCES [dbo].[Tracers] ([TracerID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Cases_TesterID]
    ON [dbo].[Cases]([TesterID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Cases_TracerID]
    ON [dbo].[Cases]([TracerID] ASC);

