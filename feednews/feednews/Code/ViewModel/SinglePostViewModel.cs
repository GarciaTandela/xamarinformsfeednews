using feednews.Code.Model;
using feednews.Code.Service;
using System.Threading.Tasks;

namespace feednews.Code.ViewModel
{
    class SinglePostViewModel : PostModel
    {
        //FeedService Object
        FeedService FeedService = new FeedService();

        //Constructors
        public SinglePostViewModel(string id)
        {
            Task.Run(async () => { await PostInfo(id); });
        }

        //List single post info
        private async Task PostInfo(string id)
        {
            var result = await FeedService.SinglePost(id);
            if (result == null)
            {

            }
            else
            {
                Title = result.Title;
                ImageUrl = result.ImageUrl;
                Content = result.Content;
            }
            
        }
    }
}
