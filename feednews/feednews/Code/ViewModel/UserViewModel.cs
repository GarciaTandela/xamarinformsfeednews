using feednews.Code.Model;
using feednews.Code.Service;
using feednews.Code.View;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace feednews.Code.ViewModel
{
    public class UserViewModel : UserModel
    {
        //Page and Navigation Instanciation
        Page Page;
        INavigation Navigation;

        //UserService Object
        UserService UserService = new UserService();
        
        //UserModel Object
        UserModel model;

        //Commands
        #region Commands
        public Command CreateUserCommand { get; set; }
        public Command SignUpCommand { get; set; }
        public Command LoginCommand { get; set; }
        public Command LogoutCommand { get; set; }
        #endregion

        //Constructor
        public UserViewModel(Page page, INavigation navigation)
        {
            Page = page;
            Navigation = navigation;

            //Commands
            CreateUserCommand = new Command(async () => await CreateUser(), () => !IsBusy);
            SignUpCommand = new Command(async () => await SignUp());
            LoginCommand = new Command(async () => await Login(), () => !IsBusy);
            LogoutCommand = new Command(async () => await Logout());
        }

        //Login User
        private async Task Login()
        {
            IsBusy = true;

            model = new UserModel()
            {
                Email = Email,
                Password = Password
            };

            var result = await UserService.Login(model);

            if (result=="Logged")
            {

                Password = "";
                Email = "";

                await Navigation.PushAsync(new FeedNews());

            }
            else
            {
                await Page.DisplayAlert("Info", result, "OK");
            }

            IsBusy = false;
        }

        //Create User
        private async Task CreateUser()
        {
            IsBusy = true;

            model = new UserModel()
            {
                Name=Name,
                Password=Password,
                Email=Email
            };

            if (string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Password) && string.IsNullOrEmpty(Email))
            {
                await Page.DisplayAlert("Info","Empty fields!","OK");
            }
            else if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Email))
            {
                await Page.DisplayAlert("Info", "One of the fields is empty!", "OK");
            }
            else
            {
                var result = await UserService.CreateUser(model);

                if (result.Contains(":"))
                {

                    Name = "";
                    Password = "";
                    Email = "";

                    await Page.DisplayAlert("Sucess", result, "OK");
                    await Navigation.PushAsync(new MainPage());

                }
                else
                {
                    await Page.DisplayAlert("Info", result, "OK");
                }
            }

            IsBusy = false;
        }

        //Register Navigation
        private async Task SignUp()
        {
            await Navigation.PushAsync(new Register());
        }

        //LOGOUT Navigation
        private async Task Logout()
        {
            SecureStorage.RemoveAll();
            Application.Current.Properties.Remove("Auth_Token");
            Application.Current.Properties.Remove("Auth_userId");
            await Navigation.PushAsync(new MainPage());
        }
    }
}
