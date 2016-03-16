using System;
using System.Diagnostics;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppXF.Data;
using AppXF.Model;
using Java.Lang;
using Telerik.XamarinForms.DataControls.ListView;
using Xamarin.Forms;
using Exception = System.Exception;

namespace AppXF.Views
{
    public partial class FoodTab : ContentPage
    {
        IList<DataPlate> plates = new List<DataPlate>();
        
        private string localFolder;

        public FoodTab()
        {
            InitializeComponent();
           
        }
        
        private void PlateGridTapped(object sender, ItemTapEventArgs e)
        {
            var dataPlate = e.Item as DataPlate;
            if (dataPlate != null)
            {
                
                if (dataPlate.IsSynth)
                {
                    DependencyService.Get<ITextToSpeech>().Speak(dataPlate.ItemName, "en-US");
                }
                else
                {
                    DependencyService.Get<IVoiceRecorder>().Play(dataPlate.AudioPath);
                }
            }
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                // localFolder = DependencyService.Get<IFolderChecker>().GetFolderName();
                this.BindingContext = this;
                plates = DataBaseInstance.DatabaseInstance.GetDataPlates("Food").Where(x=>x.ImagePath!=null).ToList();
                //Device.OnPlatform(
                //    Android: () =>
                //    {

                //        for (int i = 0; i < 100; i++)
                //        {
                //            plates.Add(plates[1]);
                //        }
                //    },
                //    iOS: () =>
                //    {
                //        for (int i = 0; i < 20; i++)
                //        {
                //            plates.Add(plates[1]);
                //        }
                //        foreach (var plate in plates)
                //        {
                //            plate.ImagePath =
                //                plate.ImagePath.Replace(
                //                    "magic",
                //                    localFolder);
                //        }
                //    });

                if (plates.Count > 0)
                {

                    plateGrid.ItemsSource = plates;

                }
            }
            catch (Exception e)
            {



            }

            plateGrid.ItemTapped -= PlateGridTapped;
            plateGrid.ItemTapped += PlateGridTapped;
            
        }

        protected override void OnDisappearing() //не работает на айфон
        {
            base.OnDisappearing();
            plateGrid.ItemTapped -= PlateGridTapped;
             plateGrid.BindingContext = null;
            plateGrid.ItemsSource = null;
            //plateGrid = null;
            plates = null;
            BindingContext = null;
            
            GC.Collect();


        }
        
    }
}
