using Microsoft.Maui.Controls;

namespace MicrophoneTestApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void OnRecordClicked(object sender, EventArgs e)
        {
            DisplayAlert("Info", "Record button clicked", "OK");
        }

        void OnStopClicked(object sender, EventArgs e)
        {
            DisplayAlert("Info", "Stop button clicked", "OK");
        }

        void OnPlayClicked(object sender, EventArgs e)
        {
            DisplayAlert("Info", "Play button clicked", "OK");
        }
    }
}
