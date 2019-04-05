CREATE TABLE [dbo].[PhoneType] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (100)  NOT NULL,
    [Description]  VARCHAR (4000) NOT NULL,
    [DisplayName]  VARCHAR (100)  NOT NULL,
    [DisplayOrder] INT            NULL,
    [IsActive]     BIT            CONSTRAINT [DF__PhoneType__IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedBy]    VARCHAR (100)  CONSTRAINT [DF__PhoneType__CreatedBy] DEFAULT (suser_sname()) NOT NULL,
    [CreatedDate]  DATETIME       CONSTRAINT [DF__PhoneType__CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]   VARCHAR (100)  NULL,
    [ModifiedDate] DATETIME       NULL,
    [RowVersion]   ROWVERSION     NOT NULL,
    CONSTRAINT [PKC__PhoneType__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [UKC__PhoneType__Name] UNIQUE NONCLUSTERED ([Name] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX__PhoneType__Name]
    ON [dbo].[PhoneType]([Name] ASC);

