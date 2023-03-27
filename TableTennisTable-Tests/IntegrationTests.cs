using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using TableTennisTable_CSharp;

namespace TableTennisTable_Tests
{
    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public void TestPrintsEmptyGame()
        {
            var app = CreateApp();

            Assert.AreEqual("No players yet", app.SendCommand("print"));
        }

        private App CreateApp()
        {
            return new App(new League(), new LeagueRenderer(), new FileService());
        }
    }
}
