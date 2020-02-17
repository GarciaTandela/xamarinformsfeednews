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
    public partial class SinglePost : ContentPage
    {
        SinglePostViewModel context;
        public SinglePost(string id)
        {
            InitializeComponent();
            context = new SinglePostViewModel(id);
            BindingContext = context;
        }
    }
}