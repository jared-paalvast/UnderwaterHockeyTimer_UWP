using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using Microsoft.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UnderwaterHockeyTimer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TimeSetup : Page
    {
        public static bool firstGameStarted = false;
        public TimeSetup()
        {
            this.InitializeComponent();
            InitialiseElements();
            InitialiseEvents();
        }
        private void InitialiseElements()
        {
            nmbxGameHalfLength.Value = GlobalVariables.HalfLength;
            nmbxGameHalves.Value = GlobalVariables.AmmountOfHalves;
            nmbxGameNoIncrement.Value = GlobalVariables.NoIncrement;
            nmbxGameNoStart.Value = GlobalVariables.GameNumber;
            nmbxHalfTimeLength.Value = GlobalVariables.HalfTimeLength;
            nmbxTimeBetweenMax.Value = GlobalVariables.IntervalMax;
            nmbxTimeBetweenMin.Value = GlobalVariables.IntervalMin;
        }
        private void InitialiseEvents()
        {
            nmbxGameHalfLength.ValueChanged += ValueChanged;
            nmbxGameHalves.ValueChanged += ValueChanged;
            nmbxGameNoIncrement.ValueChanged += ValueChanged;
            nmbxGameNoStart.ValueChanged += ValueChanged;
            nmbxHalfTimeLength.ValueChanged += ValueChanged;
            nmbxTimeBetweenMax.ValueChanged += ValueChanged;
            nmbxTimeBetweenMin.ValueChanged += ValueChanged;
            tmpkFirstGame.TimeChanged += TimeChanged;
        }
        private void ValueChanged(object sender, object e)
        {
            NumberBox numbox = (NumberBox)sender;
            int temp, temp2;
            try
            {
                switch (numbox.Name)
                {
                    case "nmbxGameHalfLength":
                        temp = (int)nmbxGameHalfLength.Value;
                        if (temp < 1)
                        {
                            MessageBox.Show("Game half must be at least 1 minute long.");
                            nmbxGameHalfLength.Value = 1;
                        }
                        else
                        {
                            GlobalVariables.HalfLength = temp;
                        }

                        break;
                    case "nmbxGameHalves":
                        temp = (int)nmbxGameHalves.Value;
                        if (temp < 1 || temp > 2)
                        {
                            MessageBox.Show("There can be only 1 or 2 halves.");
                            nmbxGameHalves.Value = 1;
                        }
                        else
                        {
                            GlobalVariables.AmmountOfHalves = temp;
                        }

                        break;
                    case "nmbxGameNoIncrement":
                        temp = (int)nmbxGameNoIncrement.Value;
                        if (temp < 1)
                        {
                            MessageBox.Show("Game number must increment by at least 1");
                            nmbxGameNoIncrement.Value = 1;
                        }
                        else
                        {
                            GlobalVariables.NoIncrement = temp;
                        }

                        break;
                    case "nmbxGameNoStart":
                        temp = (int)nmbxGameNoStart.Value;
                        if (temp < 1)
                        {
                            MessageBox.Show("Game number must start with a minimum value of 1");
                            nmbxGameNoStart.Value = 1;
                        }
                        else
                        {
                            GlobalVariables.GameNumber = temp;
                        }

                        break;
                    case "nmbxHalfTimeLength":
                        temp = (int)nmbxHalfTimeLength.Value;
                        if (temp < 1)
                        {
                            MessageBox.Show("Half time length must be at least 1 minute long.");
                            nmbxHalfTimeLength.Value = 1;
                        }
                        else
                        {
                            GlobalVariables.HalfTimeLength = temp;
                        }

                        break;
                    case "nmbxTimeBetweenMax":
                        temp = (int)nmbxTimeBetweenMax.Value;
                        temp2 = (int)nmbxTimeBetweenMin.Value;
                        if (temp <= temp2)
                        {
                            MessageBox.Show("The maximum time between games must be at least 1 minute longer than the minimum.");
                            nmbxTimeBetweenMax.Value = temp2 + 1;
                        }
                        else
                        {
                            GlobalVariables.IntervalMax = temp;
                        }

                        break;
                    case "nmbxTimeBetweenMin":
                        temp = (int)nmbxTimeBetweenMin.Value;
                        temp2 = (int)nmbxTimeBetweenMax.Value;
                        if (temp < 1 || temp >= temp2)
                        {
                            MessageBox.Show("The minimum time between games must be at least 1 minute long.");
                            nmbxTimeBetweenMin.Value = temp2 - 1;
                        }
                        else
                        {
                            GlobalVariables.IntervalMin = temp;
                        }

                        break;
                    default:
                        break;
                }
            }
            catch { }
        }
        private async void TimeChanged(object sender, object e)
        {
            TimePicker tmpk = (TimePicker)sender;
            switch (tmpk.Name)
            {
                case "tmpkFirstGame":
                    if (firstGameStarted)
                    {
                        MessageDialog message = new MessageDialog("You have already started the first game\nDo you wish to proceed?", "Warning!!");
                        message.Commands.Add(new UICommand { Label = "Yes", Id = 1 });
                        message.Commands.Add(new UICommand { Label = "Cancel", Id = 2 });
                        var result = await message.ShowAsync();
                        int.TryParse(result.Id.ToString(), out int temp);
                        if (temp == 1)
                        {
                            SetFirstGame();
                        }
                    }
                    else
                    {
                        SetFirstGame();
                    }

                    break;
                default:
                    break;
            }
        }
        private void SetFirstGame()
        {
            bool isTimeValid = DateTime.TryParse(tmpkFirstGame.Time.ToString(), out DateTime setGameTime);
            if (isTimeValid)
            {
                MessageBox.Show($"First game set to: {setGameTime.ToString("h:mmtt")}");
                GameTimerControl.FirstGame(setGameTime);
                firstGameStarted = true;
            }
            else
            {
                MessageBox.Show("Error setting time! Please try again.");
            }
        }
    }
}
