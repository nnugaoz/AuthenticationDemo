//实时监控类
function RealTimeMonitor(MapContainer, CenterPointLng, CentrePointLat) {
    this.LineMarkerList = new Array();
    this.StationMarkerList = new Array();
    this.CarMarkerList = new Array();
    this.Map = new BMap.Map(MapContainer);

    var point = new BMap.Point(CenterPointLng, CentrePointLat);
    this.Map.centerAndZoom(point, 15);
    this.Map.enableScrollWheelZoom(true);
    setInterval(this.Refresh, 5000, { MonitorObj: this });
}

//开始监控线路
RealTimeMonitor.prototype.StartLineMonitoring = function (LineID) {

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

             //记录线路轨迹
             var lLineMarker = new LineMarker();
             lLineMarker.LineID = LineID;
             lLineMarker.LinePolyLine = polyline;
             that.LineMarkerList.push(lLineMarker);

             //解析线路站点
             for (var i = 0; i < lData.Data[1].length; i++) {

                 //判断站点是否已经在地图上标注
                 var j = 0;
                 for (; j < that.StationMarkerList.length; j++) {
                     if (that.StationMarkerList[j].StationID == lData.Data[1][i].SID) {
                         that.StationMarkerList[j].RLineList.push(LineID);
                         break;
                     }
                 }

                 if (j >= that.StationMarkerList.length) {

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

                     //创建站点名称标识
                     var label = new BMap.Label(lData.Data[1][i].Name, { position: point });

                     //在地图上添加站点名称标识
                     that.Map.addOverlay(label);

                     var lStationMarker = new StationMarker();
                     lStationMarker.StationIconMarker = marker;
                     lStationMarker.StationNameMarker = label;
                     lStationMarker.StationID = lData.Data[1][i].SID;
                     lStationMarker.RLineList = new Array();
                     lStationMarker.RLineList.push(LineID);

                     //记录站点标识
                     that.StationMarkerList.push(lStationMarker);
                 }
             }

             //解析目前线路上安排的车辆信息
             for (var i = 0; i < lData.Data[2].length; i++) {

                 //判断车辆是否已经在地图上标注
                 var j = 0;
                 for (; j < that.CarMarkerList.length; j++) {
                     if (that.CarMarkerList[j].CarID == lData.Data[2][i].CarID) {
                         that.CarMarkerList[j].RLineList.push(LineID);
                         break;
                     }
                 }

                 if (j >= that.CarMarkerList.length) {

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

                     //创建车号标识
                     var mCarNoLabel = new BMap.Label(lData.Data[2][i].CarNO, { position: point })
                     mCarNoLabel.setStyle({ border: 'none', backgroundColor: '#33a0fa' });

                     //在地图上添加车号标识
                     that.Map.addOverlay(mCarNoLabel);

                     var lCarMarker = new CarMarker();
                     lCarMarker.CarID = lData.Data[2][i].CarID;
                     lCarMarker.CarIconMarker = mBusMarker;
                     lCarMarker.CarNoMarker = mCarNoLabel;
                     lCarMarker.RLineList = new Array();
                     lCarMarker.RLineList.push(LineID);

                     //记录车辆标识
                     that.CarMarkerList.push(lCarMarker);
                 }
             }
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
    for (var i = 0; i < this.LineMarkerList.length; i++) {
        if (this.LineMarkerList[i].LineID == LineID) {
            this.Map.removeOverlay(this.LineMarkerList[i].LinePolyLine);
            this.LineMarkerList.splice(i, 1);
            break;
        }
    }
    for (var i = 0; i < this.StationMarkerList.length;) {
        for (var j = 0; j < this.StationMarkerList[i].RLineList.length; j++) {
            if (this.StationMarkerList[i].RLineList[j] == LineID) {
                this.StationMarkerList[i].RLineList.splice(j, 1);
                break;
            }
        }

        if (this.StationMarkerList[i].RLineList.length == 0) {
            this.Map.removeOverlay(this.StationMarkerList[i].StationIconMarker);
            this.Map.removeOverlay(this.StationMarkerList[i].StationNameMarker);
            this.StationMarkerList.splice(i, 1);
        } else {
            i++;
        }
    }

    for (var i = 0; i < this.CarMarkerList.length;) {
        for (var j = 0; j < this.CarMarkerList[i].RLineList.length; j++) {
            if (this.CarMarkerList[i].RLineList[j] == LineID) {
                this.CarMarkerList[i].RLineList.splice(j, 1);
                break;
            }
        }

        if (this.CarMarkerList[i].RLineList.length == 0) {
            this.Map.removeOverlay(this.CarMarkerList[i].CarIconMarker);
            this.Map.removeOverlay(this.CarMarkerList[i].CarNoMarker);
            this.CarMarkerList.splice(i, 1);
        } else {
            i++;
        }
    }
}

//刷新线路上车辆的实时位置
RealTimeMonitor.prototype.Refresh = function (event) {

    var that = event.MonitorObj;
    for (var i = 0; i < that.LineMarkerList.length; i++) {
        var lLineID = that.LineMarkerList[i].LineID;

        $.ajax({
            type: 'get'
            , url: '/Handler/Line.ashx?RequestMethod=GetLatestPosition&LineID=' + lLineID
            , success: function (pData) {
                var lData = JSON.parse(pData);

                //解析目前线路上安排的车辆信息
                for (var i = 0; i < lData.Data[0].length; i++) {
                    //车辆ID
                    var lCarID = lData.Data[0][i].CarID;
                    //车辆经纬度
                    var point = new BMap.Point(lData.Data[0][i].Lng, lData.Data[0][i].Lat);

                    var j = 0;
                    for (; j < that.CarMarkerList.length; j++) {

                        var lCarMarker = that.CarMarkerList[j];

                        if (lCarMarker.CarID == lCarID) {
                            //设置最新位置
                            lCarMarker.CarIconMarker.setPosition(point);
                            lCarMarker.CarNoMarker.setPosition(point);
                            break;
                        }
                    }

                    if (j >= that.CarMarkerList.length) {
                        //车辆图标
                        var myIcon = new BMap.Icon("bus.png", new BMap.Size(32, 32), {
                            anchor: new BMap.Size(16, 32)
                        });

                        //创建车辆标识
                        var mBusMarker = new BMap.Marker(point, { icon: myIcon });

                        //在地图上添加车辆标识
                        that.Map.addOverlay(mBusMarker);

                        //创建车号标识
                        var mCarNoLabel = new BMap.Label(lData.Data[0][i].CarNO, { position: point })
                        mCarNoLabel.setStyle({ border: 'none', backgroundColor: '#33a0fa' });

                        //在地图上添加车号标识
                        that.Map.addOverlay(mCarNoLabel);

                        var lCarMarker = new CarMarker();
                        lCarMarker.CarID = lCarID;
                        lCarMarker.CarIconMarker = mBusMarker;
                        lCarMarker.CarNoMarker = mCarNoLabel;
                        lCarMarker.RLineList = new Array();
                        lCarMarker.RLineList.push(lLineID);

                        //记录车辆标识
                        that.CarMarkerList.push(lCarMarker);
                    }
                }
            }
        });
    }
}

//线路类
function LineMarker() {
    this.LineID = "";
    this.LinePolyLine = null;
}

function CarMarker() {
    this.CarIconMarker = null;
    this.CarNoMarker = null;
    this.CarID = "";
    this.RLineList = null;
}

function StationMarker() {
    this.StationIconMarker = null;
    this.StationNameMarker = null;
    this.StationID = "";
    this.RLineList = null;
}
