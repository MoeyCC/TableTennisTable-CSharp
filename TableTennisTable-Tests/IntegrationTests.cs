using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
            var game = CreateApp();
            
            //Act
            var result = game.SendCommand("add player Alice");

            //Assert
            Assert.AreEqual("Added player Alice", result);
        }

        [TestMethod]
        public void RecordWin_Should_UpdateLeagueStandings()
        {
            //Arrange
            var game = CreateApp();
            game.SendCommand("add player Alice");
            game.SendCommand("add player Bob");
            game.SendCommand("add player Harry");
            game.SendCommand("add player Susan");
            game.SendCommand("record win Susan Bob");
            var expected = @"                              -------------------
                              |      Alice      |
                              -------------------
                    ------------------- -------------------
                    |      Susan      | |      Harry      |
                    ------------------- -------------------
          ------------------- ------------------- -------------------
          |       Bob       | |                 | |                 |
          ------------------- ------------------- -------------------";
            
            //Act
            var result = game.SendCommand("print");

            //Assert
            //var susanRowIndex = league.GetRows().FindIndex(row => row.GetPlayers().Contains("Susan"));
            //var bobRowIndex = league.GetRows().FindIndex(row => row.GetPlayers().Contains("Bob"));
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestGetWinner()
        {
            //Arrange
            var game = CreateApp();
            game.SendCommand("add player Alice");
            game.SendCommand("add player Bob");
            game.SendCommand("add player Harry");

            //Act
            var result = game.SendCommand("winner");

            //Assert
            Assert.AreEqual("Alice", result);
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
        public void Save_Should_SaveLeagueToFile_Using_Mock_FileService()
        {
            //Arrange
            var league = new League();
            var leagueRenderer = new LeagueRenderer();
            var mockFileService = new Mock<IFileService>();
            var game = new App(league, leagueRenderer, mockFileService.Object);
            game.SendCommand("add player Alice");
            game.SendCommand("add player Bob");
            var filePath = "saved_league.txt";
            mockFileService.Setup(f => f.Save(filePath, league));

            //Act
            var result = game.SendCommand("save saved_league.txt");

            //Assert
            Assert.AreEqual("Saved saved_league.txt", result);

            //Verify that the save method is called
            mockFileService.Verify(f => f.Save(filePath, league));   
        } 

        [TestMethod]
        public void Save_Should_SaveLeagueToFile_Using_Real_FileService()
        {
            // Arrange
            var game = CreateApp();
            game.SendCommand("add player Alice");
            game.SendCommand("add player Susan");
            game.SendCommand("add player Harry");
            game.SendCommand("add player Bob");
            game.SendCommand("add player Ben");
            game.SendCommand("add player Donald");
            game.SendCommand("save League.txt");
            var expected = @"                              -------------------
                              |      Alice      |
                              -------------------
                    ------------------- -------------------
                    |      Susan      | |      Harry      |
                    ------------------- -------------------
          ------------------- ------------------- -------------------
          |       Bob       | |       Ben       | |     Donald      |
          ------------------- ------------------- -------------------";

            // Act
            //var result = game.SendCommand("print");
            string resultFile = File.ReadAllText("League.txt");

            // Assert
            Assert.AreEqual(expected, game.SendCommand("print"));
            Assert.AreEqual("Alice\r\nSusan,Harry\r\nBob,Ben,Donald\r\n", resultFile); 
            /* 
            Assert.AreEqual("Alice,Bob,Charlie", app.SendCommand("print"));
            Assert.AreEqual("Saved League.txt", app.SendCommand("save League.txt"));

            string resultFile = File.ReadAllText("League.txt");
            Assert.AreEqual("Alice\r\nBob,Charlie\r\n", resultFile); 
            */
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
