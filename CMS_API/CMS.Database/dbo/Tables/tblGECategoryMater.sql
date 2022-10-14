CREATE TABLE [dbo].[tblGECategoryMater]
(
	[Id] BIGINT NOT NULL   IDENTITY(1,1), 
    [Name] NVARCHAR (500) NULL,
    [EnumValue]  VARCHAR(20) NOT NULL UNIQUE,
    [ImagePath]  NVARCHAR (4000) NULL,
    [IsShowInMain]   BIT NOT NULL DEFAULT 0,
    [IsShowDataInMain]   BIT NOT NULL DEFAULT 0,
    [IsSingleEntry]   BIT NOT NULL DEFAULT 0,
    [SortedOrder] INT NULL,
    [CreatedBy]  BIGINT NOT NULL,
    [CreatedOn]  DATETIME  NOT NULL DEFAULT getdate(),
    [ModifiedBy] BIGINT NOT NULL,
    [ModifiedOn] DATETIME  NOT NULL DEFAULT getdate(),
    [IsActive]   BIT NOT NULL DEFAULT 1,
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_tblGECategoryMater] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tblGECategoryMater_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [tblUserMaster]([UserId]),
    CONSTRAINT [FK_tblGECategoryMater_ModifiedBy] FOREIGN KEY ([ModifiedBy]) REFERENCES [tblUserMaster]([UserId])

)
