/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:       :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

SET IDENTITY_INSERT [dbo].[tblRoleType] ON 
GO
INSERT [dbo].[tblRoleType] ([RoleId], [RoleName], [RoleLevel], [ParentRoleId], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsActive], [IsDeleted]) VALUES (1, N'Super Admin', 0, NULL, GETDATE(), NULL,GETDATE(), NULL, 1, 0)
INSERT [dbo].[tblRoleType] ([RoleId], [RoleName], [RoleLevel], [ParentRoleId], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsActive], [IsDeleted]) VALUES (2, N'Admin', 1, NULL, GETDATE(), NULL,GETDATE(), NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[tblRoleType] OFF
GO

SET IDENTITY_INSERT [dbo].[tblUserMaster] ON 
GO
INSERT [dbo].[tblUserMaster] ([UserId], [Email], [FirstName], [LastName], [DOB], [Address], [Password], [Mobile], [RoleId], [ProfilePhoto], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsActive], [IsDeleted]) VALUES (1, N'sandeep.suthar08@gmail.com', N'Sandeep', N'Suthar', NULL, NULL, N'123', NULL, 1, NULL, GETDATE(), NULL, GETDATE(), NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[tblUserMaster] OFF
GO

SET IDENTITY_INSERT [dbo].[tblLookupTypeMaster] ON 
GO
INSERT [dbo].[tblLookupTypeMaster] ([Id], [Name], [EnumValue], [IsSubLookup],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete]) VALUES (1, N'Gender', N'GENDER',0, 1,GETDATE(), 1, GETDATE(), 1, 0)
GO                                                             
INSERT [dbo].[tblLookupTypeMaster] ([Id], [Name], [EnumValue], [IsSubLookup],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete]) VALUES (2, N'Product Category', N'Product_Category',1, 1,GETDATE(), 1, GETDATE(), 1, 0)
GO                                                             
INSERT [dbo].[tblLookupTypeMaster] ([Id], [Name], [EnumValue], [IsSubLookup],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete]) VALUES (3, N'Caption Tag', N'Caption_Tag',0, 1, GETDATE(), 1,GETDATE(), 1, 0)
GO
SET IDENTITY_INSERT [dbo].[tblLookupTypeMaster] OFF
GO
