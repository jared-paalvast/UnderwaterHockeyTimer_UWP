using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace UnderwaterHockeyTimer
{
    public sealed partial class ScorerPage : Page
    {
        public ScorerPage()
        {
            this.InitializeComponent();
            StartupMethod();
            InitialiseElements();
        }
        private void StartupMethod()
        {
            DispatcherTimer textTimer = new DispatcherTimer();
            textTimer.Interval = TimeSpan.FromMilliseconds(1);
            textTimer.Tick += OnTimed;
            textTimer.Start();
        }
        private void OnTimed(object sender, object e)
        {
            BackColourUpdate();
            GameTextUpdate();            
        }
        private void InitialiseElements()
        {
            btnGoalBlack.Click += ButtonTrigger;
            btnGoalWhite.Click += ButtonTrigger;
            btnRefTimeout.Click += ButtonTrigger;
            btnTimeBlack.Click += ButtonTrigger;
            btnTimeWhite.Click += ButtonTrigger;
            BackgroundGrid.SizeChanged += GridHandle;
            BackgroundGrid.Loaded += GridHandle;
        }
        private void GridHandle(object sender, object e)
        {
            TextSizeUpdate();
            ButtonsEnabled();
        }
        private void ButtonTrigger(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Name)
            {
                case "btnGoalBlack":
                    TeamBlack.Goal();
                    break;
                case "btnGoalWhite":
                    TeamWhite.Goal();
                    break;
                case "btnRefTimeout":
                    GlobalMethods.RefTime();
                    break;
                case "btnTimeBlack":
                    TeamBlack.TeamTime();
                    break;
                case "btnTimeWhite":
                    TeamWhite.TeamTime();
                    break;
                default:
                    break;
            }
        }
        private void BackColourUpdate()
        {
            this.BackgroundGrid.Background = new SolidColorBrush(GlobalVariables.Colour);
        }
        private void ButtonsEnabled()
        {
            if (GlobalVariables.TeamTimeEnabled)
            {
                if (TeamWhite.TeamTimeTaken)
                {
                    btnTimeWhite.IsEnabled = false;
                }
                else
                {
                    btnTimeWhite.IsEnabled = true;
                }

                if (TeamBlack.TeamTimeTaken)
                {
                    btnTimeBlack.IsEnabled = false;
                }
                else
                {
                    btnTimeBlack.IsEnabled = true;
                }
            }
            else
            {
                btnTimeBlack.IsEnabled = false;
                btnTimeWhite.IsEnabled = false;
            }
        }
        private void TextSizeUpdate()
        {
            try
            {
                txtScoreBlack.FontSize = FontSizeCalc(rtngScoreBlack.ActualHeight, rtngScoreBlack.ActualWidth, 2.5);
                txtScoreWhite.FontSize = FontSizeCalc(rtngScoreWhite.ActualHeight, rtngScoreWhite.ActualWidth, 2.5);
                txtCourtTime.FontSize = FontSizeCalc(rtngCourtTime.ActualHeight, rtngCourtTime.ActualWidth, 16);
                txtGameTime.FontSize = FontSizeCalc(rtngGameTime.ActualHeight, rtngGameTime.ActualWidth, 6);
                txtGameText.FontSize = FontSizeCalc(rtngGameText.ActualHeight, rtngGameText.ActualWidth, 16);
                txtGameNo.FontSize = FontSizeCalc(rtngGameNo.ActualHeight, rtngGameNo.ActualWidth, 10);
                btnGoalBlack.FontSize = FontSizeCalc(btnGoalBlack.ActualHeight, btnGoalBlack.ActualWidth, 10);
                btnGoalWhite.FontSize = FontSizeCalc(btnGoalWhite.ActualHeight, btnGoalWhite.ActualWidth, 10);
                btnTimeBlack.FontSize = FontSizeCalc(btnTimeBlack.ActualHeight, btnTimeBlack.ActualWidth, 12);
                btnTimeWhite.FontSize = FontSizeCalc(btnTimeWhite.ActualHeight, btnTimeWhite.ActualWidth, 12);
                btnRefTimeout.FontSize = FontSizeCalc(btnRefTimeout.ActualHeight, btnRefTimeout.ActualWidth, 10);
            }
            catch (Exception) { }
        }
        private void GameTextUpdate()
        {
            txtCourtTime.Text = GlobalMethods.CourtTime();
            txtGameTime.Text = GlobalMethods.GameTime();
            txtScoreBlack.Text = TeamBlack.Score.ToString();
            txtScoreWhite.Text = TeamWhite.Score.ToString();
            txtGameText.Text = GlobalVariables.GameText;
            txtGameNo.Text = GlobalMethods.GameNumber();
        }
        private double FontSizeCalc(double width, double height, double division)
        {
            return ((width + height) / division);
        }
    }
}
