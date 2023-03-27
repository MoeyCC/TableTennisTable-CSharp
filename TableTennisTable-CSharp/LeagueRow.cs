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
            _maxSize = maxSize;
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
    }
}
