using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Extensions;
using SalesApp.models;
using SalesApp.wizard;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SalesApp.Pages;
using static SalesApp.models.CRMModel;
using Plugin.Messaging;
using SalesApp.DBModel;
using Plugin.AudioRecorder;
using System.IO;
using Rg.Plugins.Popup.Services;
using System.Diagnostics;

namespace SalesApp.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrmLeadPage : ContentPage
    {
        String UploadData = "";
        bool start_record = false;
        Stopwatch stopWatch = new Stopwatch();



        AudioRecorderService recorder;
        AudioPlayer player;

        List<CRMLead> crmLeadAll;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<string, string>("MyApp", "leadListUpdated", (sender, arg) =>
            {
               // List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();
                crmLeadListView.ItemsSource = App.crmList;

            });
        }

        private async void RefreshDataconstructor()
        {
            await RefreshData();
        }

        async Task RefreshData()
        {
            List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();
        }
              
        public CrmLeadPage()
        {
            Title = "CRM Leads";
            BackgroundColor = Color.White;
            InitializeComponent();


           // player = new AudioPlayer();

            recorder = new AudioRecorderService
            {
                           
               // StopRecordingAfterTimeout = true,
                TotalAudioTimeout = TimeSpan.FromSeconds(50),
               // AudioSilenceTimeout = TimeSpan.FromSeconds(2)
            };

           
            if (App.filterstring == "Month")
            {
                RefreshDataconstructor();
            }

            if (Device.RuntimePlatform == Device.Android)
            {
                //Fixes an android bug where the search bar would be hidden
                searchBar.HeightRequest = 40.0;
            }


            if(App.NetAvailable == true)
            {
                
                var result1 = from y in App.crmListDb
                              where y.yellowimg_string == "yellowcircle.png"
                              select y;

                if (result1.Count() == 0)
                {
                    crmLeadListView.ItemsSource = App.crmList;
                }

                else
                {
                    crmLeadListView.ItemsSource = App.crmListDb;
                }
            }

        

            else if(App.NetAvailable == false)
            {
              crmLeadListView.ItemsSource = App.crmListDb;
            }
                      
         //   crmLeadListView.ItemsSource = App.crmList;

            crmLeadListView.Refreshing += this.RefreshRequested;

            var plusRecognizer = new TapGestureRecognizer();
            plusRecognizer.Tapped += async (s, e) => {
     
                await Navigation.PushPopupAsync(new LeadCreationPage());

            };
            plus.GestureRecognizers.Add(plusRecognizer);

        }

        private async void RefreshRequested(object sender, object e)
        {
            //await Task.Delay(2000);
            //List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();

            crmLeadListView.IsRefreshing = true;

              await RefreshData();

            if (App.filterstring == "Month")
            {
                RefreshDataconstructor();
            }

            if (App.NetAvailable == true )
            {                
                crmLeadListView.ItemsSource = App.crmList;
                crmLeadListView.IsRefreshing = false;

                //var result1 = from y in App.crmListDb
                //              where y.yellowimg_string == "yellowcircle.png"
                //              select y;

                //if (result1.Count() == 0)
                //{
                //    await Task.Delay(2000);
                //    List<CRMLead> crmLeadData1 = Controller.InstanceCreation().crmLeadData();
                //    crmLeadListView.ItemsSource = App.crmList;
                //    crmLeadListView.EndRefresh();
                //}

                //else
                //{
                //    crmLeadListView.ItemsSource = App.crmListDb;
                //    crmLeadListView.EndRefresh();
                //}

                //await Task.Delay(2000);
                //List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();
                //  List<CRMLead> crmLeadData = Controller.InstanceCreation().crmFilterData();

            }

            else if (App.NetAvailable == false)
            {
               // await Task.Delay(500);
                crmLeadListView.ItemsSource = App.crmListDb;
                crmLeadListView.EndRefresh();
            }

            crmLeadListView.EndRefresh();

        }

        private void Toolbar_Search_Activated(object sender, EventArgs e)
        {
            if (searchBar.IsVisible)
            {
                searchBar.IsVisible = false;
            }
            else { searchBar.IsVisible = true; }
        }

        private void Toolbar_Filter_Activated(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new FilterPopupPage("tab1"));
        }


        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    MessagingCenter.Subscribe<Dictionary<string,dynamic>, string>(vals, "NotifyMsg", (sender, arg) =>
        //    {
        //        string retarg = arg;
        //        List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();
        //    });
        //}


        async Task RecordAudio()
        {
            try
            {
                if (!recorder.IsRecording) //Record button clicked
                {

                    var currentpage = new LoadingAlert();
                    await PopupNavigation.PushAsync(currentpage);

                    recorder.StopRecordingOnSilence = false;
                  //  recorder.AudioStreamDetails.SampleRate  = 0xA0000;
                //    recorder.AudioStreamDetails.BitsPerSample = 

                    await PopupNavigation.PopAsync();
                    // recorder.StopRecordingOnSilence = TimeoutSwitch.IsToggled;

                    //RecordButton.IsEnabled = false;
                    //PlayButton.IsEnabled = false;

                    //start recording audio

                    //recorder.IsRecording = true;
                    var audioRecordTask = await recorder.StartRecording();

                    //RecordButton.Text = "Stop Recording";
                    //RecordButton.IsEnabled = true;

               //  await audioRecordTask;

                    //RecordButton.Text = "Record";
                    //PlayButton.IsEnabled = true;
                }
                else //Stop button clicked
                {
                   // RecordButton.IsEnabled = false;

                    //stop the recording...
                    await recorder.StopRecording();

                   // RecordButton.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                //blow up the app!
                throw ex;
            }
        }



        async void phoneClicked(object sender, EventArgs e1)
        {
            var args = (TappedEventArgs)e1;

         
            try
            {
                CRMLead myobj = args.Parameter as CRMLead;

                var phoneDialer = CrossMessaging.Current.PhoneDialer;
                if (phoneDialer.CanMakePhoneCall && myobj.phone != "")
                {
                    start_record = true;


                    await DisplayAlert("Alert", "Your call will be recorded", "Ok");

                    DependencyService.Get<IPhoneCall>().makecall(myobj.phone);

                  //  await   recorder.StopRecordingOnSilence = false;


                    await Task.Delay(5000);

                  //  Thread.Sleep(10000);
              DependencyService.Get<IAudioRecorder>().StartRecording();
                                     

                  //  var res1 = Controller.InstanceCreation().UpdateLeadCreationData("ir.attachment", "create", vals);

                    var res =   await DisplayAlert("Alert", "Save the record?","Ok","Cancel");

                    if (res)
                    {

                      //  await recorder.StopRecording();

                       //stopWatch.Stop();

                        //await recorder.StopRecording();
                        //// Get the elapsed time as a TimeSpan value.
                        //TimeSpan ts = stopWatch.Elapsed;

                       
                        //try
                        //{
                        //    var filePath = recorder.GetAudioFilePath();

                        //    if (filePath != null)
                        //    {
                        //       // PlayButton.IsEnabled = false;
                        //       // RecordButton.IsEnabled = false;

                        //        player.Play(filePath);
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    //blow up the app!
                        //    throw ex;
                        //}

                     DependencyService.Get<IAudioRecorder>().StopRecording();  

                        var currentpage = new LoadingAlert();
                        await PopupNavigation.PushAsync(currentpage);


                        Dictionary<string, dynamic> vals = new Dictionary<string, dynamic>();

                        //string UploadData = DependencyService.Get<IAudioRecorder>().RecordAudio();

                        string UploadData = DependencyService.Get<IAudioRecorder>().OutputString();

                        vals["res_model"] = "crm.lead";
                        vals["res_id"] = myobj.id;
                        vals["name"] = "test.3gp";
                        vals["datas"] = UploadData;
                        vals["datas_fname"] = "test.3gp";

                        var res1 = Controller.InstanceCreation().UpdateLeadCreationData("ir.attachment", "create", vals);

                        DependencyService.Get<IAudioRecorder>().PlayRecording();

                     //  var res1 = Controller.InstanceCreation().UpdateLeadCreationData("ir.attachment", "create", vals);

                        await DisplayAlert("Alert", "Record updated successfully", "Ok");

                        await PopupNavigation.PopAsync();


                    }
                    else
                    {
                        // await DisplayAlert("Alert", "Try Again", "Ok");
                      //  await PopupNavigation.PopAsync();
                    }
                                                          
                }
                else
                {
                    start_record = false;
                    await Navigation.PushPopupAsync(new PhoneWizard());
                }
            }
            catch
            {
                CRMLeadDB myobj = args.Parameter as CRMLeadDB;
                var phoneDialer = CrossMessaging.Current.PhoneDialer;
                if (phoneDialer.CanMakePhoneCall && myobj.phone != "")
                {
                    phoneDialer.MakePhoneCall(myobj.phone);
                }
                else
                {
                    await Navigation.PushPopupAsync(new PhoneWizard());
                }
            }
        }


        private void meetingClicked(object sender, EventArgs e1)
        {
            try
            {
                App.Current.MainPage = new MasterPage(new CalendarPage());
            }

            catch
            {
                if (App.NetAvailable == false)
                {
                    DisplayAlert("Alert", "Need Internet Connection", "Ok");
                }
            }
        }

        //private void OnLabelTapped(Object sender, EventArgs e)
        //{


        //   // CRMLeadDB imageSender = (CRMLeadDB)sender;
        //        var args = (TappedEventArgs)e;
        //        CRMLeadDB myobj = args.Parameter as CRMLeadDB;
           
        // //   CRMLeadDB masterItemObj = (CRMLeadDB)e.Item

        //    int j = 0;
        //    //Your code here
        //}

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {               
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                //if (App.crmList.Count != 0)
                //{
                //    crmLeadListView.ItemsSource = App.crmList;
                //}

                //else
                //{
                //    crmLeadListView.ItemsSource = App.crmListDb;
                //}

                if (App.NetAvailable == true)
                {

                    var result1 = from y in App.crmListDb
                                  where y.yellowimg_string == "yellowcircle.png"
                                  select y;

                    if (result1.Count() == 0)
                    {
                        crmLeadListView.ItemsSource = App.crmList;
                    }

                    else
                    {
                        crmLeadListView.ItemsSource = App.crmListDb;
                    }
                }

                else
                {
                    crmLeadListView.ItemsSource = App.crmListDb;
                }

            }
            else
            {

                var result1 = from y in App.crmListDb
                              where y.yellowimg_string == "yellowcircle.png"
                              select y;

                if (result1.Count() == 0)
                {
                    crmLeadListView.ItemsSource = App.crmList.Where(x => x.customer.ToLower().Contains(e.NewTextValue.ToLower()) || x.name.ToLower().Contains(e.NewTextValue.ToLower()));
                }

                else
                {
                    crmLeadListView.ItemsSource = App.crmListDb.Where(x => x.customer.ToLower().Contains(e.NewTextValue.ToLower()) || x.name.ToLower().Contains(e.NewTextValue.ToLower()));
                }
            }
        }

        private void crmLeadListView_ItemTapped(object sender, ItemTappedEventArgs ea)
        {
            //if (App.NetAvailable == true && App.crmList.Count != App.crmListDb.Count && App.crmListDb.Count != 0)
            //{
            //    CRMLeadDB masterItemObj = (CRMLeadDB)ea.Item;
            //    Navigation.PushPopupAsync(new CrmLeadDetailWizard(masterItemObj, "Lead"));
            //}

            //else if (App.NetAvailable == true && App.crmList.Count == App.crmListDb.Count)
            //{
            //    CRMLeadDB masterItemObj = (CRMLeadDB)ea.Item;
            //    Navigation.PushPopupAsync(new CrmLeadDetailWizard(masterItemObj, "Lead"));
            //}

            //else if(App.NetAvailable == true && App.crmList.Count > App.crmListDb.Count)
            //{
            //    CRMLead masterItemObj = (CRMLead)ea.Item;
            //    Navigation.PushPopupAsync(new CrmLeadDetailWizard(masterItemObj, "Lead"));
            //}

            //else if(App.NetAvailable == false)
            //{
            //    CRMLeadDB masterItemObj = (CRMLeadDB)ea.Item;
            //    Navigation.PushPopupAsync(new CrmLeadDetailWizard(masterItemObj, "Lead"));
            //}


             //  List<CRMLead> crmLeadData = Controller.InstanceCreation().crmLeadData();



            //CRMLead masterItemObj = (CRMLead)ea.Item;
            //Navigation.PushPopupAsync(new CrmLeadDetailWizard(masterItemObj, "Lead"));

            if (App.NetAvailable == true)
            {
                crmLeadListView.ItemsSource = App.crmList;

                var result1 = from y in App.crmListDb
                              where y.yellowimg_string == "yellowcircle.png"
                              select y;

                if (result1.Count() == 0)
                {
                    try
                    {
                        CRMLead masterItemObj = (CRMLead)ea.Item;
                        Navigation.PushPopupAsync(new CrmLeadDetailWizard(masterItemObj, "Lead"));
                    }

                    catch
                    {
                        CRMLeadDB masterItemObj = (CRMLeadDB)ea.Item;
                        Navigation.PushPopupAsync(new CrmLeadDetailWizard(masterItemObj, "Lead"));
                    }
                }

                else
                {
                    try
                    {
                        CRMLeadDB masterItemObj = (CRMLeadDB)ea.Item;
                        Navigation.PushPopupAsync(new CrmLeadDetailWizard(masterItemObj, "Lead"));
                    }
                    catch{
                        CRMLead masterItemObj = (CRMLead)ea.Item;
                        Navigation.PushPopupAsync(new CrmLeadDetailWizard(masterItemObj, "Lead"));
                    }
                }
            }


            else if (App.NetAvailable == false)
            {
                try
                {
                    CRMLeadDB masterItemObj = (CRMLeadDB)ea.Item;
                    Navigation.PushPopupAsync(new CrmLeadDetailWizard(masterItemObj, "Lead"));
                }

                catch
                {
                    CRMLead masterItemObj = (CRMLead)ea.Item;
                    Navigation.PushPopupAsync(new CrmLeadDetailWizard(masterItemObj, "Lead"));
                }
            }


        }
    }
}
