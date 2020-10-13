CREATE TABLE [dbo].[List]
(
  [Id] INT NOT NULL PRIMARY KEY IDENTITY,
  [Title] VARCHAR(100) NOT NULL,
  [BoardId] INT NOT NULL,
  [CreatedDate] DATETIME2 NOT NULL DEFAULT GetDate(),
  [CreatedBy] INT NULL, -- To correlate a userId in the future
  [ModifiedDate] DATETIME2 NOT NULL DEFAULT GetDate(),

  CONSTRAINT FK_Board FOREIGN KEY (BoardId)
  REFERENCES dbo.Board (Id)
)
