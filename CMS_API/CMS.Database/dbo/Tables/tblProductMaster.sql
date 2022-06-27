CREATE TABLE [dbo].[tblProductMaster] (
    [Id]         BIGINT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (2000) NOT NULL,
    [ImagePath]        NVARCHAR (1000) NULL,
    [CategoryId]       BIGINT NOT NULL,
    [SubCategoryId]       BIGINT NOT NULL,
    [Desc]       NTEXT NULL,
    [Price]      DECIMAL (18)   NULL,
    [CaptionTagId]    BIGINT NULL,
    [Summary]    NTEXT NULL,
    [CreatedBy]  BIGINT NOT NULL,
    [CreatedOn]  DATETIME       NOT NULL DEFAULT getdate(),
    [ModifiedBy] BIGINT NOT NULL,
    [ModifiedOn] DATETIME       NOT NULL DEFAULT getdate(),
    [IsActive]   BIT            NOT NULL DEFAULT 1,
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tblProductMaster_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [tblLookupMaster]([Id]),
    CONSTRAINT [FK_tblProductMaster_SubCategoryId] FOREIGN KEY ([SubCategoryId]) REFERENCES [tblSubLookupMaster]([Id]),
    CONSTRAINT [FK_tblProductMaster_CaptionTagId] FOREIGN KEY ([CaptionTagId]) REFERENCES [tblLookupMaster]([Id]),
    CONSTRAINT [FK_tblProductMaster_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [tblUserMaster]([UserId]),
    CONSTRAINT [FK_tblProductMaster_ModifiedBy] FOREIGN KEY ([ModifiedBy]) REFERENCES [tblUserMaster]([UserId])

);

