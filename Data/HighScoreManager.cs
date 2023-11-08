using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Data
{
    public class HighScoreManager
    {
        
        private const string JsonFilePath = "C:\\Users\\gertv\\OneDrive\\Windesheim\\Jaar2\\OOSD\\C#_Programmeren\\Aftekenopdrachten\\Memory\\Data\\highscores.json"; // pas dit aan naar waar je eigen data mapje staat
        private const int MaxHighScores = 10;

        private List<HighScore> highScores = new List<HighScore>();

        public HighScoreManager()
        {
            if (!File.Exists(JsonFilePath)) // Voeg een controle toe om te zien of het bestand al bestaat
            {
                SaveToJsonFile(); // Als het bestand niet bestaat, maak het dan aan
            }
            else
            {
                // Als het JSON-bestand al bestaat, laad het
                string json = File.ReadAllText(JsonFilePath);
                highScores = JsonSerializer.Deserialize<List<HighScore>>(json);
            }
        }

        public List<HighScore> GetTopHighScores()
        {
            highScores = highScores.OrderByDescending(highscore => highscore.Score).ToList(); // Sorteer de lijst op score in aflopende volgorde
            return highScores.Take(MaxHighScores).ToList();
        }

        public void printHighScore()
        {
            for (int i = 0; i < highScores.Count; i++)
            {
                Console.WriteLine($"{i + 1}.    Gebruiker: {highScores[i].UserName}, Score: {highScores[i].Score}, Aantal kaarten: {highScores[i].AantalKaarten}");
            }
        }

        public void AddHighScore(string userName, double scoreValue, int aantalKaarten)
        {
            //nieuwe score toevoegen aan de lijst highscore
            highScores.Add(new HighScore { UserName = userName, Score = scoreValue, AantalKaarten = aantalKaarten *2 });

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
        public void VerwijderHighscores()
        {
            // Maak de lijst met highscores leeg
            highScores.Clear();

            // Sla de lege lijst op in JSON om het bestaande bestand te overschrijven
            SaveToJsonFile();
        }
    }
}