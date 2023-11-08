using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class AskFunctions
    {
        //Vraag om het rijnummer
        public int AskRow()
        {
            int turnedCardRow = 0;
            Console.WriteLine("Omdraaien: geef de rij om een kaart om te draaien.");
            var turnedCardRowInsert = Console.ReadLine();
            if (turnedCardRowInsert != null)
            {
                turnedCardRow = int.Parse(turnedCardRowInsert);
            }
            else
            {
                Console.WriteLine("we hate you");
            }
            return turnedCardRow;
        }

        //Vraag om het kolomnummer
        public int AskColumn()
        {
            int turnedCardCol = 0;
            Console.WriteLine("Omdraaien: geef de kolom om een kaart om te draaien.");
            var turnedCardColInsert = Console.ReadLine();
            if (turnedCardColInsert != null)
            {
                turnedCardCol = int.Parse(turnedCardColInsert);
            }
            else
            {
                Console.WriteLine("we hate you");
            }
            return turnedCardCol;
        }
    }
}
