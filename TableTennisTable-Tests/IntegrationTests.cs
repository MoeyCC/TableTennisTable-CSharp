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
        const string fileName = "Frogs.txt";

        App game; 

        [TestInitialize]
        public void TestInitialize(){
            game = new App(new League(), new LeagueRenderer(), new FileService());
        }

        [TestMethod]
        public void TestPrintsEmptyGame()
        {
            Assert.AreEqual("No players yet", game.SendCommand("print"));
        }        

        [TestMethod]
        public void AddPlayer_Should_Add_Player_To_League()
        {
            //Act
            var result = game.SendCommand("add player Alice");

            //Assert
            Assert.AreEqual("Added player Alice", result);
        }

        [TestMethod]
        public void RecordWin_Should_Update_League_Standings()
        {
            //Arrange
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
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_Get_Winner()
        {
            //Arrange
            game.SendCommand("add player Alice");
            game.SendCommand("add player Bob");
            game.SendCommand("add player Harry");

            //Act
            var result = game.SendCommand("winner");

            //Assert
            Assert.AreEqual("Alice", result);
        }

        [TestMethod]
        public void Print_Should_Display_League_With_Correct_Format()
        {
            //Arrange
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
        public void Save_Should_Save_League_To_File_Using_Mock_FileService()
        {
            //Arrange
            var league = new League();
            var leagueRenderer = new LeagueRenderer();
            var mockFileService = new Mock<IFileService>();
            var gameApp = new App(league, leagueRenderer, mockFileService.Object);
            game.SendCommand("add player Alice");
            game.SendCommand("add player Bob");
            var filePath = "saved_league.txt";
            mockFileService.Setup(f => f.Save(filePath, league));

            //Act
            var result = gameApp.SendCommand("save saved_league.txt");

            //Assert
            Assert.AreEqual("Saved saved_league.txt", result);

            //Verify that the save method is called
            mockFileService.Verify(f => f.Save(filePath, league));   
        } 

        [TestMethod]
        public void Save_Should_Save_League_To_File_Using_Real_FileService()
        {
            // Arrange
            //const string FileName = "frogsAreCool";
            game.SendCommand("add player Alice");
            game.SendCommand("add player Susan");
            game.SendCommand("add player Harry");
            game.SendCommand("add player Bob");
            game.SendCommand("add player Ben");
            game.SendCommand("add player Donald");
            //game.SendCommand($"save {FileName}.txt");
            game.SendCommand($"save {fileName}");
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
            string resultFile = File.ReadAllText($"{fileName}");

            // Assert
            //Assert.AreEqual(expected, game.SendCommand("print"));
            Assert.AreEqual("Alice\r\nSusan,Harry\r\nBob,Ben,Donald\r\n", resultFile); 
        } 

        [TestMethod]
        public void Load_Should_Load_Saved_League()
        {
            //Arrange     
            //var gamePrint = game.SendCommand("print");

            //const string savedResultFile = "SavedGameResults.txt";
            
            //Act
            //game.SendCommand($"load {savedResultFile}");
            
            //Assert
            //Assert.AreEqual(gamePrint, savedResultFile); 
        }
    }
}
