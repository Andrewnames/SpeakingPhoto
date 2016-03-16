using Foundation;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using BabyPicker.CellModel;
using BabyPicker.Data;
using BabyPicker.DataSource;
using BabyPicker.Model;
using BabyPicker.Services;
using CoreGraphics;
using UIKit;

namespace BabyPicker
{
    class CollectionViewFlowDelegate : UICollectionViewDelegateFlowLayout
    {
        public override CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            return new SizeF(80.0f, 80.0f);
        }

     
        
        public override bool ShouldHighlightItem(UICollectionView collectionView, NSIndexPath indexPath)
        {
            return true;
        }

        public override void ItemHighlighted(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.CellForItem(indexPath);
            cell.ContentView.BackgroundColor = UIColor.Yellow;
        }

        public override void ItemUnhighlighted(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.CellForItem(indexPath);
            cell.ContentView.BackgroundColor = UIColor.White;
        }
    }
    partial class NewItemCollectionViewController : UICollectionViewController
	{
        PlateDataServices plateDataService = new PlateDataServices();
        
	    private List<DataPlate> _plates;
       
        public NewItemCollectionViewController (IntPtr _layout) : base (_layout)
		{ 
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            //plates = new List<DataPlate>()
            //{
            //    new DataPlate(),
            //    new DataPlate()
            //};


            //plateDataService.SaveDataPlate(new DataPlate());
            //plateDataService.SaveDataPlate(new DataPlate());
            //  var temp = DataBaseInstance.DatabaseInstance.GetAlbums();
            _plates = plateDataService.GetPlateCells();
            var datasource = new PlateDataSource(_plates, this);
            CollectionView.Source = datasource;
            CollectionView.Delegate = new CollectionViewFlowDelegate();
            CollectionView.ContentInset = new UIEdgeInsets(50, 0, 0, 0);

        }
 
	}
}
