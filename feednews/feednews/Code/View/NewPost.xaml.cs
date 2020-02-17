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
    public partial class NewPost : ContentPage
    {
        CreatePostViewModel context;
        public NewPost()
        {
            InitializeComponent();
            context = new CreatePostViewModel(this, Navigation);
            BindingContext = context;
        }
    }
}