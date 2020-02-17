using feednews.Code.Model;
using feednews.Code.Service;
using feednews.Code.View;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace feednews.Code.ViewModel
{
    public class FeedViewModel:PostModel
    {
        //Page and Navigation Instanciation
        Page Page;
        INavigation Navigation;

        //FeedService Object
        FeedService FeedService = new FeedService();

        //The status of the user being loaded on FeedNews
        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(); }
        }

        //Commands
        public Command NewPostCommand { get; set; }
        public Command<object> SinglePostCommand { get; set; }
        public Command<object> DeletePostCommand { get; set; }

        //Constructor
        public FeedViewModel(Page page, INavigation navigation)
        {
            Page = page;
            Navigation = navigation;
            
            //Loads when the app starts
            Task.Run(async () => {await ListPosts(); });
            Task.Run(async () => { await GetUserData(); });

            //Commands instanciation
            NewPostCommand = new Command(async () => await NewPost());
            SinglePostCommand = new Command<object>(async (p) => await SinglePost(p));
            DeletePostCommand = new Command<object>(async (p) => await DeletePost(p));
        }

        //List all posts
        private async Task ListPosts()
        {
            var result = await FeedService.GetPosts();
            if (result.Posts.Count == 0)
            {

            }
            else
            {
                Posts = result.Posts;
            }
        }

        //Get logged user data
        private async Task GetUserData()
        {
            string userId = await SecureStorage.GetAsync("Auth_userId");
            string Token = await SecureStorage.GetAsync("Auth_Token");

            var result = await FeedService.getUserData(userId,Token);

            if (!result.Contains("Not"))
            {
                
                Status ="Status: "+result;

            }
            else
            {
                await Page.DisplayAlert("Information", result, "OK");
            }
        }

        //NewPost Navigation
        private async Task NewPost()
        {
            await Navigation.PushAsync(new NewPost());
        }

        //SinglePost Navigation with post ID
        private async Task SinglePost(object p)
        {
            var contact = p as InsertPost;
            if (contact != null) await Navigation.PushAsync(new SinglePost(contact._Id));
        }

        //DeletePost
        private async Task DeletePost(object p)
        {
            var contact = p as InsertPost;
            var result = contact != null && await FeedService.DeletePost(contact._Id);

            if (result == true)
            {
                Posts.Remove(contact);
            }
            else
            {
                await Page.DisplayAlert("Info", "Post not deleted", "OK");
            }
        }


    }
}
