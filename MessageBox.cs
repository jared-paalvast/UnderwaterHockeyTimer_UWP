using System;
using Windows.UI.Popups;

namespace UnderwaterHockeyTimer
{
    public class MessageBox
    {
        public int ID { get; set; } = -1;
        public static async void Show(string title)
        {
            var dialog = new MessageDialog("");
            dialog.Title = title;
            dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
            await dialog.ShowAsync();
        }
    }
}
