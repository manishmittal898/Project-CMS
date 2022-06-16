CREATE TABLE [dbo].[tblProductImage] (
    [Id]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [FileName]   NVARCHAR (250) NULL,
    [FilePath]   NVARCHAR(MAX)         NULL,
    [Product_Id] BIGINT            NULL,
    [CreatedOn]  DATETIME       DEFAULT (getdate()) NOT NULL,
    [CreatedBy]  BIGINT         NULL,
    [ModifiedOn] DATETIME       NOT NULL DEFAULT GETDATE(),
    [ModifiedBy] BIGINT         NOT NULL ,
    [IsActive]   BIT            DEFAULT ((1)) NOT NULL,
    [IsDeleted]  BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tblProductImage_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [tblUserMaster]([UserId]),
    CONSTRAINT [FK_tblProductImage_ModifiedBy] FOREIGN KEY ([ModifiedBy]) REFERENCES [tblUserMaster]([UserId])
);

