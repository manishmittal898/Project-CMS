CREATE TABLE [dbo].[tblGeneralEntry]
(
	[Id] BIGINT NOT NULL IDENTITY(1,1),
    [Title] NVARCHAR(400) NULL, 
    [CategoryId] BIGINT NOT NULL,
    [Description] NTEXT NULL,
    [DataId] VARCHAR(50) NULL, 
    [ImagePath]        NVARCHAR (1000) NULL,
    [SortedOrder] INT            NULL,
	[CreatedOn]  DATETIME       DEFAULT (getdate()) NOT NULL,
    [CreatedBy]  BIGINT         NULL,
    [ModifiedOn] DATETIME       NOT NULL DEFAULT GETDATE(),
    [ModifiedBy] BIGINT         NOT NULL ,
    [IsActive]   BIT            DEFAULT ((1)) NOT NULL,
    [IsDeleted]  BIT            DEFAULT ((0)) NOT NULL,
    
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tblGeneralEntry_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [tblUserMaster]([UserId]),
    CONSTRAINT [FK_tblGeneralEntry_ModifiedBy] FOREIGN KEY ([ModifiedBy]) REFERENCES [tblUserMaster]([UserId]),
    CONSTRAINT [FK_tblGeneralEntry_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [TblGecategoryMater]([Id]),
   
    
)
