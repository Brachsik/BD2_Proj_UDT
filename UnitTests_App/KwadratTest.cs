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
    public class KwadratTest
    {
        private DBQueryConnection db;
        public KwadratTest()
        {
            db = new DBQueryConnection();
        }

        [TestMethod]
        public void TestInsertKwadratSuccess()
        {
            string query = "INSERT INTO dbo.Kwadrat VALUES('0/0/0/1/1/1/1/0');";
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
        public void KwadratZlaIloscDanych()
        {
            string query = "INSERT INTO dbo.Kwadrat VALUES('1/1/1');";
            string result = "";

            try
            {
                db.InsertQuery(query);
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            Assert.AreEqual("Niepoprawna liczba argumentów! (wymagane 8)", result);
        }

        [TestMethod]
        public void KwadratZleDane()
        {
            string query = "INSERT INTO dbo.Kwadrat VALUES('1/zle/2/3/1/1/1/1');";
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
        public void TestDeleteKwadrat()
        {
            string query = "INSERT INTO dbo.Kwadrat VALUES('0/0/0/1/1/1/1/0');";
            string delete_query = "DELETE dbo.Kwadrat WHERE Kwadrat.WyznaczWspolrzedne() = CAST('(0/0/0/1/1/1/1/0)' AS nvarchar) ;";
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
