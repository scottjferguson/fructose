CREATE TABLE [dbo].[CustomerAttributeType] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             VARCHAR (100)  NOT NULL,
    [Description]      VARCHAR (4000) NOT NULL,
    [DisplayName]      VARCHAR (100)  NOT NULL,
    [DisplayOrder]     INT            NULL,
    [IsValueEncrypted] BIT            CONSTRAINT [DF__CustomerAttributeType__IsValueEncrypted] DEFAULT ((0)) NOT NULL,
    [IsActive]         BIT            CONSTRAINT [DF__CustomerAttributeType__IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedBy]        VARCHAR (100)  CONSTRAINT [DF__CustomerAttributeType__CreatedBy] DEFAULT (suser_sname()) NOT NULL,
    [CreatedDate]      DATETIME       CONSTRAINT [DF__CustomerAttributeType__CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]       VARCHAR (100)  NULL,
    [ModifiedDate]     DATETIME       NULL,
    [RowVersion]       ROWVERSION     NOT NULL,
    CONSTRAINT [PKC__CustomerAttributeType__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [UKC__CustomerAttributeType__Name] UNIQUE NONCLUSTERED ([Name] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX__CustomerEventType__Name]
    ON [dbo].[CustomerAttributeType]([Name] ASC);

