using feednews.Code.Model;
using feednews.Code.Service;
using feednews.Code.View;
using Plugin.Media;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
namespace feednews.Code.ViewModel
{
    class CreatePostViewModel : PostModel
    {
        //Page and Navigation Instanciation
        Page Page;
        INavigation Navigation;

        //Variable responsable for storaging the image fullpath
        private string photo;

        //FeedService Object
        FeedService FeedService = new FeedService();

        //InsertPostModel
        PostModel PostModel;

        //Commands
        public Command CreatePostCommand { get; set; }
        public Command ChoosePhotoCommand { get; set; }

        //Constructor
        public CreatePostViewModel(Page page, INavigation navigation)
        {
            //Page and Navigation
            Page = page;
            Navigation = navigation;

            //Comands
            ChoosePhotoCommand = new Command(async () => await ChoosePhoto());
            CreatePostCommand = new Command(async () => await InsertPost());
        }

        //Choose Photo 
        private async Task ChoosePhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Page.DisplayAlert("Ops", "Gallery not supported.", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();
            try
            {
                if (file.Path.Length > 0)
                {
                    photo = file.Path;
                }

            }
            catch (Exception ex)
            {
                return;
            }

            ImageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;

            });

        }

        //Create a new post
        private new async Task InsertPost()
        {
            //Set the data
            PostModel = new PostModel()
            {
                Title = Title,
                Content = Content
            };

            if (string.IsNullOrEmpty(Title) && string.IsNullOrEmpty(Content))
            {
                await Page.DisplayAlert("Info", "Empty fields!", "OK");
            }
            else if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Content))
            {
                await Page.DisplayAlert("Info", "One of the fields is empty!", "OK");
            }
            else
            {

                //Send the data and wait for the result
                var Result = await FeedService.CreatePost(photo, PostModel);

                //Verify if errors on ImageError
                var ImageError = await SecureStorage.GetAsync("ImageError");

                if (ImageError != null && Result == null)
                {
                    await Page.DisplayAlert("Info", ImageError, "OK");
                    SecureStorage.Remove("ImageError");
                }
                else
                {
                    SecureStorage.Remove("ImageError");
                    Title = "";
                    Content = "";
                    await Page.DisplayAlert("Info", "Post created successfully!", "OK");
                    await Navigation.PushAsync(new FeedNews());
                }
            }
            


        }

    }
}
