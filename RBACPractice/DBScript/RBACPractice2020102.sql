USE [master]
GO
/****** Object:  Database [RBACPracticeDB]    Script Date: 2020/1/15 18:09:04 ******/
CREATE DATABASE [RBACPracticeDB] ON  PRIMARY 
( NAME = N'RBACPracticeDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER_2014\MSSQL\DATA\RBACPracticeDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'RBACPracticeDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER_2014\MSSQL\DATA\RBACPracticeDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
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
USE [RBACPracticeDB]
GO
/****** Object:  UserDefinedFunction [dbo].[SF_GetPermissionSortNumberAvailable]    Script Date: 2020/1/15 18:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[SF_GetPermissionSortNumberAvailable]
(
	@PID VARCHAR(50)
)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @MaxSort INT

	-- Add the T-SQL statements to compute the return value here
	SELECT @MaxSort = MAX(Sort) FROM T_Permission WHERE PID=@PID

	IF @MaxSort IS NULL
	BEGIN
		SET @MaxSort=1
	END
	ELSE
	BEGIN
		SET @MaxSort=@MaxSort+1
	END

	-- Return the result of the function
	RETURN @MaxSort

END

GO
/****** Object:  Table [dbo].[T_Dictionary]    Script Date: 2020/1/15 18:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Dictionary](
	[ID] [varchar](50) NOT NULL,
	[DKey] [varchar](50) NULL,
	[DValue] [varchar](50) NULL,
	[DTID] [varchar](50) NULL,
	[EditMan] [varchar](50) NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_T_Dictionary] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_Dictionary_Type]    Script Date: 2020/1/15 18:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Dictionary_Type](
	[ID] [varchar](50) NOT NULL,
	[DTKey] [varchar](50) NULL,
	[DTValue] [varchar](50) NULL,
	[EditMan] [varchar](50) NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_T_Dictionary_Type] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_Permission]    Script Date: 2020/1/15 18:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Permission](
	[ID] [varchar](50) NOT NULL,
	[Name] [varchar](50) NULL,
	[PID] [varchar](50) NULL,
	[Sort] [int] NULL,
	[Addr] [varchar](200) NULL,
	[RIdentify] [varchar](50) NULL,
	[TypeDID] [varchar](50) NULL,
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
/****** Object:  Table [dbo].[T_Role]    Script Date: 2020/1/15 18:09:04 ******/
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
/****** Object:  Table [dbo].[T_Role_Permission]    Script Date: 2020/1/15 18:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Role_Permission](
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
/****** Object:  Table [dbo].[T_User]    Script Date: 2020/1/15 18:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_User](
	[ID] [varchar](50) NOT NULL,
	[Name] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[IsSysAdmin] [bit] NULL,
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
/****** Object:  Table [dbo].[T_User_Role]    Script Date: 2020/1/15 18:09:04 ******/
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
INSERT [dbo].[T_Dictionary] ([ID], [DKey], [DValue], [DTID], [EditMan], [EditDate]) VALUES (N'9fed91ca-bf0c-40b3-9fed-a15079d0c656', N'PermissionType_Button', N'按钮权限', N'b6eab833-6a95-4bdc-88e9-08e642aabf22', N'Admin', CAST(N'2020-01-14 22:50:25.873' AS DateTime))
INSERT [dbo].[T_Dictionary] ([ID], [DKey], [DValue], [DTID], [EditMan], [EditDate]) VALUES (N'bfd08440-b6a0-4078-b3ed-3aa950900429', N'PermissionType_Menu', N'菜单权限', N'b6eab833-6a95-4bdc-88e9-08e642aabf22', N'Admin', CAST(N'2020-01-14 22:49:55.020' AS DateTime))
INSERT [dbo].[T_Dictionary_Type] ([ID], [DTKey], [DTValue], [EditMan], [EditDate]) VALUES (N'b6eab833-6a95-4bdc-88e9-08e642aabf22', N'PermissionType', N'权限类型', N'Admin', CAST(N'2020-01-14 22:06:52.863' AS DateTime))
INSERT [dbo].[T_Permission] ([ID], [Name], [PID], [Sort], [Addr], [RIdentify], [TypeDID], [EditMan], [EditDate]) VALUES (N'040167b3-1701-432c-af6e-d65bc4500403', N'用户管理', N'', 1, N'/Html/T_User/T_UserList.html', N'T_User_List', NULL, N'Admin', CAST(N'2020-01-02 00:00:00.000' AS DateTime))
INSERT [dbo].[T_Permission] ([ID], [Name], [PID], [Sort], [Addr], [RIdentify], [TypeDID], [EditMan], [EditDate]) VALUES (N'0974e0ce-44a9-479d-a9d5-621a5b26d64b', N'删除', N'040167b3-1701-432c-af6e-d65bc4500403', 3, N'', N'', N'9fed91ca-bf0c-40b3-9fed-a15079d0c656', N'Admin', CAST(N'2020-01-15 16:19:33.643' AS DateTime))
INSERT [dbo].[T_Permission] ([ID], [Name], [PID], [Sort], [Addr], [RIdentify], [TypeDID], [EditMan], [EditDate]) VALUES (N'1f894f32-c055-4a5b-a5e1-c6f72b2f9931', N'新增', N'040167b3-1701-432c-af6e-d65bc4500403', 1, N'', N'', N'9fed91ca-bf0c-40b3-9fed-a15079d0c656', N'Admin', CAST(N'2020-01-15 16:18:57.537' AS DateTime))
INSERT [dbo].[T_Permission] ([ID], [Name], [PID], [Sort], [Addr], [RIdentify], [TypeDID], [EditMan], [EditDate]) VALUES (N'33d2c448-0426-42ea-9a33-4a93d87ae2ce', N'字典项管理', N'', 4, N'', N'', N'bfd08440-b6a0-4078-b3ed-3aa950900429', N'Admin', CAST(N'2020-01-15 16:07:17.297' AS DateTime))
INSERT [dbo].[T_Permission] ([ID], [Name], [PID], [Sort], [Addr], [RIdentify], [TypeDID], [EditMan], [EditDate]) VALUES (N'571e5795-de59-4cb2-a085-0b31e615fc6c', N'字典项管理', N'', 5, N'', N'', N'bfd08440-b6a0-4078-b3ed-3aa950900429', N'Admin', CAST(N'2020-01-15 16:07:35.303' AS DateTime))
INSERT [dbo].[T_Permission] ([ID], [Name], [PID], [Sort], [Addr], [RIdentify], [TypeDID], [EditMan], [EditDate]) VALUES (N'90ea674d-37e5-408c-88a6-0e4cdf2d79cd', N'修改', N'040167b3-1701-432c-af6e-d65bc4500403', 2, N'', N'', N'9fed91ca-bf0c-40b3-9fed-a15079d0c656', N'Admin', CAST(N'2020-01-15 16:19:20.883' AS DateTime))
INSERT [dbo].[T_Permission] ([ID], [Name], [PID], [Sort], [Addr], [RIdentify], [TypeDID], [EditMan], [EditDate]) VALUES (N'e140bbaa-22a3-4849-aed0-f2cb0167f9f6', N'角色管理', N'', 2, N'/Html/T_Role/T_RoleList.html', N'T_Role_List', NULL, N'Admin', CAST(N'2020-01-02 00:00:00.000' AS DateTime))
INSERT [dbo].[T_Permission] ([ID], [Name], [PID], [Sort], [Addr], [RIdentify], [TypeDID], [EditMan], [EditDate]) VALUES (N'ee60c93b-0345-41c4-b637-1e1d46211cbd', N'权限管理', N'', 3, N'/Html/T_Permission/T_PermissionList.html', N'T_Permission_List', NULL, N'Admin', CAST(N'2020-01-02 00:00:00.000' AS DateTime))
INSERT [dbo].[T_Role] ([ID], [Name], [EditMan], [EditDate]) VALUES (N'245d130e-1cb0-42b4-b7ca-3f9a075dbe34', N'用户管理员', N'Admin', CAST(N'2020-01-14 15:49:37.673' AS DateTime))
INSERT [dbo].[T_Role] ([ID], [Name], [EditMan], [EditDate]) VALUES (N'680b2777-d419-4abb-8de3-b7507b1b5f97', N'角色管理员', N'Admin', CAST(N'2020-01-14 15:49:19.197' AS DateTime))
INSERT [dbo].[T_Role] ([ID], [Name], [EditMan], [EditDate]) VALUES (N'767012AD-825B-43D8-A69B-2CB969E4D50F', N'系统管理员', N'Admin', CAST(N'2020-01-14 15:52:14.557' AS DateTime))
INSERT [dbo].[T_Role] ([ID], [Name], [EditMan], [EditDate]) VALUES (N'b05fa424-ae69-4b95-9f7c-3bef1f2c7be5', N'权限管理员', N'Admin', CAST(N'2020-01-12 21:52:14.557' AS DateTime))
INSERT [dbo].[T_Role_Permission] ([ID], [RID], [MID], [EditMan], [EditDate]) VALUES (N'2ea15513-3acd-4619-afac-f3cef667540b', N'245d130e-1cb0-42b4-b7ca-3f9a075dbe34', N'040167b3-1701-432c-af6e-d65bc4500403', N'Admin', CAST(N'2020-01-14 15:49:37.673' AS DateTime))
INSERT [dbo].[T_Role_Permission] ([ID], [RID], [MID], [EditMan], [EditDate]) VALUES (N'502AF6C5-3266-4C35-A931-156DF0514ED2', N'767012AD-825B-43D8-A69B-2CB969E4D50F', N'040167b3-1701-432c-af6e-d65bc4500403', N'Admin', CAST(N'2020-01-14 21:52:14.557' AS DateTime))
INSERT [dbo].[T_Role_Permission] ([ID], [RID], [MID], [EditMan], [EditDate]) VALUES (N'502AF6C5-3266-4C35-A931-156DF0514ED3', N'767012AD-825B-43D8-A69B-2CB969E4D50F', N'ee60c93b-0345-41c4-b637-1e1d46211cbd', N'Admin', CAST(N'2020-01-14 21:52:14.557' AS DateTime))
INSERT [dbo].[T_Role_Permission] ([ID], [RID], [MID], [EditMan], [EditDate]) VALUES (N'502AF6C5-3266-4C35-A931-156DF0514ED4', N'767012AD-825B-43D8-A69B-2CB969E4D50F', N'e140bbaa-22a3-4849-aed0-f2cb0167f9f6', N'Admin', CAST(N'2020-01-14 21:52:14.557' AS DateTime))
INSERT [dbo].[T_Role_Permission] ([ID], [RID], [MID], [EditMan], [EditDate]) VALUES (N'6ab0b0e3-a6c6-4c9f-a7b4-ffdcf75c0846', N'b05fa424-ae69-4b95-9f7c-3bef1f2c7be5', N'ee60c93b-0345-41c4-b637-1e1d46211cbd', N'Admin', CAST(N'2020-01-12 21:52:14.557' AS DateTime))
INSERT [dbo].[T_Role_Permission] ([ID], [RID], [MID], [EditMan], [EditDate]) VALUES (N'6f617bcc-17f5-4cd5-b697-b0bddbb11d9c', N'680b2777-d419-4abb-8de3-b7507b1b5f97', N'e140bbaa-22a3-4849-aed0-f2cb0167f9f6', N'Admin', CAST(N'2020-01-14 15:49:19.197' AS DateTime))
INSERT [dbo].[T_User] ([ID], [Name], [Password], [IsSysAdmin], [EditMan], [EditDate]) VALUES (N'0D6DD652-6C58-421C-853E-A4B0EA43D8D1', N'admin', N'1', 1, N'Admin', CAST(N'2020-01-14 15:31:06.043' AS DateTime))
INSERT [dbo].[T_User] ([ID], [Name], [Password], [IsSysAdmin], [EditMan], [EditDate]) VALUES (N'2ce6cba3-d529-4f68-a9f0-0f806c621c90', N'wubb', N'1', NULL, N'Admin', CAST(N'2020-01-14 15:50:17.937' AS DateTime))
INSERT [dbo].[T_User] ([ID], [Name], [Password], [IsSysAdmin], [EditMan], [EditDate]) VALUES (N'93940eba-a8ac-4516-bc5e-1801532c2110', N'gaoz', N'1', NULL, N'Admin', CAST(N'2020-01-12 22:03:06.043' AS DateTime))
INSERT [dbo].[T_User] ([ID], [Name], [Password], [IsSysAdmin], [EditMan], [EditDate]) VALUES (N'd95ce716-c769-4014-82e0-470ed843710d', N'qinl', N'1', NULL, N'Admin', CAST(N'2020-01-14 15:50:03.197' AS DateTime))
INSERT [dbo].[T_User_Role] ([ID], [UID], [RID], [EditMan], [EditDate]) VALUES (N'25B46115-7C06-49DB-A33D-F8DA43E171C7', N'0D6DD652-6C58-421C-853E-A4B0EA43D8D1', N'767012AD-825B-43D8-A69B-2CB969E4D50F', N'Admin', CAST(N'2020-01-14 22:03:06.047' AS DateTime))
INSERT [dbo].[T_User_Role] ([ID], [UID], [RID], [EditMan], [EditDate]) VALUES (N'94ead401-7a7b-4324-a434-499ba2054a45', N'd95ce716-c769-4014-82e0-470ed843710d', N'245d130e-1cb0-42b4-b7ca-3f9a075dbe34', N'Admin', CAST(N'2020-01-14 15:50:03.197' AS DateTime))
INSERT [dbo].[T_User_Role] ([ID], [UID], [RID], [EditMan], [EditDate]) VALUES (N'c42829dc-18aa-4163-92cd-630abb28182e', N'93940eba-a8ac-4516-bc5e-1801532c2110', N'b05fa424-ae69-4b95-9f7c-3bef1f2c7be5', N'Admin', CAST(N'2020-01-12 22:03:06.047' AS DateTime))
INSERT [dbo].[T_User_Role] ([ID], [UID], [RID], [EditMan], [EditDate]) VALUES (N'cb0d5519-9bc2-414c-b738-7e25196f409b', N'2ce6cba3-d529-4f68-a9f0-0f806c621c90', N'680b2777-d419-4abb-8de3-b7507b1b5f97', N'Admin', CAST(N'2020-01-14 15:50:17.937' AS DateTime))
/****** Object:  StoredProcedure [dbo].[T_Dictionary_Page]    Script Date: 2020/1/15 18:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[T_Dictionary_Page] 
@Name VARCHAR(50) ,
@DTID VARCHAR(50) ,
@BeginIndex INT
, @EndIndex INT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @RowCnt AS INT
SELECT @RowCnt = COUNT(*) FROM T_Dictionary WHERE 1=1 
AND Name LIKE '%'+@Name+'%'
AND DTID LIKE '%'+@DTID+'%'
SELECT * FROM(
SELECT
ROW_NUMBER()OVER(ORDER BY ID) RowIdx
, @RowCnt RowCnt
 , *
FROM T_Dictionary WHERE 1=1 
AND Name LIKE '%'+@Name+'%'
AND DTID LIKE '%'+@DTID+'%'
)T
WHERE RowIdx >= @BeginIndex AND RowIdx <= @EndIndex
END

GO
/****** Object:  StoredProcedure [dbo].[T_Dictionary_Type_Page]    Script Date: 2020/1/15 18:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[T_Dictionary_Type_Page] 
@Name VARCHAR(50) ,
@BeginIndex INT
, @EndIndex INT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @RowCnt AS INT
SELECT @RowCnt = COUNT(*) FROM T_Dictionary_Type WHERE 1=1 
AND Name LIKE '%'+@Name+'%'
SELECT * FROM(
SELECT
ROW_NUMBER()OVER(ORDER BY ID) RowIdx
, @RowCnt RowCnt
 , *
FROM T_Dictionary_Type WHERE 1=1 
AND Name LIKE '%'+@Name+'%'
)T
WHERE RowIdx >= @BeginIndex AND RowIdx <= @EndIndex
END

GO
/****** Object:  StoredProcedure [dbo].[T_Permission_Page]    Script Date: 2020/1/15 18:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[T_Permission_Page] 
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
SELECT @RowCnt = COUNT(*) FROM T_Permission WHERE 1=1 
AND Name LIKE '%'+@Name+'%'
AND PID LIKE '%'+@PID+'%'
AND Sort LIKE '%'+@Sort+'%'
AND Addr LIKE '%'+@Addr+'%'
SELECT * FROM(
SELECT
ROW_NUMBER()OVER(ORDER BY ID) RowIdx
, @RowCnt RowCnt
 , *
FROM T_Permission WHERE 1=1 
AND Name LIKE '%'+@Name+'%'
AND PID LIKE '%'+@PID+'%'
AND Sort LIKE '%'+@Sort+'%'
AND Addr LIKE '%'+@Addr+'%'
)T
WHERE RowIdx >= @BeginIndex AND RowIdx <= @EndIndex
END



GO
/****** Object:  StoredProcedure [dbo].[T_Role_Menu_Page]    Script Date: 2020/1/15 18:09:04 ******/
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
SELECT @RowCnt = COUNT(*) FROM T_Role_Permission WHERE 1=1 
AND RID LIKE '%'+@RID+'%'
AND MID LIKE '%'+@MID+'%'
SELECT * FROM(
SELECT
ROW_NUMBER()OVER(ORDER BY ID) RowIdx
, @RowCnt RowCnt
 , *
FROM T_Role_Permission WHERE 1=1 
AND RID LIKE '%'+@RID+'%'
AND MID LIKE '%'+@MID+'%'
)T
WHERE RowIdx >= @BeginIndex AND RowIdx <= @EndIndex
END



GO
/****** Object:  StoredProcedure [dbo].[T_Role_Page]    Script Date: 2020/1/15 18:09:04 ******/
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
/****** Object:  StoredProcedure [dbo].[T_User_Page]    Script Date: 2020/1/15 18:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[T_User_Page] @Name VARCHAR(50)
,@RoleID VARCHAR(50)
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
	FROM T_User LEFT JOIN @TempUserRNames AS TempUserRNames ON T_User.ID = TempUserRNames.UID
	WHERE 1 = 1
		AND T_User.Name LIKE '%' + @Name + '%'
		AND TempUserRNames.RIDs LIKE '%' + @RoleID +'%'

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
					AND TempUserRNames.RIDs LIKE '%' + @RoleID +'%'
		) T
	WHERE RowIdx >= @BeginIndex
		AND RowIdx <= @EndIndex
END



GO
/****** Object:  StoredProcedure [dbo].[T_User_Role_Page]    Script Date: 2020/1/15 18:09:04 ******/
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
/****** Object:  StoredProcedure [dbo].[V_Role]    Script Date: 2020/1/15 18:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[V_Role]
	-- Add the parameters for the stored procedure here
	@RID VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @TempRole TABLE(
	ID VARCHAR(50)
	,Name VARCHAR(50)
	,EditMan VARCHAR(50)
	,EditDate Datetime
	,MIDs VARCHAR(500)
	,MNames VARCHAR(500)
	);

	INSERT INTO @TempRole
	SELECT ID,Name,EditMan,EditDate,NULL,NULL
	FROM T_Role

	DECLARE @TempRoleMenu TABLE(
	Idx INT
	,ID VARCHAR(50)
	,MID VARCHAR(50)
	,MName VARCHAR(50)
	);
	INSERT INTO @TempRoleMenu
	SELECT 
	ROW_NUMBER()OVER(ORDER BY T_Role.ID) AS Idx
	,T_Role.ID
	,T_Permission.ID AS MID
	,T_Permission.Name AS MName
	FROM T_Role LEFT JOIN T_Role_Permission ON T_Role.ID=T_Role_Permission.RID
	LEFT JOIN T_Permission ON T_Role_Permission.MID = T_Permission.ID

	DECLARE @TempRoleMenu_Idx INT
	DECLARE @TempRoleMenu_ID VARCHAR(50)
	DECLARE @TempRoleMenu_MID VARCHAR(50)
	DECLARE @TempRoleMenu_MName VARCHAR(50)
	DECLARE @TempRoleMenu_Cnt INT

	SELECT @TempRoleMenu_Cnt=COUNT(*) 
	FROM T_Role LEFT JOIN T_Role_Permission ON T_Role.ID=T_Role_Permission.RID
	LEFT JOIN T_Permission ON T_Role_Permission.MID = T_Permission.ID

	SET @TempRoleMenu_Idx=1

	WHILE @TempRoleMenu_Idx<=@TempRoleMenu_Cnt
	BEGIN
		SELECT @TempRoleMenu_ID=ID
		,@TempRoleMenu_MID=MID
		,@TempRoleMenu_MName=MName
		FROM @TempRoleMenu
		WHERE Idx=@TempRoleMenu_Idx

		UPDATE @TempRole SET 
		MIDs=CASE WHEN MIDs IS NULL THEN '' ELSE MIDs+',' END + @TempRoleMenu_MID
		,MNames=CASE WHEN MNames IS NULL THEN '' ELSE MNames+',' END + @TempRoleMenu_MName
		WHERE ID=@TempRoleMenu_ID

		SET @TempRoleMenu_Idx=@TempRoleMenu_Idx+1
	END

	SELECT * FROM @TempRole WHERE ID LIKE '%' + @RID +'%'

END


GO
/****** Object:  StoredProcedure [dbo].[V_User]    Script Date: 2020/1/15 18:09:04 ******/
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Dictionary', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Dictionary', @level2type=N'COLUMN',@level2name=N'DValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典类型ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Dictionary', @level2type=N'COLUMN',@level2name=N'DTID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Dictionary', @level2type=N'COLUMN',@level2name=N'EditMan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Dictionary', @level2type=N'COLUMN',@level2name=N'EditDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Dictionary'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Dictionary_Type', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典类型名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Dictionary_Type', @level2type=N'COLUMN',@level2name=N'DTValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Dictionary_Type', @level2type=N'COLUMN',@level2name=N'EditMan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Dictionary_Type', @level2type=N'COLUMN',@level2name=N'EditDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典类型表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Dictionary_Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Permission', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Permission', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父菜单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Permission', @level2type=N'COLUMN',@level2name=N'PID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Permission', @level2type=N'COLUMN',@level2name=N'Sort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Permission', @level2type=N'COLUMN',@level2name=N'Addr'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限类型字典表ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Permission', @level2type=N'COLUMN',@level2name=N'TypeDID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Permission', @level2type=N'COLUMN',@level2name=N'EditMan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Permission', @level2type=N'COLUMN',@level2name=N'EditDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Role', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Role', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Role', @level2type=N'COLUMN',@level2name=N'EditMan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Role', @level2type=N'COLUMN',@level2name=N'EditDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Role_Permission', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Role_Permission', @level2type=N'COLUMN',@level2name=N'RID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Role_Permission', @level2type=N'COLUMN',@level2name=N'MID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Role_Permission', @level2type=N'COLUMN',@level2name=N'EditMan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Role_Permission', @level2type=N'COLUMN',@level2name=N'EditDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否系统管理员' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User', @level2type=N'COLUMN',@level2name=N'IsSysAdmin'
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
