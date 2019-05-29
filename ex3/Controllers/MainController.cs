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
        Save saver;
        double recordTime;

        public MainController (){
            client = ClientConnect.getInstance();
            saver = new Save();
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
        public void savePoint(string data, string fileName)
        {
            if (recordTime > 0)
            {
                saver.addPoint(data);
                recordTime -= 0.25;
                return;
            }
            if (recordTime == 0)
            {
                saver.saveToFile(fileName);
            }
        }
        
        [HttpGet]
        public ActionResult display(string ip, int port, int? rate)
        {
            Data d = Data.getInstance();
            client.connect(ip, port);

            if (String.IsNullOrEmpty(rate.ToString()))
            {
                client.start();

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

            Session["rate"] = rate;
            Session["fileName"] = fileName;


            return View();
        }

        public ActionResult load(string fileName, int rate)
        {

            Session["rate"] = rate;
            Session["fileName"] = fileName;


            return View();
        }
    }
}
