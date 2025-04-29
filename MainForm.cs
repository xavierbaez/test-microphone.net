using System;
using System.Windows.Forms;
using NAudio.Wave;

namespace MicrophoneTestApp
{
    public partial class MainForm : Form
    {
        private WaveInEvent waveIn;
        private WaveFileWriter writer;
        private WaveOutEvent waveOut;
        private string outputFilePath = "test.wav";

        public MainForm()
        {
            InitializeComponent();
        }

        private void ButtonRecord_Click(object sender, EventArgs e)
        {
            waveIn = new WaveInEvent();
            waveIn.WaveFormat = new WaveFormat(44100, 1);
            waveIn.DataAvailable += OnDataAvailable;
            writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);
            waveIn.StartRecording();
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            if (waveIn != null)
            {
                waveIn.StopRecording();
                waveIn.Dispose();
                waveIn = null;
            }
            if (writer != null)
            {
                writer.Close();
                writer.Dispose();
                writer = null;
            }
        }

        private void ButtonPlay_Click(object sender, EventArgs e)
        {
            var audioFile = new AudioFileReader(outputFilePath);
            waveOut = new WaveOutEvent();
            waveOut.Init(audioFile);
            waveOut.Play();
        }

        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            if (writer != null)
            {
                writer.Write(e.Buffer, 0, e.BytesRecorded);
                writer.Flush();
            }
        }
    }
}
