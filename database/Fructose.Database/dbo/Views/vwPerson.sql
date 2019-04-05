
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
