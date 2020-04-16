using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;

using Xamarin.Forms;
using static SalesApp.models.CRMModel;

namespace SalesApp.Pages
{
    public partial class SerialSelectionPage : PopupPage
    {
        string serialnew = "";
        public SerialSelectionPage()
        {
            InitializeComponent();
                     
            if (App.NetAvailable == true)
            {
                serialpickerListView.ItemsSource = App.serialList;

            }

            if (App.NetAvailable == false)
            {

                //proesult = App.ProductListDb;
               // taxpickerListView.ItemsSource = App.taxListdb;
            }
        }
        public SerialSelectionPage(string serialstring)
        {
            InitializeComponent();

            serialnew = serialstring;

            if (App.NetAvailable == true)
            {
                serialpickerListView.ItemsSource = App.serialList;

            }

            if (App.NetAvailable == false)
            {

                //proesult = App.ProductListDb;
               // taxpickerListView.ItemsSource = App.taxListdb;
            }
        }

        private void serialpickerListView_ItemTapped(object sender, ItemTappedEventArgs ea)
        {

            if (serialnew == "serial_new")
            {
                serial_list masterItemObj = (serial_list)ea.Item;

                MessagingCenter.Send<string, String>("MyApp", "serialnewPickerMsg", masterItemObj.name);

                Navigation.PopPopupAsync();
            }

            else
            {
                serial_list masterItemObj = (serial_list)ea.Item;

                MessagingCenter.Send<string, String>("MyApp", "serialPickerMsg", masterItemObj.name);

                Navigation.PopPopupAsync();
            }
          
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            ViewCell obj = (ViewCell)sender;
            obj.View.BackgroundColor = Color.FromHex("#f0eaea");
            //  m_title.TextColor = Color.Red;
        }
    }
}
