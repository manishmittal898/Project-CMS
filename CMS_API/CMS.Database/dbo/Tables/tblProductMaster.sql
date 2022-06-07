CREATE TABLE [dbo].[tblProductMaster] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (250) NULL,
    [Desc]       NVARCHAR (500) NULL,
    [Price]      DECIMAL (18)   NULL,
    [Caption]    NVARCHAR (250) NULL,
    [Summary]    NVARCHAR (500) NULL,
    [CreatedBy]  NVARCHAR (250) NULL,
    [CreatedOn]  DATETIME       NULL,
    [ModifiedBy] NVARCHAR (250) NULL,
    [ModifiedOn] DATETIME       NULL,
    [IsActive]   BIT            NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC)
);

