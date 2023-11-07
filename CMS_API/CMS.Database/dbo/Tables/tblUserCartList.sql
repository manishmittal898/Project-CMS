CREATE TABLE [dbo].[tblUserCartList]
(

	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] BIGINT NOT NULL, 
    [ProductId] BIGINT NOT NULL, 
    [SizeId] BIGINT NOT NULL, 
    [Quantity] BIGINT NOT NULL, 
    [AddedOn]  DATETIME NOT NULL DEFAULT getdate(),
    IsCheckout BIT NOT NULL DEFAULT (1),
    CONSTRAINT [FK_tblUserCartList_UserId] FOREIGN KEY ([UserId]) REFERENCES tblUserMaster([UserId]) ,
    CONSTRAINT [FK_tblUserCartList_ProductId] FOREIGN KEY (ProductId) REFERENCES [tblProductMaster]([Id]), 
    CONSTRAINT [FK_tblUserCartList_SizeId] FOREIGN KEY (SizeId) REFERENCES [tblLookupMaster]([Id]), 

)