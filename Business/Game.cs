using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Business
{
    public class Game
    {
        public int CouplesDone { get; set; } // hoeveel paren aan kaarten er juist omgedraaid zijn
        public int Tries { get; set; } // hoeveel keren er geprobeerd is 2 kaarten te matchen
        public List<Card>[,] AllCards { get; set; }
        public string Name { get; set; }
        public HighScoreManager HighScoreManager { get; set; } = new HighScoreManager();


        public List<Card> TotalCouples(int couples)//Bepaalt hoeveelheid paren er in het spel zijn
        {
            List<Card> cardList = new List<Card>();
            int singles = 0;

            for (int i = 0; i < couples; i++)
            {
                cardList.Add(new Card(singles++, i)); // maak de eerste kaart met dit id aan
                cardList.Add(new Card(singles++, i)); // maak de paar (kopie) van deze kaart aan
            }
            return cardList;  //Vul lijst met kaarten, ieder uniek kaart komt er 2 keer in
        }
        
        public bool Match(Card card1, Card card2)
        {
            Tries++;//Pogingen wordt verhoogd
            if (card1 != null && card2 != null)
            {
                if (card1.Value == card2.Value)//Vergelijk de Values van 2 kaarten
                {
                    CouplesDone++;//Paren die compleet zijn wordt verghoogt als beide ID's gelijk zijn
                    return true;
                }
                card1.Turned = false;
                card2.Turned = false;
            }
            return false;
        }

        //Draai een kaart om zodat je de ID ziet op het bord
        public Card TurnCard(int row, int column)
        {
            try
            {
                List<Card> cardInList = AllCards[row - 1, column - 1];
                Card card = cardInList[0];
                card.Turned = true;
                return card;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Deze kaart ligt niet in het spel, probeer het opnieuw.");
                return null;
            }
        }

        //Zet de kaarten op random posities neer op het bord
        public List<Card>[,] ShuffleCard(List<Card> cards)
        {
            List<Card>[,] shuffledCards = new List<Card>[2, cards.Count / 2];
            Random random = new Random();

            // Een lijst met alle beschikbare plaatsen voor kaarten op het bord
            List<(int, int)> availableIndices = new List<(int, int)>();

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < cards.Count / 2; j++)
                {
                    availableIndices.Add((i, j));
                }
            }

            // Nu gaan we de kaarten willekeurig verdelen
            foreach (Card card in cards)
            {
                if (availableIndices.Count == 0)
                {
                    // Als er geen beschikbare plekken meer zijn, stop met verdelen
                    break;
                }

                int randomIndex = random.Next(0, availableIndices.Count);
                (int row, int col) = availableIndices[randomIndex];
                shuffledCards[row, col] ??= new List<Card>();
                shuffledCards[row, col].Add(card);

                // Verwijder het gebruikte index paar
                availableIndices.RemoveAt(randomIndex);
            }

            return shuffledCards;
        }

        //Bereken de totale score
        //timeElapsed komt van de stopwatch
        public double CalculateScore(double timeElapsed, string naam, int totalCouples) ///////////////////////veranderd
        {
            if (Tries != 0)
            {

                double number = Math.Pow(AllCards.Length, 2);
                number = (number / (timeElapsed * Tries)) * 1000;
                //HighScoreManager highScoreManager = new HighScoreManager();
                new HighScore(naam, number, totalCouples, HighScoreManager);
                //HighScoreManager = highScoreManager;
                return number;
            }
            return 0;
        }

        //Print het bord uit met rij- en kolomm-nummers
        public void PrintBoard(List<Card>[,] shuffledCards)
        {
            int numRows = shuffledCards.GetLength(0);
            int numCols = shuffledCards.GetLength(1);

            // Print de kolomnummers aan de bovenkant
            Console.Write("      ");
            for (int col = 0; col < numCols; col++)
            {
                Console.Write($"{col + 1}     ");
            }
            Console.WriteLine();

            //Print de rijnummers
            for (int row = 0; row < numRows; row++)
            {
                PrintCards(row, numCols, shuffledCards);
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        //Print de kaarten van het bord
        //Als een kaart omgedraaid is wordt het ID getoond anders een ?
        public void PrintCards(int row, int numCols, List<Card>[,] shuffledCards)
        {
            Console.Write($"{row + 1}     ");

            for (int col = 0; col < numCols; col++)
            {
                List<Card> cards = shuffledCards[row, col];

                foreach (Card card in cards)
                {
                    if (card.Turned)
                    {
                        Console.Write(card.Value + "     ");
                    }
                    else
                    {
                        Console.Write("?     ");
                    }

                }
            }
        }
    }
}
