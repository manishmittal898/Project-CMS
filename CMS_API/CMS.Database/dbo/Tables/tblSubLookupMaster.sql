CREATE TABLE [dbo].[tblSubLookupMaster] (
    [Id]          BIGINT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (500) NULL,
    [ImagePath]        NVARCHAR (1000) NULL,
    [SortedOrder] INT            NULL,
    [LookUpId]    BIGINT            NOT NULL,
    [CreatedOn]   DATETIME       DEFAULT (getdate()) NOT NULL,
    [CreatedBy]   BIGINT         NULL,
    [ModifiedOn]  DATETIME       NOT NULL DEFAULT (GETDATE()),
    [ModifiedBy]  BIGINT         NULL,
    [IsActive]    BIT            DEFAULT ((1)) NOT NULL,
    [IsDeleted]   BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
   CONSTRAINT [tblSubLookupMaster_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [tblUserMaster]([UserId]),
    CONSTRAINT [tblSubLookupMaster_ModifiedBy] FOREIGN KEY ([ModifiedBy]) REFERENCES [tblUserMaster]([UserId]),
    CONSTRAINT [tblSubLookupMaster_LookUpId] FOREIGN KEY ([LookUpId]) REFERENCES [dbo].[tblSubLookupTypeMaster] ([Id]),

);

