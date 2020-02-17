using feednews.Code.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace feednews
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        UserViewModel context;
        public MainPage()
        {
            InitializeComponent();
            context = new UserViewModel(this, Navigation);
            BindingContext = context;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        
    }
}
