CREATE TABLE [dbo].[User](
	[Id] [bigint] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [varchar](60) NOT NULL,
	[Email] [varchar](60) NOT NULL,
	[Telephone] [varchar](60)  NULL
);



INSERT INTO [dbo].[User]
           ([Name]
           ,[Email]
           ,[Telephone])
     VALUES
           ('Vincius Augusto Alves de Oliveira'
           ,'vinicius_augusto2@hotmail.com'
           ,'(19) 99262-4856');

INSERT INTO [dbo].[User]
           ([Name]
           ,[Email]
           ,[Telephone])
     VALUES
           ('Augusto Vincius Oliveira Alves'
           ,'augusto_vinicius2@hotmail.com'
           ,'(19) 3222-2222');


INSERT INTO [dbo].[User]
           ([Name]
           ,[Email]
           ,[Telephone])
     VALUES
           ('Alves Oliveira Augusto Vincius'
           ,'alves_oliveira2@hotmail.com'
           ,'(11) 99111-8888');