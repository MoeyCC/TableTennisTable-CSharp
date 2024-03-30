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

        [TestMethod]
        public void AddPlayer_Should_AddPlayerToLeague()
        {
            //Arrange
            var league = new League();
            var leagueRenderer = new LeagueRenderer();
            var fileService = new FileService();
            var game = new App(league, leagueRenderer, fileService);

            //Act
            game.SendCommand("add player Alice");

            //Assert
            string playerName = league.GetRows().SelectMany(row => row.GetPlayers()).First(); // Alice in league
            Assert.AreEqual("Alice", playerName);
            Assert.AreEqual(1, league.GetRows().Count); // 1 row in league
            Assert.AreEqual(1, league.GetRows().Sum(row => row.GetPlayers().Count())); // 1 player in league
        }

        [TestMethod]
        public void RecordWin_Should_UpdateLeagueStandings()
        {
            //Arrange
            var league = new League();
            var leagueRenderer = new LeagueRenderer();
            var fileService = new FileService();
            var game = new App(league, leagueRenderer, fileService);
            game.SendCommand("add player Alice");
            game.SendCommand("add player Bob");
            game.SendCommand("add player Harry");
            game.SendCommand("add player Susan");
            
            //Act
            game.SendCommand("record win Susan Bob");

            //Assert
            var susanRowIndex = league.GetRows().FindIndex(row => row.GetPlayers().Contains("Susan"));
            var bobRowIndex = league.GetRows().FindIndex(row => row.GetPlayers().Contains("Bob"));
            Assert.IsTrue(susanRowIndex < bobRowIndex);
        }

        [TestMethod]
        public void TestGetWinner()
        {
            //Arrange
            var league = new League();
            var leagueRenderer = new LeagueRenderer();
            var fileService = new FileService();
            var game = new App(league, leagueRenderer, fileService);
            game.SendCommand("add player Alice");
            game.SendCommand("add player Bob");
            game.SendCommand("add player Harry");

            //Act
            var winner = game.SendCommand("winner");

            //Assert
            Assert.AreEqual("Alice", winner);
        }

        [TestMethod]
        public void Print_Should_DisplayLeagueWithCorrectFormat()
        {
            //Arrange
            var league = new League();
            var leagueRenderer = new LeagueRenderer();
            var fileService = new FileService();
            var game = new App(league, leagueRenderer, fileService);
            game.SendCommand("add player Alice");
            game.SendCommand("add player Bob");
            game.SendCommand("add player Harry");
            game.SendCommand("add player Susan");
            game.SendCommand("add player Ben");
            game.SendCommand("add player Donald");
            game.SendCommand("record win Susan Bob");

            //Act
            var actual = game.SendCommand("print");
            var expected = @"                              -------------------
                              |      Alice      |
                              -------------------
                    ------------------- -------------------
                    |      Susan      | |      Harry      |
                    ------------------- -------------------
          ------------------- ------------------- -------------------
          |       Bob       | |       Ben       | |     Donald      |
          ------------------- ------------------- -------------------";

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Save_Should_SaveLeagueToFile()
        {
            //Arrange
            var league = new League();
            var leagueRenderer = new LeagueRenderer();
            var fileService = new FileService();
            var game = new App(league, leagueRenderer, fileService);
            game.SendCommand("add player Alice");
            game.SendCommand("add player Bob");
            var filePath = "saved_league";

            //Act
            game.SendCommand("save saved_league");

            //Assert
            Assert.IsTrue(File.Exists(filePath)); 
        }

        [TestMethod]
        public void Load_Should_LoadSavedLeague()
        {
            //Arrange
                        
            
            //Act
            
            //Assert
            
        }
    }
}
