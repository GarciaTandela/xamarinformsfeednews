using feednews.Code.View;
using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace feednews
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            /*{
                BarBackgroundColor = Color.DarkMagenta,
                BarTextColor = Color.White,

            };*/
            
        }

        protected override void OnStart()
        {
            if (Application.Current.Properties.ContainsKey("Auth_userId") && Application.Current.Properties["Auth_userId"] != null)
            {

                // do something with id
                MainPage = new NavigationPage(new FeedNews());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }

            Debug.WriteLine("OnStart");
        }

        protected override void OnSleep()
        {
            Debug.WriteLine("OnSleep");
        }

        protected override void OnResume()
        {
            Debug.WriteLine("OnResume");
        }
    }
}
