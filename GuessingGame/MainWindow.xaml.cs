using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;


namespace GuessingGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Base window
    /// </summary>
    public partial class MainWindow : Window
    {
        public StartMenu StartMenu { get; set; }
        public ChoosePlaySet ChoosePlaySet { get; set; }
        public Player Player { get; set; }
        public string PicturePath { get; set; }
        public bool NewDataSetLoaded { get; set; }
        public string CurrentPicSet { get; set; }
        public readonly string BasePath = $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}" +
                                          $"PicSets{Path.DirectorySeparatorChar}";
        public int CurrentSetPoints;
        public int CurrentRound;
        public int CorrectRounds;
        private List<string> _availablePictureList;

        public MainWindow()
        {
            InitializeComponent();
            StartMenu = new StartMenu(this);
            contentControl.Content = StartMenu;
            NewDataSetLoaded = false;
            PlayerNameText.Visibility = Visibility.Hidden;
            PlayerPoints.Visibility = Visibility.Hidden;
            SetButtonsVisibility(Visibility.Hidden);
        }

        /// <summary>
        /// proceed to next round
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Proceed_Click(object sender, RoutedEventArgs e)
        {
            LoadPictureList();
            var img = new ShowImage(this, ref _availablePictureList);
            contentControl.Content = img;
         
            PlayerNameText.Text = Player.PlayerName;
            CurrentRound++;
            CurrentLevelInfoText.Text = "Round: " + CurrentRound + "/10";

            ProceedButton.Visibility = Visibility.Visible;
            PlayerNameText.Visibility = Visibility.Visible;
            CurrentLevelInfoText.Visibility = Visibility.Visible;
            PlayerPoints.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// adds earned points for round
        /// </summary>
        public void AddPointsForRound()
        {
            PlayerPoints.Text = "Points: " + Player.Points;
            PlayerPoints.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// loads new picture addresses if new dataset was chosen
        /// </summary>
        private void LoadPictureList()
        {
            if (NewDataSetLoaded)
            {
                PicturePath = BasePath + CurrentPicSet + $"{ Path.DirectorySeparatorChar}";
                _availablePictureList = Directory.GetFiles(PicturePath, "*.jpg").ToList();
                NewDataSetLoaded = false;
            }
        }

        private void BackToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you really want to go back to menu? \n" +
                                "You won't be able to continue with this name.", "",
                MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }

            contentControl.Content = StartMenu;
            ResetPoints();
            Player.Points = 0;
            ChoosePlaySet.LoadLeaderboard();

            SetButtonsVisibility(Visibility.Hidden);
            PlayerNameText.Visibility = Visibility.Hidden;
            PlayerPoints.Visibility = Visibility.Hidden;
            QuitButton.Visibility = Visibility.Visible;
        }

        private void BackToPicSetButton_Click(object sender, RoutedEventArgs e)
        {
            if (_availablePictureList.Count > 2 && CurrentRound != 10)
            {
                if (MessageBox.Show("Do you really want to go back to picture sets? \n" +
                                    "You won't be able to continue with this set.", "",
                    MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    return;
                }
            }

            contentControl.Content = ChoosePlaySet;
            ResetPoints();
            ChoosePlaySet.LoadLeaderboard();
            SetButtonsVisibility(Visibility.Hidden);
            BackToMenuButton.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Quit the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// sets button visibility
        /// </summary>
        /// <param name="visibility"></param>
        private void SetButtonsVisibility(Visibility visibility)
        {
            CurrentLevelInfoText.Visibility = visibility;
            ProceedButton.Visibility = visibility;
            BackToMenuButton.Visibility = visibility;
            BackToPicSetButton.Visibility = visibility;
        }

        /// <summary>
        /// reset player stats to 0
        /// </summary>
        private void ResetPoints()
        {
            CurrentRound = 0;
            CurrentSetPoints = 0;
            CorrectRounds = 0;
        }
    }
}
