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

IF  (SELECT COUNT(*) FROM  [dbo].[tblRoleType])<3
BEGIN

INSERT [dbo].[tblRoleType] ([RoleId], [RoleName], [RoleLevel], [ParentRoleId], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsActive], [IsDeleted]) VALUES (1, N'Super Admin', 0, NULL, GETDATE(), NULL,GETDATE(), NULL, 1, 0)
INSERT [dbo].[tblRoleType] ([RoleId], [RoleName], [RoleLevel], [ParentRoleId], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsActive], [IsDeleted]) VALUES (2, N'Admin', 1, NULL, GETDATE(), NULL,GETDATE(), NULL, 1, 0)
INSERT [dbo].[tblRoleType] ([RoleId], [RoleName], [RoleLevel], [ParentRoleId], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsActive], [IsDeleted]) VALUES (3, N'Customer', 2, NULL, GETDATE(), NULL,GETDATE(), NULL, 1, 0)

END

SET IDENTITY_INSERT [dbo].[tblRoleType] OFF
GO


SET IDENTITY_INSERT [dbo].[tblUserMaster] ON 
GO
IF  (SELECT COUNT(*) FROM  [dbo].[tblUserMaster] )<3

BEGIN

INSERT [dbo].[tblUserMaster] ([UserId], [Email], [FirstName], [LastName], [DOB], [Address], [Password], [Mobile], [RoleId], [ProfilePhoto], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsActive], [IsDeleted]) VALUES (1, N'sandeep.suthar08@gmail.com', N'Sandeep', N'Suthar', NULL, NULL, N'vKkop56NIq7X09yArdmvew==', NULL, 1, NULL, GETDATE(), NULL, GETDATE(), NULL, 1, 0)
INSERT [dbo].[tblUserMaster] ([UserId], [Email], [FirstName], [LastName], [DOB], [Address], [Password], [Mobile], [RoleId], [ProfilePhoto], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsActive], [IsDeleted]) VALUES (2, N'manish1994@gmail.com', N'Manish', N'Kumar', NULL, NULL, N'vKkop56NIq7X09yArdmvew==', NULL, 1, NULL, GETDATE(), NULL, GETDATE(), NULL, 1, 0)
INSERT [dbo].[tblUserMaster] ([UserId], [Email], [FirstName], [LastName], [DOB], [Address], [Password], [Mobile], [RoleId], [ProfilePhoto], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsActive], [IsDeleted]) VALUES (3, N'storeone@gmail.com', N'Store one', N'User', NULL, NULL, N'vKkop56NIq7X09yArdmvew==', NULL, 1, NULL, GETDATE(), NULL, GETDATE(), NULL, 1, 0)

END

SET IDENTITY_INSERT [dbo].[tblUserMaster] OFF
GO

SET IDENTITY_INSERT [dbo].[tblLookupTypeMaster] ON 
GO

IF  (SELECT COUNT(*) FROM  [dbo].[tblLookupTypeMaster] )<8

BEGIN

INSERT [dbo].[tblLookupTypeMaster] ([Id], [Name], [EnumValue], [IsSubLookup],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete],[IsImage]) VALUES (1, N'Gender', N'GENDER',0, 1,GETDATE(), 1, GETDATE(), 0, 0,0)
INSERT [dbo].[tblLookupTypeMaster] ([Id], [Name], [EnumValue], [IsSubLookup],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete],[IsImage]) VALUES (2, N'Product Category', N'Product_Category',1, 1,GETDATE(), 1, GETDATE(), 1, 0,1)
INSERT [dbo].[tblLookupTypeMaster] ([Id], [Name], [EnumValue], [IsSubLookup],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete],[IsImage]) VALUES (3, N'Caption Tag', N'Caption_Tag',0, 1, GETDATE(), 1,GETDATE(), 1, 0,0)
INSERT [dbo].[tblLookupTypeMaster] ([Id], [Name], [EnumValue], [IsSubLookup],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete],[IsImage]) VALUES (4, N'Product Size', N'Product_Size',0, 1, GETDATE(), 1,GETDATE(), 1, 0,0)
INSERT [dbo].[tblLookupTypeMaster] ([Id], [Name], [EnumValue], [IsSubLookup],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete],[IsImage]) VALUES (5, N'CMS Pages', N'CMS_Page',0, 1, GETDATE(), 1,GETDATE(), 1, 0,0)
INSERT [dbo].[tblLookupTypeMaster] ([Id], [Name], [EnumValue], [IsSubLookup],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete],[IsImage]) VALUES (6, N'Address Type', N'Address_Type',0, 1, GETDATE(), 1,GETDATE(), 1, 0,0)
INSERT [dbo].[tblLookupTypeMaster] ([Id], [Name], [EnumValue], [IsSubLookup],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete],[IsImage]) VALUES (7, N'State', N'State',0, 1, GETDATE(), 1,GETDATE(), 1, 0,0)
INSERT [dbo].[tblLookupTypeMaster] ([Id], [Name], [EnumValue], [IsSubLookup],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete],[IsImage]) VALUES (8, N'Home Page Product Section', N'Product_View_Section',0, 1, GETDATE(), 1,GETDATE(), 1, 0,0)

