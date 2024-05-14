using System;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace UnderwaterHockeyTimer
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            OnLoad();
        }
        private void OnLoad()
        {
            JoystickControl.OnLoad();
            InitialiseDisplayWindow();
            InitialiseButtons();
            GameTimerControl.OnLoad();            
            SoundControl.OnLoad();            
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(titleBar);
            ContentFrame.Navigate(typeof(ScorerPage));
        }
        private void InitialiseButtons()
        {
            AppBtnHome.Click += ButtonClicked;
            AppBtnSettings.Click += ButtonClicked;
            AppBtnClock.Click += ButtonClicked;
            AppBtnFiles.Click += ButtonClicked;
            AppBtnJoystick.Click += ButtonClicked;
        }
        private async void InitialiseDisplayWindow()
        {
            CoreApplicationView newView = CoreApplication.CreateNewView();
            int newViewId = 0;
            await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Frame frame = new Frame();
                frame.Navigate(typeof(SecondDisplay), null);
                Window.Current.Content = frame;
                // You have to activate the window in order to show it later.
                Window.Current.Activate();
                newViewId = ApplicationView.GetForCurrentView().Id;
            });
            bool viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId);
        }
        private void ButtonClicked(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Name)
            {
                case "AppBtnHome":
                    ContentFrame.Navigate(typeof(ScorerPage), null, new SuppressNavigationTransitionInfo());
                    break;
                case "AppBtnSettings":
                    ContentFrame.Navigate(typeof(SettingsPage), null, new SuppressNavigationTransitionInfo());
                    break;
                case "AppBtnClock":
                    ContentFrame.Navigate(typeof(TimeSetup), null, new SuppressNavigationTransitionInfo());
                    break;
                case "AppBtnFiles":
                    ContentFrame.Navigate(typeof(FilesPage), null, new SuppressNavigationTransitionInfo());
                    break;
                case "AppBtnJoystick":
                    ContentFrame.Navigate(typeof(JoystickPage), null, new SuppressNavigationTransitionInfo());
                    break;
                default:
                    break;
            }
        }
    }
}
