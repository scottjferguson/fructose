CREATE TABLE [dbo].[PersonPhone] (
    [ID]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [PersonID]     BIGINT        NOT NULL,
    [PhoneID]      BIGINT        NOT NULL,
    [IsActive]     BIT           CONSTRAINT [DF__PersonPhone__IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedBy]    VARCHAR (100) CONSTRAINT [DF__PersonPhone__CreatedBy] DEFAULT (suser_sname()) NOT NULL,
    [CreatedDate]  DATETIME      CONSTRAINT [DF__PersonPhone__CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]   VARCHAR (100) NULL,
    [ModifiedDate] DATETIME      NULL,
    [RowVersion]   ROWVERSION    NOT NULL,
    CONSTRAINT [PKC__PersonPhone__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FKN__PersonPhone__PersonID] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Person] ([ID]),
    CONSTRAINT [FKN__PersonPhone__PhoneID] FOREIGN KEY ([PhoneID]) REFERENCES [dbo].[Phone] ([ID])
);

