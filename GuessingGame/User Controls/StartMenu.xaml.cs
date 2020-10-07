using System.Windows;
using System.Windows.Controls;


namespace GuessingGame
{
    /// <summary>
    /// Interaction logic for StartMenu.xaml
    /// </summary>
    public partial class StartMenu : UserControl
    {
        private MainWindow Root { get; }

        public StartMenu(MainWindow r)
        {
            InitializeComponent();
            Root = r;
        }

        /// <summary>
        /// after choosing player name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlayerNameBox.Text == "")
            {
                MessageBox.Show("Please enter a name!", "", MessageBoxButton.OK);
                return;

            }

            Root.Player = new Player {PlayerName = PlayerNameBox.Text, Points = 0};
            
            SqliteDataAccess db = new SqliteDataAccess();
            if (db.GetPlayerPointsOrDefault(Root.Player) != -1)
            {
                if (MessageBox.Show("Please choose a different name!", "", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    PlayerNameBox.Text = "";
                    return;
                }
            }

            db.InsertPlayer(Root.Player);
            Root.ChoosePlaySet = new ChoosePlaySet(Root);
            Root.contentControl.Content = Root.ChoosePlaySet;
            Root.BackToMenuButton.Visibility = Visibility.Visible;
            Root.QuitButton.Visibility = Visibility.Hidden;
        }
    }
}
