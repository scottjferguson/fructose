CREATE TABLE [dbo].[CustomerType] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (100)  NOT NULL,
    [Description]  VARCHAR (4000) NOT NULL,
    [Code]         VARCHAR (50)   NOT NULL,
    [DisplayName]  VARCHAR (100)  NOT NULL,
    [DisplayOrder] INT            NULL,
    [IsActive]     BIT            CONSTRAINT [DF__CustomerType__IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedBy]    VARCHAR (100)  CONSTRAINT [DF__CustomerType__CreatedBy] DEFAULT (suser_sname()) NOT NULL,
    [CreatedDate]  DATETIME       CONSTRAINT [DF__CustomerType__CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]   VARCHAR (100)  NULL,
    [ModifiedDate] DATETIME       NULL,
    [RowVersion]   ROWVERSION     NOT NULL,
    CONSTRAINT [PKC__CustomerType__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [UKC__CustomerType__Code] UNIQUE NONCLUSTERED ([Code] ASC),
    CONSTRAINT [UKC__CustomerType__Name] UNIQUE NONCLUSTERED ([Name] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX__CustomerType__Name]
    ON [dbo].[CustomerType]([Name] ASC);


GO
CREATE NONCLUSTERED INDEX [IX__CustomerType__Code]
    ON [dbo].[CustomerType]([Code] ASC);

