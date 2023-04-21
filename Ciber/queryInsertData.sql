
GO
INSERT INTO [dbo].Customers(Name,Address) VALUES ('Dinh Huy','Thanh Xuan Bac'),('Hoang Minh','Bac Tu Liem')
INSERT INTO [dbo].Categories(Name,Description) VALUES ('Food','contain meat'),('Beverage','contant water')
INSERT INTO [dbo].Products(CategoryId,Name,Quantity,Description,Price) VALUES (1,'Do an sang',100,'bao gom nhieu thu',10000),(2,'Nuoc ngot',100,'chua 1000 calo',100000)

INSERT INTO [dbo].AspNetUsers
           ([Id]
           ,[UserName]
           ,[NormalizedUserName]
           ,[Email]
           ,[NormalizedEmail]
           ,[EmailConfirmed]
           ,[PasswordHash]
           ,[SecurityStamp]
           ,[ConcurrencyStamp]
           ,[PhoneNumber]
           ,[PhoneNumberConfirmed]
           ,[TwoFactorEnabled]
           ,[LockoutEnd]
           ,[LockoutEnabled]
           ,[AccessFailedCount])
     VALUES
           (1	,'hoangminhadmin',	NULL	,NULL,'HOANGMINH@GMAIL.COM',	1	,'123qwe',	'4EHC23GZ2RZMQUR7JRJVKJTA76KDDGJ4'	,'896b4756-f7b6-44ac-bb3b-e62960f39049',	NULL	,1	,0,	NULL,	1,	0)
           ,(2	,'hoangminhuser',	NULL	,NULL,'HOANGMINH@GMAIL.COM',	1	,'123qwe',	'4EHC23GZ2RZMQUR7J4JVKJTA76KDDGJ4'	,'896b4756-f7b6-44ac-bb3b-e62160f39049',	NULL	,1	,0,	NULL,	1,	0)
INSERT INTO [dbo].AspNetRoles
           ([Id]
           ,[Name]
           ,[NormalizedName]
           ,[ConcurrencyStamp])
     VALUES
           (1
           ,'AdminRole'
           ,NULL
           ,NULL), (2
           ,'UserRole'
           ,NULL
           ,NULL)
           
 INSERT INTO [dbo].AspNetUserRoles
           ([UserId]
           ,[RoleId])
     VALUES
           (1
           ,1),(2,2)
GO



