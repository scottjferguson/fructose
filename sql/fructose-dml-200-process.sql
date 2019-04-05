USE [Fructose]
GO

DECLARE
  @createdBy       AS VARCHAR(100)        = 'sferguson'
, @createdDate     AS DATETIME            = CURRENT_TIMESTAMP

BEGIN

/*
 *  ###########################################################
 *    INSERT - CustomerStatus
 *  ###########################################################
 */

IF NOT EXISTS (SELECT 1 FROM [dbo].[CustomerStatus] WHERE [Name] = 'Pending' AND [SubStatusName] IS NULL)
BEGIN
    INSERT INTO [dbo].[CustomerStatus] ([Name], [SubStatusName], [Description], [Code], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('Pending', NULL, 'The Customer data is being held in until a future date/time', 'PEND', 'Pending', 100, 1, @createdBy, @createdDate)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[CustomerStatus] WHERE [Name] = 'Pending' AND [SubStatusName] = 'Error')
BEGIN
    INSERT INTO [dbo].[CustomerStatus] ([Name], [SubStatusName], [Description], [Code], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('Pending', 'Error', 'The Customer data is being held due to a validation error', 'PNDE', 'Pending - Error', 200, 1, @createdBy, @createdDate)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[CustomerStatus] WHERE [Name] = 'Active' AND [SubStatusName] IS NULL)
BEGIN
    INSERT INTO [dbo].[CustomerStatus] ([Name], [SubStatusName], [Description], [Code], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('Active', NULL, 'The Customer is considered accepted', 'ACTV', 'Active', 300, 1, @createdBy, @createdDate)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[CustomerStatus] WHERE [Name] = 'Active' AND [SubStatusName] = 'Cancel Requested')
BEGIN
    INSERT INTO [dbo].[CustomerStatus] ([Name], [SubStatusName], [Description], [Code], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('Active', 'Cancel Requested', 'The Customer is still active but a cancel has been requested and the Customer is in a state where they can be won back', 'ATVC', 'Active - Cancel Requested', 400, 1, @createdBy, @createdDate)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[CustomerStatus] WHERE [Name] = 'Cancelled' AND [SubStatusName] IS NULL)
