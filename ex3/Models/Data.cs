﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ex3.Models
{
    #region Singleton
    
    public class Data
    {
        private string m_lon = "100";
        private string m_lat = "100";
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

    }
    #endregion
}