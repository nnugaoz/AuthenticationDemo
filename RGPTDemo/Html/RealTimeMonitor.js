//实时监控类
function RealTimeMonitor(MapContainer) {
    var map = new BMap.Map(MapContainer);
    var point = new BMap.Point(120.885506, 32.03671);
    map.centerAndZoom(point, 15);
    map.enableScrollWheelZoom(true);

    this.MonitoredLineList = new Array();
    this.Map = map;
    setInterval(this.Refresh, 5000, { MonitorObj: this });
}

//开始监控线路
RealTimeMonitor.prototype.StartLineMonitoring = function (LineID) {

    //创建被监控线路实例
    var lMonitoredLine = new MonitoredLine();
    lMonitoredLine.LineID = LineID;
    lMonitoredLine.LinePolyLine = null;
    lMonitoredLine.StationMarkerList = new Array();
    lMonitoredLine.CarMarkerList = new Array();

    var that = this;
    //获取监控线路信息
    //包括：线路轨迹、站点信息、今日线路发车时刻表中所有车辆最新位置
    $.ajax({
        type: 'get'
         , url: '/Handler/Line.ashx?RequestMethod=GetLineInfo&LineID=' + LineID
         , success: function (pData) {
             var lData = JSON.parse(pData);

             //解析线路轨迹
             var lLineTracksPointArr = new Array();
             for (var i = 0; i < lData.Data[0].length; i++) {
                 lLineTracksPointArr.push(new BMap.Point(lData.Data[0][i].Lng, lData.Data[0][i].Lat));
             }

             //绘制线路轨迹
             var polyline = new BMap.Polyline(lLineTracksPointArr, { strokeColor: "blue", strokeWeight: 6, strokeOpacity: 0.5 });
             that.Map.addOverlay(polyline);

             //记录线路轨迹（用于取消监控时，在地图上删除轨迹）
             lMonitoredLine.LinePolyLine = polyline;

             //解析线路站点
             for (var i = 0; i < lData.Data[1].length; i++) {

                 //站点经纬度
                 var point = new BMap.Point(lData.Data[1][i].Lng, lData.Data[1][i].Lat);

                 //站点表示图标
                 var myIcon = new BMap.Icon("busstation.png", new BMap.Size(32, 32), {
                     anchor: new BMap.Size(16, 32)
                 });

                 //创建站点标识
                 var marker = new BMap.Marker(point, { icon: myIcon });

                 //在地图上添加站点标识
                 that.Map.addOverlay(marker);

                 //记录站点标识
                 lMonitoredLine.StationMarkerList.push(marker);
             }

             //解析目前线路上安排的车辆信息
             for (var i = 0; i < lData.Data[2].length; i++) {

                 //车辆经纬度
                 var point = new BMap.Point(lData.Data[2][i].Lng, lData.Data[2][i].Lat);

                 //车辆图标
                 var myIcon = new BMap.Icon("bus.png", new BMap.Size(32, 32), {
                     anchor: new BMap.Size(16, 32)
                 });

                 //创建车辆标识
                 var mBusMarker = new BMap.Marker(point, { icon: myIcon });

                 //在地图上添加车辆标识
                 that.Map.addOverlay(mBusMarker);

                 //记录车辆标识
                 lMonitoredLine.CarMarkerList.push(mBusMarker);
             }

             //将被监控线路加入监控线路数组
             that.MonitoredLineList.push(lMonitoredLine);

         }
    });
}

//开始监控车辆
RealTimeMonitor.prototype.StartCarMonitoring = function () {

}

//停止监控车辆
RealTimeMonitor.prototype.StopCarMonitoring = function () {

}

//停止监控线路
RealTimeMonitor.prototype.StopLineMonitoring = function (LineID) {
    for (var i = 0; i < this.MonitoredLineList.length; i++) {
        if (this.MonitoredLineList[i].LineID == LineID) {
            this.Map.removeOverlay(this.MonitoredLineList[i].LinePolyLine);
            for (var j = 0; j < this.MonitoredLineList[i].StationMarkerList.length; j++) {
                this.Map.removeOverlay(this.MonitoredLineList[i].StationMarkerList[j]);
            }
            for (var j = 0; j < this.MonitoredLineList[i].CarMarkerList.length; j++) {
                this.Map.removeOverlay(this.MonitoredLineList[i].CarMarkerList[j]);
            }
            this.MonitoredLineList.splice(i, 1);
            break;
        }
    }
}

//刷新线路上车辆的实时位置
RealTimeMonitor.prototype.Refresh = function (event) {
    
    var that = event.MonitorObj;

    for (var i = 0; i < that.MonitoredLineList.length; i++) {
        var lLineID = that.MonitoredLineList[i].LineID;
        $.ajax({
            type: 'get'
            , url: '/Handler/Line.ashx?RequestMethod=GetLatestPosition&LineID=' + lLineID
            , success: function (pData) {
                var lData = JSON.parse(pData);
                //刷新线路上的车辆信息，先删除，后添加
                for (var i = 0; i < that.MonitoredLineList.length; i++) {
                    if (that.MonitoredLineList[i].LineID == lLineID) {
                        for (var j = 0; j < that.MonitoredLineList[i].CarMarkerList.length; j++) {
                            that.Map.removeOverlay(that.MonitoredLineList[i].CarMarkerList[j]);
                        }
                        that.MonitoredLineList[i].CarMarkerList = new Array();

                        //解析目前线路上安排的车辆信息
                        for (var k = 0; k < lData.Data[0].length; k++) {

                            //车辆经纬度
                            var point = new BMap.Point(lData.Data[0][k].Lng, lData.Data[0][k].Lat);

                            //车辆图标
                            var myIcon = new BMap.Icon("bus.png", new BMap.Size(32, 32), {
                                anchor: new BMap.Size(16, 32)
                            });

                            //创建车辆标识
                            var mBusMarker = new BMap.Marker(point, { icon: myIcon });

                            //在地图上添加车辆标识
                            that.Map.addOverlay(mBusMarker);

                            //记录车辆标识
                            that.MonitoredLineList[i].CarMarkerList.push(mBusMarker);
                        }
                        break;
                    }
                }
            }
        });
    }
}

//被监控线路类
function MonitoredLine() {
    this.LineID = "";
    this.LinePolyLine = null;
    this.StationMarkerList = new Array();
    this.CarMarkerList = new Array();
}