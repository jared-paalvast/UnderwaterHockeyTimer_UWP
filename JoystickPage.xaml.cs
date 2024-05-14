using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace UnderwaterHockeyTimer
{
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
