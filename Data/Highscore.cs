using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class HighScore
    {
        public string UserName { get; set; }
        public double Score { get; set; }

        public HighScore()
        {
            // Lege parameterloze constructor, vereist voor deserialisatie
        }
        public HighScore(string userName, double score)
        {
            Score = score;
            UserName = userName;
            HighScoreManager highScoreManagerhigh = new HighScoreManager();
            highScoreManagerhigh.AddHighScore(userName, score);
        }

        public HighScore(string userName, double score, HighScoreManager highScoreManager)
        {
            Score = score;
            UserName = userName;
            highScoreManager.AddHighScore(userName, score);
        }
    }

}
