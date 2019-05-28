using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace ex3.Models
{
    #region Singleton
    public class ClientConnect
    {
       
        TcpClient client;
        public static ClientConnect instance = null;

        private ClientConnect() {}

        public static ClientConnect getInstance() {
            if (instance == null) {
                instance = new ClientConnect();
            }
            return instance;
        }

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

            return Encoding.UTF8.GetString(byteToSend, 0, byteToSend.Length);
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

        public string parseValue(string data) {
            int startindex = data.IndexOf((char)39);
            int Endindex = data.LastIndexOf((char)39);
            string outputstring = data.Substring(startindex + 1, Endindex - startindex - 1);
            return outputstring;
        }

        public void start()
        {
            Data d = Data.getInstance();
            NetworkStream nwStream = client.GetStream();

            write("get /position/latitude-deg\r\n" , nwStream);
            d.M_lat = parseValue(read(nwStream));
            write("get /position/longitude-deg\r\n",nwStream);
            d.M_lon = parseValue(read(nwStream));


        }


    }
    #endregion
}