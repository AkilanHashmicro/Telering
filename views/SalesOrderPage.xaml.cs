using Rg.Plugins.Popup.Extensions;
using SalesApp.DBModel;
using SalesApp.models;
using SalesApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Xamarin.Forms.Xaml;
using static SalesApp.models.CRMModel;

namespace SalesApp.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesOrderPage : ContentPage
    {
        //public List<SalesModel> getSalesOrderDetails()
        //{
        //    //List<SalesModel> quotationData = Controller.InstanceCreation().salesOrderData("sales");
        //    //return quotationData;

        //    List<SalesModel> quotationData1 = Controller.InstanceCreation().salesOrderData1();
        //    return quotationData1;

        //}


  

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<string, string>("MyApp", "so_swipped", async(sender, arg) =>
            {
                if(App.so_tapped)
                {

                    act_ind.IsRunning = true;
                           
                    await Task.Run(() =>  App.salesOrderList = Controller.InstanceCreation().GetSalesQrder());
                    salesOrderListView.ItemsSource = App.salesOrderList;
                    App.so_tapped = false;

                    act_ind.IsRunning = false;
                }

                else
                {
                    salesOrderListView.ItemsSource = App.salesOrderList;
                }

                //    salesQuotationListView.ItemsSource = App.salesQuotList;
            });
        }

        public SalesOrderPage()
        {
            Title = "Sales Order";

            BackgroundColor = Color.White;
            InitializeComponent();

            try
            {
                //  List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();
                if (App.so_rpc)
                {
                    App.salesOrderList = Controller.InstanceCreation().GetSalesQrder();
                    salesOrderListView.ItemsSource = App.salesOrderList;
                }
                else
                {
                    salesOrderListView.ItemsSource = App.salesOrderList;
                }
            }

            catch (Exception ea)
            {
               
            }

            salesOrderListView.Refreshing += this.RefreshRequested;
        }


        private void OnMenuItemTapped(object sender, ItemTappedEventArgs ea)
        {

            if (App.NetAvailable == true)
            {
              //  App.Current.MainPage = new MasterPage(new SalesOrderListviewDetail(ea.Item as SalesOrder));

                Navigation.PushPopupAsync(new SalesOrderListviewDetail(ea.Item as SalesOrder));
            }

            if (App.NetAvailable == false)
            {
               // App.Current.MainPage = new MasterPage(new SalesOrderListviewDetail(ea.Item as SalesOrderDB));
                Navigation.PushPopupAsync(new SalesOrderListviewDetail(ea.Item as SalesOrderDB));
            }
          // App.Current.MainPage = new MasterPage(new SalesOrderDetailPage(ea.Item as SalesModel));
        }

  

        private async void RefreshRequested(object sender, object e)
        {
           
            salesOrderListView.IsRefreshing = true;
            App.salesOrderList = Controller.InstanceCreation().GetSalesQrder();
            salesOrderListView.ItemsSource = App.salesOrderList;
            salesOrderListView.EndRefresh();

        }

        private void Toolbar_Search_Activated(object sender, EventArgs e)
        {
            if (searchBar.IsVisible)
            {
                searchBar.IsVisible = false;
            }
            else { searchBar.IsVisible = true; }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                salesOrderListView.ItemsSource = App.salesOrderList; ;
            }

            else
            {
                salesOrderListView.ItemsSource = App.salesOrderList.Where(x => x.customer.ToLower().Contains(e.NewTextValue.ToLower()) || x.name.ToLower().Contains(e.NewTextValue.ToLower()));
            }
        }


        private void Toolbar_Filter_Activated(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new FilterPopupPage("tab5"));
            //Navigation.PushPopupAsync(new CrmFilterWizard());
        }

        private void Handle_FabClicked(object sender, EventArgs e)
        {

        }
    }
}