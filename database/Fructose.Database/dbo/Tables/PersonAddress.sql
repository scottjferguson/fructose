CREATE TABLE [dbo].[PersonAddress] (
    [ID]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [PersonID]     BIGINT        NOT NULL,
    [AddressID]    BIGINT        NOT NULL,
    [IsPhysical]   BIT           CONSTRAINT [DF__PersonAddress__IsPhysical] DEFAULT ((1)) NOT NULL,
    [IsShipping]   BIT           CONSTRAINT [DF__PersonAddress__IsShipping] DEFAULT ((1)) NOT NULL,
    [IsBilling]    BIT           CONSTRAINT [DF__PersonAddress__IsBilling] DEFAULT ((1)) NOT NULL,
    [IsActive]     BIT           CONSTRAINT [DF__PersonAddress__IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedBy]    VARCHAR (100) CONSTRAINT [DF__PersonAddress__CreatedBy] DEFAULT (suser_sname()) NOT NULL,
    [CreatedDate]  DATETIME      CONSTRAINT [DF__PersonAddress__CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]   VARCHAR (100) NULL,
    [ModifiedDate] DATETIME      NULL,
    [RowVersion]   ROWVERSION    NOT NULL,
    CONSTRAINT [PKC__PersonAddress__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FKN__PersonAddress__AddressID] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Address] ([ID]),
    CONSTRAINT [FKN__PersonAddress__PersonID] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Person] ([ID])
);

