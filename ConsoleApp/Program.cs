using Business;
using ConsoleApp;
using System.Diagnostics;
using Data;


//Start van het spel Memory
Console.WriteLine("Welkom bij memory!");
Game game = new Game();
AskFunctions askFunctions = new AskFunctions();



//Vraag om de naam

string naam = null;
while (string.IsNullOrWhiteSpace(naam))
{
    Console.WriteLine("Wat is je naam?");
    naam = Console.ReadLine();

}

//Bepaal het aantal paren kaarten

////initialiseren
List<Card> cards = new List<Card>();

int totalCouples = 0;

//
bool bol = true;
while (bol)
{
    try
    {
        Console.WriteLine("Hoeveel couples wil je hebben?");
        var totalCouplesInsert = Console.ReadLine();
        totalCouples = int.Parse(totalCouplesInsert);
        cards = game.TotalCouples(totalCouples);
        askFunctions.Kolomrijen(totalCouples);
        bol = false;
    }
    catch (FormatException exp)
    {
        Console.WriteLine($"er is een fout " + exp.Message);
        Console.WriteLine($"typ een getal");

    }
}

//Schud de kaarten
game.AllCards = game.ShuffleCard(cards);
game.PrintBoard(game.AllCards);

//Maak en start een stopwatch
Stopwatch sw = new Stopwatch();
sw.Start();

//Blijf spelen totdat alle paren gevonden zijn
while (true)
{
    if (totalCouples != game.CouplesDone)
    {
        //Vraag de rij en kolomnummer van 2 kaarten om die vervolgens te matchen
        int turnedCardRowCard1 = askFunctions.AskRow();
        int turnedCardColCard1 = askFunctions.AskColumn();
        Card card1 = game.TurnCard(turnedCardRowCard1, turnedCardColCard1);
        game.PrintBoard(game.AllCards);
        int turnedCardRowCard2 = askFunctions.AskRow();
        int turnedCardColCard2 = askFunctions.AskColumn();
        Card card2 = game.TurnCard(turnedCardRowCard2, turnedCardColCard2);
        game.PrintBoard(game.AllCards);
        game.Match(card1, card2);
    }
    else
    {
        break;
    }

    //Print het bord, hier staan alleen de gevonden paren op
    Console.WriteLine("Zo ziet het bord er nu uit");
    game.PrintBoard(game.AllCards);
}

//Stop de stopwatch en bereken de score
sw.Stop();
double timeElapsed = sw.Elapsed.TotalSeconds;
Console.WriteLine("Time elapsed = " + timeElapsed);


var score = game.CalculateScore(timeElapsed, game.AllCards.Length);
new HighScore(naam, score, totalCouples);
Console.WriteLine("Score = " + score);
Console.WriteLine();
Console.WriteLine("Highscores top 10");

HighScoreManager high = new HighScoreManager();
high.printHighScore();

//clear leader bord
//game.HighScoreManager.VerwijderHighscores(); 
// Verwijder alle highscores