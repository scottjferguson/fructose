CREATE TABLE [dbo].[CustomerSearch] (
    [ID]               BIGINT        IDENTITY (1, 1) NOT NULL,
    [CustomerID]       BIGINT        NOT NULL,
    [SearchTermTypeID] INT           NOT NULL,
    [SearchTerm]       VARCHAR (900) NOT NULL,
    [CreatedBy]        VARCHAR (100) CONSTRAINT [DF__CustomerSearch__CreatedBy] DEFAULT (suser_sname()) NOT NULL,
    [CreatedDate]      DATETIME      CONSTRAINT [DF__CustomerSearch__CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PKC__CustomerSearch__ID] PRIMARY KEY NONCLUSTERED ([ID] ASC),
    CONSTRAINT [FKN__CustomerSearch__CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customer] ([ID]),
    CONSTRAINT [FKN__CustomerSearch__SearchTermTypeID] FOREIGN KEY ([SearchTermTypeID]) REFERENCES [dbo].[CustomerSearchTermType] ([ID])
);


GO
CREATE CLUSTERED INDEX [IX__CustomerSearch__SearchTerm]
    ON [dbo].[CustomerSearch]([SearchTerm] ASC);

