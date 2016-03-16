using System;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AppXF.Droid;
using FFImageLoading.Forms.Droid;
using Xamarin.Forms;
using Xamarin.Media;
using Environment = System.Environment;

[assembly: Xamarin.Forms.Dependency(typeof(MainActivity))]
[assembly: Xamarin.Forms.ExportRenderer(typeof(Telerik.XamarinForms.DataControls.RadListView), typeof(Telerik.XamarinForms.DataControlsRenderer.Android.ListViewRenderer))]
namespace AppXF.Droid
{
    [Activity(Label = "AppXF", Icon = "@drawable/icon", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity, IPictureTaker
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            LoadDatabase();
            CachedImageRenderer.Init();
            global::Xamarin.Forms.Forms.Init(this, bundle);
            Telerik.XamarinForms.Common.Android.TelerikForms.Init();
            try
            {

                LoadApplication(new App());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);

            }
        }

        public override void OnLowMemory()
        {
            base.OnLowMemory();
            GC.Collect();
        }

        public void SnapPic()
        {
            var activity = Forms.Context as Activity;
            var picker = new MediaPicker(activity);
            var intent = picker.GetTakePhotoUI(new StoreCameraMediaOptions()
            {
                Name = "test.jpg",
                Directory = "Plural"
            });
            activity?.StartActivityForResult(intent, 1);
        }

        public void ChosePic()
        {
            var activity = Forms.Context as Activity;
            var picker = new MediaPicker(activity);
            var intent = picker.GetPickPhotoUI();
            activity?.StartActivityForResult(intent, 1);
        }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (resultCode == Result.Canceled)
                return;
            var mediafile = await data.GetMediaFileExtraAsync(Forms.Context);

            MessagingCenter.Send<IPictureTaker, string>(this, "pictureTaken", mediafile.Path);
        }


        public void LoadDatabase()
        {
            string dbName = "Plates.db";
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string dbPath = System.IO.Path.Combine(folder, dbName);

            if (!File.Exists(dbPath))
            {
                using (BinaryReader br = new BinaryReader(Assets.Open(dbName)))
                {
                    using (BinaryWriter bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int len = 0;
                        while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, len);
                        }
                    }
                }
            }




        }
    }



}

