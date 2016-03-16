using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Speech.Tts;
using Android.Views;
using Android.Widget;
using AppXF.Droid;
using Xamarin.Forms;

[assembly:Xamarin.Forms.Dependency(typeof(Speech_Droid))]
namespace AppXF.Droid
{
    class Speech_Droid : Java.Lang.Object, ITextToSpeech,TextToSpeech.IOnInitListener
    {
        private TextToSpeech speaker;
        private string toSpeak;

        public Speech_Droid()
        {
                
        }



        public void Speak(string text, string lang)
        {
            var context = Forms.Context;
            toSpeak = text;
            if (speaker==null)
            {
                speaker = new TextToSpeech(context, this);
            }
            else
            {
                var p = new Dictionary<string , string>();
                speaker.Speak(toSpeak, QueueMode.Flush, p);
            }
        }

        public void OnInit(OperationResult status)
        {
            if (status==OperationResult.Success)
            { 
                var p = new Dictionary<string , string>();

                speaker.Speak(toSpeak, QueueMode.Flush,  p);
            }
        }
    }
}