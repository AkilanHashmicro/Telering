using Rg.Plugins.Popup.Pages;
using Syncfusion.SfBusyIndicator.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingAlert : PopupPage
    {
        public LoadingAlert()
        {
            InitializeComponent();
            //busyindicator.AnimationType = AnimationTypes.Ball;
            //// busyIndicator.BackgroundColor = Color.Blue;
            //busyindicator.TextColor = Color.White;
            //busyindicator.Title = "Loading…";
        }
               
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // Method for animation child in PopupPage
        // Invoced after custom animation end

        protected override void OnAppearingAnimationEnd()
        {
           // return Content.FadeTo(1);
            base.OnDisappearingAnimationEnd();
        }

        // Method for animation child in PopupPage
        // Invoked before custom animation begin

        protected override void OnDisappearingAnimationBegin()
        {
           // return Content.FadeTo(1);

            base.OnDisappearingAnimationBegin();
        }

        protected override bool OnBackButtonPressed()
        {
            // Prevent hide popup
            return base.OnBackButtonPressed();
            //return true;
        }

        // Invoced when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return default value - CloseWhenBackgroundIsClicked
            //return base.OnBackgroundClicked();
            return false;
        }
    }
}