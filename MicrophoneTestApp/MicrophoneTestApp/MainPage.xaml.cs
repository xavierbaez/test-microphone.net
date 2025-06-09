using Microsoft.Maui.Controls;
using System;
using System.IO;
using System.Media;
using System.Windows.Forms;
using NAudio.Wave;

namespace MicrophoneTestApp
{
    public partial class MainPage : ContentPage
    {
        private WaveInEvent waveIn;
        private WaveFileWriter writer;
        private string outputFilePath;
        private SoundPlayer player;

        public MainPage()
        {
            InitializeComponent();
            outputFilePath = Path.Combine(FileSystem.CacheDirectory, "recording.wav");
        }

        private void OnRecordClicked(object sender, EventArgs e)
        {
            try
            {
                waveIn = new WaveInEvent();
                waveIn.WaveFormat = new WaveFormat(44100, 1);
                writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);
                waveIn.DataAvailable += (s, a) =>
                {
                    writer.Write(a.Buffer, 0, a.BytesRecorded);
                };
                waveIn.RecordingStopped += (s, a) =>
                {
                    writer.Dispose();
                    writer = null;
                    waveIn.Dispose();
                };
                waveIn.StartRecording();
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"Recording failed: {ex.Message}", "OK");
            }
        }

        private void OnStopClicked(object sender, EventArgs e)
        {
            try
            {
                waveIn?.StopRecording();
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"Stop failed: {ex.Message}", "OK");
            }
        }

        private void OnPlayClicked(object sender, EventArgs e)
        {
            try
            {
                player = new SoundPlayer(outputFilePath);
                player.Play();
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"Playback failed: {ex.Message}", "OK");
            }
        }
    }
}
