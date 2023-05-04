# Table Tennis Table - C#

1. Fork this repo

   - Click `Fork` in the top-right of the page - this will create a copy of this repo in **your own GitHub account**

2. Clone (download) the repo

   - Go to your newly-created fork of the repo (on GitHub).
   - Click `Clone or download` (the green button on the right).
   - Make sure the page says `Clone with SSH` (rather than `Clone with HTTPS`).
   - Open your git client (e.g. GitKraken) and use this link to clone the repo.  
     Your trainer will be able to help you with this.

3. "Cloning the repo" will create a folder on your computer with the files from this repo.  
   Open this folder in Visual Studio Code.

4. Open a command-prompt in the project folder.  
   Your trainer can show you how to do this, if you need any help.

5. Change directory to "TableTennisTable-CSharp", and run this command to run your code:  
   `dotnet run`

6. Run this command from the root directory to test your code:  
   `dotnet test`

## Commands

When the app is running the following commands can be run:

- `add player <name>`- adds a player of that name.
  Example: `add player Alice`

- `print`- prints the state of the league

- `record win <winner> <loser> `- records the result of a match.
  Example: `record win Alice Mike`

- `winner`- finds the current winner

- `save <filepath>` - saves the current league to a JSON file.
  Example: `save my_league.json`

- `load <filepath>` - loads a saved league from a JSON file.
  Example: `load my_league.json`

- `quit` - quits the game
