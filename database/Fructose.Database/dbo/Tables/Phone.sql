CREATE TABLE [dbo].[Phone] (
    [ID]                 BIGINT        IDENTITY (1, 1) NOT NULL,
    [PhoneTypeID]        INT           NOT NULL,
    [PhoneNumber]        VARCHAR (50)  NOT NULL,
    [PhoneNumberNumeric] VARCHAR (50)  NOT NULL,
    [IsValidated]        BIT           NULL,
    [CreatedBy]          VARCHAR (100) CONSTRAINT [DF__Phone__CreatedBy] DEFAULT (suser_sname()) NOT NULL,
    [CreatedDate]        DATETIME      CONSTRAINT [DF__Phone__CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]         VARCHAR (100) NULL,
    [ModifiedDate]       DATETIME      NULL,
    [RowVersion]         ROWVERSION    NOT NULL,
    CONSTRAINT [PKC__Phone__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FKN__Phone__PhoneTypeID] FOREIGN KEY ([PhoneTypeID]) REFERENCES [dbo].[PhoneType] ([ID])
);


GO
CREATE NONCLUSTERED INDEX [IX__Phone__PhoneNumber]
    ON [dbo].[Phone]([PhoneNumber] ASC);

