using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UnderwaterHockeyTimer
{
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
            InitialiseEvents();
            InitialiseElements();
        }
        private void InitialiseEvents()
        {
            btnUpdateCatchUpTime.Click += ButtonClicked;
            chkSuddenDeath.Checked += CheckChanged;
            chkSuddenDeath.Unchecked += CheckChanged;
            chkTeamTimeout.Checked += CheckChanged;
            chkTeamTimeout.Unchecked += CheckChanged;
            chkCapScore.Checked += CheckChanged;
            chkCapScore.Unchecked += CheckChanged;
            nmbxGoalsBlack.ValueChanged += ValueChanged;
            nmbxGoalsWhite.ValueChanged += ValueChanged;            
        }
        private void InitialiseElements()
        {
            nmbxCatchup.Value = GlobalVariables.CatchUpTime;
            nmbxGoalsBlack.Value = TeamBlack.Score;
            nmbxGoalsWhite.Value = TeamWhite.Score;
            chkSuddenDeath.IsChecked = GlobalVariables.SuddenDeath;
            chkTeamTimeout.IsChecked = GlobalVariables.TeamTimeEnabled;            
        }        
        private void ValueChanged(object sender, object e)
        {
            NumberBox numbox = (NumberBox)sender;
            int score;
            switch (numbox.Name)
            {
                case "nmbxGoalsBlack":
                    score = (int)nmbxGoalsBlack.Value;
                    if (score < 0)
                    {
                        MessageBox.Show("Minimum ammount of goals is 0");
                        nmbxGoalsBlack.Value = 0;
                    }
                    else
                    {
                        TeamBlack.Score = score;
                    }

                    break;
                case "nmbxGoalsWhite":
                    score = (int)nmbxGoalsWhite.Value;
                    if (score < 0)
                    {
                        MessageBox.Show("Minimum ammount of goals is 0");
                        nmbxGoalsWhite.Value = 0;
                    }
                    else
                    {
                        TeamWhite.Score = score;
                    }

                    break;
                default:
                    break;
            }
        }
        private void ButtonClicked(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Name == "UpdateCatchUpTime")
            {
                GlobalVariables.CatchUpTime = (int)nmbxCatchup.Value;
            }
        }
        private void CheckChanged(object sender, RoutedEventArgs e)
        {
            CheckBox chkbx = (CheckBox)sender;
            switch (chkbx.Name)
            {
                case "chkSuddenDeath":
                    GlobalVariables.SuddenDeath = chkSuddenDeath.IsChecked.Value;
                    break;
                case "chkTeamTimeout":
                    GlobalVariables.TeamTimeEnabled = chkTeamTimeout.IsChecked.Value;
                    break;
                case "chkCapScore":
                    GlobalVariables.CapScoreEnabled = chkCapScore.IsChecked.Value;
                    break;
                default:
                    break;
            }
        }
    }
}
