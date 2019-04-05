CREATE TABLE [dbo].[AddressHistory] (
    [ID]            BIGINT        IDENTITY (1, 1) NOT NULL,
    [AddressID]     BIGINT        NOT NULL,
    [AddressTypeID] INT           NOT NULL,
    [GeographyID]   BIGINT        NULL,
    [Line1]         VARCHAR (500) NOT NULL,
    [Line2]         VARCHAR (500) NULL,
    [Line3]         VARCHAR (500) NULL,
    [City]          VARCHAR (200) NOT NULL,
    [StateProv]     VARCHAR (2)   NOT NULL,
    [PostalCode]    VARCHAR (20)  NOT NULL,
    [CountryCode]   VARCHAR (2)   NOT NULL,
    [CreatedBy]     VARCHAR (100) CONSTRAINT [DF__AddressHistory__CreatedBy] DEFAULT (suser_sname()) NOT NULL,
    [CreatedDate]   DATETIME      CONSTRAINT [DF__AddressHistory__CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PKC__AddressHistory__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FKN__AddressHistory__AddressID] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Address] ([ID])
);

