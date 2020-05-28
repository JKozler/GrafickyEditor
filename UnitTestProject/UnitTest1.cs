using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GrafickyEditor;
using System.IO;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        //opravit
        Load load;
        Pages pages;
        Password pass;
        public UnitTest1()
        {
            load = new Load();
            pages = new Pages();
            pass = new Password();
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
        [TestMethod]
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
        [TestMethod]
        public void Control_If_Password_Is_Right()
        {
            //Arrange
            string s = "Mala";

            //Act
            bool result = pass.ControlPass(s);

            //Assert
            Assert.IsFalse(result);
        }
    }
}
