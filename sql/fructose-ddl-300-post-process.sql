USE [Fructose]
GO

PRINT 'Running DDL Post-Process...'
GO

CREATE VIEW [dbo].[vwPerson]
AS
    SELECT
          p.[ID]
        , p.[FirstName]
        , p.[LastName]
        , p.[IsActive]
        , p.[CreatedBy]
        , p.[CreatedDate]
        , p.[ModifiedBy]
        , p.[ModifiedDate]
    FROM [dbo].[Person] p (NOLOCK)
GO

CREATE VIEW [dbo].[vwCustomer]
AS
    SELECT
          c.[ID]
        , c.[OrganizationID]
        , c.[PersonID]
        , c.[CustomerStatusID]
        , c.[CustomerTypeID]
        , c.[CustomerNumber]
        , p.[FirstName]
        , p.[LastName]
        , c.[JoinDate]
        , cs.[Name]                      AS [CustomerStatusName]
        , ct.[Name]                      AS [CustomerTypeName]
        , c.[ReferenceIdentifier]
        , c.[IsActive]
        , p.[IsActive]                   AS [IsPersonActive]
        , c.[CreatedBy]
        , c.[CreatedDate]
        , c.[ModifiedBy]
        , c.[ModifiedDate]
        , p.[CreatedBy]                  AS [PersonCreatedBy]
        , p.[CreatedDate]                AS [PersonCreatedDate]
        , p.[ModifiedBy]                 AS [PersonModifiedBy]
        , p.[ModifiedDate]               AS [PersonModifiedDate]
    FROM [dbo].[Customer] c (NOLOCK)
        INNER JOIN [dbo].[CustomerStatus] cs (NOLOCK) ON c.CustomerStatusID = cs.ID
        INNER JOIN [dbo].[CustomerType] ct (NOLOCK) ON c.CustomerTypeID = ct.ID
        INNER JOIN [dbo].[Person] p (NOLOCK) ON c.PersonID = p.ID
GO

PRINT 'Complete'
GO