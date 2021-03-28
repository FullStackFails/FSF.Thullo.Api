﻿CREATE TABLE [dbo].[Comment]
(
  [Id] INT NOT NULL PRIMARY KEY IDENTITY,
  [Comment] VARCHAR(4000) NOT NULL,
  [CreatedBy] UNIQUEIDENTIFIER NOT NULL,
  [CreatedDate] DATETIME2 NOT NULL,
  [CardId] INT NOT NULL,

  CONSTRAINT FK_Comment_Card FOREIGN KEY (CardId)
  REFERENCES dbo.Card (Id)
  ON DELETE CASCADE
)
