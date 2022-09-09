using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage;
using Windows.Storage.Pickers;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UnderwaterHockeyTimer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
            rdoBuzzerSoundOne.Checked += RadioChanged;
            rdoBuzzerSoundOne.Unchecked += RadioChanged;
            rdoBuzzerSoundTwo.Checked += RadioChanged;
            rdoBuzzerSoundTwo.Unchecked += RadioChanged;
            rdoCustomBuzzerSound.Checked += RadioChanged;
            rdoCustomBuzzerSound.Unchecked += RadioChanged;
        }
        private void InitialiseElements()
        {
            nmbxCatchup.Value = GlobalVariables.CatchUpTime;
            nmbxGoalsBlack.Value = TeamBlack.Score;
            nmbxGoalsWhite.Value = TeamWhite.Score;
            chkSuddenDeath.IsChecked = GlobalVariables.SuddenDeath;
            chkTeamTimeout.IsChecked = GlobalVariables.TeamTimeEnabled;
            rdoBuzzerSoundOne.IsChecked = GlobalVariables.BuzzerSoundOneSet;
            rdoBuzzerSoundTwo.IsChecked = GlobalVariables.BuzzerSoundTwoSet;
            rdoCustomBuzzerSound.IsChecked = GlobalVariables.CustomBuzzerSoundSet;
        }
        private async void RadioChanged(object sender, object e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Name == "rdoBuzzerSoundOne" && rdoBuzzerSoundOne.IsChecked.Value)
            {

            }
            else if (rdo.Name == "rdoBuzzerSoundTwo" && rdoBuzzerSoundTwo.IsChecked.Value)
            {

            }
            else if (rdo.Name == "rdoCustomBuzzerSound" && rdoCustomBuzzerSound.IsChecked.Value)
            {
                var openPicker = new FileOpenPicker();
                openPicker.FileTypeFilter.Add(".mp3");
                openPicker.FileTypeFilter.Add(".wav");
                StorageFile file = await openPicker.PickSingleFileAsync();
                if (file != null)
                {
                    SoundControl.SetCustomBuzzer(file.Path.ToString()); ;
                }
                else
                {
                    MessageBox.Show("Setting buzzer operation failed. Reseting to default sound.");
                    rdoCustomBuzzerSound.IsChecked = false;
                    rdoBuzzerSoundOne.IsChecked = true;
                }
            }
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
