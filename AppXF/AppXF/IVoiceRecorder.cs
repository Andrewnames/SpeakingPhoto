namespace AppXF
{
    public interface IVoiceRecorder
    {
        bool  PrepareRecord();
        bool Record();
        void Play(string path);
        void StopRecord();
    }
}