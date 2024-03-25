using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using TableTennisTable_CSharp;

namespace TableTennisTable_Tests
{
    [TestClass]
    public class AppTests
    {
        [TestMethod]
        public void TestPrintsCurrentState()
        {
            var league = new League();
            var renderer = new Mock<ILeagueRenderer>();
            renderer.Setup(r => r.Render(league)).Returns("Rendered League");

            var app = new App(league, renderer.Object, null);

            Assert.AreEqual("Rendered League", app.SendCommand("print"));
        }

        [TestMethod]
        public void AddingPlayerShouldReturnConfirmationMessage()
        {
            // Arrange
            var mockLeague = new Mock<League>();
            var app = new App(mockLeague.Object, null, null);

            // Setup the behavior of the League mock
            mockLeague.Setup(l => l.AddPlayer("Alice"));

            // Act
            string result = app.SendCommand("add player Alice");

            // Assert
            Assert.AreEqual("Added player Alice", result);
        }

        [TestMethod]
        public void RecordingWinShouldReturnConfirmationMessage()
        {
            //Arrange
            var mockLeague = new Mock<League>();
            var app = new App(mockLeague.Object, null, null);

            //Setup behavior of the League mock
            mockLeague.Setup(l => l.RecordWin("Alice", "Bob"));

            //Act
            string result = app.SendCommand("record win Alice Bob");

            //Assert
            Assert.AreEqual("Recorded Alice win against Bob", result);
        }

        [TestMethod]
        public void RequestingLeagueWinnerShouldReturnCorrectText()
        {
            //Arrange
            var mockLeague = new Mock<League>();
            var app = new App(mockLeague.Object, null, null);

            //Setup behavior of League mock
            mockLeague.Setup(l => l.GetWinner()).Returns("winner");

            //Act
            var result = app.SendCommand("winner");

            //Assert
            Assert.AreEqual("winner", result);
        }

        [TestMethod]
        public void SavingLeagueToFileShouldReturnConfirmation()
        {
            //Arrange
            var mockFileService = new Mock<IFileService>();
            var app = new App(null, null, mockFileService.Object);

            //Setup behavior of FileService mock
            mockFileService.Setup(f => f.Save("filename", It.IsAny<League>()));

            //Act
            var result = app.SendCommand("save filename");

            //Assert
            Assert.AreEqual("Saved filename", result);            
        }

        [TestMethod]
        public void LoadSavedGameShouldReturnConfirmation()
        {
            //Arrange
            Mock<IFileService> mockFileService = new Mock<IFileService>();
            App app = new App(null, null, mockFileService.Object);

            //Set up behavior of 
            mockFileService.Setup(f => f.Load("filename"));

            //Act
            string result = app.SendCommand("load filename");

            //Assert
            Assert.AreEqual("Loaded filename", result);
        }
    }
}
