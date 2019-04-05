CREATE TABLE [dbo].[CustomerEvent] (
    [ID]                  BIGINT        IDENTITY (1, 1) NOT NULL,
    [CustomerID]          BIGINT        NOT NULL,
    [CustomerEventTypeID] INT           NOT NULL,
    [EventDate]           DATETIME      CONSTRAINT [DF__CustomerEvent__EventDate] DEFAULT (getutcdate()) NOT NULL,
    [Notes]               VARCHAR (MAX) NULL,
    [IsSuppressed]        BIT           CONSTRAINT [DF__CustomerEvent__IsSuppressed] DEFAULT ((0)) NOT NULL,
    [CreatedBy]           VARCHAR (100) CONSTRAINT [DF__CustomerEvent__CreatedBy] DEFAULT (suser_sname()) NOT NULL,
    [CreatedDate]         DATETIME      CONSTRAINT [DF__CustomerEvent__CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]          VARCHAR (100) NULL,
    [ModifiedDate]        DATETIME      NULL,
    [RowVersion]          ROWVERSION    NOT NULL,
    CONSTRAINT [PKC__CustomerEvent__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FKN__CustomerEvent__CustomerEventTypeID] FOREIGN KEY ([CustomerEventTypeID]) REFERENCES [dbo].[CustomerEventType] ([ID]),
    CONSTRAINT [FKN__CustomerEvent__CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[CustomerEvent] ([ID])
);

