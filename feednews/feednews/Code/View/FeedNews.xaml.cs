using feednews.Code.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace feednews.Code.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedNews : MasterDetailPage 
    {
        FeedViewModel context;
        public FeedNews()
        {
            InitializeComponent();
            context = new FeedViewModel(this, Navigation);
            BindingContext = context;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }


    }
}