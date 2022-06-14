CREATE TABLE [dbo].[tblRoleType] (
    [RoleId]       INT            IDENTITY (1, 1) NOT NULL,
    [RoleName]     NVARCHAR (250) NOT NULL,
    [RoleLevel]    INT            NULL,
    [ParentRoleId] INT            NULL,
    [CreatedOn]    DATETIME       DEFAULT (getdate()) NULL,
    [CreatedBy]    BIGINT         NULL,
    [ModifiedOn]   DATETIME       DEFAULT (getdate()) NULL,
    [ModifiedBy]   BIGINT         NULL,
    [IsActive]     BIT            DEFAULT ((1)) NOT NULL,
    [IsDeleted]    BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([RoleId] ASC),
    CONSTRAINT [FK_tblRoleType_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [tblUserMaster]([UserId]),
    CONSTRAINT [FK_tblRoleType_ModifiedBy] FOREIGN KEY ([ModifiedBy]) REFERENCES [tblUserMaster]([UserId])
);

