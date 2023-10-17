﻿CREATE TABLE [dbo].[tblProductMaster] (
    [Id]         BIGINT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (2000) NOT NULL,
    [ImagePath]        NVARCHAR (1000) NULL,
    [CategoryId]       BIGINT NOT NULL,
    [SubCategoryId]       BIGINT NULL,
    [Desc]       NTEXT NULL,
    [Price]      DECIMAL (18)   NULL,
    [SellingPrice]      DECIMAL (18)   NULL,
    [CaptionTagId]    BIGINT NULL,
    [ViewSectionId]    BIGINT NULL,
    [Discount] BIGINT NULL DEFAULT 0, 
    [OccasionId] BIGINT NULL, 
    [FabricId] BIGINT NULL,
    [LengthId] BIGINT NULL,
    [ColorId] BIGINT NULL,
    [PatternID] BIGINT NULL,
    [UniqueID] NVARCHAR(400) NULL,
    [Summary]    NTEXT NULL,
    [MetaTitle]        NVARCHAR (1000) NULL,
    [MetaDesc]        NVARCHAR (4000) NULL,
    [CreatedBy]  BIGINT NOT NULL,
    [CreatedOn]  DATETIME       NOT NULL DEFAULT getdate(),
    [ModifiedBy] BIGINT NOT NULL,
    [ModifiedOn] DATETIME       NOT NULL DEFAULT getdate(),
    [IsActive]   BIT            NOT NULL DEFAULT 1,
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [Keyword] NVARCHAR(4000) NULL, 
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tblProductMaster_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [tblLookupMaster]([Id]),
    CONSTRAINT [FK_tblProductMaster_SubCategoryId] FOREIGN KEY ([SubCategoryId]) REFERENCES [tblSubLookupMaster]([Id]),
    CONSTRAINT [FK_tblProductMaster_CaptionTagId] FOREIGN KEY ([CaptionTagId]) REFERENCES [tblLookupMaster]([Id]),
    CONSTRAINT [FK_tblProductMaster_ViewSectionId] FOREIGN KEY ([ViewSectionId]) REFERENCES [tblLookupMaster]([Id]),   
    CONSTRAINT [FK_tblProductMaster_OccasionId] FOREIGN KEY ([OccasionId]) REFERENCES [tblLookupMaster]([Id]),
    CONSTRAINT [FK_tblProductMaster_FabricId] FOREIGN KEY ([FabricId]) REFERENCES [tblLookupMaster]([Id]),
    CONSTRAINT [FK_tblProductMaster_LengthId] FOREIGN KEY ([LengthId]) REFERENCES [tblLookupMaster]([Id]),
    CONSTRAINT [FK_tblProductMaster_ColorId] FOREIGN KEY ([ColorId]) REFERENCES [tblLookupMaster]([Id]), 
    CONSTRAINT [FK_tblProductMaster_PatternId] FOREIGN KEY ([PatternId]) REFERENCES [tblLookupMaster]([Id]),    
    CONSTRAINT [FK_tblProductMaster_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [tblUserMaster]([UserId]),
    CONSTRAINT [FK_tblProductMaster_ModifiedBy] FOREIGN KEY ([ModifiedBy]) REFERENCES [tblUserMaster]([UserId])
);

