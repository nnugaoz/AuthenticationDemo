USE [Auth]
GO

/****** Object:  Table [dbo].[T_Weight]    Script Date: 2019/11/13 17:14:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[T_Weight](
	[ID] [varchar](50) NOT NULL,
	[CarNO] [varchar](50) NULL,
	[Gross] [decimal](18, 6) NULL,
	[Tare] [decimal](18, 6) NULL,
	[Net] [decimal](18, 6) NULL,
	[GrossTime] [varchar](50) NULL,
	[TareTime] [varchar](50) NULL,
	[NetTime] [varchar](50) NULL,
	[EditMan] [varchar](50) NULL,
	[EditDate] [varchar](50) NULL,
 CONSTRAINT [PK_T_Weight] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Weight', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Weight', @level2type=N'COLUMN',@level2name=N'CarNO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ë��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Weight', @level2type=N'COLUMN',@level2name=N'Gross'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ƥ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Weight', @level2type=N'COLUMN',@level2name=N'Tare'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Weight', @level2type=N'COLUMN',@level2name=N'Net'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ë��ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Weight', @level2type=N'COLUMN',@level2name=N'GrossTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ƥ��ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Weight', @level2type=N'COLUMN',@level2name=N'TareTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Weight', @level2type=N'COLUMN',@level2name=N'NetTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�༭��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Weight', @level2type=N'COLUMN',@level2name=N'EditMan'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�༭ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Weight', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���ؼ�¼��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Weight'
GO


