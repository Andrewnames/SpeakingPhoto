using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Text;
using BabyPicker.CellModel;
using BabyPicker.Model;
using CoreGraphics;
using Foundation;
using SQLite;
using SQLitePCL;
using UIKit;


namespace BabyPicker.DataSource
{

    class PlateDataSource : UICollectionViewSource, IUICollectionViewDelegateFlowLayout
    {


        static NSString plateCellId = new NSString("PlateCell");

        private List<DataPlate> plates;

        public PlateDataSource(List<DataPlate> _plates, UICollectionViewController controller)
        {
            controller.CollectionView.RegisterClassForCell(typeof(PlateCell), plateCellId);
            plates = _plates;
        }

        public override nint NumberOfSections(UICollectionView collectionView)
        {
            return 1;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return plates.Count;
        }


        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var plateCell = collectionView.DequeueReusableCell(plateCellId, indexPath) as PlateCell;
            var plate = plates[indexPath.Row];
             
            plateCell.Image = CreateImageFromString(plate.ItemImage);
            plateCell.LabelView.Text = plate.ID.ToString() ;
         // plateCell.Frame = new CGRect(plateCell.Frame.X, plateCell.Frame.Y, 100, 100);

            // plateCell.Image = UIImage.FromBundle("Icon.png");

            return plateCell;
        }

        private UIImage CreateImageFromString(string itemImage)
        {
            var t = CreateStringFromImage(new UIImage(Path.Combine(NSBundle.MainBundle.ResourcePath, "icon.png")));
            itemImage = t;
            byte[] bytes = Convert.FromBase64String(itemImage);
            var imageData = NSData.FromArray(bytes);
            return UIImage.LoadFromData(imageData);
        }

        private string CreateStringFromImage(UIImage image)
        {
            
            var imageData = image.AsJPEG(0.5f);
            return imageData.GetBase64EncodedData(NSDataBase64EncodingOptions.None).ToString();
        }

      

 
        

    }
}
