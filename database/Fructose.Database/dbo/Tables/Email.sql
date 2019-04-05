CREATE TABLE [dbo].[Email] (
    [ID]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [EmailTypeID]  INT           NOT NULL,
    [EmailAddress] VARCHAR (500) NOT NULL,
    [IsValidated]  BIT           NULL,
    [CreatedBy]    VARCHAR (100) CONSTRAINT [DF__Email__CreatedBy] DEFAULT (suser_sname()) NOT NULL,
    [CreatedDate]  DATETIME      CONSTRAINT [DF__Email__CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]   VARCHAR (100) NULL,
    [ModifiedDate] DATETIME      NULL,
    [RowVersion]   ROWVERSION    NOT NULL,
    CONSTRAINT [PKC__Email__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FKN__Email__EmailTypeID] FOREIGN KEY ([EmailTypeID]) REFERENCES [dbo].[EmailType] ([ID])
);


GO
CREATE NONCLUSTERED INDEX [IX__Email__EmailAddress]
    ON [dbo].[Email]([EmailAddress] ASC);

