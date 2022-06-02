CREATE TABLE [dbo].[tblProductImage] (
    [Id]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [FileName]   NVARCHAR (250) NULL,
    [FilePath]   BIGINT         NULL,
    [Product_Id] INT            NULL,
    [CreatedOn]  DATETIME       DEFAULT (getdate()) NULL,
    [CreatedBy]  BIGINT         NULL,
    [ModifiedOn] DATETIME       NULL,
    [ModifiedBy] BIGINT         NULL,
    [IsActive]   BIT            DEFAULT ((1)) NOT NULL,
    [IsDeleted]  BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[tblUserMaster] ([UserId]),
    FOREIGN KEY ([Product_Id]) REFERENCES [dbo].[tblProductMaster] ([Id])
);

