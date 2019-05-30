﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ex3.Models
{
    public class Save
    {
        string m_allData = "";

        public Save() { }

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

        public void saveToFile(string fileName) {

            string path = Directory.GetCurrentDirectory();
            path += "\\" + fileName;
            List<string> result = m_allData.Split(',').ToList();
            System.IO.File.WriteAllLines(path, result);
        }

        public void addPoint(string data) { // need to get data seperated by commas.
            m_allData += "," + data;
        }
    }
}