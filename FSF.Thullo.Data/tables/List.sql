﻿CREATE TABLE [dbo].[List]
(
  [Id] INT NOT NULL PRIMARY KEY IDENTITY,
  [Title] VARCHAR(100) NOT NULL,
  [BoardId] INT NOT NULL,
  [CreatedDate] DATETIME2 NOT NULL DEFAULT GetDate(),
  [CreatedBy] UNIQUEIDENTIFIER NOT NULL,

  CONSTRAINT FK_List_Board FOREIGN KEY (BoardId)
  REFERENCES dbo.Board (Id)
  ON DELETE CASCADE
)
