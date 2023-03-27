using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TableTennisTable_CSharp;
using TechTalk.SpecFlow;

namespace TableTennisTable_Tests
{
    [Binding]
    public class GameSteps
    {
        // Current app instance
        private App app;

        // Last response
        private String response;

        [BeforeScenario]
        public void CreateApp()
        {
            var league = new League();
            var leagueRenderer = new LeagueRenderer();
            var fileService = new FileService();
            app = new App(league, leagueRenderer, fileService);
        }

        [Given("the league has no players")]
        public void GivenTheLeagueHasNoPlayers()
        {
            // Nothing to do - the default league starts with no players
        }
        
        [When("I print the league")]
        public void WhenIPrintTheLeague()
        {
            response = app.SendCommand("print");
        }

        [Then("I should see \"(.*)\"")]
        public void ThenIShouldSeeThatThereAreNoPlayers(string expected)
        {
            Assert.AreEqual(expected, response);
        }
    }
}
