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

        public string read()
        {
            if (client == null)
            {
                Console.WriteLine("Client not connected - can't read");
                return null;
            }

            NetworkStream nwStream = client.GetStream();
            BinaryReader reader = new BinaryReader(nwStream);

            return reader.ReadString();

        }

        public void write(string command)
        {
            if (client == null)
            {
                Console.WriteLine("Client not connected - can't write");
                return;
            }
            NetworkStream nwStream = client.GetStream();
            BinaryWriter writer = new BinaryWriter(nwStream);

            writer.Write(command);
            
        }

        public void start()
        {
            Data d = Data.getInstance();
            
            write("get /position/latitude-deg\r\n");
            d.M_lat = read();
            write("get /position/longitude-deg\r\n");
            d.M_lon = read();

        }


    }
}