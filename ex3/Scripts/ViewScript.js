
var prevLat;
var prevLon;
var isFirstIter = true;
var i = 1;


function parseXml(xml) {
    var xmlDoc = $.parseXML(xml);
    $xml = $(xmlDoc);
}


function draw(ctx, rout, lat, lon) {

    ctx.clearRect(0, 0, window.innerWidth, window.innerHeight);
    //var lat = (Math.floor(Math.random() * 600));
    //var lon = (Math.floor(Math.random() * 500));


    lat = lat + i;
    lon = lon + i;
    createPoint(ctx, lat, lon);
    i += 10;
    //alert(lat);
    // alert(lon);

    createLine(rout, lat, lon);
    savePrev(lat, lon);


}
function savePrev(lat, lon) {
    prevLat = lat;
    prevLon = lon;
}

function createLine(rout, lat, lon) {
    if (!isFirstIter) {
        rout.beginPath();
        rout.moveTo(prevLat, prevLon);
        rout.lineTo(lat, lon);
        rout.strokeStyle = "red";
        rout.stroke();
        ctx.closePath();
    }
    isFirstIter = false;
}

function createPoint(ctx, lat, lon) {
    ctx.beginPath();
    ctx.arc(lat, lon, 8, 0, Math.PI * 2);
    ctx.fillStyle = "red";
    ctx.strokeStyle = "black";
    ctx.lineWidth = 3;
    ctx.fill();
    ctx.stroke();
    ctx.closePath();
}


//var t = session;
//var s = t["lat"];
//alert(s);

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
    alert("Inside SecondMisoin");
    myTimer = (function (ctx) {

        $.post(getPoint).done(function (xml) {
            
            parseXml(xml);



            var lon = (parseFloat($xml.find("lon").text() ) + 180) * (screen.height / 360);
            var lat = (parseFloat($xml.find("lat").text()) + 90) * (screen.width / 180);
            var rudder = parseFloat($xml.find("rudder").text());
            var throttle = parseFloat($xml.find("throttle").text());



            draw(ctx, rout, lat, lon);
            if (document.getElementById("isSaveNeeded").value == "true") {

                var data = lat + "," + lon + "," + rudder + "," + throttle + ",";
                //alert(data);
                $.post(savePoint, { data });

            }
            

        });

    });
    setInterval(function () { myTimer(ctx); }, 1000);

}