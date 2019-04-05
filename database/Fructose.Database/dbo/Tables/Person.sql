CREATE TABLE [dbo].[Person] (
    [ID]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [FirstName]    VARCHAR (100) NOT NULL,
    [LastName]     VARCHAR (100) NOT NULL,
    [IsActive]     BIT           CONSTRAINT [DF__Person__IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedBy]    VARCHAR (100) CONSTRAINT [DF__Person__CreatedBy] DEFAULT (suser_sname()) NOT NULL,
    [CreatedDate]  DATETIME      CONSTRAINT [DF__Person__CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]   VARCHAR (100) NULL,
    [ModifiedDate] DATETIME      NULL,
    [RowVersion]   ROWVERSION    NOT NULL,
    CONSTRAINT [PKC__Person__ID] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX__Person__FirstName]
    ON [dbo].[Person]([FirstName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX__Person__LastName]
    ON [dbo].[Person]([LastName] ASC);

