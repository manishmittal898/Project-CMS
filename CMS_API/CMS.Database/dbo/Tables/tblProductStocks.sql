CREATE TABLE [dbo].[tblProductStocks]
(
	[Id]  BIGINT  IDENTITY (1, 1) NOT NULL, 
    [ProductId] BIGINT NOT NULL,
    [SizeId] bigint NOT NULL, 
    [UnitPrice] DECIMAL NULL,
    [SellingPrice] DECIMAL NULL,
    [Discount] BIGINT NULL DEFAULT 0,
    [Quantity] INT NULL,
    CONSTRAINT [PK_tblProductStocks] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tblProductStocks_ProductId] FOREIGN KEY (ProductId) REFERENCES [tblProductMaster]([Id]), 
    CONSTRAINT [FK_tblProductStocks_SizeId] FOREIGN KEY ([SizeId]) REFERENCES [tblLookupMaster]([Id])
)
