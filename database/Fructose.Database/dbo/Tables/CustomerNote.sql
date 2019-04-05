CREATE TABLE [dbo].[CustomerNote] (
    [ID]                 BIGINT        IDENTITY (1, 1) NOT NULL,
    [CustomerID]         BIGINT        NOT NULL,
    [CustomerNoteTypeID] INT           NOT NULL,
    [Note]               VARCHAR (MAX) NOT NULL,
    [IsSuppressed]       BIT           CONSTRAINT [DF__CustomerNote__IsSuppressed] DEFAULT ((0)) NOT NULL,
    [CreatedBy]          VARCHAR (100) CONSTRAINT [DF__CustomerNote__CreatedBy] DEFAULT (suser_sname()) NOT NULL,
    [CreatedDate]        DATETIME      CONSTRAINT [DF__CustomerNotel__CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]         VARCHAR (100) NULL,
    [ModifiedDate]       DATETIME      NULL,
    [RowVersion]         ROWVERSION    NOT NULL,
    CONSTRAINT [PKC__CustomerNote__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FKN__CustomerNote__CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customer] ([ID]),
    CONSTRAINT [FKN__CustomerNote__CustomerNoteTypeID] FOREIGN KEY ([CustomerNoteTypeID]) REFERENCES [dbo].[CustomerNoteType] ([ID])
);

