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
        public int AantalKaarten { get; set; }

        public HighScore()
        {
            // Lege parameterloze constructor, vereist voor deserialisatie
        }
        public HighScore(string userName, double score, int kaarten)
        {
            Score = score;
            UserName = userName;
            AantalKaarten = kaarten;
            HighScoreManager highScoreManagerhigh = new HighScoreManager();
            highScoreManagerhigh.AddHighScore(userName, score, kaarten);
        }

        public HighScore(string userName, double score, int kaarten, HighScoreManager highScoreManager)
        {
            Score = score;
            UserName = userName;
            AantalKaarten = kaarten;
            highScoreManager.AddHighScore(userName, score, kaarten);
        }
    }

}
