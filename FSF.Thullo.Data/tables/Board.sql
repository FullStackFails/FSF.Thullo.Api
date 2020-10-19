CREATE TABLE [dbo].[Board]
(
  [Id] INT NOT NULL PRIMARY KEY IDENTITY,
  [Title] VARCHAR(100) NOT NULL,
  [Description] VARCHAR(4000) NULL,
  [CoverPhoto] VARCHAR(4000) NOT NULL,
  [IsPrivate] BIT NOT NULL DEFAULT 0,
  [CreatedDate] DATETIME2 NOT NULL DEFAULT GetDate(),
  [CreatedBy] INT NULL, -- To correlate a userId in the future
  [ModifiedDate] DATETIME2 NOT NULL DEFAULT GetDate()
)
