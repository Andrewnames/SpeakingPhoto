using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FFImageLoading.Forms.Touch;
using Foundation;
using Telerik.XamarinForms.DataControlsRenderer.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.ExportRenderer(typeof(Telerik.XamarinForms.DataControls.RadListView), typeof(Telerik.XamarinForms.DataControlsRenderer.iOS.ListViewRenderer))]

namespace AppXF.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            CachedImageRenderer.Init();

            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Plates.db");
           
            var appdir = NSBundle.MainBundle.ResourcePath;
            var seedFile = Path.Combine(appdir, "Plates.db");
            if (!File.Exists(path))
            {
               // File.Delete(path);
                File.Copy(seedFile, path);
            }

            try
            {
                new Telerik.XamarinForms.DataControlsRenderer.iOS.ListViewRenderer();
                global::Xamarin.Forms.Forms.Init();
                Telerik.XamarinForms.Common.iOS.TelerikForms.Init();
                LoadApplication(new App());

                return base.FinishedLaunching(app, options);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
