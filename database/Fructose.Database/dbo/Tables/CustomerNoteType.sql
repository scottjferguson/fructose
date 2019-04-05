CREATE TABLE [dbo].[CustomerNoteType] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (100)  NOT NULL,
    [Description]  VARCHAR (4000) NOT NULL,
    [DisplayName]  VARCHAR (100)  NOT NULL,
    [DisplayOrder] INT            NULL,
    [IsActive]     BIT            CONSTRAINT [DF__CustomerNoteType__IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedBy]    VARCHAR (100)  CONSTRAINT [DF__CustomerNoteType__CreatedBy] DEFAULT (suser_sname()) NOT NULL,
    [CreatedDate]  DATETIME       CONSTRAINT [DF__CustomerNoteType__CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]   VARCHAR (100)  NULL,
    [ModifiedDate] DATETIME       NULL,
    [RowVersion]   ROWVERSION     NOT NULL,
    CONSTRAINT [PKC__CustomerNoteType__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [UKC__CustomerNoteType__Name] UNIQUE NONCLUSTERED ([Name] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX__CustomerNote__Name]
    ON [dbo].[CustomerNoteType]([Name] ASC);

