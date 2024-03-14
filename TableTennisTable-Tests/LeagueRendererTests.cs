using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TableTennisTable_CSharp;

namespace TableTennisTable_Tests
{
    [TestClass]
    public class LeagueRendererTests
    {
        [TestMethod]
        public void GivenPlayersExists_WhenUserTypesPrint_ThenLeaguePrinted()
        {
            // Arrange
            League league = new League();
            var leagueRenderer = new LeagueRenderer();
            var fileService = new FileService();
            var game = new App(league, leagueRenderer, fileService);
            league.AddPlayer("Megan");
            league.AddPlayer("Lizzie");
            league.AddPlayer("Mia");
            league.AddPlayer("Nat");
            league.AddPlayer("Blake");
            league.AddPlayer("Mohan");
            var expected = @"                              -------------------
                              |      Megan      |
                              -------------------
                    ------------------- -------------------
                    |     Lizzie      | |       Mia       |
                    ------------------- -------------------
          ------------------- ------------------- -------------------
          |       Nat       | |      Blake      | |      Mohan      |
          ------------------- ------------------- -------------------";

            // Act
            var result = game.SendCommand("print");

            // Assert
            Assert.AreEqual( expected, result);            
        }

        [TestMethod]
        public void GivenNoPlayersExists_WhenUserTypesPrint_ThenMessagePrinted()
        {
            // Arrange
            League league = new League();
            var leagueRenderer = new LeagueRenderer();
            var fileService = new FileService();
            var game = new App(league, leagueRenderer, fileService);
            
            // Act
            var result = game.SendCommand("print");

            // Assert
            Assert.AreEqual( "No players yet", result);            
        }        
    }
}
