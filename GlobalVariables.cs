using Windows.UI;

namespace UnderwaterHockeyTimer
{
    public static class GlobalVariables
    {
        public static int Seconds { get; set; } = 0;
        public static int Minutes { get; set; } = 0;
        public static int SecondsTemp { get; set; } = 0;
        public static int MinutesTemp { get; set; } = 0;
        public static int CatchUpTime { get; set; } = 0;
        public static int HalfLength { get; set; } = 10;
        public static int AmmountOfHalves { get; set; } = 2;
        public static int HalfTimeLength { get; set; } = 2;
        public static int IntervalMax { get; set; } = 10;
        public static int IntervalMin { get; set; } = 2;
        public static int GameNumber { get; set; } = 1;
        public static int NoIncrement { get; set; } = 1;
        public static int CurrentGame { get; set; }
        public static string GameText { get; set; } = "";
        public static string GameTextTemp { get; set; }
        public static bool SuddenDeath { get; set; } = false;
        public static bool RefTimeEnabled { get; set; } = false;
        public static bool TeamTimeEnabled { get; set; } = false;
        public static bool TeamTimeTaken { get; set; } = false;
        public static bool CapScoreEnabled { get; set; } = false;        
        public static bool JoystickGoalsEnabled { get; set; } = false;
        public static bool JoystickTeamTimeEnabled { get; set; } = false;
        public static bool JoystickRefTimeEnabled { get; set; } = false;
        public static bool JoystickControlsEnabled { get; set; } = false;
        public static bool BuzzerSoundOneSet { get; set; } = true;
        public static bool BuzzerSoundTwoSet { get; set; } = false;
        public static bool CustomBuzzerSoundSet { get; set; } = false;
        public static Color Colour { get; set; } = Color.FromArgb(255, 65, 65, 65);/*Red = 255, 220, 0, 0*/
    }
}
