using System;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;

namespace UnderwaterHockeyTimer
{
    public sealed partial class FilesPage : Page
    {
        private static StorageFolder defaultFolder;
        public static StorageFolder directoryMain;
        private static string pathGameOutput;
        public FilesPage()
        {
            this.InitializeComponent();
            InitialiseEvents();
            DefaultFolderLocation();
        }
        private static async void DefaultFolderLocation()
        {
            defaultFolder = await StorageFolder.GetFolderFromPathAsync(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        }
        private void InitialiseEvents()
        {
            btnShowFileLocation.Click += ButtonClicked;
            btnSetFileLocation.Click += ButtonClicked;
        }
        private void ButtonClicked(object sender, object e)
        {
            Button btn = (Button)sender;
            switch (btn.Name)
            {            
                case "btnShowFileLocation":
                    if (directoryMain != null)                    
                        MessageBox.Show($"Files are located as follows:\n{directoryMain.Path}");
                    else
                        MessageBox.Show($"Files are located as follows:\n{defaultFolder.Path}");
                    break;
                case "btnSetFileLocation":
                    SetFileLocation();
                    break;
                default:
                    break;
            }                   
        }
        private async void SetFileLocation()
        {
            FolderPicker folderPicker = new FolderPicker();
            folderPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            directoryMain = await folderPicker.PickSingleFolderAsync();
            if (directoryMain != null)
            {                
                CreateDirectories(directoryMain);                
            }
            else
            {
                MessageBox.Show("Nothing was selected. No changes have been made.");
            }

        }
        public static async void CreateDirectories(StorageFolder folderLocation)
        {
            try
            {
                pathGameOutput = $"game{GlobalVariables.GameNumber}.txt";
                await folderLocation.CreateFileAsync(pathGameOutput, CreationCollisionOption.OpenIfExists);
            }
            catch (Exception e)
            {
                MessageBox.Show($"File Control error code as follows.\nError: {e}");
            }
        }
        public static async void WholeGameOutput(string text)
        {
            try
            {
                if (directoryMain != null)
                {
                    StorageFile myFile = await directoryMain.GetFileAsync(pathGameOutput);
                    await FileIO.AppendTextAsync(myFile, text + Environment.NewLine);
                }
                else
                {
                    CreateDirectories(defaultFolder);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"File write error: {e}");
                return;
            }
        }

    }
}
