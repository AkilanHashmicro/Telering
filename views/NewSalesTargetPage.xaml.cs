using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Services;
using SalesApp.models;
using Xamarin.Forms;
using static SalesApp.models.CRMModel;

namespace SalesApp.views
{
    public partial class NewSalesTargetPage : ContentPage
    {
        List<NewSalesTarget> saleresult = new List<NewSalesTarget>();
        public NewSalesTargetPage()
        {
            InitializeComponent();

            saleresult = Controller.InstanceCreation().newsalesTargetData();
            salesOrderListView.ItemsSource = saleresult;

            if (saleresult[0].team_name == null ||saleresult[0].team_year == null ||
                saleresult[0].team_month == null)
            {

                overstacck.IsVisible = false;
               // frame_color.BackgroundColor = Color.FromHex("#F0EEEF");
            }

        }

        async void Loadingalertcall()
        {
            await PopupNavigation.PopAllAsync();
        }

        private void OnMenuItemTapped(object sender, ItemTappedEventArgs ea)
        {

            //    App.Current.MainPage = new MasterPage(new SalesOrderListviewDetail(ea.Item as SalesOrder));
            // App.Current.MainPage = new MasterPage(new SalesOrderDetailPage(ea.Item as SalesModel));

            Navigation.PushAsync(new NewSalesTargetDetailPage(ea.Item as NewSalesTarget));
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            ViewCell obj = (ViewCell)sender;
            // obj.View.BackgroundColor = Color.FromHex("#414141");  
            obj.View.BackgroundColor = Color.White;
        }
    }
}
