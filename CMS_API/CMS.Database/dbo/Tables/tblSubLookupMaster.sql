CREATE TABLE [dbo].[tblSubLookupMaster] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (250) NULL,
    [SortedOrder] INT            NULL,
    [LookUpId]    INT            NOT NULL,
    [CreatedOn]   DATETIME       DEFAULT (getdate()) NULL,
    [CreatedBy]   BIGINT         NULL,
    [ModifiedOn]  DATETIME       NULL,
    [ModifiedBy]  BIGINT         NULL,
    [IsActive]    BIT            DEFAULT ((1)) NOT NULL,
    [IsDeleted]   BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[tblUserMaster] ([UserId]),
    FOREIGN KEY ([LookUpId]) REFERENCES [dbo].[tblLookupMaster] ([Id])
);

