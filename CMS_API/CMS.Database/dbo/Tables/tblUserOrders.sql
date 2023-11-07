CREATE TABLE [dbo].[tblUserOrders]
(
	
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    [UserId] BIGINT NOT NULL, 
    [StatusId] BIGINT NOT NULL, 
    [TransectionId] NVARCHAR(100) NOT NULL, 
    [Date] DATETIME NOT NULL DEFAULT  GETDATE(), 
    [PaymentModeId] BIGINT NOT NULL, 
    [AddressId] BIGINT NOT NULL, 
    [CouponId] BIGINT NULL, 
    [CouponDiscount] DECIMAL NULL, 
    [TotalAmount] DECIMAL NULL, 
    [DiscountAmount] DECIMAL NULL,
    CONSTRAINT [FK_tblUserOrders_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [tblStatusMaster]([Id]),    
    CONSTRAINT [FK_tblUserOrders_PaymentModeId] FOREIGN KEY ([PaymentModeId]) REFERENCES [tblLookupMaster]([Id]),
    CONSTRAINT [FK_tblUserOrders_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [tblUserAddressMaster]([Id]),
    CONSTRAINT [FK_tblUserOrders_CouponId] FOREIGN KEY ([CouponId]) REFERENCES [tblLookupMaster]([Id]),
    CONSTRAINT [FK_tblUserOrders_UserId]  FOREIGN KEY ([UserId]) REFERENCES [dbo].[tblUserMaster] ([UserId]),
    

)
