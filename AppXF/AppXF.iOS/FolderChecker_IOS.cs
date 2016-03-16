using System;
using System.Collections.Generic;
using System.Text;
using AppXF.iOS;
using Xamarin.Forms;

[assembly:Xamarin.Forms.Dependency(typeof(FolderChecker_IOS))]
namespace AppXF.iOS
{
    class FolderChecker_IOS : IFolderChecker
    {
        public string GetFolderName()
        {
           return  (Environment.GetFolderPath(Environment.SpecialFolder.Personal));
             
        }
    }
}
