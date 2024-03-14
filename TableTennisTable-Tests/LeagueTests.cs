using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TableTennisTable_CSharp;

namespace TableTennisTable_Tests
{
    [TestClass]
    public class LeagueTests
    {
        [TestMethod]
        public void GivenLeagueExists_WhenAddingPlayer_ThenPlayerIsAdded()
        {
            // Arrange
            League league = new League();

            // Act
            league.AddPlayer("Bob");

            // Assert
            var rows = league.GetRows();
            Assert.AreEqual(1, rows.Count);
            var firstRowPlayers = rows.First().GetPlayers();
            Assert.AreEqual(1, firstRowPlayers.Count);
            CollectionAssert.Contains(firstRowPlayers, "Bob");
        }

        [TestMethod]
        public void GivenLeagueExists_WhenAddingDuplicatePlayer_ThenExceptionIsThrown()
        {
            // Arrange
            League league = new League();
            league.AddPlayer("Bob");

            // Act
            var exception = Assert.ThrowsException<ArgumentException>(() => league.AddPlayer("Bob"));
                        
            // Assert
            Assert.AreEqual("Cannot add player Bob because they are already in the game", exception.Message);
        }
        
        [TestMethod]
        public void GivenLeagueWithPlayers_WhenGettingRows_ThenCorrectNumberOfRowsReturned()
        {
            //Arrange
            League league = new League();
            league.AddPlayer("Mohan");
            league.AddPlayer("Steph");

            //Act
            var rows = league.GetRows();

            //Assert            
            Assert.AreEqual(2, rows.Count); //should be 2 rows
        }

        [TestMethod]
        public void GivenLeagueWithNoPlayers_WhenGettingRows_ThenNoRowsReturned()
        {
            //Arrange
            League league = new League();
            
            //Act
            var rows = league.GetRows();

            //Assert            
            Assert.AreEqual(0, rows.Count); //should be 0 rows 
        }

        [TestMethod]
        public void GivenLeagueWithPlayersAndAWinIsRecorded_WhenGettingRow_ThenRowContainsWinner()
        {
            //Arrange
            League league = new League();
            league.AddPlayer("Mohan");
            league.AddPlayer("Megan");
            league.AddPlayer("Steph");
            league.AddPlayer("Blake");
            league.AddPlayer("Christine");
            league.RecordWin("Christine", "Steph");
            var rows = league.GetRows();

            //Act
            var secondRowPlayers = rows[1].GetPlayers();            

            //Assert            
            CollectionAssert.Contains(secondRowPlayers, "Megan"); //second row should contain
            CollectionAssert.Contains(secondRowPlayers, "Christine"); //Megan and Christine
        }

        [TestMethod]
        public void GivenLeagueWithPlayers_WhenWinIsRecordedWithNonExistentPlayer_ThenExceptionThrown()
        {
            //Arrange
            League league = new League();
            league.AddPlayer("Mohan");
            league.AddPlayer("Megan");
            league.AddPlayer("Steph");
            league.AddPlayer("Blake");
            league.AddPlayer("Christine");
            
            //Act
            var exception = Assert.ThrowsException<ArgumentException>(() => league.RecordWin("Harry", "Megan"));

            //Assert            
            Assert.AreEqual("Player Harry is not in the game", exception.Message);
        }

        [TestMethod]
        public void GivenLeagueWithPlayersAndANewLeagueWinnerIsRecorded_WhenGettingWinner_ThenWinnerReturned()
        {
            //Arrange
            League league = new League();
            league.AddPlayer("Megan");
            league.AddPlayer("Lizzie");            
            league.AddPlayer("Steph");
            league.AddPlayer("Nat");
            league.AddPlayer("Blake");
            league.RecordWin("Steph", "Megan");
                        
            //Act
            var winner = league.GetWinner();

            //Assert            
            Assert.AreEqual("Steph", winner);
        }

        [TestMethod]
        public void GivenLeagueWithNoPlayers_WhenGettingWinner_ThenNullReturned()
        {
            //Arrange
            League league = new League();
                                    
            //Act
            var winner = league.GetWinner();

            //Assert            
            Assert.AreEqual(null, winner);
        }
    }
}

