CREATE TABLE [dbo].[Customer] (
    [ID]                  BIGINT        IDENTITY (1, 1) NOT NULL,
    [OrganizationID]      BIGINT        NOT NULL,
    [PersonID]            BIGINT        NOT NULL,
    [CustomerStatusID]    INT           NOT NULL,
    [CustomerTypeID]      INT           NOT NULL,
    [CustomerNumber]      VARCHAR (50)  NOT NULL,
    [JoinDate]            DATETIME      CONSTRAINT [DF__Customer__JoinDate] DEFAULT (getutcdate()) NOT NULL,
    [IsActive]            BIT           CONSTRAINT [DF__Customer__IsActive] DEFAULT ((1)) NOT NULL,
    [ReferenceIdentifier] VARCHAR (100) NULL,
    [CreatedBy]           VARCHAR (100) CONSTRAINT [DF__Customer__CreatedBy] DEFAULT (suser_sname()) NOT NULL,
    [CreatedDate]         DATETIME      CONSTRAINT [DF__Customer__CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]          VARCHAR (100) NULL,
    [ModifiedDate]        DATETIME      NULL,
    [RowVersion]          ROWVERSION    NOT NULL,
    CONSTRAINT [PKC__Customer__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FKN__Customer__CustomerStatusID] FOREIGN KEY ([CustomerStatusID]) REFERENCES [dbo].[CustomerStatus] ([ID]),
    CONSTRAINT [FKN__Customer__CustomerTypeID] FOREIGN KEY ([CustomerTypeID]) REFERENCES [dbo].[CustomerType] ([ID]),
    CONSTRAINT [FKN__Customer__PersonID] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Person] ([ID]),
    CONSTRAINT [UKC__Customer__PersonID] UNIQUE NONCLUSTERED ([PersonID] ASC),
    CONSTRAINT [UKC__Customer__ReferenceIdentifier] UNIQUE NONCLUSTERED ([ReferenceIdentifier] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX__Customer__ReferenceIdentifier]
    ON [dbo].[Customer]([ReferenceIdentifier] ASC);

