using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Services;
using SalesApp.Pages;
using Xamarin.Forms;
using static SalesApp.models.CRMModel;

namespace SalesApp.views
{
    public partial class NewSalesTargetDetailPage : ContentPage
    {
        List<target_line> saleresult = new List<target_line>();
        public NewSalesTargetDetailPage(NewSalesTarget item)
        {
            InitializeComponent();

            Sales_target_layout.IsVisible = true;
            actual_target_layout.IsVisible = false;

          //  targetListview.ItemsSource = item.target_line;
            name_val.Text = item.team_name;
            year_val.Text = item.team_year;

            br_month.Text = item.team_month  + " " + "TG";
            br_ac_month.Text = item.team_month + " " + "AC";

            cat_month.Text = item.team_month + " " + "TG";
            cat_ac_month.Text = item.team_month + " " + "AC";

            subcat_month.Text = item.team_month + " " + "TG";
            subcat_ac_month.Text = item.team_month + " " + "AC";

            pro_month.Text = item.team_month + " " + "TG";
            pro_ac_month.Text = item.team_month + " " + "AC";

            //actual_br_month.Text = item.team_month;
            //actual_cat_month.Text = item.team_month;
            //actual_subcat_month.Text = item.team_month;
            //actual_pro_month.Text = item.team_month;

            brandListview.RowHeight = 40;

            brandListview.HeightRequest = item.target_vs_actual_brand.Count * 40;
            catListview.HeightRequest = item.target_vs_actual_category.Count * 40;
            subcatListview.HeightRequest = item.target_vs_actual_subcategory.Count * 40;
            productListview.HeightRequest = item.target_vs_actual_product.Count * 40;

            brandListview.ItemsSource = item.target_vs_actual_brand;
            catListview.ItemsSource = item.target_vs_actual_category;
            subcatListview.ItemsSource = item.target_vs_actual_subcategory;
            productListview.ItemsSource = item.target_vs_actual_product;

            //brandListview.HeightRequest = item.sale_brand_lines.Count * 35;
            //catListview.HeightRequest = item.sale_category_lines.Count * 35;
            //subcatListview.HeightRequest = item.sale_sub_category_lines.Count * 35;
            //productListview.HeightRequest = item.sale_brand_lines.Count * 35;

            //brandListview.ItemsSource = item.sale_brand_lines;
            //catListview.ItemsSource = item.sale_category_lines;
            //subcatListview.ItemsSource = item.sale_sub_category_lines;
            //productListview.ItemsSource = item.sale_product_lines;


            //act_brandListview.RowHeight = 35;

            //act_brandListview.HeightRequest = item.actual_brand_lines.Count * 35;
            //actual_catListview.HeightRequest = item.actual_category_lines.Count * 35;
            //actual_subcatListview.HeightRequest = item.sale_sub_category_lines.Count * 35;
            //actual_productListview.HeightRequest = item.actual_product_lines.Count * 35;


            //act_brandListview.ItemsSource = item.actual_brand_lines;
            //actual_catListview.ItemsSource = item.actual_category_lines;
            //actual_subcatListview.ItemsSource = item.actual_sub_category_lines;
            //actual_productListview.ItemsSource = item.actual_product_lines;



//Brand 
            var openimgbrandRecognizer = new TapGestureRecognizer();
            openimgbrandRecognizer.Tapped += (s, e) => {

                closeimg_brand.IsVisible = true;
                openimg_brand.IsVisible = false;

                brandGrid.IsVisible = true;
                brandList.IsVisible = true;

                //brandListview.ItemsSource = item.target_vs_actual_brand;
                //brandListview.HeightRequest = item.target_vs_actual_brand.Count * 35;


            
            };
            openimg_brand.GestureRecognizers.Add(openimgbrandRecognizer);


            var closeimgbrandRecognizer = new TapGestureRecognizer();
            closeimgbrandRecognizer.Tapped += (s, e) => {

                closeimg_brand.IsVisible = false;
                openimg_brand.IsVisible = true;

                brandGrid.IsVisible = false;
                brandList.IsVisible = false;

            };
            closeimg_brand.GestureRecognizers.Add(closeimgbrandRecognizer);


  //Actual Brand 

            //var actualopenimgbrandRecognizer = new TapGestureRecognizer();
            //actualopenimgbrandRecognizer.Tapped += (s, e) => {

            //    actual_closeimg_brand.IsVisible = true;
            //    actual_openimg_brand.IsVisible = false;

            //    actual_brandGrid.IsVisible = true;
            //    actual_brandList.IsVisible = true;

            //    act_brandListview.ItemsSource = item.actual_brand_lines;
            //    act_brandListview.HeightRequest = item.actual_brand_lines.Count * 35;


            //};
            //actual_openimg_brand.GestureRecognizers.Add(actualopenimgbrandRecognizer);


            //var actualcloseimgbrandRecognizer = new TapGestureRecognizer();
            //actualcloseimgbrandRecognizer.Tapped += (s, e) => {

            //    actual_closeimg_brand.IsVisible = false;
            //    actual_openimg_brand.IsVisible = true;

            //    actual_brandGrid.IsVisible = false;
            //    actual_brandList.IsVisible = false;

            //};
            //actual_closeimg_brand.GestureRecognizers.Add(actualcloseimgbrandRecognizer);


 // Category

            var openimgcatRecognizer = new TapGestureRecognizer();
            openimgcatRecognizer.Tapped += (s, e) => {

                closeimg_cat.IsVisible = true;
                openimg_cat.IsVisible = false;

                catGrid.IsVisible = true;
                catList.IsVisible = true;

                //catListview.ItemsSource = item.target_vs_actual_category;
                //catListview.HeightRequest = item.target_vs_actual_category.Count * 35;
            };
            openimg_cat.GestureRecognizers.Add(openimgcatRecognizer);


            var closeimgcatRecognizer = new TapGestureRecognizer();
            closeimgcatRecognizer.Tapped += (s, e) => {

                closeimg_cat.IsVisible = false;
                openimg_cat.IsVisible = true;

                catGrid.IsVisible = false;
                catList.IsVisible = false;

            };
            closeimg_cat.GestureRecognizers.Add(closeimgcatRecognizer);


 // Actual Category

            //var actual_openimgcatRecognizer = new TapGestureRecognizer();
            //actual_openimgcatRecognizer.Tapped += (s, e) => {

            //    actual_closeimg_cat.IsVisible = true;
            //    actual_openimg_cat.IsVisible = false;

            //    actual_catGrid.IsVisible = true;
            //    actual_catList.IsVisible = true;

            //    actual_catListview.ItemsSource = item.actual_category_lines;
            //    actual_catListview.HeightRequest = item.actual_category_lines.Count * 35;
            //};
            //actual_openimg_cat.GestureRecognizers.Add(actual_openimgcatRecognizer);


            //var actual_closeimgcatRecognizer = new TapGestureRecognizer();
            //actual_closeimgcatRecognizer.Tapped += (s, e) => {

            //    actual_closeimg_cat.IsVisible = false;
            //    actual_openimg_cat.IsVisible = true;

            //    actual_catGrid.IsVisible = false;
            //    actual_catList.IsVisible = false;

            //};
            //actual_closeimg_cat.GestureRecognizers.Add(actual_closeimgcatRecognizer);


//  Sub Category

            var openimgsubcatRecognizer = new TapGestureRecognizer();
            openimgsubcatRecognizer.Tapped += (s, e) => {

                closeimg_subcat.IsVisible = true;
                openimg_subcat.IsVisible = false;

                //subcatListview.ItemsSource = item.target_vs_actual_subcategory;
                //subcatListview.HeightRequest = item.target_vs_actual_subcategory.Count * 35;

                subcatGrid.IsVisible = true;
                subcatList.IsVisible = true;
            };
            openimg_subcat.GestureRecognizers.Add(openimgsubcatRecognizer);


            var closeimgsubcatRecognizer = new TapGestureRecognizer();
            closeimgsubcatRecognizer.Tapped += (s, e) => {

                closeimg_subcat.IsVisible = false;
                openimg_subcat.IsVisible = true;

                subcatGrid.IsVisible = false;
                subcatList.IsVisible = false;

            };
            closeimg_subcat.GestureRecognizers.Add(closeimgsubcatRecognizer);
 


 // Actual Sub Category

            //var actual_openimgsubcatRecognizer = new TapGestureRecognizer();
            //actual_openimgsubcatRecognizer.Tapped += (s, e) => {

            //    actual_closeimg_subcat.IsVisible = true;
            //    actual_openimg_subcat.IsVisible = false;

            //    actual_subcatListview.ItemsSource = item.actual_sub_category_lines;
            //    actual_subcatListview.HeightRequest = item.actual_sub_category_lines.Count * 35;

            //    actual_subcatGrid.IsVisible = true;
            //    actual_subcatList.IsVisible = true;
            //};
            //actual_openimg_subcat.GestureRecognizers.Add(actual_openimgsubcatRecognizer);


            //var actual_closeimgsubcatRecognizer = new TapGestureRecognizer();
            //actual_closeimgsubcatRecognizer.Tapped += (s, e) => {

            //    actual_closeimg_subcat.IsVisible = false;
            //    actual_openimg_subcat.IsVisible = true;

            //    actual_subcatGrid.IsVisible = false;
            //    actual_subcatList.IsVisible = false;

            //};
            //actual_closeimg_subcat.GestureRecognizers.Add(actual_closeimgsubcatRecognizer);


 // product Category


            var openimgproductRecognizer = new TapGestureRecognizer();
            openimgproductRecognizer.Tapped += (s, e) => {

                closeimg_product.IsVisible = true;
                openimg_product.IsVisible = false;

                //productListview.ItemsSource = item.target_vs_actual_product;
                //productListview.HeightRequest = item.target_vs_actual_product.Count * 35;

                productGrid.IsVisible = true;
                productList.IsVisible = true;
            };
            openimg_product.GestureRecognizers.Add(openimgproductRecognizer);


            var closeimgproducttRecognizer = new TapGestureRecognizer();
            closeimgproducttRecognizer.Tapped += (s, e) => {

                closeimg_product.IsVisible = false;
                openimg_product.IsVisible = true;

                productGrid.IsVisible = false;
                productList.IsVisible = false;

            };
            closeimg_product.GestureRecognizers.Add(closeimgproducttRecognizer);


   // Actual  product Category


            //var actual_openimgproductRecognizer = new TapGestureRecognizer();
            //actual_openimgproductRecognizer.Tapped += (s, e) => {

            //    actual_closeimg_product.IsVisible = true;
            //    actual_openimg_product.IsVisible = false;

            //    actual_productListview.ItemsSource = item.actual_product_lines;               
            //    actual_productListview.HeightRequest = item.actual_product_lines.Count * 35;

            //    actual_productGrid.IsVisible = true;
            //    actual_productList.IsVisible = true;
            //};
            //actual_openimg_product.GestureRecognizers.Add(actual_openimgproductRecognizer);


            //var actual_closeimgproducttRecognizer = new TapGestureRecognizer();
            //actual_closeimgproducttRecognizer.Tapped += (s, e) => {

            //    actual_closeimg_product.IsVisible = false;
            //    actual_openimg_product.IsVisible = true;

            //    actual_productGrid.IsVisible = false;
            //    actual_productList.IsVisible = false;

            //};
            //actual_closeimg_product.GestureRecognizers.Add(actual_closeimgproducttRecognizer);

        }


        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            ViewCell obj = (ViewCell)sender;
            obj.View.BackgroundColor = Color.FromHex("#FFFFFF");
        }


