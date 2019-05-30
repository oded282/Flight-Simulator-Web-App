
var prevLat;
var prevLon;
var isFirstIter = true;
var i = 1;

var createPoint = function (ctx, lat, lon) {
    ctx.beginPath();
    ctx.arc(lat, lon, 8, 0, Math.PI * 2);
    ctx.fillStyle = "red";
    ctx.strokeStyle = "black";
    ctx.lineWidth = 3;
    ctx.fill();
    ctx.stroke();
    ctx.closePath();
}

var canvacePoint = document.getElementById("point");
var canvasRout = document.getElementById("rout");


canvacePoint.width = window.innerWidth;
canvacePoint.height = window.innerHeight;
canvacePoint.style.position = "absolute";

canvasRout.width = window.innerWidth;
canvasRout.height = window.innerHeight;
canvasRout.style.position = "absolute";

var ctx = canvacePoint.getContext("2d");
var rout = canvasRout.getContext("2d");



var isFirstMission = document.getElementById("first").value;
if ("true" == isFirstMission) {


    var lat = document.getElementById("lat").value;
    var lon = document.getElementById("lat").value;

    lat = (parseFloat(lat) + 90) * (screen.height / 180);
    lon = (parseFloat(lon) + 180) * (screen.height / 360);

    createPoint(ctx, lat, lon);

}
else {

    myTimer = (function (ctx) {

        $.post(postUrl).done(function (xml) {

            var xmlDoc = $.parseXML(xml);
            $xml = $(xmlDoc);
            ctx.clearRect(0, 0, window.innerWidth, window.innerHeight);
            //var lat = (Math.floor(Math.random() * 600));
            //var lon = (Math.floor(Math.random() * 500));
            
            var lon = (parseFloat($xml.find("lon").text() ) + 180) * (screen.height / 360);
            var lat = (parseFloat($xml.find("lat").text()) + 90) * (screen.width / 180);
            lat = lat + i;
            lon = lon + i;
            createPoint(ctx, lat, lon);
            i += 10;
             //alert(lat);
            // alert(lon);
            
            if (isFirstIter) {
                rout.beginPath();
                rout.moveTo(prevLat, prevLon);
                rout.lineTo(lat, lon);
                rout.strokeStyle = "red";
                // rout.lineWidth = 10;
                rout.stroke();
                ctx.closePath();
            }
            prevLat = lat;
            prevLon = lon;

        });
    });
    setInterval(function () { myTimer(ctx); }, 1000);

}