CREATE TABLE [dbo].[TestingCentres] (
    [TestingCentreID] INT            IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (MAX) NULL,
    [Postcode]        NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_TestingCentres] PRIMARY KEY CLUSTERED ([TestingCentreID] ASC)
);

