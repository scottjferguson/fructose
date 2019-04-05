CREATE TABLE [dbo].[CustomerAttribute] (
    [ID]                      BIGINT         IDENTITY (1, 1) NOT NULL,
    [CustomerID]              BIGINT         NOT NULL,
    [CustomerAttributeTypeID] INT            NOT NULL,
    [AttributeValue]          VARCHAR (4000) NOT NULL,
    [CreatedBy]               VARCHAR (100)  CONSTRAINT [DF__CustomerAttribute__CreatedBy] DEFAULT (suser_sname()) NOT NULL,
    [CreatedDate]             DATETIME       CONSTRAINT [DF__CustomerAttribute__CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]              VARCHAR (100)  NULL,
    [ModifiedDate]            DATETIME       NULL,
    [RowVersion]              ROWVERSION     NOT NULL,
    CONSTRAINT [PKC__CustomerAttribute__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FKN__CustomerAttribute__CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customer] ([ID]),
    CONSTRAINT [FKN__CustomerAttributet__CustomerAttributeTypeID] FOREIGN KEY ([CustomerAttributeTypeID]) REFERENCES [dbo].[CustomerAttributeType] ([ID])
);

