using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Rg.Plugins.Popup.Services;
using SalesApp.models;
using SalesApp.Pages;
using SalesApp.views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading;

using static SalesApp.models.CRMModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Plugin.Geolocator;
using SQLite;
using System.Collections.ObjectModel;
using SalesApp.Persistance;
using SalesApp.DBModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SalesApp.OdooRpc.Model;
//using Android.Locations;


namespace SalesApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        public static SQLiteConnection _connection;
        public static List<ProductsList> ProductListDb = new List<ProductsList>();
        public static List<SalesQuotationDB> SalesQuotationListDb = new List<SalesQuotationDB>();
        public static List<SalesOrderDB> SalesOrderListDb = new List<SalesOrderDB>();
        public static List<CRMOpportunitiesDB> CRMOpportunitiesListDb = new List<CRMOpportunitiesDB>();

        public static List<taxes> taxListdb = new List<taxes>();

        public static List<UserModelDB> UserListDb = new List<UserModelDB>();


        public static List<CRMLeadDB> crmListDb = new List<CRMLeadDB>();
        public static Dictionary<int, string> cusdictDb = new Dictionary<int, string>();

        public static List<Customers> cusListDB = new List<Customers>();
       
        public static int userid_db = 0;
       // public static List<SalesOrder> SalesOrderDb = new List<SalesOrder>();

       // public static ObservableCollection<DbTable> _samp;

        private static Stopwatch stopWatch = new Stopwatch();
        private const int defaultTimespan = 1;
        //  public static var calattnList = "";

        public static List<next_activity> nextActivityList = new List<next_activity>();
        public static List<ProductsList> productList = new List<ProductsList>();
        public static List<LocationsList> locationsList = new List<LocationsList>();
       
        public static List<branch> branchList = new List<branch>();
        public static List<warehouse> warehousList = new List<warehouse>();
        public static List<analytic> analayticList = new List<analytic>();

        public static List<taxes> taxList = new List<taxes>();

        public static List<serial_list> serialList = new List<serial_list>();

        public static List<ContactsCreationList> concreationList = new List<ContactsCreationList>();

        public static List<paytermList> paytermList = new List<paytermList>();

        public static List<commisiongroupList> commisiongroupList = new List<commisiongroupList>();

        public static List<taxes> taxListRemove = new List<taxes>();
        public static List<serial_list> serialListRemove = new List<serial_list>();
        public static Dictionary<int, string> cusdict = new Dictionary<int, string>();
        public static Dictionary<int, string> reasondict = new Dictionary<int, string>();
        public static Dictionary<int, string> tagsDict = new Dictionary<int, string>();
        public static List<Customers> cusList = new List<Customers>();
        public static List<stages> stageList = new List<stages>();

        public static List<CRMLead> crmList = new List<CRMLead>();
        public static List<CRMOpportunities> crmOpprList = new List<CRMOpportunities>();
        public static List<SalesQuotation> salesQuotList = new List<SalesQuotation>();
        public static List<SalesQuotation> draftQuotList = new List<SalesQuotation>();
        public static List<SalesOrder> salesOrderList = new List<SalesOrder>();

        public static List<OrderLine> orderLineList = new List<OrderLine>();

        public static List<Product_PriceList> product_PriceList = new List<Product_PriceList>();
        public static List<all_delivery_method> all_delivery_method = new List<all_delivery_method>();

        public static List<account_journal> journalList = new List<account_journal>();

        public static int userid = 0;
        public static int partner_id = 0;
        public static string partner_name = "";
        public static string partner_image = "";
        public static string partner_email = "";

        public static string cpstring = "";
        public static string calselecteddate = "";
        public static string filterstring = "Month";
        public static Dictionary<string, dynamic> filterdict = new Dictionary<string, dynamic>();

        public static Dictionary<int, string> salesteam = new Dictionary<int, string>();
        public static Dictionary<int, string> salespersons = new Dictionary<int, string>();
        public static Dictionary<int, string> crmleadtags = new Dictionary<int, string>();
        public static Dictionary<int, string> countrydict = new Dictionary<int, string>();
        public static Dictionary<int, string> statedict = new Dictionary<int, string>();

        public static Dictionary<dynamic, dynamic> cus_address = new Dictionary<dynamic, dynamic>();

      

        public static  TimeSpan ts = stopWatch.Elapsed;

        public static Boolean user_gps_enabled = false;
        public static int user_gps_time = 0;
        public static String user_gps_period = "";

        public static bool NetAvailable = false;
        public static bool responseState = false;

        public static string StockFilterProduct = "";
        public static string StockFilterWarehouse = "";

        public static  bool tap_plusbtncheck = true;

        public static string user_location_string = "";

        public static bool load_rpc = true;

        public static int createContact_Id = 0;

        public static bool sq_rpc = false;
        public static bool so_rpc = false;
        public static bool so_tapped = true;


        public App()
        {
            InitializeComponent();

            App._connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            App._connection.CreateTable<UserModelDB>();
            try
            {
                var details = (from y in App._connection.Table<UserModelDB>() select y).ToList();
              //  App.UserListDb = details;
              
                if (App.cusList.Count == 0 && details.Count != 0)
                {
                    foreach (var res in details)
                    {
                        App.cusList = JsonConvert.DeserializeObject<List<Customers>>(res.customers_list);
                        App.productList = JsonConvert.DeserializeObject<List<ProductsList>>(res.products);
                        App.journalList = JsonConvert.DeserializeObject<List<account_journal>>(res.journal_list);
                        App.warehousList = JsonConvert.DeserializeObject<List<warehouse>>(res.warehouse_list);
                        App.journalList = JsonConvert.DeserializeObject<List<account_journal>>(res.journal_list);
                        App.salespersons = JsonConvert.DeserializeObject<Dictionary<int, string>>(res.sales_persons);
                        App.taxList = JsonConvert.DeserializeObject<List<taxes>>(res.tax_list);
                        App.partner_id = res.partnerid;
                        App.partner_name = res.user_name;
                        App.partner_image = res.user_image_medium;
                        App.partner_email = res.user_email;
                        App.userid = res.userid;
                    }
                }
            }
            catch (Exception ex)
            {
                int i = 0;
            }


            if (Settings.UserName.Length > 0 && Settings.UserPassword.Length > 0)
            {
              // App.Current.MainPage = new MasterPage(new LogoutPage());

                String res = "";

                    try
                    {
                     //  res = Controller.InstanceCreation().login(Settings.UserUrlName, Settings.UserDbName, Settings.UserName, Settings.UserPassword);
                       App.Current.MainPage = new MasterPage(new CrmTabbedPage("tab4"));
                       App.sq_rpc = true;
                     //   PopupNavigation.PushAsync( new CalendarPopupPage());
                    }

                   catch(Exception ea)
                    {
                        if (ea.Message.Contains("(Network is unreachable)") || ea.Message.Contains("NameResolutionFailure"))
                        {
                            App.NetAvailable = false;
                        }

                        else if (ea.Message.Contains("(503) Service Unavailable"))
                        {
                            App.responseState = false;
                        }
                    }

                    if (App.NetAvailable == false)
                    {
                    
                        App.Current.MainPage = new MasterPage(new CrmTabbedPage());
                    }
                

            }

            else
            {
                //  MainPage = new MainPage();
                MainPage = new SalesApp.views.LoginPage();
                //  MainPage = new SalesApp.views.GoogleMapScreen();
            }


          
        }


    
        //// Back Thread Ends

        //async Task FieldsData()
        //{

        //    await Task.Run(() =>
        //    {
        //     //  var user_details = App._connection.Query<UserModelDB>("SELECT * from UserModelDB");
        //        var user_details = (from y in App._connection.Table<UserModelDB>() select y).ToList();

        //        Device.StartTimer(new TimeSpan(0, 0, 1), () =>
        //        {
        //            if (App.cusList.Count == 0 && user_details.Count == 0 && Settings.UserId != 0)
        //            {
        //                App.cusList = Controller.InstanceCreation().GetCustomersList();
        //                JObject sales_persons = Controller.InstanceCreation().GetSalespersonsList();
        //                App.salespersons = sales_persons.ToObject<Dictionary<int, string>>();
        //                App.journalList = Controller.InstanceCreation().GetjournalList();
        //                App.taxList = Controller.InstanceCreation().GettaxList();
        //                App.warehousList = Controller.InstanceCreation().GetwarehouseList();
        //                App.productList = Controller.InstanceCreation().GetProductssList();
        //                MessagingCenter.Send<string, string>("MyApp", "FieldsListUpdated", "true");

        //             }
        //            return true;
        //        });
                                            
        //     });

        //}

        protected  override async void OnStart()
        {

            //  Task.Run(async () => timer());
            //  await MainRefreshData();
          //  await  FieldsData();
                
            // Handle when your app starts
        }

        private async void timer()
        {
            // ***********SalesQuotationDB

            //  await MainRefreshData();

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps

            stopWatch.Reset();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            stopWatch.Reset();
        }


    }

}
