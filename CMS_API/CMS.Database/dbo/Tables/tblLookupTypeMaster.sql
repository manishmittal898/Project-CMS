CREATE TABLE [dbo].[tblLookupTypeMaster] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (250) NULL,
    [IsActive]   BIT            NULL,
    [CreatedBy]  NVARCHAR (250) NULL,
    [CreatedOn]  DATETIME       NULL,
    [ModifiedBy] NVARCHAR (250) NULL,
    [ModifiedOn] DATETIME       NULL,
    CONSTRAINT [PK_tblLookupMaster] PRIMARY KEY CLUSTERED ([Id] ASC)
);

