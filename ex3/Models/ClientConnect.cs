using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace ex3.Models
{
    public class ClientConnect
    {
        TcpClient client;

        public void connect(string ip , int port)
        {
            try
            {
                client = new TcpClient();
                client.Connect(ip, port);
                Console.Write("connect sucssesfuly");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void disconnect()
        {
            if (client == null)
            {
                Console.WriteLine("Client not connected- can't disconnect");
                return;
            }
            client.Close();
            client = null;
        }

        public string read(NetworkStream nwStream)
        {
            if (client == null)
            {
                Console.WriteLine("Client not connected - can't read");
                return null;
            }

            //NetworkStream nwStream = client.GetStream();

            byte[] byteToSend = new byte[512] ;
            nwStream.Read(byteToSend, 0, byteToSend.Length);

            return byteToSend.ToString();
        }

        public void write(string command , NetworkStream nwStream)
        {
            if (client == null)
            {
                Console.WriteLine("Client not connected - can't write");
                return;
            }
            //NetworkStream nwStream = client.GetStream();
            byte[] byteToSend = ASCIIEncoding.ASCII.GetBytes(command);
            nwStream.Write(byteToSend, 0, byteToSend.Length);
            
        }

        public void start()
        {
            Data d = Data.getInstance();
            NetworkStream nwStream = client.GetStream();

            write("get /position/latitude-deg\r\n" , nwStream);
            d.M_lat = read(nwStream);
            write("get /position/longitude-deg\r\n",nwStream);
            d.M_lon = read(nwStream);

        }


    }
}