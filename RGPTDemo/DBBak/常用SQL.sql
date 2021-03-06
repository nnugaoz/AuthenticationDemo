--发车时刻表
SELECT 
T1.DDate
,T1.DTime
,T2.Name
,T2.Tag
,T3.CarNO
 FROM T_Line_Departure_Schedule T1
 LEFT JOIN T_Line T2 ON T1.LID=T2.ID
 LEFT JOIN T_Car T3 ON T1.CarID=T3.ID
WHERE T1.DDate='2020-01-24'
ORDER BY Name,T1.DTime 

--查询车辆最新位置
EXEC GetCarLatestPosition 'CD288980-3966-4855-B877-296861425AC6'

--查询线路排车
SELECT T2.Name
,T2.Tag
,T3.CarNO
,T1.Seq
  FROM T_Line_Car T1 LEFT JOIN T_Line T2 ON T1.LID=T2.ID
  LEFT JOIN T_Car T3 ON T1.CarID=T3.ID
  ORDER BY T2.Name,T2.Tag,T1.Seq

  --删除发车时刻表
  --delete from [RGPT].[dbo].[T_Line_Departure_Schedule] WHERE DDate='2020-01-21'

  --更新发车时刻表
  --update [RGPT].[dbo].[T_Line_Departure_Schedule] set DDate='2020-01-21'  WHERE DDate='2020-01-22'

  --删除车辆运行轨迹
  --delete from [dbo].[T_CarTrack]


  --select * 
  --from [dbo].[T_CarTrack]
  --order by CarID,CollectTime
  
  --select * from T_TrackData where line='CD288980-3966-4855-B877-296861425AC6'
  --and lat='32.028529335853555'



