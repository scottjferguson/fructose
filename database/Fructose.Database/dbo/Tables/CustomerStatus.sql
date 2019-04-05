CREATE TABLE [dbo].[CustomerStatus] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (100)  NOT NULL,
    [SubStatusName] VARCHAR (100)  NULL,
    [Description]   VARCHAR (4000) NOT NULL,
    [Code]          VARCHAR (50)   NOT NULL,
    [DisplayName]   VARCHAR (100)  NOT NULL,
    [DisplayOrder]  INT            NULL,
    [IsActive]      BIT            CONSTRAINT [DF__CustomerStatus__IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedBy]     VARCHAR (100)  CONSTRAINT [DF__CustomerStatus__CreatedBy] DEFAULT (suser_sname()) NOT NULL,
    [CreatedDate]   DATETIME       CONSTRAINT [DF__CustomerStatus__CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]    VARCHAR (100)  NULL,
    [ModifiedDate]  DATETIME       NULL,
    [RowVersion]    ROWVERSION     NOT NULL,
    CONSTRAINT [PKC__CustomerStatus__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [UKC__CustomerStatus__Code] UNIQUE NONCLUSTERED ([Code] ASC),
    CONSTRAINT [UKC__CustomerStatus__Name__SubStatusName] UNIQUE NONCLUSTERED ([Name] ASC, [SubStatusName] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX__CustomerStatus__Name]
    ON [dbo].[CustomerStatus]([Name] ASC);


GO
CREATE NONCLUSTERED INDEX [IX__CustomerStatus__SubStatusName]
    ON [dbo].[CustomerStatus]([SubStatusName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX__CustomerStatus__Code]
    ON [dbo].[CustomerStatus]([Code] ASC);

