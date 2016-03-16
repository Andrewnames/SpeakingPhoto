using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppXF.Views
{
    public partial class MainPlatesPage : TabbedPage
    {
        public MainPlatesPage()
        {
            InitializeComponent();
            
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
           
            this.Children.Clear();
            BindingContext = null;
            
            GC.Collect();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }
    }
}
