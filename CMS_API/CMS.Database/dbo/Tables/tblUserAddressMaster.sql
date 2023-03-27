CREATE TABLE [dbo].[tblUserAddressMaster]
(
	[Id]  BIGINT NOT NULL   IDENTITY(1,1), 
    [UserId] BIGINT NOT NULL,
	[FullName] NVARCHAR(1000) NOT NULL, 
    [Mobile]  NVARCHAR (15) NOT NULL,
    [BuildingNumber] NVARCHAR(500) NULL, 
    [Address] NVARCHAR(MAX) NULL, 
    [PinCode] VARCHAR(10) NOT NULL, 
    [Landmark] NVARCHAR(2000) NULL, 
    [City] NVARCHAR(200) NOT NULL, 
    [StateId] BIGINT NULL,    
    [AddressType] BIGINT NULL, 
    [IsPrimary] BIT NOT NULL DEFAULT 0, 
    [CreatedBy]  BIGINT NOT NULL,
    [CreatedOn]  DATETIME  NOT NULL DEFAULT getdate(),
    [ModifiedBy] BIGINT NOT NULL,
    [ModifiedOn] DATETIME  NOT NULL DEFAULT getdate(),
    [IsActive]   BIT NOT NULL DEFAULT 1,
    [IsDelete] BIT NOT NULL DEFAULT 0, 
   
    CONSTRAINT [FK_tblUserAddressMaster_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[tblUserMaster] ([UserId]),
    CONSTRAINT [FK_tblUserAddressMaster_StateId] FOREIGN KEY ([StateId]) REFERENCES [tblLookupMaster]([Id]),
    CONSTRAINT [FK_tblUserAddressMaster_AddressType] FOREIGN KEY ([AddressType]) REFERENCES [tblLookupMaster]([Id]), 
    CONSTRAINT [FK_tblUserAddressMaster_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[tblUserMaster] ([UserId]),
    CONSTRAINT [FK_tblUserAddressMaster_ModifiedBy] FOREIGN KEY ([ModifiedBy]) REFERENCES [dbo].[tblUserMaster] ([UserId]), 
    CONSTRAINT [PK_tblUserAddressMaster] PRIMARY KEY ([Id]), 
  
)
GO
 