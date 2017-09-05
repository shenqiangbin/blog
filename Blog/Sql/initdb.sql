/*文章*/
CREATE TABLE [Article] (
  [ArticleId] INTEGER PRIMARY KEY AUTOINCREMENT, 
  [Title] NVARCHAR(50) NOT NULL, 
  [Content] nvARCHAR(5000) NOT NULL, 
  [DisplayCreatedTime] DATETIME NOT NULL, 
  [CreatedTime] DATETIME NOT NULL, 
  [UpdateTime] DATETIME, 
  [Enable] INT NOT NULL, 
  CONSTRAINT [] PRIMARY KEY ([ArticleId]));

/*分类*/
CREATE TABLE [Category] (
  [CategoryId] INT NOT NULL, 
  [Name] NVARCHAR(50) NOT NULL,  
  [CreatedTime] DATETIME NOT NULL, 
  [UpdateTime] DATETIME, 
  [Enable] INT NOT NULL, 
  CONSTRAINT [] PRIMARY KEY ([CategoryId]));