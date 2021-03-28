﻿CREATE TABLE [dbo].[Card]
(
  [Id] INT NOT NULL PRIMARY KEY IDENTITY,
  [Title] VARCHAR(100) NOT NULL,
  [Description] VARCHAR(4000) NULL,
  [CoverImage] VARCHAR(4000) NOT NULL,
  [ListId] INT NOT NULL,
  [CreatedDate] DATETIME2 NOT NULL DEFAULT GetDate(),
  [CreatedBy] UNIQUEIDENTIFIER NOT NULL,

  CONSTRAINT FK_Card_List FOREIGN KEY (ListId)
  REFERENCES dbo.List (Id)
)
