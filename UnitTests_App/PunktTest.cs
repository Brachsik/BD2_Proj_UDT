using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using projectUDT_app;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace UnitTests_App
{
    [TestClass()]
    public class PunktTest
    {

        private DBQueryConnection db;
        public PunktTest()
        {
            db = new DBQueryConnection();
        }

        [TestMethod]
        public void TestInsertPunktSuccess()
        {
            string query = "INSERT INTO dbo.Punkt VALUES('21/34');";
            string result = "";

            try
            {
                db.InsertQuery(query);
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            Assert.AreEqual("Pomyslnie wprowadzono nowe dane!", result);
        }

        [TestMethod]
        public void PunktZlaIloscDanych()
        {
            string query = "INSERT INTO dbo.Punkt VALUES('1/1/1');";
            string result = "";

            try
            {
                db.InsertQuery(query);
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            Assert.AreEqual("Niepoprawna liczba argumentów! (wymagane 2)", result);
        }

        [TestMethod]
        public void PunktZleDane()
        {
            string query = "INSERT INTO dbo.Punkt VALUES('1/zle');";
            string result = "";

            try
            {
                db.InsertQuery(query);
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            Assert.AreEqual("Niepoprawny typ danych!", result);
        }

        [TestMethod]
        public void TestDeletePunkt()
        {
            string query = "INSERT INTO dbo.Punkt VALUES('50/41');";
            string delete_query = "DELETE dbo.Punkt WHERE Punkt.WyznaczWspolrzedne() = CAST('(50/41)' AS nvarchar) ;";
            string result = "";

            try
            {
                db.DeleteQuery(delete_query);
            }
            catch (Exception e)
            {
            }

            try
            {
                db.InsertQuery(query);
            }
            catch (Exception e)
            {
            }

            try
            {
                db.DeleteQuery(delete_query);
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            Assert.AreEqual("Pomyslne usuniecie liczby rekordow: 1", result);

            try
            {
                db.DeleteQuery(delete_query);
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            Assert.AreEqual("Brak danych do usuniecia!", result);

        }

    }
}
