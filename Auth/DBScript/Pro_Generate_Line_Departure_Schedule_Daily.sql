USE [RGPT]
GO
/****** Object:  StoredProcedure [dbo].[Pro_Generate_Line_Departure_Schedule_Daily]    Script Date: 2019/12/7 10:42:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	根据线路发车计划，生成第二天线路发车时刻表
-- =============================================
ALTER PROCEDURE [dbo].[Pro_Generate_Line_Departure_Schedule_Daily]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--第二天日期部分
	DECLARE @Tomorrow VARCHAR(50)
	--末班时间
	DECLARE @LDateTime DATETIME
	--发班时间
	DECLARE @DDateTime DATETIME

	--每条线路的发车计划
	DECLARE @ID VARCHAR(50)
	DECLARE @LID VARCHAR(50)
	DECLARE @FTime VARCHAR(50)
	DECLARE @LTime VARCHAR(50)
	DECLARE @Interval INT

	--发车计划中的车号
	DECLARE @CarID VARCHAR(50)

	--定义线路发车计划临时表
	DECLARE @Temp_Line_Departure_Plan TABLE(
		Idx INT
		,ID VARCHAR(50)
		,LID VARCHAR(50)
		,FTime VARCHAR(50)
		,LTime VARCHAR(50)
		,Interval INT
	)
	--遍历线路发车计划用下标
	DECLARE @Loopi INT

	--线路发车计划总数
	DECLARE @Counti INT

	--定义线路配车临时表
	DECLARE @Temp_Line_Car TABLE(
	Idx INT
	,ID VARCHAR(50)
	,LID VARCHAR(50)
	,CarID VARCHAR(50)
	,Seq INT)

	--用来遍历线路配车表的下标
	DECLARE @Loopj INT

	--线路配车总数
	DECLARE @Countj INT

	--查找发车计划插入临时表
	INSERT INTO @Temp_Line_Departure_Plan(
	Idx
	,ID
	,LID
	,Interval
	,FTime
	,LTime)
	SELECT ROW_NUMBER()OVER(ORDER BY ID) Idx
	,ID
	,LID
	,Interval
	,FTime
	,LTime
	FROM T_Line_Departure_Plan

	--设置线路发车计划总数及初始化遍历下标
	SELECT @Counti=COUNT(*) FROM @Temp_Line_Departure_Plan
	SET @Loopi=1

	--获取第二天日期
	SET @Tomorrow=CONVERT(VARCHAR(50),DATEADD(DAY,1,GETDATE()),23)

	WHILE @Loopi<=@Counti
	BEGIN 
		--取一条线路发车计划
		SELECT 	@ID=ID,@LID=LID, @FTime=FTime, @LTime=LTime,@Interval=Interval FROM @Temp_Line_Departure_Plan WHERE Idx=@Loopi
		
		--取出该线路的配车信息，插入临时表
		INSERT INTO @Temp_Line_Car(Idx,ID,LID,CarID,Seq)
		SELECT ROW_NUMBER()OVER(ORDER BY Seq) Idx,ID,LID,CarID,Seq FROM T_Line_Car WHERE LID=@LID ORDER BY Seq

		SELECT @Countj=COUNT(*) FROM @Temp_Line_Car
		SET @Loopj=1
		
		--根据线路发车计划（首末班时间+发车间隔），生成线路发车时刻表
		--发车时间，初始化为首班车时间
		SET @DDateTime=CONVERT(DATETIME,@Tomorrow+' ' +@FTime,120)

		--末班车时间
		SET @LDateTime=CONVERT(DATETIME,@Tomorrow+' ' +@LTime,120)

		--只要发车时间小于末班车时间
		WHILE @DDateTime<@LDateTime
		BEGIN
			
			--从配车表中，按顺序找一辆车
			SELECT @CarID=CarID FROM @Temp_Line_Car WHERE Idx=@Loopj
			SET @Loopj=@Loopj+1
			IF @Loopj>@Countj
			BEGIN
				SET @Loopj=1
			END

			--新增一条发车时刻表
			INSERT INTO T_Line_Departure_Schedule(ID,LID,CarID,DDate,DTime)
			VALUES(NEWID(),@LID,@CarID,@Tomorrow,CONVERT(VARCHAR(5),@DDateTime,8))

			--指向下一个发车时间
			SET @DDateTime=DATEADD(MINUTE,@Interval,@DDateTime)
			
		END

		--从配车表中，按顺序找一辆车
		SELECT @CarID=CarID FROM @Temp_Line_Car WHERE Idx=@Loopj
		SET @Loopj=@Loopj+1
		IF @Loopj>@Countj
		BEGIN
			SET @Loopj=1
		END

		--插入末班车时刻表
		INSERT INTO T_Line_Departure_Schedule(ID,LID,CarID,DDate,DTime)
		VALUES(NEWID(),@LID,@CarID,@Tomorrow,CONVERT(VARCHAR(5),@LDateTime,8))
		
		SET @Loopi=@Loopi+1
	END
END
