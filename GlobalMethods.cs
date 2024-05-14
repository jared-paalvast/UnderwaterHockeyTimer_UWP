using System;
using Windows.UI;

namespace UnderwaterHockeyTimer
{
    class GlobalMethods
    {
        public static string GameTime()
        {
            if (GlobalVariables.CurrentGame == 7 || GlobalVariables.RefTimeEnabled || GlobalVariables.TeamTimeTaken)
            {
                return TimeCalc(GlobalVariables.SecondsTemp, GlobalVariables.MinutesTemp);
            }
            else
            {
                return TimeCalc(GlobalVariables.Seconds, GlobalVariables.Minutes);
            }
        }
        public static async void CapScore(string Colour)
        {
            if (GlobalVariables.CapScoreEnabled)
            {
                CapScoreDialog dialog = new CapScoreDialog();
                await dialog.ShowAsync();
                FilesPage.WholeGameOutput($"{Colour} team goal awarded at | Scorer Cap Number: {dialog.Result} | Half: {GlobalVariables.GameText} | Game Time: {GameTime()} | {CourtTime()}");
            }
            else
            {
                FilesPage.WholeGameOutput($"{Colour} team goal awarded at | Half: {GlobalVariables.GameText} | Game Time: {GameTime()} | {CourtTime()}");
            }
        }
        private static string TimeCalc(int sec, int min)
        {
            string disSec, disMin;
            if (min < 10)
            {
                disMin = $"0{min}";
            }
            else
            {
                disMin = min.ToString();
            }

            if (sec < 10)
            {
                disSec = $"0{sec}";
            }
            else
            {
                disSec = sec.ToString();
            }

            return $"{disMin}:{disSec}";
        }
        public static string GameNumber()
        {
            return $"Game No. {GlobalVariables.GameNumber}";
        }
        public static string CourtTime()
        {
            return "Court time: " + DateTime.Now.ToString("hh:mm:sstt").ToUpper();
        }
        public static void ResetTeamTimeTaken()
        {
            TeamBlack.TeamTimeTaken = false;
            TeamWhite.TeamTimeTaken = false;
        }
        public static void ResetGoals()
        {
            TeamBlack.Score = 0;
            TeamWhite.Score = 0;
        }
        public static void RefTime()
        {
            string gameText = GlobalVariables.GameText;
            if (!GlobalVariables.TeamTimeTaken && GlobalVariables.CurrentGame == 2 || GlobalVariables.CurrentGame == 4)
            {
                if (GlobalVariables.RefTimeEnabled)
                {
                    GlobalVariables.GameText = GlobalVariables.GameTextTemp;
                    GlobalVariables.Colour = Color.FromArgb(255, 65, 65, 65);
                    FilesPage.WholeGameOutput($"Ref Time finished lasting: {GameTime()} | Until {CourtTime()}");
                    GlobalVariables.RefTimeEnabled = false;
                    GameTimerControl.tmrUp = false;
                    GameTimerControl.tmrDown = true;
                }
                else
                {
                    GlobalVariables.GameTextTemp = gameText;
                    FilesPage.WholeGameOutput($"Ref Time started at | Half: {GlobalVariables.GameText} | Game Time: {GameTime()} | {CourtTime()}");
                    GlobalVariables.GameText = "Ref Time";
                    GlobalVariables.Colour = Color.FromArgb(255, 220, 0, 0);
                    GlobalVariables.RefTimeEnabled = true;
                    GameTimerControl.tmrUp = true;
                    GameTimerControl.tmrDown = false;
                }
            }
            else
            {
                MessageBox.Show("Referee time can only be called during play.");
            }
        }

    }
}
