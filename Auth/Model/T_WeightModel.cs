public class T_WeightModel{
//主键
//MaxLength=50
public string ID{get;set;}
//车号
//MaxLength=50
public string CarNO{get;set;}
//品名
//MaxLength=50
public string ProductName{get;set;}
//毛重
//MaxLength=9
public decimal Gross{get;set;}
//皮重
//MaxLength=9
public decimal Tare{get;set;}
//净重
//MaxLength=9
public decimal Net{get;set;}
//毛重时间
//MaxLength=50
public string GrossTime{get;set;}
//皮重时间
//MaxLength=50
public string TareTime{get;set;}
//净重时间
//MaxLength=50
public string NetTime{get;set;}
//编辑人
//MaxLength=50
public string EditMan{get;set;}
//编辑时间
//MaxLength=50
public string EditDate{get;set;}
}
