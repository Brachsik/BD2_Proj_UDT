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
    public class KoloTest
    {

        private DBQueryConnection db;
        public KoloTest()
        {
            db = new DBQueryConnection();
        }

        [TestMethod]
        public void TestInsertKoloSuccess()
        {
            string query = "INSERT INTO dbo.Kolo VALUES('21/34/43/32');";
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
        public void KoloZlaIloscDanych()
        {
            string query = "INSERT INTO dbo.Kolo VALUES('1/1/1');";
            string result = "";

            try
            {
                db.InsertQuery(query);
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            Assert.AreEqual("Niepoprawna liczba argumentów! (wymagane 4)", result);
        }

        [TestMethod]
        public void KoloZleDane()
        {
            string query = "INSERT INTO dbo.Kolo VALUES('1/zle/2/3');";
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
        public void TestDeleteKolo()
        {
            string query = "INSERT INTO dbo.Kolo VALUES('50/41/40/40');";
            string delete_query = "DELETE dbo.Kolo WHERE Kolo.WyznaczWspolrzedne() = CAST('(50/41/40/40)' AS nvarchar) ;";
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
