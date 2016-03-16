using AppXF;
using AVFoundation;

[assembly: Xamarin.Forms.Dependency(typeof(Speech))]
public class Speech : ITextToSpeech
{
    private AVSpeechSynthesizer speechSynthesizer;
    public Speech() { }

    public void Speak(string text, string lang)
    {
        if (speechSynthesizer==null)
        {
            speechSynthesizer = new AVSpeechSynthesizer();
        }

        var speechUtterance = new AVSpeechUtterance(text)
        {
            Rate = AVSpeechUtterance.MaximumSpeechRate / 2,
            Voice = AVSpeechSynthesisVoice.FromLanguage(lang),
            Volume = 0.5f,
            PitchMultiplier = 1.0f
        };

        speechSynthesizer.SpeakUtterance(speechUtterance);
    }

  
}