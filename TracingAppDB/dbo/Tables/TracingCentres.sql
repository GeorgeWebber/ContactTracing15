CREATE TABLE [dbo].[TracingCentres] (
    [TracingCentreID] INT            IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (MAX) NULL,
    [Postcode]        NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_TracingCentres] PRIMARY KEY CLUSTERED ([TracingCentreID] ASC)
);

