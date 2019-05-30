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
            saver.SaveToFile();
        }

        [HttpPost]
        public void SavePoint()
        {
            string lat = Data.getInstance().M_lat;
            string lon = Data.getInstance().M_lon;
            string rudder = Data.getInstance().M_rudder;
            string throttle = Data.getInstance().M_throttle;
            string data = lat + "," + lon + "," + rudder + "," + throttle + ",";
            saver.addPoint(data);

        }
        
        [HttpGet]
        public ActionResult display(string ip, int port, int? rate)
        {
            Data d = Data.getInstance();
            client.connect(ip, port);

            Session["isSaveNeeded"] = "false";

            if (String.IsNullOrEmpty(rate.ToString()))
            {
                client.start();
                rate = -1;
                Session["lat"] = d.M_lat;
                Session["lon"] = d.M_lon;
                Session["first mission"] = "true";
                return View();
            }

            Session["first mission"] = "false";
            Session["rate"] = rate;


            if (!ip.Contains('.'))
            {
                Session["fileName"] = ip;
                Session["isLoad"] = true;

                return View();

            }

            Session["isLoad"] = false;

            

            return View();
        }

        public ActionResult save(string ip, int port, int rate, int recordTime, string fileName)
        {
            Data d = Data.getInstance();
            client.connect(ip, port);
             saver.M_fileName = fileName;

            Session["rate"] = rate;
            Session["isSaveNeeded"] = "true";
            Session["first mission"] = "false";
            Session["recordTime"] = recordTime;

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
