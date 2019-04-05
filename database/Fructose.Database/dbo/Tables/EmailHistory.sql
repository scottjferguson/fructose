CREATE TABLE [dbo].[EmailHistory] (
    [ID]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [EmailID]      BIGINT        NOT NULL,
    [EmailTypeID]  INT           NOT NULL,
    [EmailAddress] VARCHAR (500) NOT NULL,
    [IsValidated]  BIT           NULL,
    [CreatedBy]    VARCHAR (100) CONSTRAINT [DF__EmailHistory__CreatedBy] DEFAULT (suser_sname()) NOT NULL,
    [CreatedDate]  DATETIME      CONSTRAINT [DF__EmailHistory__CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PKC__EmailHistory__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FKN__EmailHistory__EmailID] FOREIGN KEY ([EmailID]) REFERENCES [dbo].[Email] ([ID])
);

