using System;
using System.Collections.Generic;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;

namespace BabyPicker.CellModel
{
    class PlateCell : UICollectionViewCell
    {
        private UIImageView imageView;
        public UILabel LabelView { get; set; }



        [Export("initWithFrame:")]
        public PlateCell(CGRect frame) : base(frame)
        {


            BackgroundView = new UIView { BackgroundColor = UIColor.Orange };

            SelectedBackgroundView = new UIView { BackgroundColor = UIColor.Green };

            ContentView.Layer.BorderColor = UIColor.LightGray.CGColor;
            ContentView.Layer.BorderWidth = 2.0f;
            ContentView.BackgroundColor = UIColor.White;
            ContentView.Transform = CGAffineTransform.MakeScale(0.8f, 0.8f);

            imageView = new UIImageView(UIImage.FromBundle("icon.png")) {Center = ContentView.Center};
            //imageView = _imageView;
             imageView.Transform = CGAffineTransform.MakeScale(0.7f, 0.7f);

            ContentView.AddSubview(imageView);


            LabelView = new UILabel
            {
                BackgroundColor = UIColor.Clear,
                TextColor = UIColor.DarkGray,
                TextAlignment = UITextAlignment.Center
            };

            ContentView.AddSubview(LabelView);
            // Frame = new CGRect(0, 0, 200, 200);
        }





        public UIImage Image
        {
            set
            {
                imageView.Image = value;
            }
        }


    }
}
