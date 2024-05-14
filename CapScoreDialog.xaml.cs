using Windows.UI.Xaml.Controls;

namespace UnderwaterHockeyTimer
{
    public enum CapScoreResult
    {
        One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Eleven, Twelve, Thirteen, Fourteen, Fifteen, Unknown
    }
    public sealed partial class CapScoreDialog : ContentDialog
    {
        public CapScoreResult Result { get; private set; }
        public CapScoreDialog()
        {
            this.InitializeComponent();
            InitialiseElements();
        }
        private void InitialiseElements()
        {
            btnOne.Click += ButtonClicked;
            btnTwo.Click += ButtonClicked;
            btnThree.Click += ButtonClicked;
            btnFour.Click += ButtonClicked;
            btnFive.Click += ButtonClicked;
            btnSix.Click += ButtonClicked;
            btnSeven.Click += ButtonClicked;
            btnEight.Click += ButtonClicked;
            btnNine.Click += ButtonClicked;
            btnTen.Click += ButtonClicked;
            btnEleven.Click += ButtonClicked;
            btnTwelve.Click += ButtonClicked;
            btnThirteen.Click += ButtonClicked;
            btnFourteen.Click += ButtonClicked;
            btnFifteen.Click += ButtonClicked;
            btnUnknown.Click += ButtonClicked;
        }
        private void ButtonClicked(object sender, object e)
        {
            Button btn = (Button)sender;
            if (btn.Name == "btnOne")
            {
                Result = CapScoreResult.One;
            }

            if (btn.Name == "btnTwo")
            {
                Result = CapScoreResult.Two;
            }

            if (btn.Name == "btnThree")
            {
                Result = CapScoreResult.Three;
            }

            if (btn.Name == "btnFour")
            {
                Result = CapScoreResult.Four;
            }

            if (btn.Name == "btnFive")
            {
                Result = CapScoreResult.Five;
            }

            if (btn.Name == "btnSix")
            {
                Result = CapScoreResult.Six;
            }

            if (btn.Name == "btnSeven")
            {
                Result = CapScoreResult.Seven;
            }

            if (btn.Name == "btnEight")
            {
                Result = CapScoreResult.Eight;
            }

            if (btn.Name == "btnNine")
            {
                Result = CapScoreResult.Nine;
            }

            if (btn.Name == "btnTen")
            {
                Result = CapScoreResult.Ten;
            }

            if (btn.Name == "btnEleven")
            {
                Result = CapScoreResult.Eleven;
            }

            if (btn.Name == "btnTwelve")
            {
                Result = CapScoreResult.Twelve;
            }

            if (btn.Name == "btnThirteen")
            {
                Result = CapScoreResult.Thirteen;
            }

            if (btn.Name == "btnFourteen")
            {
                Result = CapScoreResult.Fourteen;
            }

            if (btn.Name == "btnFifteen")
            {
                Result = CapScoreResult.Fifteen;
            }

            if (btn.Name == "btnUnknown")
            {
                Result = CapScoreResult.Unknown;
            }

            this.Hide();
        }
    }
}
