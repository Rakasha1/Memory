using System;
using System.Collections.Generic;
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
using Business;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public Game MainGame {  get; set; }
        public RegisterWindow()
        {
            InitializeComponent();
            // Center the window on the screen
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //Maak een nieuwe game aan
            MainGame = new Game();
        }

        private void GameStartButton_Click(object sender, RoutedEventArgs e)
        {
            //Name en couples bepalen
            MainGame.Name = NaamTextBox.Text;
            try
            {
                //Aantal paren bepalen
                int couples = Int32.Parse(AantalParenTextBox.Text);
                var lijst = MainGame.TotalCouples(couples);

                //Door naar het spel gaan
                //MessageBox.Show(MainGame.Name.ToString(), lijst.Count().ToString()); //Check of naam wordt meegegeven aan Game
                GameWindow gameWindow = new GameWindow();
                gameWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vul een getal in! " + ex.GetType() + ex.Message);
            }
            
        }

       
    }
}
