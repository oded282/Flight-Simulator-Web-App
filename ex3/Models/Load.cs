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
        private bool m_isLoaded = false;
        string[] m_data;
        int index = 0;

        public static Load instance = null;

        private Load() {}

        public static Load getInstance() {
            if (instance == null) {
                instance = new Load();
            }
            return instance;
        }

        public string[] M_data
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

        public void loadFromFile(string fileName)
        {
            string path = Directory.GetCurrentDirectory();
            path += "\\" + fileName;
            m_data = System.IO.File.ReadAllLines(path);
            instance.m_isLoaded = true; 
        }

        public void getNextPoint()
        {
            Data data = Data.getInstance();
            data.M_lat = m_data[0];
            data.M_lon = m_data[1];
            data.M_rudder = m_data[2];
            data.M_throttle = m_data[3];
            Array.Copy(m_data, 4, m_data, 0, m_data.Length - 4);
        }


    }
    #endregion
}