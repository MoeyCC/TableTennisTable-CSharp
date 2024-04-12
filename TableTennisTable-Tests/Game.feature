Feature: Game
    Scenario: Empty League
	    Given the league has no players
	    When I print the league
	    Then I should see "No players yet"
	