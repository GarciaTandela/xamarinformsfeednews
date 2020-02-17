using feednews.Code.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace feednews.Code.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        UserViewModel context;
        public Register()
        {
            InitializeComponent();
            context = new UserViewModel(this, Navigation);
            BindingContext = context;

        }
    }
}