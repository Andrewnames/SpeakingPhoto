using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppXF.Views
{
    public partial class MenuPage : ContentPage
    {

        public ListView ListView { get { return listView; } }
        private List<MasterPageItem> masterPageItems;

        public MenuPage()
        {
            InitializeComponent();

              masterPageItems = new List<MasterPageItem>
            {
                new MasterPageItem
                {
                    Title = "Add New Photo",
                //    IconSource = "contacts.png",
                    IconSource = "ic_add_a_photo_white.png",
                 TargetType = "AddPlatePage"
                },
                //new MasterPageItem
                //{
                //    Title = "About",
                //  //  IconSource = "toodo.png",
                //    IconSource = "icon.png",
                //    //TargetType = typeof(TodoListPage)
                //},
                //new MasterPageItem
                //{
                //    Title = "Reminders",
                //   // IconSource = "reminders.png",
                //    IconSource = "icon.png",
                //    // TargetType = typeof(ReminderPage)
                //}
            }; 
            listView.ItemsSource = masterPageItems;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            GC.Collect();
            //listView.ItemsSource = null;
            //masterPageItems = null;
        }




    }
}
