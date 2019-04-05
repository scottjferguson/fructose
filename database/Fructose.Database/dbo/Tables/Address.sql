CREATE TABLE [dbo].[Address] (
    [ID]            BIGINT        IDENTITY (1, 1) NOT NULL,
    [AddressTypeID] INT           NOT NULL,
    [GeographyID]   BIGINT        NULL,
    [Line1]         VARCHAR (500) NOT NULL,
    [Line2]         VARCHAR (500) NULL,
    [Line3]         VARCHAR (500) NULL,
    [City]          VARCHAR (200) NOT NULL,
    [StateProv]     VARCHAR (2)   NOT NULL,
    [PostalCode]    VARCHAR (20)  NOT NULL,
    [CountryCode]   VARCHAR (2)   NOT NULL,
    [CreatedBy]     VARCHAR (100) CONSTRAINT [DF__Address__CreatedBy] DEFAULT (suser_sname()) NOT NULL,
    [CreatedDate]   DATETIME      CONSTRAINT [DF__Address__CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]    VARCHAR (100) NULL,
    [ModifiedDate]  DATETIME      NULL,
    [RowVersion]    ROWVERSION    NOT NULL,
    CONSTRAINT [PKC__Address__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FKN__Address__AddressTypeID] FOREIGN KEY ([AddressTypeID]) REFERENCES [dbo].[AddressType] ([ID])
);


GO
CREATE NONCLUSTERED INDEX [IX__Address__Line1]
    ON [dbo].[Address]([Line1] ASC);


GO
CREATE NONCLUSTERED INDEX [IX__Address__City]
    ON [dbo].[Address]([City] ASC);


GO
CREATE NONCLUSTERED INDEX [IX__Address__StateProv]
    ON [dbo].[Address]([StateProv] ASC);


GO
CREATE NONCLUSTERED INDEX [IX__Address__PostalCode]
    ON [dbo].[Address]([PostalCode] ASC);


GO
CREATE NONCLUSTERED INDEX [IX__Address__CountryCode]
    ON [dbo].[Address]([CountryCode] ASC);

