using System;
using Windows.UI.Xaml;
using Windows.UI;

namespace UnderwaterHockeyTimer
{
    public static class GameTimerControl
    {
        public static void OnLoad()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(980);
            timer.Tick += OnTimed;
            timer.Start();
        }
        private static bool soundEnabled = false;
        public static bool tmrDown = false, tmrUp = false, gameSelect = false;
        private static string newLine = Environment.NewLine;
        private static void OnTimed(object sender, object e)
        {
            if (tmrDown)
                TimerCountDown();
            if (tmrUp)
                TimerCountUp();
            if (GlobalVariables.TeamTimeTaken)
                TeamTimeCheck();
            if (gameSelect)
                GameSelection();
            if (soundEnabled)
                SoundCheck();
        }
        public static void FirstGame(DateTime dateTime)
        {
            TimeSpan calc = dateTime.Subtract(DateTime.Now);
            int.TryParse(calc.ToString(@"hh"), out int hours);
            int.TryParse(calc.ToString(@"mm"), out int minutes);
            int.TryParse(calc.ToString(@"ss"), out int seconds);
            while (hours > 0)
            {
                hours--;
                minutes += 60;
            }
            GlobalVariables.Minutes = minutes;
            GlobalVariables.Seconds = seconds + 1;
            GlobalVariables.Colour = Color.FromArgb(255, 220, 0, 0);
            GlobalVariables.GameText = "First game starts in";
            GlobalVariables.CurrentGame = 1;
            soundEnabled = true;
            tmrDown = true;
        }
        public static string GameDetailsToFile()
        {
            return ($"Game No {GlobalVariables.GameNumber} details:" + Environment.NewLine +
                $"Game Half Length: {GlobalVariables.HalfLength}" + Environment.NewLine + $"Game Half Time Length: {GlobalVariables.HalfTimeLength}" +
                Environment.NewLine + $"Time Between Games Max: {GlobalVariables.IntervalMax}" + Environment.NewLine +
                $"Time Between Games Min: {GlobalVariables.IntervalMin}" + Environment.NewLine +
                $"Ammount Of Halves: {GlobalVariables.AmmountOfHalves}");
        }
        private static void GameSelection()
        {
            if (GlobalVariables.CurrentGame == 1)/*First Half code*/
            {                
                GlobalVariables.Colour = Color.FromArgb(255, 65, 65, 65);
                GlobalVariables.GameText = "First Half";
                GlobalMethods.ResetGoals();
                GlobalMethods.ResetTeamTimeTaken();
                GlobalVariables.Minutes = GlobalVariables.HalfLength;
                GlobalVariables.CurrentGame = 2;                
                FilesPage.WholeGameOutput(GameDetailsToFile() + Environment.NewLine + $"First Half started at {GlobalMethods.CourtTime()}");
            }
            else if (GlobalVariables.CurrentGame == 2)/*Half time code*/
            {
                soundEnabled = true;
                GlobalVariables.Colour = Color.FromArgb(255, 220, 0, 0);
                if (GlobalVariables.AmmountOfHalves == 1)
                {
                    GlobalVariables.CurrentGame = 4;
                }
                else if (GlobalVariables.AmmountOfHalves == 2)
                {
                    GlobalVariables.Minutes = GlobalVariables.HalfLength;
                    GlobalVariables.GameText = "Half Time";
                    GlobalVariables.CurrentGame = 3;
                    FilesPage.WholeGameOutput($"Half time started at {GlobalMethods.CourtTime()}");
                }
            }
            else if (GlobalVariables.CurrentGame == 3)/*Second half code*/
            {
                GlobalVariables.Colour = Color.FromArgb(255, 65, 65, 65);
                GlobalVariables.GameText = "Second Half";
                GlobalVariables.Minutes = GlobalVariables.HalfLength;
                GlobalMethods.ResetTeamTimeTaken();
                GlobalVariables.CurrentGame = 4;
                FilesPage.WholeGameOutput($"Second Half started at {GlobalMethods.CourtTime()}");
            }
            else if (GlobalVariables.CurrentGame == 4)/*Endgame code*/
            {
                soundEnabled = true;
                if (GlobalVariables.SuddenDeath && TeamBlack.Score == TeamWhite.Score)
                {
                    GlobalVariables.CurrentGame = 5;
                }
                else
                {
                    GlobalVariables.CurrentGame = 7;
                }
            }
            if (GlobalVariables.CurrentGame == 5 || GlobalVariables.CurrentGame == 6)/*Sudden death code*/
            {
                GlobalVariables.Colour = Color.FromArgb(255, 220, 0, 0);
                if (GlobalVariables.CurrentGame == 5)/*Sudden death starting code*/
                {
                    GlobalVariables.GameText = "SUDDEN DEATH\nstarts in";
                    GlobalVariables.Minutes = 1;
                    GlobalVariables.CatchUpTime += 60;
                    GlobalVariables.CurrentGame = 6;
                }
                else if (GlobalVariables.CurrentGame == 6)/*Sudden death code*/
                {
                    FilesPage.WholeGameOutput($"SUDDEN DEATH started at {GlobalMethods.CourtTime()}");
                    GlobalVariables.GameText = "SUDDEN DEATH";
                    tmrUp = true;
                    tmrDown = false;
                }
            }
            if (GlobalVariables.CurrentGame == 7)/*Game over code*/
            {
                GlobalVariables.CatchUpTime++;
                GlobalVariables.Colour = Color.FromArgb(255, 220, 0, 0);
                FilesPage.WholeGameOutput($"Game No {GlobalVariables.GameNumber} finished at {GlobalMethods.CourtTime()}");
                GlobalVariables.GameText = "Game over!\nNext game in";
                int numMax = GlobalVariables.IntervalMax * 60;
                while (GlobalVariables.CatchUpTime > 0)
                {
                    if (numMax > (GlobalVariables.IntervalMin * 60))
                    {
                        GlobalVariables.CatchUpTime--;
                        numMax--;
                    }
                    else
                    {
                        break;
                    }
                }
                do
                {
                    if (numMax >= 60)
                    {
                        GlobalVariables.MinutesTemp++;
                        numMax -= 60;
                    }
                    else
                    {
                        break;
                    }
                } while (true);
                GlobalVariables.Minutes = GlobalVariables.MinutesTemp;
                GlobalVariables.Seconds = numMax;
                GlobalVariables.MinutesTemp = 0;
                GlobalVariables.GameNumber += GlobalVariables.NoIncrement;
                GlobalVariables.CurrentGame = 1;
            }
            gameSelect = false;
            GlobalVariables.CatchUpTime++;
            if (GlobalVariables.CurrentGame == 7 || GlobalVariables.RefTimeEnabled || GlobalVariables.TeamTimeTaken)
            {
                GlobalVariables.Colour = Color.FromArgb(255, 220, 0, 0);
            }
        }
        private static void SoundCheck()
        {
            if (GlobalVariables.Minutes == 0)
            {
                if (GlobalVariables.Seconds == 30)
                {
                    SoundControl.DefaultDing();
                }
                else if (GlobalVariables.Seconds > 0 && GlobalVariables.Seconds <= 10)
                {
                    SoundControl.DefaultDing();
                }
                else if (GlobalVariables.Seconds == 0)
                {
                    SoundControl.DefaultDing();
                    soundEnabled = false;
                }
            }
        }
        private static void TimerCountDown()
        {
            if (GlobalVariables.Seconds > 0)
            {
                GlobalVariables.Seconds--;
            }
            else
            {
                if (GlobalVariables.Minutes > 0)
                {
                    GlobalVariables.Seconds += 59;
                    GlobalVariables.Minutes--;
                }
                else
                {
                    gameSelect = true;
                }
            }
            GlobalVariables.MinutesTemp = 0;
            GlobalVariables.SecondsTemp = 0;
        }
        private static void TimerCountUp()
        {
            if (GlobalVariables.SecondsTemp >= 60)
            {
                GlobalVariables.MinutesTemp++;
                GlobalVariables.SecondsTemp = 0;
            }
            else
            {
                GlobalVariables.SecondsTemp++;
            }

            GlobalVariables.CatchUpTime++;
        }
        private static void TeamTimeCheck()
        {
            if (GlobalVariables.SecondsTemp > 0)
            {
                GlobalVariables.SecondsTemp--;
            }
            else
            {
                if (GlobalVariables.MinutesTemp > 0)
                {
                    GlobalVariables.SecondsTemp += 59;
                    GlobalVariables.MinutesTemp--;
                }
                else
                {
                    GlobalVariables.TeamTimeTaken = false;
                    tmrDown = true;
                    GlobalVariables.GameText = GlobalVariables.GameTextTemp;
                    GlobalVariables.Colour = Color.FromArgb(255, 65, 65, 65);
                }
            }
        }
    }
}
