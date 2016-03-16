using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace BabyPicker
{
	partial class ActionCollectionViewController : UICollectionViewController
	{
        UIButton buttonStarRect;
        public ActionCollectionViewController (IntPtr handle) : base (handle)
		{
            buttonStarRect = UIButton.FromType(UIButtonType.RoundedRect);
            buttonStarRect.Layer.CornerRadius = 2;
            buttonStarRect.Layer.BorderWidth = 1;
            View.AddSubview(buttonStarRect);
        }

       
    }
}
