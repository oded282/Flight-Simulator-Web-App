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
        Load loader;
        Data data;

        public MainController (){
            client = ClientConnect.getInstance();
            saver = Save.getInstance();
            loader = Load.getInstance();
            data = Data.getInstance();

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
            if (loader.M_isLoaded)
            {
                loader.getNextPoint();
                Session["isDone"] = loader.M_isDone.ToString();

            }
            else
            {
                this.client.start(); 
            }

            return ToXml(this.data);
        }

        [HttpPost]
        public void SaveToFile()
        {
            saver.SaveToFile();
        }

        [HttpPost]
        public void SavePoint()
        {
          

            string lat = this.data.M_lat;
            string lon = this.data.M_lon;
            string rudder = this.data.M_rudder;
            string throttle = this.data.M_throttle;
            string data = lat + "," + lon + "," + rudder + "," + throttle + ",";
            saver.addPoint(data);

        }
        
        [HttpGet]
        public ActionResult display(string ip, int port, int? rate)
        {

            Session["isDone"] = "false";
            Session["isSaveNeeded"] = "false";
            Session["first mission"] = "false";

            if (!ip.Contains('.'))
            {
                rate = -1;
                Session["rate"] = port;
                this.loader.M_fileName = ip;
                this.loader.loadFromFile();
                this.loader.M_isLoaded = true;

                return View();

            }

            client.connect(ip, port);

            if (String.IsNullOrEmpty(rate.ToString()))
            {
                client.start();
                rate = -1;
                Session["lat"] = this.data.M_lat;
                Session["lon"] = this.data.M_lon;
                Session["first mission"] = "true";
                return View();
            }

            Session["rate"] = rate;


            return View();
        }

        public ActionResult save(string ip, int port, int rate, int recordTime, string fileName)
        {
            client.connect(ip, port);
             saver.M_fileName = fileName;
            Session["isDone"] = "false";
            Session["rate"] = rate;
            Session["isSaveNeeded"] = "true";
            Session["first mission"] = "false";
            Session["recordTime"] = recordTime;

            return View("display");
        }

       
    }
}
