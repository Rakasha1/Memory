using Business;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFApp
{
    public partial class GameWindow : Window
    {
        private List<string> ImageSources; // Lijst van afbeeldingsbronnen
        Game WPFgame = new Game();
        private List<Card> ShuffledCards; // Lijst van kaarten na het schudden
        private bool OneIsTurned = false;
        private Card TurnedCard;
        private Button TurnedButton;


        public GameWindow()
        {
            InitializeComponent();
            // Center the window on the screen
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            int rowCount = 5; // Pas dit aan op basis van de gebruikersinvoer.
            int columnCount = 2; // Pas dit aan op basis van de gebruikersinvoer.

            //Zoek de directory van de solution op
            string domainDirectory = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
            string solutionDirectory = System.IO.Path.GetFullPath(System.IO.Path.Combine(domainDirectory, @"..\..\..\"));
            //MessageBox.Show(domainDirectory);
            ImageSources = new List<string> // dit moet komen op basis van de gebruikersinvoer
            {
            solutionDirectory + "Data\\Pictures\\ezel1.jpg",
            solutionDirectory + "Data\\Pictures\\ezel2.jpg",
            solutionDirectory + "Data\\Pictures\\ezel3.jpg",
            solutionDirectory + "Data\\Pictures\\ezel4.jpg",
            solutionDirectory + "Data\\Pictures\\ezel5.jpg",
            solutionDirectory + "Data\\Pictures\\ezel6.jpg",
            solutionDirectory + "Data\\Pictures\\ezel7.jpg",
            solutionDirectory + "Data\\Pictures\\ezel8.jpg",
            solutionDirectory + "Data\\Pictures\\ezel9.jpg",
            solutionDirectory + "Data\\Pictures\\ezel10.jpg",
            solutionDirectory + "Data\\Pictures\\ezel11.jpg",
            };

            var cards = WPFgame.TotalCouples(5);
            //MessageBox.Show(cards.Count().ToString());
            var cardsCompleted = GiveCardsPictures(cards);
            ShuffledCards = ShuffleCards(cardsCompleted);

            MemoryKaartenToevoegen(rowCount, columnCount);
        }

        //Methode om aan de kaarten een image toe te wijzen
        private List<Card> GiveCardsPictures(List<Card> cards)
        {
            for (int i = 0; i < cards.Count(); i = i + 2)
            {
                cards[i].ImagePath = ImageSources[i];
                cards[i + 1].ImagePath = ImageSources[i];
            }
            return cards;
        }

        // Functie om kaarten te schudden
        private List<Card> ShuffleCards(List<Card> cards)
        {
            Random random = new Random();
            List<Card> shuffled = new List<Card>(cards);
            int n = shuffled.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                var value = shuffled[k];
                shuffled[k] = shuffled[n];
                shuffled[n] = value;
            }
            return shuffled;
        }


        // ER MOET NOG CODE KOMEN WAARIN HET AANTAL KAARTEN WORDT VERWERKT
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            //Krijg index van
            Button button = (Button)sender;
            var nummer = button.Name.Substring(button.Name.Length - 1);
            int getalIndex = int.Parse(nummer);

            if (OneIsTurned)
            {
                TurnButton(sender);
                if (!(WPFgame.Match(TurnedCard, ShuffledCards[getalIndex])))
                {
                    System.Threading.Thread.Sleep(1000);
                    button.Content = "?";
                    TurnedButton.Content = "?";
                }
                OneIsTurned = false;
            }
            else
            {
                TurnButton(sender);
                TurnedButton = (Button)sender;
            }

            //Als alle couples gevonden zijn, door naar einde scherm
            if (WPFgame.CouplesDone == 5)
            {
                EindeWindow eindeWindow = new EindeWindow();
                eindeWindow.Show();
                this.Close();
            }
        }

        private void TurnButton(object sender)
        {
            Button button = (Button)sender;

            // Zoek de bijbehorende afbeeldingsbron op basis van de rij en kolomindex
            var nummer = button.Name.Substring(button.Name.Length - 1);
            int imageIndex = int.Parse(nummer);
            var tempCard = ShuffledCards[imageIndex];
            if (!OneIsTurned)
            {
                TurnedCard = tempCard;
            }

            if (imageIndex >= 0 && imageIndex < ShuffledCards.Count)
            {

                // Maak een nieuwe Image aan met de bijbehorende afbeeldingsbron
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(tempCard.ImagePath));

                // Vervang de inhoud van de knop door de Image
                button.Content = image;

                // Geef aan dat de kaart omgedraaid is
                tempCard.Turned = true;
                OneIsTurned = true;

            }
        }


        private void MemoryKaartenToevoegen(int rowCount, int columnCount)
        {
            // maak het scherm leeg
            MemoryGrid.RowDefinitions.Clear();
            MemoryGrid.ColumnDefinitions.Clear();

            //print het aantal kollomen en rijen die opgegeven zijn
            for (int i = 0; i < rowCount; i++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                MemoryGrid.RowDefinitions.Add(rowDefinition);
            }

            for (int i = 0; i < columnCount; i++)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                MemoryGrid.ColumnDefinitions.Add(columnDefinition);
            }
            int cardid = 0;
            for (int row = 0; row < rowCount; row++)
            {
                // maak elk vakje een button en maak de 'achterkant' van onze kaarten een ?
                for (int column = 0; column < columnCount; column++)
                {
                    Button button = new Button();
                    button.Name = $"Button_{ShuffledCards[cardid].ID.ToString()}";
                    cardid++;

                    button.Content = "?";

                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, column);
                    MemoryGrid.Children.Add(button);

                    button.Click += Button_Click;

                }
            }
        }


    }
}
