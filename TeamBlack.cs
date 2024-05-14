using System;
using Windows.UI;
using Windows.UI.Popups;

namespace UnderwaterHockeyTimer
{
    public static class TeamBlack
    {
        public static int Score { get; set; }
        public static bool TeamTimeTaken { get; set; }
        public static async void TeamTime()
        {
            if (GlobalVariables.GameText != "Ref Time" && !TeamTimeTaken && !GlobalVariables.TeamTimeTaken && GlobalVariables.CurrentGame == 2 || GlobalVariables.CurrentGame == 4)
            {
                MessageDialog message = new MessageDialog("Are you sure you want to start black team timeout?", "Black Timeout");
                message.Commands.Add(new UICommand { Label = "Yes", Id = 1 });
                message.Commands.Add(new UICommand { Label = "Cancel", Id = 0 });
                var result = await message.ShowAsync();
                int.TryParse(result.Id.ToString(), out int temp);
                if (temp == 1)
                {
                    FilesPage.WholeGameOutput($"Black team timeout taken at | Half: {GlobalVariables.GameText} | Game Time: {GlobalMethods.GameTime()} | {GlobalMethods.CourtTime()}");
                    GlobalVariables.CatchUpTime += 61;
                    TeamTimeTaken = true;
                    GlobalVariables.TeamTimeTaken = true;
                    GameTimerControl.tmrDown = false;
                    GlobalVariables.MinutesTemp = 1;
                    GlobalVariables.GameTextTemp = GlobalVariables.GameText;
                    GlobalVariables.GameText = "Timeout Black";
                    GlobalVariables.Colour = Color.FromArgb(255, 220, 0, 0);
                }
            }
            else
            {
                MessageBox.Show("Team timeout can only be taken during play");
            }
        }
        public static async void Goal()
        {
            MessageDialog message = new MessageDialog("Are you sure you want to award a goal to black?");
            message.Commands.Add(new UICommand { Label = "Yes", Id = 1 });
            message.Commands.Add(new UICommand { Label = "Cancel", Id = 0 });
            string gametext = GlobalVariables.GameText;
            if (gametext.Length > 7)
            {
                gametext.Remove(7);
            }

            if (GlobalVariables.RefTimeEnabled || gametext == "Timeout" || GlobalVariables.CurrentGame == 3 || GlobalVariables.CurrentGame == 5)
            {
                var result = await message.ShowAsync();
                int.TryParse(result.Id.ToString(), out int temp);
                if (temp == 1)
                {
                    Score++;
                    GlobalMethods.CapScore("Black");
                }
            }
            else if (GlobalVariables.CurrentGame == 7)
            {
                var result = await message.ShowAsync();
                int.TryParse(result.Id.ToString(), out int temp);
                if (temp == 1)
                {
                    Score++;
                    GlobalVariables.CurrentGame = 1;
                    GameTimerControl.gameSelect = true;
                    GameTimerControl.tmrUp = false;
                    GameTimerControl.tmrDown = true;
                    GlobalMethods.CapScore("Black");
                }
            }
            else if (GlobalVariables.CurrentGame == 2 || GlobalVariables.CurrentGame == 4)
            {
                Score++;
                GlobalMethods.CapScore("Black");
            }
            else
            {
                MessageBox.Show("Goal cannot be awarded right now");
            }
        }
    }
}
