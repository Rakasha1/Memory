using Business;
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
        public int kolomrij { get; set; }

        public int AskRow()
        {
            int turnedCardRow = 0;

            bool bol = true;
            while (bol)
            {
                bol = false;
                try
                {
                    Console.WriteLine("Omdraaien: geef de rij om een kaart om te draaien.");
                    var turnedCardRowInsert = Console.ReadLine();
                    turnedCardRow = int.Parse(turnedCardRowInsert);
                    if (turnedCardRowInsert != null && turnedCardRow > 2)
                    {
                        Console.WriteLine($" geef een getal tussen de 1 en 2");
                        bol = true;
                    }

                }
                catch (FormatException exp)
                {
                    Console.WriteLine($"{exp.Message}: typ een getal in (int)");
                    bol = true;
                }
            }

            return turnedCardRow;
        }


        //Vraag om het kolomnummer
        public int AskColumn()
        {
            int turnedCardCol = 0;
            Game game = new Game();
            bool bol = true;
            while (bol)
            {
                bol = false;
                try
                {
                    Console.WriteLine("Omdraaien: geef de kolom om een kaart om te draaien.");
                    var turnedCardColInsert = Console.ReadLine();
                    turnedCardCol = int.Parse(turnedCardColInsert);
                    if (turnedCardColInsert != null && turnedCardCol > kolomrij)
                    {
                        Console.WriteLine($" geef een getal tussen de 1 en {kolomrij}");
                        bol = true;
                    }
                }
                catch (FormatException exp)
                {
                    Console.WriteLine($"{exp.Message}: typ een getal in (int)");
                    bol = true;

                }
            }


            return turnedCardCol;
        }
        public void Kolomrijen(int kolomrijen)
        {
            kolomrij = kolomrijen;

        }

    }
}