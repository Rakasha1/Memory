using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Data
{
    public class HighScoreManager
    {
        private const string JsonFilePath = "highscores.json";
        private const int MaxHighScores = 10;

        private List<HighScore> highScores = new List<HighScore>();

        public HighScoreManager()
        {
            if (File.Exists(JsonFilePath))
            {
                // Als het JSON-bestand al bestaat, laad het
                string json = File.ReadAllText(JsonFilePath);
                highScores = JsonSerializer.Deserialize<List<HighScore>>(json);
            }
            else
            {
                // Als het JSON-bestand niet bestaat, maak een leeg bestand aan
                SaveToJsonFile();
            }

        }

        public List<HighScore> GetTopHighScores()
        {
            highScores = highScores.OrderByDescending(highscore => highscore.Score).ToList(); // Sorteer de lijst op score in aflopende volgorde
            return highScores.Take(MaxHighScores).ToList();
        }

        public void printHighScore()
        {
            foreach (var highScore in highScores)
            {
                Console.WriteLine($"Gebruiker: {highScore.UserName}, Score: {highScore.Score}");
            }
        }

        public void AddHighScore(string userName, double scoreValue)
        {
            //nieuwe score toevoegen aan de lijst highscore
            highScores.Add(new HighScore { UserName = userName, Score = scoreValue });
            // highScores = highScores.OrderByDescending(highscore => highscore.Score).ToList(); // Sorteer de lijst op score in aflopende volgorde
            //sorteerd lijst
            GetTopHighScores();

            //de laatste score verwijderen tot de lijst niet meer groter is dan 1-
            while (highScores.Count > MaxHighScores)
            {
                //een highscore verwijderen.
                highScores.RemoveAt(highScores.Count - 1);
            }

            // Sla de gegevens op in JSON
            SaveToJsonFile();
        }

        public void SaveToJsonFile()
        {
            string json = JsonSerializer.Serialize(highScores);

            // Schrijf de JSON-gegevens naar het bestand
            File.WriteAllText(JsonFilePath, json);
        }
    }
}