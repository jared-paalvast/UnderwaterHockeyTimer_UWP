using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UnderwaterHockeyTimer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SecondDisplay : Page
    {
        public SecondDisplay()
        {
            this.InitializeComponent();
            InitialiseElements();
            StartupMethod();
        }
        private void StartupMethod()
        {
            DispatcherTimer textTimer = new DispatcherTimer();
            textTimer.Interval = TimeSpan.FromMilliseconds(1);
            textTimer.Tick += OnTextTimed;
            textTimer.Start();
        }
        private void InitialiseElements()
        {
            BackgroundGrid.SizeChanged += GridHandle;
        }
        private void GridHandle(object sender, object e)
        {
            TextSizeUpdate();
        }
        private void OnTextTimed(object sender, object e)
        {
            BackColourUpdate();
            GameTextUpdate();
        }
        private void BackColourUpdate()
        {
            this.BackgroundGrid.Background = new SolidColorBrush(GlobalVariables.Colour);
        }
        private void TextSizeUpdate()
        {
            try
            {
                txtScoreBlack.FontSize = FontSizeCalc(rtngScoreBlack.ActualHeight, rtngScoreBlack.ActualWidth, 2.5);
                txtScoreWhite.FontSize = FontSizeCalc(rtngScoreWhite.ActualHeight, rtngScoreWhite.ActualWidth, 2.5);
                txtCourtTime.FontSize = FontSizeCalc(rtngCourtTime.ActualHeight, rtngCourtTime.ActualWidth, 16);
                txtGameTime.FontSize = FontSizeCalc(rtngGameTime.ActualHeight, rtngGameTime.ActualWidth, 6);
                txtGameText.FontSize = FontSizeCalc(rtngGameText.ActualHeight, rtngGameText.ActualWidth, 16);
                txtGameNo.FontSize = FontSizeCalc(rtngGameNo.ActualHeight, rtngGameNo.ActualWidth, 10);
            }
            catch (Exception) { }
        }
        private void GameTextUpdate()
        {
            txtCourtTime.Text = GlobalMethods.CourtTime();
            txtScoreBlack.Text = TeamBlack.Score.ToString();
            txtScoreWhite.Text = TeamWhite.Score.ToString();
            txtGameText.Text = GlobalVariables.GameText;
            txtGameNo.Text = GlobalMethods.GameNumber();
            txtGameTime.Text = GlobalMethods.GameTime();
        }
        private double FontSizeCalc(double width, double height, double division)
        {
            return ((width + height) / division);
        }
    }
}
