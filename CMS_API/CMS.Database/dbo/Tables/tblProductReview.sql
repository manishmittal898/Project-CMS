﻿CREATE TABLE [dbo].[tblProductReview] (
    [Id]               BIGINT          IDENTITY (1, 1) NOT NULL,
    [Title]            NVARCHAR (500)  NULL,
    [Description]      NVARCHAR (4000) NULL,
    [Rating]           INT             NULL,
    [ProductId]        BIGINT             NOT NULL,
    [FileId]           VARCHAR(50) NULL, 
    [CreatedOn]        DATETIME        DEFAULT (getdate()) NOT NULL,
    [CreatedBy]        BIGINT          NULL,
    [ModifiedOn]       DATETIME        NOT NULL DEFAULT getdate(),
    [ModifiedBy]       BIGINT          NULL,
    [IsActive]         BIT             DEFAULT ((1)) NOT NULL,
    [IsDeleted]        BIT             DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
  CONSTRAINT [tblProductReview_CreatedBy]  FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[tblUserMaster] ([UserId]),
  CONSTRAINT [tblProductReview_ProductId]  FOREIGN KEY ([ProductId]) REFERENCES [dbo].[tblProductMaster] ([Id])

);

