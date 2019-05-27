using ex3.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Mvc;
using System.Xml;

namespace ex3.Controllers
{
    public class MainController : Controller
    {
        ClientConnect client;

        private string ToXml(Data data)
        {
            //Initiate XML stuff
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlWriter writer = XmlWriter.Create(sb, settings);

            writer.WriteStartDocument();

            data.ToXml(writer);

            writer.WriteEndDocument();
            writer.Flush();
            return sb.ToString();
        }

        [HttpPost]
        public string GetPoint()
        {
            //client.start();
            //Data d = Data.getInstance();
            Data data = Data.getInstance();


            return ToXml(data);
        }

        [HttpGet]
        public ActionResult display(string ip , int port)
        {
            this.client = new ClientConnect();
            client.connect(ip, port);
            //client.start();

            Data d = Data.getInstance();
            Session["lat"] = d.M_lat;
            Session["lon"] = d.M_lon;

            return View();
        }

        public ActionResult display(string ip, int port, int rate)
        {
            this.client = new ClientConnect();
            client.connect(ip, port);
            //client.start();

            Data d = Data.getInstance();
            Session["lat"] = d.M_lat;
            Session["lon"] = d.M_lon;
            Session["rate"] = rate;

            return View();
        }

    }
}
