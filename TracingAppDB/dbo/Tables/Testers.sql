CREATE TABLE [dbo].[Testers] (
    [TesterID]        INT            IDENTITY (1, 1) NOT NULL,
    [Username]        NVARCHAR (MAX) NULL,
    [TestingCentreID] INT            NOT NULL,
    CONSTRAINT [PK_Testers] PRIMARY KEY CLUSTERED ([TesterID] ASC),
    CONSTRAINT [FK_Testers_TestingCentres_TestingCentreID] FOREIGN KEY ([TestingCentreID]) REFERENCES [dbo].[TestingCentres] ([TestingCentreID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Testers_TestingCentreID]
    ON [dbo].[Testers]([TestingCentreID] ASC);

