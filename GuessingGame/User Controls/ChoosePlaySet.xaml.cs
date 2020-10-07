using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GuessingGame
{
    /// <summary>
    /// Interaction logic for GameRound.xaml
    /// </summary>
    public partial class ChoosePlaySet : UserControl
    {
        private MainWindow Root { get; }
        private List<Player> _players = new List<Player>();
        private List<string> _subdirs;


        public ChoosePlaySet(MainWindow r)
        {
            InitializeComponent();
            Root = r;
            LoadSetNames();
            PlayerWelcomeText.Text += Environment.NewLine + Root.Player.PlayerName + "!";

            LoadLeaderboard();
        }

        /// <summary>
        /// load the leaderboard
        /// </summary>
        public void LoadLeaderboard()
        {
            SqliteDataAccess db = new SqliteDataAccess();

            _players = db.GetAllPlayers();

            PlayersLeaderboardGrid.ItemsSource = _players;
            PlayersLeaderboardGrid.ColumnWidth = 160;
            PlayersLeaderboardGrid.RowHeight = 30;
            PlayersLeaderboardGrid.RowHeaderWidth = 20;
            PlayersLeaderboardGrid.CanUserReorderColumns = false;
            PlayersLeaderboardGrid.CanUserSortColumns = false;
            PlayersLeaderboardGrid.CanUserResizeColumns = false;
            PlayersLeaderboardGrid.CanUserResizeRows = false;
            PlayersLeaderboardGrid.CanUserDeleteRows = false;
            PlayersLeaderboardGrid.CanUserAddRows = false;
        }

        /// <summary>
        /// add row number to leaderboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        /// <summary>
        /// set better leaderboard column names
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
            => e.Column.Header = ((PropertyDescriptor)e.PropertyDescriptor).DisplayName;

        /// <summary>
        /// load available picture sets
        /// </summary>
        private void LoadSetNames()
        {
            _subdirs = Directory.GetDirectories(Root.BasePath).Select(System.IO.Path.GetFileName).ToList();
            PicSetComboBox.ItemsSource = _subdirs;

        }

        /// <summary>
        /// Remove currently chosen picture set from available to play, so the set can be played only once
        /// </summary>
        private void UpdateAvailablePicSets()
        {
            _subdirs.Remove(PicSetComboBox.Text);
            PicSetComboBox.Items.Refresh();
        }

        /// <summary>
        /// confirm chosen picture set
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (PicSetComboBox.Text == "")
            {
                MessageBox.Show("Please select a picture set!", "", MessageBoxButton.OK);
            }
            else
            {
                Root.CurrentPicSet = PicSetComboBox.Text;
                Root.NewDataSetLoaded = true;
                UpdateAvailablePicSets();
                Root.ProceedButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                Root.BackToMenuButton.Visibility = Visibility.Visible;
                Root.BackToPicSetButton.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// clear the DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearDBButton_Click(object sender, RoutedEventArgs e)
        {
            SqliteDataAccess db = new SqliteDataAccess();
            db.ClearDB(Root.Player);
            ClearDBButton.IsEnabled = false;
            LoadLeaderboard();
        }
    }
}