BEGIN
    INSERT INTO [dbo].[CustomerStatus] ([Name], [SubStatusName], [Description], [Code], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('Cancelled', NULL, 'The Customer was once active with the platform but the cancel request has been fulfilled', 'CANC', 'Cancelled', 500, 1, @createdBy, @createdDate)
END

/*
 *  ###########################################################
 *    INSERT - CustomerType
 *  ###########################################################
 */

IF NOT EXISTS (SELECT 1 FROM [dbo].[CustomerType] WHERE [Name] = 'Personal')
BEGIN
    INSERT INTO [dbo].[CustomerType] ([Name], [Description], [Code], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('Personal', 'The Customer has a personal account for their use only', 'PERS', 'Personal', 100, 1, @createdBy, @createdDate)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[CustomerType] WHERE [Name] = 'Business')
BEGIN
    INSERT INTO [dbo].[CustomerType] ([Name], [Description], [Code], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('Business', 'The Customer is considered to be a business organization', 'BUSI', 'Business', 200, 1, @createdBy, @createdDate)
END

/*
 *  ###########################################################
 *    INSERT - CustomerNoteType
 *  ###########################################################
 */

IF NOT EXISTS (SELECT 1 FROM [dbo].[CustomerNoteType] WHERE [Name] = 'General')
BEGIN
    INSERT INTO [dbo].[CustomerNoteType] ([Name], [Description], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('General', 'A general note to be left on the Customer''s account', 'General', 100, 1, @createdBy, @createdDate)
END

/*
 *  ###########################################################
 *    INSERT - CustomerSearchTermType
 *  ###########################################################
 */

IF NOT EXISTS (SELECT 1 FROM [dbo].[CustomerSearchTermType] WHERE [Name] = 'FirstName')
BEGIN
    INSERT INTO [dbo].[CustomerSearchTermType] ([Name], [Description], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('FirstName', 'The first name of the Customer', 'First Name', 100, 1, @createdBy, @createdDate)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[CustomerSearchTermType] WHERE [Name] = 'LastName')
BEGIN
    INSERT INTO [dbo].[CustomerSearchTermType] ([Name], [Description], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('LastName', 'The last name of the Customer', 'Last Name', 200, 1, @createdBy, @createdDate)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[CustomerSearchTermType] WHERE [Name] = 'CustomerNumber')
BEGIN
    INSERT INTO [dbo].[CustomerSearchTermType] ([Name], [Description], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('CustomerNumber', 'The Customer''s unique identifier (known by the Customer, not for internal use only)', 'Customer Number', 300, 1, @createdBy, @createdDate)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[CustomerSearchTermType] WHERE [Name] = 'AddressLine1')
BEGIN
    INSERT INTO [dbo].[CustomerSearchTermType] ([Name], [Description], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('AddressLine1', 'Line 1 of the Customer''s address (regardless of AddressTypeID)', 'Address Line 1', 400, 1, @createdBy, @createdDate)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[CustomerSearchTermType] WHERE [Name] = 'AddressLine2')
BEGIN
    INSERT INTO [dbo].[CustomerSearchTermType] ([Name], [Description], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('AddressLine2', 'Line 2 of the Customer''s address (regardless of AddressTypeID)', 'Address Line 2', 500, 1, @createdBy, @createdDate)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[CustomerSearchTermType] WHERE [Name] = 'AddressLine3')
BEGIN
    INSERT INTO [dbo].[CustomerSearchTermType] ([Name], [Description], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('AddressLine3', 'Line 3 of the Customer''s address (regardless of AddressTypeID)', 'Address Line 3', 600, 1, @createdBy, @createdDate)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[CustomerSearchTermType] WHERE [Name] = 'City')
BEGIN
    INSERT INTO [dbo].[CustomerSearchTermType] ([Name], [Description], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('City', 'The City the Customer''s address (regardless of AddressTypeID)', 'City', 700, 1, @createdBy, @createdDate)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[CustomerSearchTermType] WHERE [Name] = 'StateProv')
BEGIN
    INSERT INTO [dbo].[CustomerSearchTermType] ([Name], [Description], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('StateProv', 'The State the Customer''s address (regardless of AddressTypeID)', 'StateProv', 800, 1, @createdBy, @createdDate)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[CustomerSearchTermType] WHERE [Name] = 'PostalCode')
BEGIN
    INSERT INTO [dbo].[CustomerSearchTermType] ([Name], [Description], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('PostalCode', 'The Postal Code the Customer''s address (regardless of AddressTypeID)', 'Postal Code', 900, 1, @createdBy, @createdDate)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[CustomerSearchTermType] WHERE [Name] = 'EmailAddress')
BEGIN
    INSERT INTO [dbo].[CustomerSearchTermType] ([Name], [Description], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('EmailAddress', 'The Email Address the Customer (regardless of EmailTypeID)', 'Email Address', 1000, 1, @createdBy, @createdDate)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[CustomerSearchTermType] WHERE [Name] = 'PhoneNumber')
BEGIN
    INSERT INTO [dbo].[CustomerSearchTermType] ([Name], [Description], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('PhoneNumber', 'The Phone Number the Customer (regardless of PhoneTypeID)', 'Phone Number', 1100, 1, @createdBy, @createdDate)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[CustomerSearchTermType] WHERE [Name] = 'CustomerAttribute')
BEGIN
    INSERT INTO [dbo].[CustomerSearchTermType] ([Name], [Description], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('CustomerAttribute', 'The Attribute Value of the Customer', 'Customer Attribute', 1200, 1, @createdBy, @createdDate)
END

/*
 *  ###########################################################
 *    INSERT - CustomerEventType
 *  ###########################################################
 */

IF NOT EXISTS (SELECT 1 FROM [dbo].[CustomerEventType] WHERE [Name] = 'StatusChange')
BEGIN
    INSERT INTO [dbo].[CustomerEventType] ([Name], [Description], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('StatusChange', 'Records whenever a Customer''s status changes', 'Status Change', 100, 1, @createdBy, @createdDate)
END

/*
 *  ###########################################################
 *    INSERT - CustomerAttributeType
 *  ###########################################################
 */

IF NOT EXISTS (SELECT 1 FROM [dbo].[CustomerAttributeType] WHERE [Name] = 'NationalIdentifier')
BEGIN
    INSERT INTO [dbo].[CustomerAttributeType] ([Name], [Description], [DisplayName], [DisplayOrder], [IsValueEncrypted], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('NationalIdentifier', 'The national unique identifier of the Customer (i.e. for the US, NationalIdentifier is synonymous with Social Security Number)', 'National Identifier', 100, 1, 1, @createdBy, @createdDate)
END

/*
 *  ###########################################################
 *    INSERT - AddressType
 *  ###########################################################
 */

IF NOT EXISTS (SELECT 1 FROM [dbo].[AddressType] WHERE [Name] = 'Home')
BEGIN
    INSERT INTO [dbo].[AddressType] ([Name], [Description], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('Home', 'Indicates a Home address', 'Home', 100, 1, @createdBy, @createdDate)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[AddressType] WHERE [Name] = 'Billing')
BEGIN
    INSERT INTO [dbo].[AddressType] ([Name], [Description], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('Billing', 'Indicates a Billing address', 'Billing', 200, 1, @createdBy, @createdDate)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[AddressType] WHERE [Name] = 'Shipping')
BEGIN
    INSERT INTO [dbo].[AddressType] ([Name], [Description], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('Shipping', 'Indicates a Shipping address', 'Shipping', 300, 1, @createdBy, @createdDate)
END

/*
 *  ###########################################################
 *    INSERT - EmailType
 *  ###########################################################
 */

IF NOT EXISTS (SELECT 1 FROM [dbo].[EmailType] WHERE [Name] = 'Personal')
BEGIN
    INSERT INTO [dbo].[EmailType] ([Name], [Description], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('Personal', 'Indicates a Personal email address', 'Personal', 100, 1, @createdBy, @createdDate)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[EmailType] WHERE [Name] = 'Business')
BEGIN
    INSERT INTO [dbo].[EmailType] ([Name], [Description], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('Business', 'Indicates a Business email address', 'Business', 200, 1, @createdBy, @createdDate)
END

/*
 *  ###########################################################
 *    INSERT - PhoneType
 *  ###########################################################
 */

IF NOT EXISTS (SELECT 1 FROM [dbo].[PhoneType] WHERE [Name] = 'Home')
BEGIN
    INSERT INTO [dbo].[PhoneType] ([Name], [Description], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('Home', 'Indicates a Home phone number', 'Home', 100, 1, @createdBy, @createdDate)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[PhoneType] WHERE [Name] = 'Work')
BEGIN
    INSERT INTO [dbo].[PhoneType] ([Name], [Description], [DisplayName], [DisplayOrder], [IsActive], [CreatedBy], [CreatedDate])
    VALUES ('Work', 'Indicates a Work phone number', 'Work', 200, 1, @createdBy, @createdDate)
END

END