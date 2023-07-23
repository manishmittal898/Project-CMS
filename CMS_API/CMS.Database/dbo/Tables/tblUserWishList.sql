CREATE TABLE [dbo].[tblUserWishList]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] BIGINT NOT NULL, 
    [ProductId] BIGINT NOT NULL, 

    CONSTRAINT [FK_tblUserWishList_UserId] FOREIGN KEY ([UserId]) REFERENCES tblUserMaster([UserId]) ,
    CONSTRAINT [FK_tblUserWishList_ProductId] FOREIGN KEY (ProductId) REFERENCES [tblProductMaster]([Id]), 

)
