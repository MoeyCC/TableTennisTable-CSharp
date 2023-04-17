using System;
using System.Collections.Generic;

namespace TableTennisTable_CSharp
{
    public class LeagueRow
    {
        private int _maxSize;
        private List<string> _players;

        public LeagueRow(int maxSize)
        {
            _maxSize = maxSize; //ComputeMaxSizeWithSizeCap(maxSize); ignore this comment, it will be relevant in a later exercise
            _players = new List<string>();
        }

        public void Swap(string playerToRemove, string playerToAdd)
        {
            int index = _players.FindIndex(player => player == playerToRemove);
            if (index != -1)
            {
                _players[index] = playerToAdd;
            }
            else
            {
                throw new Exception($"Player {playerToRemove} did not exist in row: {string.Join("|", _players)}");
            }
        }

        public int GetMaxSize()
        {
            return _maxSize;
        }

        public List<string> GetPlayers()
        {
            return _players;
        }

        public void Add(string player)
        {
            if (IsFull())
            {
                throw new InvalidOperationException("Row is full");
            }
            _players.Add(player);
        }

        public bool IsFull()
        {
            return _players.Count >= _maxSize;
        }

        public bool Includes(string player)
        {
            return _players.Contains(player);
        }

        // private int ComputeMaxSizeWithSizeCap(int maxSize){
        //     var sizeCapString = Environment.GetEnvironmentVariable("TABLE_TENNIS_LEAGUE_ROW_SIZE_CAP");
        //     if (sizeCapString == null) return maxSize;

        //     var sizeCap = int.Parse(sizeCapString);
        //     return maxSize <= sizeCap
        //             ? maxSize
        //             : sizeCap;
        // }
        // Ignore this size cap code, it will be relevant in a future exercise.
    }
}