        //private void Tab1Clicked(object sender, EventArgs e)
        //{
        //    Sales_target_layout.IsVisible = true;
        //    actual_target_layout.IsVisible = false;

        //    tab1stack.BackgroundColor = Color.FromHex("#363E4B");
        //    tab1.BackgroundColor = Color.FromHex("#363E4B");
        //    tab2stack.BackgroundColor = Color.White;
        //    tab2.BackgroundColor = Color.White;
        //    tab2frame.BackgroundColor = Color.FromHex("#363E4B");
        //    tab2borderstack.BackgroundColor = Color.White;
        //    //orderLineList.IsVisible = true;
        //    //OtherInfoStack1.IsVisible = false;
        //    //OtherInfoStack2.IsVisible = false;
        //    tab1frame.BackgroundColor = Color.FromHex("#363E4B");
        //    tab1borderstack.BackgroundColor = Color.FromHex("#363E4B");
        //  //  OrderLineList1.IsVisible = true;

        //    tab2.TextColor = Color.FromHex("#363E4B");
        //    tab1.TextColor = Color.White;

        //}

        //private void Tab2Clicked(object sender, EventArgs e)
        //{
        //    Sales_target_layout.IsVisible = false;
        //    actual_target_layout.IsVisible = true;

        //    tab2stack.BackgroundColor = Color.FromHex("#363E4B");
        //    tab2.BackgroundColor = Color.FromHex("#363E4B");
        //    tab1stack.BackgroundColor = Color.White;
        //    tab1.BackgroundColor = Color.White;
        //    tab2borderstack.BackgroundColor = Color.FromHex("#363E4B");
        //    tab2frame.BackgroundColor = Color.FromHex("#363E4B");
        //    //orderLineList.IsVisible = false;
        //    //OtherInfoStack1.IsVisible = true;
        //    //OtherInfoStack2.IsVisible = true;
        //    tab1frame.BackgroundColor = Color.FromHex("#363E4B");
        //    tab1borderstack.BackgroundColor = Color.White;

        //    tab1.TextColor = Color.FromHex("#363E4B");
        //    tab2.TextColor = Color.White;


        // //   OrderLineList1.IsVisible = false;
        //}

    }
}
