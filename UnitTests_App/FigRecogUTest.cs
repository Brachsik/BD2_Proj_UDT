using Microsoft.VisualStudio.TestTools.UnitTesting;
using projectUDT_app;
using System;

namespace UnitTests_App
{
    [TestClass]
    public class FigRecogUTest
    {
        [TestMethod]
        public void FigrecogTest()
        {
            Assert.AreEqual("Punkt", Program.Figrecog(1));
            Assert.AreEqual("Prosta", Program.Figrecog(2));
            Assert.AreEqual("Trojkat", Program.Figrecog(3));
            Assert.AreEqual("Kwadrat", Program.Figrecog(4));
            Assert.AreEqual("Prostokat", Program.Figrecog(5));
            Assert.AreEqual("Kolo", Program.Figrecog(6));

            string result = "";

            try
            {
                Program.Figrecog(7);
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            Assert.AreEqual("Powrot do menu...", result);
        }

        [TestMethod]
        public void FigrecoTestOnBadInput()
        {
            string result="";
            try
            {
                Program.Figrecog(9);
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            Assert.AreEqual("Wybrano niewlasciwa opcje!", result);
        }

    }
}
