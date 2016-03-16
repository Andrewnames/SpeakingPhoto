using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppXF.Droid;
using Xamarin.Forms;
using Uri = Android.Net.Uri;
[assembly: Xamarin.Forms.Dependency(typeof(VoiceRecorder_Droid))]
namespace AppXF.Droid
{
    class VoiceRecorder_Droid : IVoiceRecorder
    {
        private MediaPlayer player;
        private MediaRecorder recorder;
        private string audioFilePath;
        public bool PrepareRecord()
        {
            try
            {
                string fileName = $"Myfile{DateTime.Now.ToString("yyyyMMddHHmmss")}.wav";
                audioFilePath = Path.Combine(Path.GetTempPath(), fileName);
                if (recorder == null)
                {
                    recorder = new MediaRecorder();
                }
                else
                {
                    recorder.Reset();
                }
                recorder.SetAudioSource(AudioSource.Mic);
                recorder.SetOutputFormat(OutputFormat.ThreeGpp);
                recorder.SetAudioEncoder(AudioEncoder.Aac);
                recorder.SetOutputFile(audioFilePath);
                recorder.Prepare();
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;
            }
            

        }

        public bool Record()
        {
            try
            {
                recorder.Start();

                return true;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.InnerException);
                return false;

            }
        }

        public void Play(string path)
        {
            var ctx = Forms.Context;
            player = MediaPlayer.Create(ctx, Uri.Parse(path));
            player.Start();
            //  player.Release();
        }

        public void StopRecord()
        {
            recorder.Stop();
            recorder.Reset();
            recorder.Release();
            MessagingCenter.Send(this, "voiceRecorded", audioFilePath);
        }
    }
}