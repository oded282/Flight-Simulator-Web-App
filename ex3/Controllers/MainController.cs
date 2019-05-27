using ex3.Models;
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


namespace ex3.Controllers
{
    public class MainController : Controller
    {

      
        public void task(string ip , int port)
        {
            connect(ip, port);
            read();
        }


        [HttpGet]
        public ActionResult display(string ip , int port)
        {
            //Thread thread = new Thread(() => task(ip, port));
            //thread.Start();
            //TODO connect to flight

            Data d = Data.getInstance();

            return View();
        }
    }
}
