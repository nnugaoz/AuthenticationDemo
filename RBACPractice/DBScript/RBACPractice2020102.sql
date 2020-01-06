USE [master]
GO
/****** Object:  Database [RBACPracticeDB]    Script Date: 2020/1/6 18:20:51 ******/
CREATE DATABASE [RBACPracticeDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RBACPracticeDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER_2014\MSSQL\DATA\RBACPracticeDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'RBACPracticeDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER_2014\MSSQL\DATA\RBACPracticeDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [RBACPracticeDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RBACPracticeDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RBACPracticeDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RBACPracticeDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RBACPracticeDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RBACPracticeDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RBACPracticeDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [RBACPracticeDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RBACPracticeDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RBACPracticeDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RBACPracticeDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RBACPracticeDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RBACPracticeDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RBACPracticeDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RBACPracticeDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RBACPracticeDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RBACPracticeDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RBACPracticeDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RBACPracticeDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RBACPracticeDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RBACPracticeDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RBACPracticeDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RBACPracticeDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RBACPracticeDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RBACPracticeDB] SET RECOVERY FULL 
GO
ALTER DATABASE [RBACPracticeDB] SET  MULTI_USER 
GO
ALTER DATABASE [RBACPracticeDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RBACPracticeDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RBACPracticeDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RBACPracticeDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [RBACPracticeDB] SET DELAYED_DURABILITY = DISABLED 
GO
USE [RBACPracticeDB]
GO
/****** Object:  Table [dbo].[T_Menu]    Script Date: 2020/1/6 18:20:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Menu](
	[ID] [varchar](50) NOT NULL,
	[Name] [varchar](50) NULL,
	[PID] [varchar](50) NULL,
	[Sort] [int] NULL,
	[Addr] [varchar](200) NULL,
	[EditMan] [varchar](50) NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_Role]    Script Date: 2020/1/6 18:20:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Role](
	[ID] [varchar](50) NOT NULL,
	[Name] [varchar](50) NULL,
	[EditMan] [varchar](50) NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_Role_Menu]    Script Date: 2020/1/6 18:20:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Role_Menu](
	[ID] [varchar](50) NOT NULL,
	[RID] [varchar](50) NULL,
	[MID] [varchar](50) NULL,
	[EditMan] [varchar](50) NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_Role_Menu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_User]    Script Date: 2020/1/6 18:20:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_User](
	[ID] [varchar](50) NOT NULL,
	[Name] [varchar](50) NULL,
	[EditMan] [varchar](50) NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_User_Role]    Script Date: 2020/1/6 18:20:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_User_Role](
	[ID] [varchar](50) NOT NULL,
	[UID] [varchar](50) NOT NULL,
	[RID] [varchar](50) NOT NULL,
	[EditMan] [varchar](50) NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_User_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[T_Menu] ([ID], [Name], [PID], [Sort], [Addr], [EditMan], [EditDate]) VALUES (N'040167b3-1701-432c-af6e-d65bc4500403', N'用户管理', N'', 1, N'/Html/T_User/T_UserList.html', N'Admin', CAST(N'2020-01-02 00:00:00.000' AS DateTime))
INSERT [dbo].[T_Menu] ([ID], [Name], [PID], [Sort], [Addr], [EditMan], [EditDate]) VALUES (N'22758274-22e1-4630-8c9f-45e9632e6cae', N'菜单管理', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[T_Menu] ([ID], [Name], [PID], [Sort], [Addr], [EditMan], [EditDate]) VALUES (N'5557dd22-95ac-4162-8f3d-b898892a23c9', N'角色管理', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[T_Menu] ([ID], [Name], [PID], [Sort], [Addr], [EditMan], [EditDate]) VALUES (N'6811ccd2-a68d-4166-86d7-9ae8450bf975', N'人员管理', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[T_Menu] ([ID], [Name], [PID], [Sort], [Addr], [EditMan], [EditDate]) VALUES (N'e140bbaa-22a3-4849-aed0-f2cb0167f9f6', N'角色管理', N'', 2, N'/Html/T_Role/T_RoleList.html', N'Admin', CAST(N'2020-01-02 00:00:00.000' AS DateTime))
INSERT [dbo].[T_Menu] ([ID], [Name], [PID], [Sort], [Addr], [EditMan], [EditDate]) VALUES (N'ee60c93b-0345-41c4-b637-1e1d46211cbd', N'菜单管理', N'', 3, N'/Html/T_Menu/T_MenuList.html', N'Admin', CAST(N'2020-01-02 00:00:00.000' AS DateTime))
INSERT [dbo].[T_Role] ([ID], [Name], [EditMan], [EditDate]) VALUES (N'75101a1e-1ce1-4c74-ba12-a24ad2f26010', N'系统管理员', N'Admin', CAST(N'2020-01-02 00:00:00.000' AS DateTime))
INSERT [dbo].[T_Role] ([ID], [Name], [EditMan], [EditDate]) VALUES (N'91b7a513-33b0-4a9a-bceb-7737897cbc10', N'过磅数据统计员', N'Admin', CAST(N'2020-01-02 00:00:00.000' AS DateTime))
INSERT [dbo].[T_Role] ([ID], [Name], [EditMan], [EditDate]) VALUES (N'b78e8e54-4633-4a05-ac3e-0b3c352adbb7', N'基础数据维护员', N'Admin', CAST(N'2020-01-02 00:00:00.000' AS DateTime))
INSERT [dbo].[T_User] ([ID], [Name], [EditMan], [EditDate]) VALUES (N'1153ec66-16e9-4e7e-a3de-fbe1c4d3938d', N'wubb', N'Admin', CAST(N'2020-01-06 14:58:19.190' AS DateTime))
INSERT [dbo].[T_User] ([ID], [Name], [EditMan], [EditDate]) VALUES (N'7395d5bb-fd1a-49a2-a12b-81ae61d60fb9', N'qinl', N'Admin', CAST(N'2020-01-06 16:06:30.653' AS DateTime))
INSERT [dbo].[T_User] ([ID], [Name], [EditMan], [EditDate]) VALUES (N'bb80c8fc-7756-4cda-8488-8a4ec42d481e', N'gaoz', N'Admin', CAST(N'2020-01-06 16:06:22.493' AS DateTime))
INSERT [dbo].[T_User_Role] ([ID], [UID], [RID], [EditMan], [EditDate]) VALUES (N'06c33564-5df1-45d4-836b-b3fd7ddbffb8', N'1153ec66-16e9-4e7e-a3de-fbe1c4d3938d', N'75101a1e-1ce1-4c74-ba12-a24ad2f26010', N'Admin', CAST(N'2020-01-06 14:58:19.193' AS DateTime))
INSERT [dbo].[T_User_Role] ([ID], [UID], [RID], [EditMan], [EditDate]) VALUES (N'2b359b4d-b290-4b7d-b8ea-ec2e3f6d3995', N'7395d5bb-fd1a-49a2-a12b-81ae61d60fb9', N'75101a1e-1ce1-4c74-ba12-a24ad2f26010', N'Admin', CAST(N'2020-01-06 16:06:30.653' AS DateTime))
INSERT [dbo].[T_User_Role] ([ID], [UID], [RID], [EditMan], [EditDate]) VALUES (N'6704f795-5205-4c0a-8285-913ff9d0ffcf', N'1153ec66-16e9-4e7e-a3de-fbe1c4d3938d', N'91b7a513-33b0-4a9a-bceb-7737897cbc10', N'Admin', CAST(N'2020-01-06 14:58:19.193' AS DateTime))
INSERT [dbo].[T_User_Role] ([ID], [UID], [RID], [EditMan], [EditDate]) VALUES (N'7af3b134-1814-4d09-8895-ac17dab451fa', N'7395d5bb-fd1a-49a2-a12b-81ae61d60fb9', N'91b7a513-33b0-4a9a-bceb-7737897cbc10', N'Admin', CAST(N'2020-01-06 16:06:30.653' AS DateTime))
INSERT [dbo].[T_User_Role] ([ID], [UID], [RID], [EditMan], [EditDate]) VALUES (N'8187839d-0efe-4c0f-8308-b24beb42f6e2', N'1153ec66-16e9-4e7e-a3de-fbe1c4d3938d', N'b78e8e54-4633-4a05-ac3e-0b3c352adbb7', N'Admin', CAST(N'2020-01-06 14:58:19.193' AS DateTime))
INSERT [dbo].[T_User_Role] ([ID], [UID], [RID], [EditMan], [EditDate]) VALUES (N'be4963ef-96e9-429d-a035-82fc5fed89f6', N'bb80c8fc-7756-4cda-8488-8a4ec42d481e', N'b78e8e54-4633-4a05-ac3e-0b3c352adbb7', N'Admin', CAST(N'2020-01-06 16:06:22.493' AS DateTime))
/****** Object:  StoredProcedure [dbo].[T_Menu_Page]    Script Date: 2020/1/6 18:20:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[T_Menu_Page] 
@Name VARCHAR(50) ,
@PID VARCHAR(50) ,
@Sort VARCHAR(50) ,
@Addr VARCHAR(50) ,
@BeginIndex INT
, @EndIndex INT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @RowCnt AS INT
SELECT @RowCnt = COUNT(*) FROM T_Menu WHERE 1=1 
AND Name LIKE '%'+@Name+'%'
AND PID LIKE '%'+@PID+'%'
AND Sort LIKE '%'+@Sort+'%'
AND Addr LIKE '%'+@Addr+'%'
SELECT * FROM(
SELECT
ROW_NUMBER()OVER(ORDER BY ID) RowIdx
, @RowCnt RowCnt
 , *
FROM T_Menu WHERE 1=1 
AND Name LIKE '%'+@Name+'%'
AND PID LIKE '%'+@PID+'%'
AND Sort LIKE '%'+@Sort+'%'
AND Addr LIKE '%'+@Addr+'%'
)T
WHERE RowIdx >= @BeginIndex AND RowIdx <= @EndIndex
END

GO
/****** Object:  StoredProcedure [dbo].[T_Role_Menu_Page]    Script Date: 2020/1/6 18:20:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[T_Role_Menu_Page] 
@RID VARCHAR(50) ,
@MID VARCHAR(50) ,
@BeginIndex INT
, @EndIndex INT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @RowCnt AS INT
SELECT @RowCnt = COUNT(*) FROM T_Role_Menu WHERE 1=1 
AND RID LIKE '%'+@RID+'%'
AND MID LIKE '%'+@MID+'%'
SELECT * FROM(
SELECT
ROW_NUMBER()OVER(ORDER BY ID) RowIdx
, @RowCnt RowCnt
 , *
FROM T_Role_Menu WHERE 1=1 
AND RID LIKE '%'+@RID+'%'
AND MID LIKE '%'+@MID+'%'
)T
WHERE RowIdx >= @BeginIndex AND RowIdx <= @EndIndex
END

GO
/****** Object:  StoredProcedure [dbo].[T_Role_Page]    Script Date: 2020/1/6 18:20:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[T_Role_Page] 
@Name VARCHAR(50) ,
@BeginIndex INT
, @EndIndex INT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @RowCnt AS INT
SELECT @RowCnt = COUNT(*) FROM T_Role WHERE 1=1 
AND Name LIKE '%'+@Name+'%'
SELECT * FROM(
SELECT
ROW_NUMBER()OVER(ORDER BY ID) RowIdx
, @RowCnt RowCnt
 , *
FROM T_Role WHERE 1=1 
AND Name LIKE '%'+@Name+'%'
)T
WHERE RowIdx >= @BeginIndex AND RowIdx <= @EndIndex
END

GO
/****** Object:  StoredProcedure [dbo].[T_User_Page]    Script Date: 2020/1/6 18:20:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[T_User_Page] @Name VARCHAR(50)
	,@BeginIndex INT
	,@EndIndex INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @TempUserRNames Table(
		UID VARCHAR(50),
		UName VARCHAR(50),
		RIDs VARCHAR(500),
		RNames VARCHAR(500),
		EditMan VARCHAR(50),
		EditDate Datetime	
	)

	INSERT INTO @TempUserRNames
	EXEC [dbo].[V_User] @UserID=N''

	DECLARE @RowCnt AS INT

	SELECT @RowCnt = COUNT(*)
	FROM T_User
	WHERE 1 = 1
		AND Name LIKE '%' + @Name + '%'

	SELECT *
	FROM (
		SELECT ROW_NUMBER() OVER (
				ORDER BY T_User.ID
				) RowIdx
			,@RowCnt RowCnt
			,T_User.ID
			,T_User.Name
			,T_User.EditMan
			,T_User.EditDate
			,TempUserRNames.RNames RoleNames
		FROM T_User
		LEFT JOIN @TempUserRNames AS TempUserRNames ON T_User.ID=TempUserRNames.UID
		WHERE 1 = 1
			AND T_User.Name LIKE '%' + @Name + '%'
		) T
	WHERE RowIdx >= @BeginIndex
		AND RowIdx <= @EndIndex
END

GO
/****** Object:  StoredProcedure [dbo].[T_User_Role_Page]    Script Date: 2020/1/6 18:20:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[T_User_Role_Page] 
@UID VARCHAR(50) ,
@RID VARCHAR(50) ,
@BeginIndex INT
, @EndIndex INT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @RowCnt AS INT
SELECT @RowCnt = COUNT(*) FROM T_User_Role WHERE 1=1 
AND UID LIKE '%'+@UID+'%'
AND RID LIKE '%'+@RID+'%'
SELECT * FROM(
SELECT
ROW_NUMBER()OVER(ORDER BY ID) RowIdx
, @RowCnt RowCnt
 , *
FROM T_User_Role WHERE 1=1 
AND UID LIKE '%'+@UID+'%'
AND RID LIKE '%'+@RID+'%'
)T
WHERE RowIdx >= @BeginIndex AND RowIdx <= @EndIndex
END

GO
/****** Object:  StoredProcedure [dbo].[V_User]    Script Date: 2020/1/6 18:20:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[V_User]
	@UserID VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @TempUserRNames Table(
		UID VARCHAR(50),
		UName VARCHAR(50),
		RIDs VARCHAR(500),
		RNames VARCHAR(500),
		EditMan VARCHAR(50),
		EditDate Datetime	
	)

	DECLARE @TempUserRName Table(
		Idx INT,
		UID VARCHAR(50),
		UName VARCHAR(50),
		RID VARCHAR(50),
		RName VARCHAR(50),
		EditMan VARCHAR(50),
		EditDate Datetime	
	)

	INSERT INTO @TempUserRName
	SELECT ROW_NUMBER()OVER(ORDER BY T_User.ID) Idx,T_User.ID,T_User.Name,T_Role.ID,T_Role.Name,T_User.EditMan,T_User.EditDate
	FROM T_User LEFT JOIN T_User_Role ON T_User.ID=T_User_Role.UID	
	LEFT JOIN T_Role ON T_User_Role.RID=T_Role.ID

	DECLARE @TempUserRName_Cnt INT
	DECLARE @TempUserRName_Idx INT
	DECLARE @TempUserRName_UID VARCHAR(50)
	DECLARE @TempUserRName_UName VARCHAR(50)
	DECLARE @TempUserRName_RID VARCHAR(50)
	DECLARE @TempUserRName_RName VARCHAR(50)
	DECLARE @TempUserRName_EditMan VARCHAR(50)
	DECLARE @TempUserRName_EditDate Datetime

	SELECT @TempUserRName_Cnt=COUNT(*) 
	FROM T_User LEFT JOIN T_User_Role ON T_User.ID=T_User_Role.UID	
	LEFT JOIN T_Role ON T_User_Role.RID=T_Role.ID

	SET @TempUserRName_Idx=1

	DECLARE @TempUserRNames_Cnt INT

	WHILE @TempUserRName_Idx<=@TempUserRName_Cnt
	BEGIN

		SELECT @TempUserRName_UID=UID
			,@TempUserRName_UName=UName
			,@TempUserRName_RID=RID
			,@TempUserRName_RName=RName
			,@TempUserRName_EditMan=EditMan
			,@TempUserRName_EditDate=EditDate
		 FROM @TempUserRName WHERE Idx=@TempUserRName_Idx

		 SET @TempUserRNames_Cnt=0
		 
		 SELECT @TempUserRNames_Cnt=COUNT(*) FROM @TempUserRNames WHERE UID=@TempUserRName_UID

		 IF @TempUserRNames_Cnt=0 
		 BEGIN
		 	INSERT INTO @TempUserRNames(UID,UName,RIDs,RNames,EditMan,EditDate)VALUES(@TempUserRName_UID,@TempUserRName_UName,@TempUserRName_RID,@TempUserRName_RName,@TempUserRName_EditMan,@TempUserRName_EditDate);
		 END
		 ELSE
		 BEGIN
			UPDATE @TempUserRNames SET RNames=RNames+','+@TempUserRName_RName, RIDs=RIDs + ',' + @TempUserRName_RID WHERE UID=@TempUserRName_UID
		 END
		 
		SET @TempUserRName_Idx=@TempUserRName_Idx+1
	END	

	SELECT * FROM @TempUserRNames WHERE UID LIKE '%' + @UserID +'%'
END

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Menu', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Menu', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父菜单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Menu', @level2type=N'COLUMN',@level2name=N'PID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Menu', @level2type=N'COLUMN',@level2name=N'Sort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Menu', @level2type=N'COLUMN',@level2name=N'Addr'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Menu', @level2type=N'COLUMN',@level2name=N'EditMan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Menu', @level2type=N'COLUMN',@level2name=N'EditDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Role', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Role', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Role', @level2type=N'COLUMN',@level2name=N'EditMan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Role', @level2type=N'COLUMN',@level2name=N'EditDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Role_Menu', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Role_Menu', @level2type=N'COLUMN',@level2name=N'RID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Role_Menu', @level2type=N'COLUMN',@level2name=N'MID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Role_Menu', @level2type=N'COLUMN',@level2name=N'EditMan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Role_Menu', @level2type=N'COLUMN',@level2name=N'EditDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User', @level2type=N'COLUMN',@level2name=N'EditMan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User', @level2type=N'COLUMN',@level2name=N'EditDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User_Role', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User_Role', @level2type=N'COLUMN',@level2name=N'UID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User_Role', @level2type=N'COLUMN',@level2name=N'RID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User_Role', @level2type=N'COLUMN',@level2name=N'EditMan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User_Role', @level2type=N'COLUMN',@level2name=N'EditDate'
GO
USE [master]
GO
ALTER DATABASE [RBACPracticeDB] SET  READ_WRITE 
GO
