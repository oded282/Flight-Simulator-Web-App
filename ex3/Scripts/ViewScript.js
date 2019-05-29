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

var ctx = document.getElementById("point").getContext('2d');


ctx.canvas.width = window.innerWidth;
ctx.canvas.height = window.innerHeight;

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
            var lat = (Math.floor(Math.random() * 600));
            var lon = (Math.floor(Math.random() * 500));
            createPoint(ctx, lat, lon);
            //var lat = (parseFloat($xml.find("lat").text()) + 90) * (screen.height / 180);
            //var lon = (parseFloat($xml.find("lon").text()) + 180) * (screen.height / 360);

            // alert(lat);
            // alert(lon);

            /*
            ctx.beginPath();
            ctx.moveTo(lat, lon);
            ctx.lineTo(300, 150);
            ctx.stroke();
            ctx.closePath();
            */

        });
    });
    setInterval(function () { myTimer(ctx); }, 500);

}