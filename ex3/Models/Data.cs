using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace ex3.Models
{
    #region Singleton
    
    public class Data
    {
        private string m_lon = "100";
        private string m_lat = "100";
        private string m_throttle = "0";
        private string m_rudder = "0";
        public static Data instance = null;

        public string M_lon
        {
            get
            {
                return m_lon;
            }
            set
            {
                m_lon = value;
            }
        }

        public string M_lat
        {
            get
            {
                return m_lat;
            }
            set
            {
                m_lat = value;
            }
        }

        public string M_throttle
        {
            get
            {
                return m_throttle;
            }
            set
            {
                m_throttle = value;
            }
        }

        public string M_rudder
        {
            get
            {
                return m_rudder;
            }
            set
            {
                m_rudder = value;
            }
        }

        private Data() {}

        public static Data getInstance()
        {
            if (instance == null)
            {
                instance = new Data();
                return instance;
            }
            return instance;
        }

        public void ToXml(XmlWriter writer)
        {
            writer.WriteStartElement("Data");
            writer.WriteElementString("lat", this.M_lat);
            writer.WriteElementString("lon", this.M_lon);
            writer.WriteElementString("rudder", this.M_lat);
            writer.WriteElementString("throttle", this.M_lon);
            writer.WriteEndElement();
        }

    }
    #endregion
}
