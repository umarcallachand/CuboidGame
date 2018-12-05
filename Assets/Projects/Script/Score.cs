using System;

namespace Projects.Script
{
    public class Score
    {
        public int Id { get; private set; }
        public string PlayerName { get; private set; }
        public int ScoreValue { get; private set; }
        private DateTime _date;

        public Score(string playerName, int score, DateTime date)
        {
            PlayerName = playerName;
            ScoreValue = score;
            _date = date;
        }

        public Score(int id, string playerName, int score, DateTime date)
        {
            Id = id;
            PlayerName = playerName;
            ScoreValue = score;
            _date = date;
        }
    }
}
