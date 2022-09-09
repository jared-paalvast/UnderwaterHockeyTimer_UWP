using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.Gaming.Input;

namespace UnderwaterHockeyTimer
{
    public static class JoystickControl
    {
        private static Gamepad gamepad;
        private static DispatcherTimer timer = new DispatcherTimer();
        private static readonly string stringToRemove = "abcdefghijklmnopqrstuvwxyz, VMP";
        private static readonly List<string> charsToRemove = new List<string>();
        private static bool[] oldBool = new bool[10];
        public static void OnLoad()
        {
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += OnTimed;
            foreach (char c in stringToRemove.ToCharArray())
            {
                charsToRemove.Add(c.ToString());
            }
        }
        public static bool CheckConnection()
        {
            if (Gamepad.Gamepads.Count > 0)
            {
                MessageBox.Show($"There are '{Gamepad.Gamepads.Count} controller(s) connected.\nGamepad list: {Gamepad.Gamepads}");
                gamepad = Gamepad.Gamepads[0];
                timer.Start();
                return true;
            }
            else
            {
                MessageBox.Show("No Controllers found! Please try again");
                timer.Stop();
                return false;
            }
        }
        private static void OnTimed(object sender, object e)
        {
            if (Gamepad.Gamepads.Count <= 0)
            {
                timer.Stop();
                MessageBox.Show("Error! No Gamepads detected!");
                GlobalVariables.JoystickControlsEnabled = false;
            }
            if (GlobalVariables.JoystickControlsEnabled)
            {
                string newState = gamepad.GetCurrentReading().Buttons.ToString();
                newState = newState.Replace("Back", string.Empty);
                newState = newState.Replace("None", string.Empty);
                newState = newState.Replace("Start", string.Empty);
                foreach (var c in charsToRemove)
                {
                    newState = newState.Replace(c, string.Empty);
                }
                newState = newState.Replace("DL", "L");
                newState = newState.Replace("DR", "R");
                newState = newState.Replace("DU", "U");
                newState = newState.Replace("DD", "D");
                newState = newState.Replace("LS", "l");
                newState = newState.Replace("RS", "r");
                newState = newState.Replace("RT", string.Empty);
                newState = newState.Replace("LT", string.Empty);
                char[] newBtn = newState.ToCharArray();
                bool[] newBool = new bool[10];
                for (int i = 0; i < newBtn.Length; i++)
                {
                    if (newBtn[i] == 'A') { newBool[0] = true; }
                    if (newBtn[i] == 'B') { newBool[1] = true; }
                    if (newBtn[i] == 'X') { newBool[2] = true; }
                    if (newBtn[i] == 'Y') { newBool[3] = true; }
                    if (newBtn[i] == 'U') { newBool[4] = true; }
                    if (newBtn[i] == 'D') { newBool[5] = true; }
                    if (newBtn[i] == 'L') { newBool[6] = true; }
                    if (newBtn[i] == 'R') { newBool[7] = true; }
                    if (newBtn[i] == 'l') { newBool[8] = true; }
                    if (newBtn[i] == 'r') { newBool[9] = true; }
                }
                if (oldBool[0] == true && newBool[0] != true) { }
                if (oldBool[0] != true && newBool[0] == true) { }
                if (oldBool[1] != true && newBool[1] == true) { }
                if (oldBool[2] != true && newBool[2] == true) { SoundControl.BuzzerStart(); }
                if (oldBool[2] == true && newBool[2] != true) { SoundControl.BuzzerStop(); }
                if (GlobalVariables.RefTimeEnabled)
                    if (oldBool[3] != true && newBool[3] == true) { GlobalMethods.RefTime(); }
                if (oldBool[4] != true && newBool[4] == true) { }
                if (oldBool[5] != true && newBool[5] == true) { }
                if (GlobalVariables.TeamTimeEnabled)
                {
                    if (GlobalVariables.JoystickTeamTimeEnabled)
                    {
                        if (oldBool[6] != true && newBool[6] == true) { TeamBlack.TeamTime(); }
                        if (oldBool[7] != true && newBool[7] == true) { TeamWhite.TeamTime(); }
                    }
                }
                if (GlobalVariables.JoystickGoalsEnabled)
                {
                    if (oldBool[8] != true && newBool[8] == true) { TeamBlack.Goal(); }
                    if (oldBool[9] != true && newBool[9] == true) { TeamWhite.Goal(); }
                }
                oldBool = newBool;
            }
            else
            {
                timer.Stop();
                MessageBox.Show("Gamepad input disabled pro-actively. (Failsafe)");
            }
        }
    }
}
