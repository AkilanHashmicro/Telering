﻿using Rg.Plugins.Popup.Extensions;
using SalesApp.models;
using SalesApp.wizard;
using SalesApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SalesApp.models.CRMModel;
using SalesApp.DBModel;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;

namespace SalesApp.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuotationPage : ContentPage
    {
        int tap_count = 0;

        protected override void OnAppearing()
        {
            base.OnAppearing();
           
            MessagingCenter.Subscribe<string, string>("MyApp", "QuotListUpdated", (sender, arg) =>
            {

                //    salesQuotationListView.ItemsSource = App.salesQuotList;
            });
        }


      
        public  QuotationPage()
        {
            Title = "Sales Quotations";

            BackgroundColor = Color.White;
            InitializeComponent();

            //if (Device.RuntimePlatform == Device.Android)
            //{
            //    //Fixes an android bug where the search bar would be hidden
            //    searchBar.HeightRequest = 40.0;
            //}
            //  salesQuotationListView.ItemsSource = getSalesQuotationDetails();

            if (App.sq_rpc)
            {
                App.salesQuotList = Controller.InstanceCreation().GetSalesQuotations();
                salesQuotationListView.ItemsSource = App.salesQuotList;

                App.sq_rpc = false;
                    
            }

            else
            {
                salesQuotationListView.ItemsSource = App.salesQuotList;
            }
          


            if(App.NetAvailable == false)
            {
                salesQuotationListView.ItemsSource = App.SalesQuotationListDb;
            }


            var plusRecognizer = new TapGestureRecognizer();
            plusRecognizer.Tapped += (s, e) => {
                                            
                Navigation.PushPopupAsync(new SalesQuotationCreationPage());

                  
            };
            plus.GestureRecognizers.Add(plusRecognizer);


            salesQuotationListView.Refreshing += this.RefreshRequested;
        }

        private void OnMenuItemTapped(object sender, ItemTappedEventArgs ea)
        {

            //  List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();


            if (App.NetAvailable)
            {

                var result1 = from y in App.SalesQuotationListDb
                              where y.yellowimg_string == "yellowcircle.png"
                              select y;

                if (result1.Count() == 0)
                {
                    try
                    {
                        Navigation.PushPopupAsync(new SalesQuotationsListviewDetail(ea.Item as SalesQuotation));

                      //  App.Current.MainPage = new MasterPage(new SalesQuotationsListviewDetail(ea.Item as SalesQuotation));
                    }
                    catch
                    {
                       // Navigation.PushPopupAsync(new SalesQuotationsListviewDetail(ea.Item as SalesQuotationDB));
                       // App.Current.MainPage = new MasterPage(new SalesQuotationsListviewDetail(ea.Item as SalesQuotationDB));
                    }
                }

                else
                {
                    try
                    {
                      //  Navigation.PushPopupAsync(new SalesQuotationsListviewDetail(ea.Item as SalesQuotationDB));
                    }
                    catch
                    {
                        Navigation.PushPopupAsync(new SalesQuotationsListviewDetail(ea.Item as SalesQuotation));
                    }
                 }


            }

            else if(App.NetAvailable == false)
            {
                try
                {
                  //  Navigation.PushPopupAsync(new SalesQuotationsListviewDetail(ea.Item as SalesQuotationDB));
                }

                catch
                {
                    Navigation.PushPopupAsync(new SalesQuotationsListviewDetail(ea.Item as SalesQuotation));
                }
            }

        }

   

        private async void RefreshRequested(object sender, object e)
        {
            salesQuotationListView.IsRefreshing = true;
        
            App.salesQuotList = Controller.InstanceCreation().GetSalesQuotations();
            salesQuotationListView.ItemsSource = App.salesQuotList;
            //else if(App.NetAvailable ==false)
            //{
            //   // await Task.Delay(500);
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
                var result1 = from y in App.SalesQuotationListDb
                              where y.yellowimg_string == "yellowcircle.png"
                              select y;

                if (result1.Count() == 0)
                {
                    salesQuotationListView.ItemsSource = App.salesQuotList;
                }

                else
                {
                    salesQuotationListView.ItemsSource = App.SalesQuotationListDb;
                }
            }

            else
            {

                var result1 = from y in App.SalesQuotationListDb
                              where y.yellowimg_string == "yellowcircle.png"
                              select y;

                if (result1.Count() == 0)
                {
                    salesQuotationListView.ItemsSource = App.salesQuotList.Where(x => x.customer.ToLower().Contains(e.NewTextValue.ToLower()) || x.name.ToLower().Contains(e.NewTextValue.ToLower()));
                }

                else
                {
                    salesQuotationListView.ItemsSource = App.SalesOrderListDb.Where(x => x.customer.ToLower().Contains(e.NewTextValue.ToLower()) || x.name.ToLower().Contains(e.NewTextValue.ToLower()));
                }

              //  salesQuotationListView.ItemsSource = App.salesQuotList.Where(x => x.name.ToLower().StartsWith(e.NewTextValue.ToLower()));

            }
        }

        private void Toolbar_Filter_Activated(object sender, EventArgs e)
        {            
                Navigation.PushPopupAsync(new FilterPopupPage("tab4"));
               //Navigation.PushPopupAsync(new CrmFilterWizard());
        }
    }
}