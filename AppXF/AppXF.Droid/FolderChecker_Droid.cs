using AppXF.Droid;
using Environment = System.Environment;

[assembly: Xamarin.Forms.Dependency(typeof(FolderChecker_Droid))]
namespace AppXF.Droid
{
    public class FolderChecker_Droid : IFolderChecker
    {
        public string GetFolderName()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }
    }
}