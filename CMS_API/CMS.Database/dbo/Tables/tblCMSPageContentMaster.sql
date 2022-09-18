CREATE TABLE [dbo].[tblCMSPageContentMaster]
(
	[Id] BIGINT NOT NULL  identity(1,1),
    [PageId]    BIGINT            NOT NULL,
    [Heading] NTEXT,
    [Content] NTEXT ,
    [SortedOrder] INT            NULL,
	[CreatedOn]   DATETIME       DEFAULT (getdate()) NOT NULL,
    [CreatedBy]   BIGINT         NULL,
    [ModifiedOn]  DATETIME       NOT NULL DEFAULT (GETDATE()),
    [ModifiedBy]  BIGINT         NULL,
    [IsActive]    BIT            DEFAULT ((1)) NOT NULL,
    [IsDeleted]   BIT            DEFAULT ((0)) NOT NULL,
      PRIMARY KEY CLUSTERED ([Id] ASC),
   CONSTRAINT [tblCMSPageContentMaster_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [tblUserMaster]([UserId]),
    CONSTRAINT [tblCMSPageContentMaster_ModifiedBy] FOREIGN KEY ([ModifiedBy]) REFERENCES [tblUserMaster]([UserId]),
    CONSTRAINT [tblCMSPageContentMaster_PageId] FOREIGN KEY ([PageId]) REFERENCES [dbo].[tblLookupMaster] ([Id]),
)
