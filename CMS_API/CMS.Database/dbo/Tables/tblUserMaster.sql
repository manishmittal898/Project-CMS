CREATE TABLE [dbo].[tblUserMaster] (
    [UserId]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [Email]        NVARCHAR (500) NOT NULL,
    [FirstName]    NVARCHAR (256) NULL,
    [LastName]     NVARCHAR (256) NULL,
    [DOB]          DATETIME       NULL,
    [Address]      NVARCHAR (MAX) NULL,
    [Password]     NVARCHAR (250) NULL,
    [Mobile]       NVARCHAR (15)  NULL,
    [GenderId]     BIGINT         NULL,
    [RoleId]       INT            NOT NULL,
    [ProfilePhoto] NVARCHAR (1000) NULL,
    [CreatedOn]    DATETIME       DEFAULT (getdate()) NULL,
    [CreatedBy]    BIGINT         NULL,
    [ModifiedOn]   DATETIME       DEFAULT (getdate()) NULL,
    [ModifiedBy]   BIGINT         NULL,
    [IsActive]     BIT            DEFAULT ((1)) NOT NULL,
    [IsDeleted]    BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC),
    FOREIGN KEY ([RoleId]) REFERENCES [dbo].[tblRoleType] ([RoleId]),
    FOREIGN KEY ([GenderId]) REFERENCES [dbo].[tblLookupMaster] ([Id])

);

