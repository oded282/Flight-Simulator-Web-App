using Microsoft.VisualStudio.TestTools.UnitTesting;
using ex3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex3.Models.Tests
{
    [TestClass()]
    public class TestTests
    {
        [TestMethod()]
        public void MainTest()
        {
            void Main(string[] args)
            {
                Save saver = new Save();
                string data = "1,2,3,4,5,6,7";
                saver.saveToFile(data, "file1");
            }
        }
    }
}