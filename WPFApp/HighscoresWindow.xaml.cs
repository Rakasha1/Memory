using Data;
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

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for HighscoresWindow.xaml
    /// </summary>
    public partial class HighscoresWindow : Window
    {
        public HighscoresWindow()
        {
            InitializeComponent();
            // Center the window on the screen
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            HighScoreManager highScoreManager = new HighScoreManager();

            //Test voor nu
            //highScoreManager.AddHighScore("fse", 45);
            //highScoreManager.AddHighScore("dsaf", 45);

            List<HighScore> highScores = highScoreManager.GetTopHighScores();
            foreach (HighScore highScore in highScores)
            {
                Label label = new Label
                {
                    //"5" is amount of cards
                    Content = highScore.UserName +"\t\t\t\t"+ highScore.Score + "\t\t\t\t" + "5",
                    FontSize = 11
                };

                //Voeg de label toe aan de stackPanel labelStack
                labelStack.Children.Add(label);
            }
        }

        private void StartMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
