using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppXF.Views;
using Xamarin.Forms;

namespace AppXF
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            masterPage.ListView.ItemSelected -= OnItemSelected;
            
            GC.Collect();
           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            masterPage.ListView.ItemSelected += OnItemSelected;

        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                Detail = new WeakReference(new AddPlatePage()).Target as Page;
                masterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}
