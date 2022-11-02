CREATE TABLE [dbo].[tblFileDataMaster]
(
	[Id] BIGINT NOT NULL   IDENTITY(1,1), 
    [DataId] VARCHAR(50) NOT NULL, 
    [Value] nvarchar(Max) NULL,
    [CreatedOn]  DATETIME       DEFAULT (getdate()) NOT NULL,
    [CreatedBy]  BIGINT         NULL,
    [ModifiedOn] DATETIME       NOT NULL DEFAULT GETDATE(),
    [ModifiedBy] BIGINT         NOT NULL ,
    
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tblFileDataMaster_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [tblUserMaster]([UserId]),
    CONSTRAINT [FK_tblFileDataMaster_ModifiedBy] FOREIGN KEY ([ModifiedBy]) REFERENCES [tblUserMaster]([UserId])
)
