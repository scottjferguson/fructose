CREATE TABLE [dbo].[PersonHistory] (
    [ID]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [PersonID]    BIGINT        NOT NULL,
    [FirstName]   VARCHAR (100) NOT NULL,
    [LastName]    VARCHAR (100) NOT NULL,
    [IsActive]    BIT           NOT NULL,
    [CreatedBy]   VARCHAR (100) CONSTRAINT [DF__PersonHistory__CreatedBy] DEFAULT (suser_sname()) NOT NULL,
    [CreatedDate] DATETIME      CONSTRAINT [DF__PersonHistory__CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PKC__PersonHistory__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FKN__PersonHistory__PersonID] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Person] ([ID])
);

