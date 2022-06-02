CREATE TABLE [dbo].[tblProductReview] (
    [Id]               BIGINT          IDENTITY (1, 1) NOT NULL,
    [Title]            NVARCHAR (500)  NULL,
    [ShortDescription] NVARCHAR (2000) NULL,
    [Description]      NVARCHAR (4000) NULL,
    [Rating]           INT             NULL,
    [ProductId]        INT             NOT NULL,
    [CreatedOn]        DATETIME        DEFAULT (getdate()) NULL,
    [CreatedBy]        BIGINT          NULL,
    [ModifiedOn]       DATETIME        NULL,
    [ModifiedBy]       BIGINT          NULL,
    [IsActive]         BIT             DEFAULT ((1)) NOT NULL,
    [IsDeleted]        BIT             DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[tblUserMaster] ([UserId]),
    FOREIGN KEY ([ProductId]) REFERENCES [dbo].[tblProductMaster] ([Id])
);

