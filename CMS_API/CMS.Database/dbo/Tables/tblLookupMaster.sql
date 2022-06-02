CREATE TABLE [dbo].[tblLookupMaster] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (250) NULL,
    [SortedOrder] INT            NULL,
    [LookUp_Type] INT            NULL,
    [CreatedBy]   NVARCHAR (250) NULL,
    [CreatedOn]   DATETIME       NULL,
    [ModifiedBy]  NVARCHAR (250) NULL,
    [ModifiedOn]  DATETIME       NULL,
    [IsActive]    BIT            NULL,
    CONSTRAINT [PK_tblLookupMasters] PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([LookUp_Type]) REFERENCES [dbo].[tblLookupTypeMaster] ([Id])
);

