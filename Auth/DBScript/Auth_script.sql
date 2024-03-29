USE [master]
GO
/****** Object:  Database [Auth]    Script Date: 2019/10/29 19:45:23 ******/
CREATE DATABASE [Auth] ON  PRIMARY 
( NAME = N'Auth', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER_2014\MSSQL\DATA\Auth.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Auth_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER_2014\MSSQL\DATA\Auth_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Auth].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Auth] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Auth] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Auth] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Auth] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Auth] SET ARITHABORT OFF 
GO
ALTER DATABASE [Auth] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Auth] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Auth] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Auth] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Auth] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Auth] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Auth] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Auth] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Auth] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Auth] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Auth] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Auth] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Auth] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Auth] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Auth] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Auth] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Auth] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Auth] SET RECOVERY FULL 
GO
ALTER DATABASE [Auth] SET  MULTI_USER 
GO
ALTER DATABASE [Auth] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Auth] SET DB_CHAINING OFF 
GO
USE [Auth]
GO
/****** Object:  Table [dbo].[T_Feature]    Script Date: 2019/10/29 19:45:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Feature](
	[ID] [varchar](50) NOT NULL,
	[FName] [varchar](50) NULL,
	[PID] [varchar](50) NULL,
	[Addr] [varchar](50) NULL,
	[Type] [varchar](1) NULL,
	[Sort] [int] NULL,
 CONSTRAINT [PK_T_Page] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_Role]    Script Date: 2019/10/29 19:45:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Role](
	[ID] [varchar](50) NOT NULL,
	[RName] [varchar](50) NOT NULL,
	[FIDS] [varchar](max) NULL,
 CONSTRAINT [PK_T_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_User]    Script Date: 2019/10/29 19:45:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_User](
	[ID] [varchar](50) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Pwd] [varchar](50) NULL,
	[RIDS] [varchar](max) NULL,
 CONSTRAINT [PK_T_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[T_Feature] ([ID], [FName], [PID], [Addr], [Type], [Sort]) VALUES (N'1A02243E-14F3-409D-92BE-B098054FD077', N'称重记录', N'', N'/Html/record.html', N'0', 6)
INSERT [dbo].[T_Feature] ([ID], [FName], [PID], [Addr], [Type], [Sort]) VALUES (N'1A9D1C05-4042-4749-9F64-DEAAB40CD0DA', N'统计报表', N'', N'/Html/report.html', N'0', 7)
INSERT [dbo].[T_Feature] ([ID], [FName], [PID], [Addr], [Type], [Sort]) VALUES (N'1B436164-474D-4915-BABA-6EE3A207427E', N'删除', N'3C8FB1EA-82C5-479E-845A-4162455B1C77', N'null', N'1', 3)
INSERT [dbo].[T_Feature] ([ID], [FName], [PID], [Addr], [Type], [Sort]) VALUES (N'2F16CF2E-5AC9-4F3A-99FB-CFDE6FC89A36', N'新增', N'39B25E93-F9CF-43CE-B935-07CA83ECB565', N'null', N'1', 1)
INSERT [dbo].[T_Feature] ([ID], [FName], [PID], [Addr], [Type], [Sort]) VALUES (N'39B25E93-F9CF-43CE-B935-07CA83ECB565', N'用户管理', N'', N'/Html/User/UserList.html', N'0', 8)
INSERT [dbo].[T_Feature] ([ID], [FName], [PID], [Addr], [Type], [Sort]) VALUES (N'3C8FB1EA-82C5-479E-845A-4162455B1C77', N'角色管理', N'', N'/Html/Role/roleList.html', N'0', 9)
INSERT [dbo].[T_Feature] ([ID], [FName], [PID], [Addr], [Type], [Sort]) VALUES (N'468A3020-79DB-4EFD-9526-7CD3A2E71D4E', N'新增', N'3C8FB1EA-82C5-479E-845A-4162455B1C77', N'null', N'1', 1)
INSERT [dbo].[T_Feature] ([ID], [FName], [PID], [Addr], [Type], [Sort]) VALUES (N'498D2532-CC78-4E8A-BD1B-4322AAF65766', N'编辑', N'39B25E93-F9CF-43CE-B935-07CA83ECB565', N'null', N'1', 2)
INSERT [dbo].[T_Feature] ([ID], [FName], [PID], [Addr], [Type], [Sort]) VALUES (N'703D9B2B-390E-4743-A2B7-398E4966C3A9', N'货品管理', N'', N'/Html/product.html', N'0', 3)
INSERT [dbo].[T_Feature] ([ID], [FName], [PID], [Addr], [Type], [Sort]) VALUES (N'7FB531FC-AECD-4FD3-9292-38EDBC8EBDB3', N'车队管理', N'', N'/Html/carteam.html', N'0', 2)
INSERT [dbo].[T_Feature] ([ID], [FName], [PID], [Addr], [Type], [Sort]) VALUES (N'87BBEF89-7F7C-4D9B-8572-B653E6E785A4', N'删除', N'39B25E93-F9CF-43CE-B935-07CA83ECB565', N'null', N'1', 3)
INSERT [dbo].[T_Feature] ([ID], [FName], [PID], [Addr], [Type], [Sort]) VALUES (N'8D0BC736-9E05-4124-88C3-D57DA90B4121', N'系统参数', N'', N'/Html/Settings.html', N'0', 11)
INSERT [dbo].[T_Feature] ([ID], [FName], [PID], [Addr], [Type], [Sort]) VALUES (N'AD473304-296A-43F4-A852-725E9DB3F57B', N'计划管理', N'', N'/Html/Plan/PlanList.html', N'0', 4)
INSERT [dbo].[T_Feature] ([ID], [FName], [PID], [Addr], [Type], [Sort]) VALUES (N'B1281B2F-17A2-403D-B95F-D899E325D241', N'车辆过磅', N'', N'/Html/weighting.html', N'0', 5)
INSERT [dbo].[T_Feature] ([ID], [FName], [PID], [Addr], [Type], [Sort]) VALUES (N'B703A039-29BD-4DBF-B3B4-BCF56F221120', N'编辑', N'3C8FB1EA-82C5-479E-845A-4162455B1C77', N'null', N'1', 2)
INSERT [dbo].[T_Feature] ([ID], [FName], [PID], [Addr], [Type], [Sort]) VALUES (N'BF16BDFD-69B8-4834-BCDF-17D3BC0C87BE', N'系统功能', N'', N'/Html/Feature/FeatureList.html', N'0', 10)
INSERT [dbo].[T_Feature] ([ID], [FName], [PID], [Addr], [Type], [Sort]) VALUES (N'DB206F2D-7352-4AC3-B251-9CE6B1FDCE10', N'车辆管理', N'', N'/Html/car.html', N'0', 1)
INSERT [dbo].[T_Role] ([ID], [RName], [FIDS]) VALUES (N'049d18ef-3cc8-4ec4-a4a0-05a04fc4d38f', N'管理员', N'DB206F2D-7352-4AC3-B251-9CE6B1FDCE10;7FB531FC-AECD-4FD3-9292-38EDBC8EBDB3;703D9B2B-390E-4743-A2B7-398E4966C3A9;AD473304-296A-43F4-A852-725E9DB3F57B;B1281B2F-17A2-403D-B95F-D899E325D241;1A02243E-14F3-409D-92BE-B098054FD077;1A9D1C05-4042-4749-9F64-DEAAB40CD0DA;39B25E93-F9CF-43CE-B935-07CA83ECB565;2F16CF2E-5AC9-4F3A-99FB-CFDE6FC89A36;498D2532-CC78-4E8A-BD1B-4322AAF65766;87BBEF89-7F7C-4D9B-8572-B653E6E785A4;3C8FB1EA-82C5-479E-845A-4162455B1C77;468A3020-79DB-4EFD-9526-7CD3A2E71D4E;B703A039-29BD-4DBF-B3B4-BCF56F221120;1B436164-474D-4915-BABA-6EE3A207427E;BF16BDFD-69B8-4834-BCDF-17D3BC0C87BE;')
INSERT [dbo].[T_Role] ([ID], [RName], [FIDS]) VALUES (N'5a4e9031-63d7-4aa3-8da4-b18b057e0575', N'生产部长', N'1A02243E-14F3-409D-92BE-B098054FD077;1A9D1C05-4042-4749-9F64-DEAAB40CD0DA;')
INSERT [dbo].[T_Role] ([ID], [RName], [FIDS]) VALUES (N'7f422562-5643-43ce-916f-1e4fe4f48a95', N'信息员', N'DB206F2D-7352-4AC3-B251-9CE6B1FDCE10;7FB531FC-AECD-4FD3-9292-38EDBC8EBDB3;703D9B2B-390E-4743-A2B7-398E4966C3A9;39B25E93-F9CF-43CE-B935-07CA83ECB565;2F16CF2E-5AC9-4F3A-99FB-CFDE6FC89A36;498D2532-CC78-4E8A-BD1B-4322AAF65766;87BBEF89-7F7C-4D9B-8572-B653E6E785A4;3C8FB1EA-82C5-479E-845A-4162455B1C77;468A3020-79DB-4EFD-9526-7CD3A2E71D4E;B703A039-29BD-4DBF-B3B4-BCF56F221120;1B436164-474D-4915-BABA-6EE3A207427E;')
INSERT [dbo].[T_Role] ([ID], [RName], [FIDS]) VALUES (N'96592a2b-e236-4353-83db-9b9c983e21bb', N'班组长', N'AD473304-296A-43F4-A852-725E9DB3F57B;B1281B2F-17A2-403D-B95F-D899E325D241;1A02243E-14F3-409D-92BE-B098054FD077;')
INSERT [dbo].[T_Role] ([ID], [RName], [FIDS]) VALUES (N'cdfe4d65-5244-478f-afd6-ce8ab2740894', N'司磅员', N'B1281B2F-17A2-403D-B95F-D899E325D241;')
INSERT [dbo].[T_User] ([ID], [UserName], [Pwd], [RIDS]) VALUES (N'1f986238-79d6-4108-bdca-a09ddf456ab6', N'gaoz', N'gaoz', N'cdfe4d65-5244-478f-afd6-ce8ab2740894;')
INSERT [dbo].[T_User] ([ID], [UserName], [Pwd], [RIDS]) VALUES (N'2c8f9585-47a7-4b64-ada9-349b2cfe4fa2', N'qinl', N'qinl', N'96592a2b-e236-4353-83db-9b9c983e21bb;cdfe4d65-5244-478f-afd6-ce8ab2740894;')
INSERT [dbo].[T_User] ([ID], [UserName], [Pwd], [RIDS]) VALUES (N'a2e714e5-3f04-4935-92ad-912838569ea0', N'admin', N'admin', N'049d18ef-3cc8-4ec4-a4a0-05a04fc4d38f;')
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Feature', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'功能名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Feature', @level2type=N'COLUMN',@level2name=N'FName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父级功能ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Feature', @level2type=N'COLUMN',@level2name=N'PID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'功能访问地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Feature', @level2type=N'COLUMN',@level2name=N'Addr'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'功能类型 0：页面功能；1：按钮功能' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Feature', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'展示顺序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Feature', @level2type=N'COLUMN',@level2name=N'Sort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统功能表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Feature'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Role', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Role', @level2type=N'COLUMN',@level2name=N'RName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限列表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Role', @level2type=N'COLUMN',@level2name=N'FIDS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统角色表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Role'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User', @level2type=N'COLUMN',@level2name=N'Pwd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色列表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User', @level2type=N'COLUMN',@level2name=N'RIDS'
GO
USE [master]
GO
ALTER DATABASE [Auth] SET  READ_WRITE 
GO
