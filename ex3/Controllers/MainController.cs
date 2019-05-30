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
        string fileName;
        double recordTime;
        Save saver;

        public MainController (){
            client = ClientConnect.getInstance();
            saver = Save.getInstance();

        }

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
            this.client.start();            
            Data data = Data.getInstance();



            return ToXml(data);
        }

        [HttpPost]
        public string LoadPoint(string fileName)
        {
            Data data = Data.getInstance();
            Load loader = Load.getInstance();
            if (!loader.M_isLoaded) {
                loader.loadFromFile(fileName);
            }
            loader.getNextPoint();
            return ToXml(data);
        }

        [HttpPost]
        public void SaveToFile()
        {
            saver.saveToFile(this.fileName);
        }

        [HttpPost]
        public void SavePoint(string data)
        {
            saver.addPoint(data);

        }
        
        [HttpGet]
        public ActionResult display(string ip, int port, int? rate)
        {

            if (!ip.Contains('.'))
            {
                Session["rate"] = rate;
                Session["fileName"] = ip;
                Session["isLoad"] = true;

                return View();

            }

            Session["isLoad"] = false;
            Session["isSaveNeeded"] = "false";


            Data d = Data.getInstance();
            client.connect(ip, port);

            if (String.IsNullOrEmpty(rate.ToString()))
            {
                client.start();
                rate = -1;
                Session["lat"] = d.M_lat;
                Session["lon"] = d.M_lon;
                Session["first mission"] = "true";
                return View();
            }

            Session["rate"] = rate;
            Session["first mission"] = "false";

            return View();
        }

        public ActionResult save(string ip, int port, int rate, int recordTime, string fileName)
        {
            Data d = Data.getInstance();
            client.connect(ip, port);
            this.recordTime = recordTime;
            this.fileName = fileName;

            Session["rate"] = rate;
            Session["fileName"] = fileName;
            Session["isSaveNeeded"] = "true";
            Session["first mission"] = "false";

            return View("display");
        }

        public ActionResult load(string fileName, int rate)
        {

            Session["rate"] = rate;
            Session["fileName"] = fileName;


            return View();
        }
    }
}
