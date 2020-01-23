using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using SalesApp.DBModel;
using SalesApp.models;
using SalesApp.Pages;
using Xamarin.Forms;
using static SalesApp.models.CRMModel;

namespace SalesApp.views
{
    public partial class DraftQuotationsPage : ContentPage
    {
        List<SalesQuotation> crmdraftData = new List<SalesQuotation>();

        private async void RefreshDataconstructor()
        {
            await RefreshData();
        }


        public DraftQuotationsPage()
        {
            InitializeComponent();

            Title = "Draft Quotations";
            BackgroundColor = Color.White;
           

            if (Device.RuntimePlatform == Device.Android)
            {
                //Fixes an android bug where the search bar would be hidden
                searchBar.HeightRequest = 40.0;
            }

          //  crmdraftData = Controller.InstanceCreation().GetDraftQuotationData();

            salesQuotationListView.ItemsSource = crmdraftData;

            //  salesQuotationListView.ItemsSource = getSalesQuotationDetails();

            //   List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();

            if (App.filterstring == "Month" && App.load_rpc == true)
            {

                RefreshDataconstructor();
            }

            if (App.NetAvailable == true)
            {

                //  salesQuotationListView.ItemsSource = App.salesQuotList;

                var result1 = from y in App.SalesQuotationListDb
                              where y.yellowimg_string == "yellowcircle.png"
                              select y;

                if (result1.Count() == 0)
                {
                    salesQuotationListView.ItemsSource = App.draftQuotList;
                }

                else
                {
                    salesQuotationListView.ItemsSource = App.SalesQuotationListDb;
                }
            }


            //else if (App.NetAvailable == false)
            //{
            //    salesQuotationListView.ItemsSource = App.SalesQuotationListDb;
            //}


            var plusRecognizer = new TapGestureRecognizer();
            plusRecognizer.Tapped += (s, e) => {

                Navigation.PushPopupAsync(new DraftQuotationCreationPage());


            };
            plus.GestureRecognizers.Add(plusRecognizer);


            salesQuotationListView.Refreshing += this.RefreshRequested;
        }

        private async void OnMenuItemTappedAsync(object sender, ItemTappedEventArgs ea)
        {

            //  List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();

            var currentpage = new LoadingAlert();
          await PopupNavigation.PushAsync(currentpage);

         
            await Navigation.PushPopupAsync(new DraftQuotationsDetailPage(ea.Item as SalesQuotation));

          //  await PopupNavigation.PushAsync(new DraftQuotationsDetailPage(ea.Item as SalesQuotation));

         //  await PopupNavigation.PopAsync();

            //if (App.NetAvailable)
            //{

            //    var result1 = from y in App.SalesQuotationListDb
            //                  where y.yellowimg_string == "yellowcircle.png"
            //                  select y;

            //    if (result1.Count() == 0)
            //    {
            //        try
            //        {
            //            Navigation.PushPopupAsync(new DraftQuotationsDetailPage(ea.Item as SalesQuotation));

            //            //  App.Current.MainPage = new MasterPage(new SalesQuotationsListviewDetail(ea.Item as SalesQuotation));
            //        }
            //        catch
            //        {
            //           // Navigation.PushPopupAsync(new DraftQuotationsDetailPage(ea.Item as SalesQuotationDB));
            //            // App.Current.MainPage = new MasterPage(new SalesQuotationsListviewDetail(ea.Item as SalesQuotationDB));
            //        }
            //    }

            //    else
            //    {
            //        try
            //        {
            //            Navigation.PushPopupAsync(new SalesQuotationsListviewDetail(ea.Item as SalesQuotationDB));
            //        }
            //        catch
            //        {
            //            Navigation.PushPopupAsync(new DraftQuotationsDetailPage(ea.Item as SalesQuotation));
            //        }
            //    }


            //}

            //else if (App.NetAvailable == false)
            //{
            //    //try
            //    //{
            //    //    Navigation.PushPopupAsync(new SalesQuotationsListviewDetail(ea.Item as SalesQuotationDB));
            //    //}

            //    //catch
            //    //{
            //    //    Navigation.PushPopupAsync(new DraftQuotationsDetailPage(ea.Item as SalesQuotation));
            //    //}
            //}

        }

        async Task RefreshData()
        {
            List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();

          //  List<SalesQuotation> crmdraftData = Controller.InstanceCreation().GetDraftQuotationData();
        }

        private async void RefreshRequested(object sender, object e)
        {
            salesQuotationListView.IsRefreshing = true;
            //   await Task.Delay(200);

            await RefreshData();

          //  List<SalesQuotation> crmdraftData = Controller.InstanceCreation().GetDraftQuotationData();

            salesQuotationListView.ItemsSource = App.draftQuotList;

            //if (App.filterstring == "Month")
            //{
            //    RefreshDataconstructor();
            //}

            //if (App.NetAvailable == true)
            //{

            //    //   List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();
            //    salesQuotationListView.ItemsSource = crmdraftData;
            //    // salesQuotationListView.EndRefresh(); 

            //    salesQuotationListView.IsRefreshing = false;
            //}


            //else if (App.NetAvailable == false)
            //{
            //    // await Task.Delay(500);
            //    salesQuotationListView.ItemsSource = App.SalesQuotationListDb;
            //    salesQuotationListView.EndRefresh();
            //}
            salesQuotationListView.EndRefresh();
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
                //var result1 = from y in App.SalesQuotationListDb
                //              where y.yellowimg_string == "yellowcircle.png"
                //              select y;

                //if (result1.Count() == 0)
                //{
                //    salesQuotationListView.ItemsSource = App.salesQuotList;
                //}

                //else
                //{
                //    salesQuotationListView.ItemsSource = App.SalesQuotationListDb;
                //}

                salesQuotationListView.ItemsSource = App.draftQuotList;
            }

            else
            {

                salesQuotationListView.ItemsSource = App.draftQuotList.Where(x => x.customer.ToLower().Contains(e.NewTextValue.ToLower()) || x.name.ToLower().Contains(e.NewTextValue.ToLower()));

                //var result1 = from y in App.SalesQuotationListDb
                //              where y.yellowimg_string == "yellowcircle.png"
                //              select y;

                //if (result1.Count() == 0)
                //{
                //    salesQuotationListView.ItemsSource = App.salesQuotList.Where(x => x.customer.ToLower().Contains(e.NewTextValue.ToLower()) || x.name.ToLower().Contains(e.NewTextValue.ToLower()));
                //}

                //else
                //{
                //    salesQuotationListView.ItemsSource = App.SalesOrderListDb.Where(x => x.customer.ToLower().Contains(e.NewTextValue.ToLower()) || x.name.ToLower().Contains(e.NewTextValue.ToLower()));
                //}

                //  salesQuotationListView.ItemsSource = App.salesQuotList.Where(x => x.name.ToLower().StartsWith(e.NewTextValue.ToLower()));

            }
        }

        private void Toolbar_Filter_Activated(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new FilterPopupPage("tab3"));
            //Navigation.PushPopupAsync(new CrmFilterWizard());
        }
    }
}