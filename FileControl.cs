using System;
using System.IO;
using Windows.Storage;

namespace UnderwaterHockeyTimer
{
    public static class FileControl
    {
        private static StorageFolder storage = ApplicationData.Current.LocalFolder;
        public static readonly string directoryMain = storage.Path;
        private static readonly string directoryText = directoryMain + @"/Text_Output/";
        private static readonly string directoryGames = directoryText + @"/Games_Output/";
        private static readonly string pathGoalsWhite = directoryText + "goalsWhite.txt", pathGoalsBlack = directoryText + "goalsBlack.txt";
        private static readonly string pathGameText = directoryText + "gameText.txt", pathGameTime = directoryText + "gameTime.txt";
        private static readonly string pathGameNumber = directoryText + "gameNo.txt";
        public static void CreateDirectories()
        {
            try
            {
                Directory.CreateDirectory(directoryMain);
                Directory.CreateDirectory(directoryText);
                Directory.CreateDirectory(directoryGames);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error code as follows.\nError: {e}");
            }
        }                
        private static void GoalsTextFile()
        {
            File.WriteAllText(pathGoalsWhite, TeamBlack.Score.ToString());
            File.WriteAllText(pathGoalsBlack, TeamWhite.Score.ToString());
        }
        private static void GameTextFile()
        {
            File.WriteAllText(pathGameText, GlobalVariables.GameText);
        }
        private static void GameTimeFile()
        {
            File.WriteAllText(pathGameTime, GlobalMethods.GameTime());
        }
        private static void GameNumberFile()
        {
            File.WriteAllText(pathGameNumber, GlobalVariables.GameNumber.ToString());
        }
        public static void WholeGameOutput(string text)
        {
            string pathGameOutput = directoryGames + $"game-{GlobalVariables.GameNumber}-output.txt";
            File.AppendAllText(pathGameOutput, text + Environment.NewLine);
        }
        public static void AllFiles()
        {
            GoalsTextFile();
            GameTextFile();
            GameTimeFile();
            GameNumberFile();
        }
    }
}
