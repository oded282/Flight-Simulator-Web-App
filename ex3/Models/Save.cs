using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ex3.Models
{
    #region singletone
    public class Save
    {
        string m_allData = "";
        string m_fileName;

        public static Save instance = null;

        private Save() { }

        public static Save getInstance()
        {
            if (instance == null)
            {
                instance = new Save();
            }
            return instance;
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

        public string M_allData
        {
            get
            {
                return m_allData;
            }
            set
            {
                m_allData = value;
            }
        }

        public void SaveToFile() {
            string path = System.AppDomain.CurrentDomain.BaseDirectory + M_fileName;
            List<string> result = m_allData.Split(',').ToList();
            result.RemoveAt(result.Count - 1);
            if (File.Exists(path)){
                System.IO.File.Delete(path);
            }
            System.IO.File.WriteAllLines(path, result);
            m_allData = "";
        }

        public void addPoint(string data) { // need to get data seperated by commas.
            m_allData += data;
        }
    }
    #endregion
}

