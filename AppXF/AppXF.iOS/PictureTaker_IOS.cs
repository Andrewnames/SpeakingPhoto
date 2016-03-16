using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AppXF.iOS;
using Xamarin.Forms;
using Xamarin.Media;

[assembly: Xamarin.Forms.Dependency(typeof(PictureTaker_IOS))]
namespace AppXF.iOS
{
    class PictureTaker_IOS : IPictureTaker
    {
        public async void SnapPic()
        {
            var picker = new MediaPicker();

             var file = await picker.TakePhotoAsync(new StoreCameraMediaOptions()
            {
                Name = "test.jpg",
                Directory = "Plur"
            })
            
            ;
            MessagingCenter.Send<IPictureTaker, string>(this, "pictureTaken", file.Path);
             

        }

        public  async void ChosePic()
        {
            var picker = new MediaPicker();

             var file = await picker.PickPhotoAsync();
 
            MessagingCenter.Send<IPictureTaker, string>(this, "pictureTaken", file.Path);

        }
    }
}
