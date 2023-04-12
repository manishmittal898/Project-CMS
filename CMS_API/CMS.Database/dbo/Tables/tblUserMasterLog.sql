CREATE TABLE [dbo].[tblUserMasterLog]
(
[Id]  UNIQUEIDENTIFIER DEFAULT NEWID(), 
[UserId] BIGINT NOT NULL,
[Token] varchar(1000)  NOT NULL,
SessionStartTime Datetime  NOT NULL  DEFAULT (getdate()),
SessionEndTime Datetime   NULL ,
    CONSTRAINT [FK_tblUserMasterLog_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[tblUserMaster] ([UserId]),
    CONSTRAINT [PK_tblUserMasterLog] PRIMARY KEY ([Id]), 

)
