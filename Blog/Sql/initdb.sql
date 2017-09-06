/*SqlLite脚本*/

/*文章*/
CREATE TABLE [Article] (
  [ArticleId] INTEGER PRIMARY KEY AUTOINCREMENT, 
  [Title] NVARCHAR(50) NOT NULL, 
  [Content] nvARCHAR(5000) NOT NULL,   
  [ContentLevel] int NOT NULL,   
  [PublishStatus] int NOT NULL,
  [DisplayCreatedTime] DATETIME NOT NULL, 
  [CreatedTime] DATETIME NOT NULL, 
  [UpdateTime] DATETIME, 
  [Enable] INT NOT NULL);

/*分类*/
CREATE TABLE [Category] (
  [CategoryId] INTEGER PRIMARY KEY AUTOINCREMENT, 
  [Name] NVARCHAR(50) NOT NULL,  
  [CreatedTime] DATETIME NOT NULL, 
  [UpdateTime] DATETIME, 
  [Enable] INT NOT NULL);