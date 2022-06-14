CREATE TABLE [dbo].[tblSubLookupTypeMaster]
(
    [Id]         BIGINT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (250) NULL,
    [CreatedBy]  BIGINT NOT NULL,
    [CreatedOn]  DATETIME       NOT NULL DEFAULT getdate(),
    [ModifiedBy] BIGINT NOT NULL,
    [ModifiedOn] DATETIME       NOT NULL DEFAULT getdate(),
    [IsActive]   BIT            NOT NULL DEFAULT 1,
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_tblSubLookupMaster] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_tblSubLookupTypeMaster_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [tblUserMaster]([UserId]),
    CONSTRAINT [FK_tblSubLookupTypeMaster_ModifiedBy] FOREIGN KEY ([ModifiedBy]) REFERENCES [tblUserMaster]([UserId])

);