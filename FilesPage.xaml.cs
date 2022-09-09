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
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UnderwaterHockeyTimer
{
    public sealed partial class FilesPage : Page
    {
        public FilesPage()
        {
            this.InitializeComponent();
            InitialiseEvents();
            OnLoad();
        }
        private void InitialiseEvents()
        {
            chkGameNumberTextOutput.Checked += CheckChanged;
            chkGameNumberTextOutput.Unchecked += CheckChanged;
            chkGameTextOutput.Checked += CheckChanged;
            chkGameTextOutput.Unchecked += CheckChanged;
            chkGameTimeOutput.Checked += CheckChanged;
            chkGameTimeOutput.Unchecked += CheckChanged;
            chkGoalTextOutput.Checked += CheckChanged;
            chkGoalTextOutput.Unchecked += CheckChanged;
            chkFilesEnabled.Checked += CheckChanged;
            chkFilesEnabled.Unchecked += CheckChanged;
            btnFileLocation.Click += ButtonClicked;
        }
        private void OnLoad()
        {
            chkFilesEnabled.IsChecked = GlobalVariables.FilesEnabled;
            chkGameNumberTextOutput.IsChecked = GlobalVariables.GameNoFileEnabled;
            chkGameTextOutput.IsChecked = GlobalVariables.GameTextFileEnabled;
            chkGameTimeOutput.IsChecked = GlobalVariables.GameTimeFileEnabled;
            chkGoalTextOutput.IsChecked = GlobalVariables.GoalFilesEnabled;
        }
        private void ButtonClicked(object sender, object e)
        {
            Button btn = (Button)sender;
            if (btn.Name == "btnFileLocation")
            {
                MessageBox.Show($"Files are located as follows:\n{FileControl.directoryMain}");
            }
        }
        private void CheckChanged(object sender, object e)
        {
            CheckBox chkbx = (CheckBox)sender;
            if (chkbx.Name == "chkFilesEnabled")
            {
                if (chkFilesEnabled.IsChecked.Value)
                {
                    GlobalVariables.FilesEnabled = true;
                    MessageBox.Show("File output has been enabled.");
                }
                else
                {
                    GlobalVariables.FilesEnabled = false;
                    MessageBox.Show("File output has been disabled.");
                }
            }
            else if (GlobalVariables.FilesEnabled)
            {
                switch (chkbx.Name)
                {
                    case "chkGameNumberTextOutput":
                        GlobalVariables.GameNoFileEnabled = chkGameNumberTextOutput.IsChecked.Value;
                        break;
                    case "chkGameTextOutput":
                        GlobalVariables.GameTextFileEnabled = chkGameTextOutput.IsChecked.Value;
                        break;
                    case "chkGameTimeOutput":
                        GlobalVariables.GameTimeFileEnabled = chkGameTimeOutput.IsChecked.Value;
                        break;
                    case "chkGoalTextOutput":
                        GlobalVariables.GoalFilesEnabled = chkGoalTextOutput.IsChecked.Value;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Files are currently not enabled.\nPlease check Files Enabled check box to enable file output");
            }
        }
    }
}
