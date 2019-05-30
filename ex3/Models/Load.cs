using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;


namespace ex3.Models
{
    #region singleton
    public class Load
    {
        bool m_isLoaded = false;
        bool m_isDone = false;
        List<string> m_data;
        string m_fileName;

        public static Load instance = null;

        private Load() {}

        public static Load getInstance() {
            if (instance == null) {
                instance = new Load();
            }
            return instance;
        }

        public bool M_isDone
        {
            get
            {
                return m_isDone;
            }
            set
            {
                m_isDone = value;
            }
        }

        public string M_fileName
        {
            get
            {
                return m_fileName;
            }
            set
            {
                m_fileName = value;
            }
        }

        public List<string> M_data
        {
            get
            {
                return m_data;
            }
            set
            {
                m_data = value;
            }
        }

        public bool M_isLoaded
        {
            get
            {
                return m_isLoaded;
            }
            set
            {
                m_isLoaded = value;
            }
        }

        public void loadFromFile()
        {
            string path = Directory.GetCurrentDirectory();
            path += "\\" + m_fileName;
            string[] data = System.IO.File.ReadAllLines(path);
            m_data = data.ToList<string>();
            instance.m_isLoaded = true; 
        }

        public void getNextPoint()
        {
            if (!(this.M_data.Count == 0))
            {
                Data data = Data.getInstance();
                data.M_lat = m_data[0];
                data.M_lon = m_data[1];
                data.M_rudder = m_data[2];
                data.M_throttle = m_data[3];
                m_data.RemoveRange(0, 4);
            }
            else
            {
                this.M_isDone = true;
            }
        }

    
    }
    #endregion
}