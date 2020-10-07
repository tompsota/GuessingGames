using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Drawing;
using BrushesColor = System.Windows.Media.Brushes;
using Image = System.Drawing.Image;

namespace GuessingGame
{
    /// <summary>
    /// Interaction logic for ShowImage.xaml
    /// </summary>
    
    public partial class ShowImage : UserControl
    {
        private MainWindow Root { get; }
        private List<string> AvailablePictureList { get; }
        private Image Picture { get; set; }
        private double CurrentPercentage { get; set; }
        private int _currentPoints = 20;
        private readonly int _maxLevel = 10;
        private readonly double _percentage = 0.05;
        private readonly List<(int, int)> _coordinates = new List<(int, int)> { (2, 1), (2, 2), (3, 1), (3, 2) };
        private readonly Random _rand = new Random();


        public ShowImage(MainWindow r, ref List<string> pictures)
        {
            InitializeComponent();
            Root = r;
            AvailablePictureList = pictures;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Root.ProceedButton.Visibility = Visibility.Hidden;
            var buttonsList = new List<Button>{Button2, Button3, Button4};
            await LoadPicture();
            AssignToButtons(buttonsList);
            buttonsList.Add(CorrectButton);
            DisplayButtons(Shuffle(buttonsList));
            HintButton.Background = BrushesColor.Gray;
        }

        /// <summary>
        /// loads the picture
        /// </summary>
        /// <returns></returns>
        private async Task LoadPicture()
        {
            string correctPicture = AvailablePictureList[_rand.Next(AvailablePictureList.Count)];
            AvailablePictureList.Remove(correctPicture);

            Picture = Image.FromFile(correctPicture);

            CurrentPercentage = _percentage;
            imgDynamic.Source = await Task.Run(() => Utils.ToBitmapImage(Utils.ResizeImage(Picture, CurrentPercentage)));
            CurrentPercentage += _percentage;
            imgDynamic.Stretch = Stretch.Uniform;
            CorrectButton.Content = Utils.GetTitleValue(correctPicture);                                                        
        }

        /// <summary>
        /// display buttons on correct coordinates
        /// </summary>
        /// <param name="buttonList"></param>
        private void DisplayButtons(List<Button> buttonList)
        {
            int current = 0;
            foreach (var b in buttonList)
            {
                Grid.SetRow(b, _coordinates[current].Item1);
                Grid.SetColumn(b, _coordinates[current].Item2);
                current++;
            }
        }

        /// <summary>
        /// Assign names to incorrect buttons
        /// </summary>
        /// <param name="buttonList"></param>
        private void AssignToButtons(List<Button> buttonList)
        {
            if (NotEnoughPictures())
            {
                return;
            }

            var picturesPoolList = new List<string>(AvailablePictureList);      // don't want to use same name twice

            foreach (var button in buttonList)
            {
                string incorrectPicture = picturesPoolList[_rand.Next(picturesPoolList.Count)];
                picturesPoolList.Remove(incorrectPicture);
                button.Content = Utils.GetTitleValue(incorrectPicture);
                button.Click += IncorrectButtonClick;
            }
        }

        /// <summary>
        /// shuffle buttons, so the correct one is at different position each time
        /// </summary>
        /// <param name="buttonList">all buttons</param>
        /// <returns>reordered buttons</returns>
        private List<Button> Shuffle(List<Button> buttonList)
        {
            Dictionary<int, Button> d = new Dictionary<int, Button>();
            foreach (var b in buttonList)
            {
                d.Add(_rand.Next(), b);
            }
            return d.OrderBy(a => a.Key).Select(b => b.Value).ToList();
        }

        /// <summary>
        /// Click on correct button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CorrectButton_OnClick(object sender, RoutedEventArgs e)
        {
            AnswerButtonClick(CorrectButton, "Correct!", BrushesColor.Green);

            if (_currentPoints > 0)
            {
                Root.Player.Points += _currentPoints;
                Root.AddPointsForRound();
                SqliteDataAccess db = new SqliteDataAccess();
                db.UpdatePlayerPoints(Root.Player);
            }

            Root.CurrentSetPoints += _currentPoints;
            Root.CorrectRounds++;
            HandleLevelProgress();

        }

        private void IncorrectButtonClick(object sender, EventArgs e)
        {
            AnswerButtonClick(sender as Button, "Incorrect :(", BrushesColor.Crimson);
            CorrectButton.Background = BrushesColor.Green;
            HandleLevelProgress();
        }

        /// <summary>
        /// determine if there is not enough pictures to continue in the game
        /// </summary>
        /// <returns></returns>
        private bool NotEnoughPictures()
        {
            if (AvailablePictureList.Count > 2)
            {
                return false;
            }

            if (MessageBox.Show("Sorry, no more pictures for you :(", "", MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                Root.BackToPicSetButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            return true;
        }

        /// <summary>
        /// set button color and message after clicking on the picture
        /// </summary>
        /// <param name="pressedButton"></param>
        /// <param name="message">message to be displayed</param>
        /// <param name="color">desired color of button</param>
        private void AnswerButtonClick(Control pressedButton, string message, System.Windows.Media.Brush color)
        {
            pressedButton.Background = color;
            GuessResultText.Text = message;
            GuessResultText.Visibility = Visibility.Visible;
            imgDynamic.Source = Utils.ToBitmapImage(new Bitmap(Picture));
        }

        /// <summary>
        /// determine if the played level was the last
        /// </summary>
        private void HandleLevelProgress()
        {
            if (Root.CurrentRound != _maxLevel)
            {
                Root.ProceedButton.Visibility = Visibility.Visible;
            }
            else
            {
                if (MessageBox.Show($"Picture set completed! \nSet score: {Root.CorrectRounds} / {_maxLevel} \n" +
                                               $"Points: {Root.CurrentSetPoints}", "", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    Root.BackToPicSetButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
            }
        }

        private async void HintButton_OnClick(object sender, RoutedEventArgs e)
        {
            imgDynamic.Source = await Task.Run(() => Utils.ToBitmapImage(Utils.ResizeImage(Picture, CurrentPercentage)));
            CurrentPercentage += _percentage;
            _currentPoints -= 4;
            if (_currentPoints <= 4)
            {
                HintButton.IsEnabled = false;
            }
        }
    }
}
