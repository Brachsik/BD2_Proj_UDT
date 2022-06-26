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
    public class TrojkatTest
    {
        private DBQueryConnection db;
        public TrojkatTest()
        {
            db = new DBQueryConnection();
        }

        [TestMethod]
        public void TestInsertTrojkatSuccess()
        {
            string query = "INSERT INTO dbo.Trojkat VALUES('-1/0/1/0/0/1');";
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
        public void TrojkatZlaIloscDanych()
        {
            string query = "INSERT INTO dbo.Trojkat VALUES('1/1/1');";
            string result = "";

            try
            {
                db.InsertQuery(query);
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            Assert.AreEqual("Niepoprawna liczba argumentów! (wymagane 6)", result);
        }

        [TestMethod]
        public void TrojkatZleDane()
        {
            string query = "INSERT INTO dbo.Trojkat VALUES('1/zle/2/3/1/1');";
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
        public void TestDeleteTrojkat()
        {
            string query = "INSERT INTO dbo.Trojkat VALUES('-1/0/1/0/0/1');";
            string delete_query = "DELETE dbo.Trojkat WHERE Trojkat.WyznaczWspolrzedne() = CAST('(-1/0/1/0/0/1)' AS nvarchar) ;";
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
