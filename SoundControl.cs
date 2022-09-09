using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media;
using Windows.Media.Audio;
using Windows.Media.Playback;
using Windows.Media.Core;
using Windows.UI.Xaml.Controls;
using Windows.Storage;

namespace UnderwaterHockeyTimer
{
    public static class SoundControl
    {
        private static MediaPlayer defaultDingSound = new MediaPlayer();
        private static MediaElement buzzerSound = new MediaElement();
        private static bool buzzerSet = false;
        public static void OnLoad()
        {            
            buzzerSound.IsLooping = true;
        }
        public static void DefaultDing()
        {
            defaultDingSound.Source = MediaSource.CreateFromUri(new Uri(@"C:\Windows\Media\Windows Background.wav"));
            defaultDingSound.Play();
        }
        public static void BuzzerStart()
        {
            if (buzzerSet == true)
            {
                buzzerSound.Play();
            }
        }
        public static void BuzzerStop()
        {
            if (buzzerSet == true)
            {
                buzzerSound.Pause();
            }
        }
        public static void SetBuzzerOne()
        {
            buzzerSound.Source = new Uri("");
        }
        public static void SetBuzzerTwo()
        {
            buzzerSound.Source = new Uri("");
        }
        public static void SetCustomBuzzer(string file)
        {            
            int temp = 0;
            try
            {
                buzzerSound.Source = new Uri(file);
            }
            catch (Exception e)
            {
                temp++;
                MessageBox.Show($"Error occured when trying to set buzzer location:\n{e}");
            }
            if (temp == 0)
            {
                buzzerSet = true;
                MessageBox.Show("Buzzer sound sucessfully set!");
            }

        }
    }
}
