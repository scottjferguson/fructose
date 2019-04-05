USE [Fructose]
GO

PRINT 'Running DML Post-Process...'
GO

SELECT * 
FROM [dbo].[vwPerson]

SELECT * 
FROM [dbo].[vwCustomer]

SELECT * 
FROM [dbo].[CustomerStatus]

SELECT * 
FROM [dbo].[CustomerType]

SELECT * 
FROM [dbo].[CustomerNoteType]

SELECT * 
FROM [dbo].[CustomerSearchTermType]

SELECT * 
FROM [dbo].[CustomerEventType]

SELECT * 
FROM [dbo].[CustomerAttributeType]

SELECT * 
FROM [dbo].[AddressType]

SELECT * 
FROM [dbo].[EmailType]

SELECT * 
FROM [dbo].[PhoneType]

PRINT 'Complete'
GO