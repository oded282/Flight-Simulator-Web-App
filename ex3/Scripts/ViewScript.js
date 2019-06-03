var prevLat;
var prevLon;
var isFirstIter = true;
var i = 1;
var recordTime = document.getElementById("recordTime").value;
var rate = document.getElementById("rate").value;
var isSave = "true";
var myVar;


function parseXml(xml) {
    var xmlDoc = $.parseXML(xml);
    $xml = $(xmlDoc);
}


function draw(ctx, rout, lat, lon) {

    ctx.clearRect(0, 0, window.innerWidth, window.innerHeight);


    lat = lat + i;
    lon = lon + i;
    createPoint(ctx, lat, lon);
    i += 10;


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
        rout.moveTo(prevLon, prevLat);
        rout.lineTo(lon,lat);
        rout.strokeStyle = "red";
        rout.stroke();
        ctx.closePath();
    }
    isFirstIter = false;
}

function createPoint(ctx, lat, lon) {
    
    ctx.beginPath();
    ctx.arc(lon, lat,  8, 0, Math.PI * 2);
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
    /*
    var lat = document.getElementById("lat").value;
    var lon = document.getElementById("lon").value;

    lat = (parseFloat(lat) + 90) * (canvacePoint.height / 180);
    lon = (parseFloat(lon) + 180) * (canvacePoint.width / 360);
    */
    
    $.post(getPoint).done(function (xml) {
        parseXml(xml);

        var lon = (parseFloat($xml.find("lon").text()) + 180) * (canvacePoint.width / 360);
        var lat = (parseFloat($xml.find("lat").text()) + 90) * (canvacePoint.height / 180);

        createPoint(ctx, lat, lon);
    });    
}

else {
    myTimer = (function (ctx) {


        $.post(getPoint).done(function (xml) {
            parseXml(xml);          
            if ($xml.find("done").text()) {
                alert("done");
                stopInterval();
            }       
            
            var isSaveNeeded = document.getElementById("isSaveNeeded").value;
            var lon = (parseFloat($xml.find("lon").text()) + 180) * (canvacePoint.width / 360);
            var lat = (parseFloat($xml.find("lat").text()) + 90) * (canvacePoint.height / 180);
            if (isSaveNeeded == "false") {
                recordTime = -1;
            }


            draw(ctx, rout, lat, lon);
            if (recordTime <= 0 && recordTime > -0.2) {
                alert("Saving..");
                isSave = "false";
                $.post(SaveToFile);
                recordTime -= 0.25;                
                
            }

            
            if (isSaveNeeded == "true" && isSave == "true") {
             
                $.post(SavePoint);
              
                recordTime -= 0.25;
                
            }           
                
        });
    });
    myVar = setInterval(function () { myTimer(ctx); }, 1000 / rate);

    function stopInterval() {
        clearInterval(myVar);

    }
}

