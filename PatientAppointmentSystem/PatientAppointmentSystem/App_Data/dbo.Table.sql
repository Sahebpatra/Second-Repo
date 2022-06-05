CREATE TABLE [dbo].[Users]
(
	[UserId] INT NOT NULL PRIMARY KEY identity, 
    [UserName] VARCHAR(50) NOT NULL unique, 
    [Password] VARCHAR(50) NULL
)
