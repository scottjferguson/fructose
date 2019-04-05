CREATE TABLE [dbo].[PersonEmail] (
    [ID]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [PersonID]     BIGINT        NOT NULL,
    [EmailID]      BIGINT        NOT NULL,
    [IsActive]     BIT           CONSTRAINT [DF__PersonEmail__IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedBy]    VARCHAR (100) CONSTRAINT [DF__PersonEmail__CreatedBy] DEFAULT (suser_sname()) NOT NULL,
    [CreatedDate]  DATETIME      CONSTRAINT [DF__PersonEmail__CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]   VARCHAR (100) NULL,
    [ModifiedDate] DATETIME      NULL,
    [RowVersion]   ROWVERSION    NOT NULL,
    CONSTRAINT [PKC__PersonEmail__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FKN__PersonEmail__EmailID] FOREIGN KEY ([EmailID]) REFERENCES [dbo].[Email] ([ID]),
    CONSTRAINT [FKN__PersonEmail__PersonID] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Person] ([ID])
);

