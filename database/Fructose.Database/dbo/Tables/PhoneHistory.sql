CREATE TABLE [dbo].[PhoneHistory] (
    [ID]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [PhoneID]     BIGINT        NOT NULL,
    [PhoneTypeID] INT           NOT NULL,
    [PhoneNumber] VARCHAR (50)  NOT NULL,
    [IsValidated] BIT           NULL,
    [CreatedBy]   VARCHAR (100) CONSTRAINT [DF__PhoneHistory__CreatedBy] DEFAULT (suser_sname()) NOT NULL,
    [CreatedDate] DATETIME      CONSTRAINT [DF__PhoneHistory__CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PKC__PhoneHistory__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FKN__PhoneHistory__PhoneID] FOREIGN KEY ([PhoneID]) REFERENCES [dbo].[Phone] ([ID])
);

