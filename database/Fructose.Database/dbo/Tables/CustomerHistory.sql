CREATE TABLE [dbo].[CustomerHistory] (
    [ID]                  BIGINT        IDENTITY (1, 1) NOT NULL,
    [CustomerID]          BIGINT        NOT NULL,
    [OrganizationID]      BIGINT        NOT NULL,
    [PersonID]            BIGINT        NOT NULL,
    [CustomerStatusID]    INT           NOT NULL,
    [CustomerTypeID]      INT           NOT NULL,
    [CustomerNumber]      VARCHAR (100) NOT NULL,
    [JoinDate]            DATETIME      NOT NULL,
    [IsActive]            BIT           NOT NULL,
    [ReferenceIdentifier] VARCHAR (100) NULL,
    [CreatedBy]           VARCHAR (100) CONSTRAINT [DF__CustomerHistory__CreatedBy] DEFAULT (suser_sname()) NOT NULL,
    [CreatedDate]         DATETIME      CONSTRAINT [DF__CustomerHistory__CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PKC__CustomerHistory__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FKN__CustomerHistory__CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customer] ([ID])
);

