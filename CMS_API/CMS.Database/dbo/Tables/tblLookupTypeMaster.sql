CREATE TABLE [dbo].[tblLookupTypeMaster] (
    [Id]         BIGINT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (250) NULL,
    [EnumValue]  VARCHAR(20) NOT NULL,
    [IsSubLookup] BIT NOT NULL DEFAULT 0, 
    [CreatedBy]  BIGINT NOT NULL,
    [CreatedOn]  DATETIME       NOT NULL DEFAULT getdate(),
    [ModifiedBy] BIGINT NOT NULL,
    [ModifiedOn] DATETIME       NOT NULL DEFAULT getdate(),
    [IsActive]   BIT            NOT NULL DEFAULT 1,
    [IsDelete] BIT NOT NULL DEFAULT 0,
    [IsImage]   BIT  NOT NULL DEFAULT 0 ,
    CONSTRAINT [PK_tblLookupMaster] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_tblLookupTypeMaster_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [tblUserMaster]([UserId]),
    CONSTRAINT [FK_tblLookupTypeMaster_ModifiedBy] FOREIGN KEY ([ModifiedBy]) REFERENCES [tblUserMaster]([UserId])

);

