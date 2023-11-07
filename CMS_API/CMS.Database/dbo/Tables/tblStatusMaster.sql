CREATE TABLE [dbo].[tblStatusMaster]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name] NVARCHAR(100) NOT NULL, 
    [Level] INT NOT NULL, 
    [IsFinal] BIT NOT NULL DEFAULT 0,
	[CreatedBy]  BIGINT NOT NULL,
    [CreatedOn]  DATETIME  NOT NULL DEFAULT getdate(),
    [ModifiedBy] BIGINT NOT NULL,
    [ModifiedOn] DATETIME  NOT NULL DEFAULT getdate(),
    [IsActive]   BIT NOT NULL DEFAULT 1,
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_tblStatusMaster_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [tblUserMaster]([UserId]),
    CONSTRAINT [FK_tblStatusMaster_ModifiedBy] FOREIGN KEY ([ModifiedBy]) REFERENCES [tblUserMaster]([UserId])
)
