CREATE TABLE [dbo].[tblUserCartList]
(

	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] BIGINT NOT NULL, 
    [ProductId] BIGINT NOT NULL, 
    [AddedOn]  DATETIME NOT NULL DEFAULT getdate(),
    CONSTRAINT [FK_tblUserCartList_UserId] FOREIGN KEY ([UserId]) REFERENCES tblUserMaster([UserId]) ,
    CONSTRAINT [FK_tblUserCartList_ProductId] FOREIGN KEY (ProductId) REFERENCES [tblProductMaster]([Id]), 

)