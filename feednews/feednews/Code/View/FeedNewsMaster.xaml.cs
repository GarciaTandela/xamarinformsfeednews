using feednews.Code.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace feednews.Code.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedNewsMaster : ContentPage
    {
        UserViewModel context;
        public FeedNewsMaster()
        {
            InitializeComponent();
            context = new UserViewModel(this, Navigation);
            BindingContext = context;
        }
    }
}