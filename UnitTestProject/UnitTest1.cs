using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GrafickyEditor;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        Load load;
        Pages pages;
        public UnitTest1()
        {
            load = new Load();
            pages = new Pages();
        }
        [TestMethod]
        public void Control_If_Name_Contains_Obr()
        {
            //Arrange
            string s = "projekt";

            //Act
            string result = load.ObrCheck(s);

            //Assert
            Assert.IsTrue((s + ".obr") == result);
        }
        public void Control_If_Checked_Contains_Elements()
        {
            //Arrange
            int i = 0;
            bool s = false;

            //Act
            bool result = pages.CheckIfCheck(i);

            //Assert
            Assert.IsTrue(s == result);
        }
    }
}