END

SET IDENTITY_INSERT [dbo].[tblLookupTypeMaster] OFF
GO

SET IDENTITY_INSERT [dbo].[tblGECategoryMater] ON 
GO
IF  (SELECT COUNT(*) FROM  [dbo].[tblGECategoryMater] )<1

BEGIN
INSERT INTO [dbo].[tblGECategoryMater] ([Id],[Name],[EnumValue],[ImagePath],[IsShowInMain],[IsShowDataInMain],[IsSingleEntry],[SortedOrder],[CreatedBy],[ModifiedBy],[IsSystemEntry],[ContentType],[IsShowThumbnail],[IsShowUrl]) VALUES (1,'Banner Image','Banner_Image',null,0,0,0,1,1,1,1,1,0,0)
END

SET IDENTITY_INSERT [dbo].[tblGECategoryMater] OFF 
GO

SET IDENTITY_INSERT [dbo].[tblLookupMaster] ON 
GO
IF  (SELECT COUNT(*) FROM  [dbo].[tblLookupMaster] )<43
BEGIN

INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete]) VALUES (1,'Home',1,6, 1, GETDATE(), 1,GETDATE(), 1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (2,'Office',2,6, 1, GETDATE(), 1,GETDATE(), 1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (3,'Andaman & Nicobar Islands',1,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (4,'Andhra Pradesh',2,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (5,'Arunachal Pradesh',3,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (6,'Assam',4,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (7,'Bihar',5,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (8,'Chandigarh',6,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (9,'Chhattisgarh',7,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (10,'Dadra & Nagar Haveli & Daman & Diu',8,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (11,'Delhi',9,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (12,'Goa',10,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (13,'Gujarat',11,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (14,'Haryana',12,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (15,'Himachal Pradesh',13,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (16,'Jammu & Kashmir',14,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (17,'Jharkhand',15,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (18,'Karnataka',16,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (19,'Kerala',17,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (20,'Ladakh',18,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (21,'Lakshadweep',19,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (22,'Madhya Pradesh',20,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (23,'Maharashtra',21,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (24,'Manipur',22,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (25,'Meghalaya',23,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (26,'Mizoram',24,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (27,'Nagaland',25,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (28,'Odisha',26,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (29,'Puducherry',27,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (30,'Punjab',28,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (31,'Rajasthan',29,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (32,'Sikkim',30,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (33,'Tamil Nadu',31,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (34,'Telangana',32,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (35,'Tripura',33,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (36,'Uttarakhand',34,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (37,'Uttar Pradesh',35,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (38,'West Bengal',36,7,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (39,'New',1,8,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (40,'Top',2,8,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (41,'Featured',3,8,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (42,'Best Selling',4,8,1, GETDATE(), 1, GETDATE(),1,0)
INSERT INTO [dbo].[tblLookupMaster]([Id],[Name],[SortedOrder],[LookUp_Type],[CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive], [IsDelete])  VALUES (43,'Trending',5,8,1, GETDATE(), 1, GETDATE(),1,0)
END

SET IDENTITY_INSERT [dbo].[tblLookupMaster] OFF 
GO