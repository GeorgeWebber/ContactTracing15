CREATE TABLE [dbo].[Tracers] (
    [TracerID]        INT            IDENTITY (1, 1) NOT NULL,
    [Username]        NVARCHAR (MAX) NULL,
    [TracingCentreID] INT            NOT NULL,
    CONSTRAINT [PK_Tracers] PRIMARY KEY CLUSTERED ([TracerID] ASC),
    CONSTRAINT [FK_Tracers_TracingCentres_TracingCentreID] FOREIGN KEY ([TracingCentreID]) REFERENCES [dbo].[TracingCentres] ([TracingCentreID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Tracers_TracingCentreID]
    ON [dbo].[Tracers]([TracingCentreID] ASC);

