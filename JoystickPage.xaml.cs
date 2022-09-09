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
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UnderwaterHockeyTimer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class JoystickPage : Page
    {
        public JoystickPage()
        {
            this.InitializeComponent();
            InitialiseEvents();
            InitialiseElements();
        }
        private void InitialiseElements()
        {
            chkGoalAward.IsChecked = GlobalVariables.JoystickGoalsEnabled;
            chkJoystickEnabled.IsChecked = GlobalVariables.JoystickControlsEnabled;
            chkRefTimeControl.IsChecked = GlobalVariables.JoystickRefTimeEnabled;
            chkTeamTimeControl.IsChecked = GlobalVariables.JoystickTeamTimeEnabled;
        }
        private void InitialiseEvents()
        {
            btnCheckJoystick.Click += ButtonClicked;
            btnKeyBindings.Click += ButtonClicked;
            chkGoalAward.Checked += CheckChanged;
            chkGoalAward.Unchecked += CheckChanged;
            chkJoystickEnabled.Checked += CheckChanged;
            chkJoystickEnabled.Unchecked += CheckChanged;
            chkRefTimeControl.Checked += CheckChanged;
            chkRefTimeControl.Unchecked += CheckChanged;
            chkTeamTimeControl.Checked += CheckChanged;
            chkTeamTimeControl.Unchecked += CheckChanged;
        }
        private void ButtonClicked(object sender, object e)
        {
            Button btn = (Button)sender;
            switch (btn.Name)
            {
                case "btnCheckJoystick":
                    JoystickControl.CheckConnection();
                    break;
                case "btnKeyBindings":
                    this.Frame.Navigate(typeof(KeyBindings), null, new SuppressNavigationTransitionInfo());
                    break;
                default:
                    break;
            }
        }
        private void CheckChanged(object sender, object e)
        {

            CheckBox chkbx = (CheckBox)sender;
            switch (chkbx.Name)
            {
                case "chkGoalAward":
                    GlobalVariables.JoystickGoalsEnabled = chkGoalAward.IsChecked.Value;
                    break;
                case "chkJoystickEnabled":
                    if (JoystickControl.CheckConnection())
                    {
                        GlobalVariables.JoystickControlsEnabled = true;
                    }
                    else
                    {
                        GlobalVariables.JoystickControlsEnabled = false;
                        chkJoystickEnabled.IsChecked = false;
                    }

                    break;
                case "chkRefTimeControl":
                    GlobalVariables.JoystickRefTimeEnabled = chkRefTimeControl.IsChecked.Value;
                    break;
                case "chkTeamTimeControl":
                    GlobalVariables.JoystickTeamTimeEnabled = chkTeamTimeControl.IsChecked.Value;
                    break;
                default:
                    break;
            }
        }
    }
}
