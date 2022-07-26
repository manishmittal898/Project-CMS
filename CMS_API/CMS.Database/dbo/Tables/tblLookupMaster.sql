CREATE TABLE [dbo].[tblLookupMaster] (
    [Id]          BIGINT  IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (500) NULL,
    [ImagePath]        NVARCHAR (1000) NULL,
    [SortedOrder] INT            NULL,
    [LookUp_Type] BIGINT            NULL,
    [CreatedBy]  BIGINT NOT NULL,
    [CreatedOn]  DATETIME       NOT NULL DEFAULT getdate(),
    [ModifiedBy] BIGINT NOT NULL,
    [ModifiedOn] DATETIME       NOT NULL DEFAULT getdate(),
    [IsActive]   BIT            NOT NULL DEFAULT 1,
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_tblLookupMasters] PRIMARY KEY CLUSTERED ([Id] ASC),
   CONSTRAINT [FK_tblLookupMaster_LookUp_Type] FOREIGN KEY ([LookUp_Type]) REFERENCES [tblLookupTypeMaster]([Id]),
   CONSTRAINT [FK_tblLookupMaster_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [tblUserMaster]([UserId]),
    CONSTRAINT [FK_tblLookupMaster_ModifiedBy] FOREIGN KEY ([ModifiedBy]) REFERENCES [tblUserMaster]([UserId])
);

