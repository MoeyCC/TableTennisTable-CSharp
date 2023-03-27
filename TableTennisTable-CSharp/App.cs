using System;

namespace TableTennisTable_CSharp
{
    public class App
    {
        private League _league;
        private ILeagueRenderer _leagueRenderer;
        private IFileService _fileService;

        public App(League initialLeague, ILeagueRenderer leagueRenderer, IFileService fileService)
        {
            _league = initialLeague;
            _leagueRenderer = leagueRenderer;
            _fileService = fileService;
        }

        public string SendCommand(string command)
        {
            try
            {
                if (command.StartsWith("add player"))
                {
                    string playerName = command.Substring(11);
                    _league.AddPlayer(playerName);
                    return $"Added player {playerName}";
                }

                if (command.StartsWith("record win"))
                {
                    string playersString = command.Substring(11);
                    var players = playersString.Split(' ');
                    string winner = players[0];
                    string loser = players[1];
                    _league.RecordWin(winner, loser);
                    return $"Recorded {winner} win against {loser}";
                }

                if (command == "print")
                {
                    return _leagueRenderer.Render(_league);
                }

                if (command == "winner")
                {
                    return _league.GetWinner();
                }

                if (command.StartsWith("save"))
                {
                    var filePath = command.Substring(5);
                    _fileService.Save(filePath, _league);
                    return $"Saved {filePath}";
                }

                if (command.StartsWith("load"))
                {
                    var filePath = command.Substring(5);
                    _league = _fileService.Load(filePath);
                    return $"Loaded {filePath}";
                }

                return $"Unknown command: {command}";
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
        }
    }
}
