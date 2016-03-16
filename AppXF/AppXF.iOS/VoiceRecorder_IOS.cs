using System;
using System.IO;
using AppXF.iOS;
using AudioToolbox;
using AVFoundation;
using Foundation;
using Xamarin.Forms;


[assembly:Dependency(typeof(VoiceRecorder_IOS))]
namespace AppXF.iOS
{
    class VoiceRecorder_IOS : IVoiceRecorder
    {


        AVAudioRecorder recorder;
        AVAudioPlayer player;
        NSError error;
        NSUrl url;
        NSDictionary settings;
        private string audioFilePath;

        private bool PrepareVariables()
        {

            var audioSession = AVAudioSession.SharedInstance();
            var err = audioSession.SetCategory(AVAudioSessionCategory.PlayAndRecord);
            if (err != null)
            {
                Console.WriteLine($"audioSession: {err}" );
                return false;
            }
            err = audioSession.SetActive(true);
            if (err != null)
            {
                Console.WriteLine($"audioSession: {err}");
                return false;
            }
            //Declare string for application temp path and tack on the file extension
            string fileName = $"Myfile{DateTime.Now.ToString("yyyyMMddHHmmss")}.wav";
             audioFilePath = Path.Combine(Path.GetTempPath(), fileName);

            Console.WriteLine("Audio File Path: " + audioFilePath);

            url = NSUrl.FromFilename(audioFilePath);
            //set up the NSObject Array of values that will be combined with the keys to make the NSDictionary
            NSObject[] values = {
                NSNumber.FromFloat (44100.0f), //Sample Rate
                NSNumber.FromInt32 ((int)AudioToolbox.AudioFormatType.LinearPCM), //AVFormat
                NSNumber.FromInt32 (2), //Channels
                NSNumber.FromInt32 (16), //PCMBitDepth
                NSNumber.FromBoolean (false), //IsBigEndianKey
                NSNumber.FromBoolean (false) //IsFloatKey
            };

            //Set up the NSObject Array of keys that will be combined with the values to make the NSDictionary
            NSObject[] keys = {
                AVAudioSettings.AVSampleRateKey,
                AVAudioSettings.AVFormatIDKey,
                AVAudioSettings.AVNumberOfChannelsKey,
                AVAudioSettings.AVLinearPCMBitDepthKey,
                AVAudioSettings.AVLinearPCMIsBigEndianKey,
                AVAudioSettings.AVLinearPCMIsFloatKey
            };

            //Set Settings with the Values and Keys to create the NSDictionary
            settings = NSDictionary.FromObjectsAndKeys(values, keys);

            //Set recorder parameters
            recorder = AVAudioRecorder.Create(url, new AudioSettings(settings), out error);

            //Set Recorder to Prepare To Record
            recorder.PrepareToRecord();
            return true;
        }


        public bool PrepareRecord()
        {
            var isReady = PrepareVariables();
            if (isReady)
            {
                 isReady = recorder.PrepareToRecord();
            }
            return isReady;
        }

        public bool Record()
        {

           var isRecord  = recorder.Record();
            return isRecord;
        }

        public void Play(string path)
        {
            if (player!=null)
            {
                player.Stop();
                player.Dispose();
            }

            url = new NSUrl(path);
           // NSError err;
            //  player = new AVAudioPlayer(url, "wav" , out err);
            player = AVAudioPlayer.FromUrl(url);

            player.Play();
        }

        public void StopRecord()
        {
            recorder.Stop();
              
            MessagingCenter.Send<IVoiceRecorder,string>(this, "voiceRecorded", audioFilePath);
        }
    }
}
