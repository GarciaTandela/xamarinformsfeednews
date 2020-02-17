using feednews.Code.Model;
using GraphQL.Client.Http;
using GraphQL.Common.Request;
using Newtonsoft.Json.Linq;
using Plugin.FileUploader;
using Plugin.FileUploader.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace feednews.Code.Service
{
    class FeedService
    {
        //getUserData
        public async Task<string> getUserData(string userId,string Token)
        {
            var getUserDataRequest = new GraphQLRequest
            {
                Query = @"
	            query Data($Id:ID)
                {
                    getUserData(userId:$Id)
                    {
                        status
                    }    
                }",
                OperationName = "Data",
                Variables = new
                {
                    Id = userId
                }
            };

            GraphQLHttpClient graphQLClient = new GraphQLHttpClient("http://192.168.0.12:8080/graphql");
            graphQLClient.DefaultRequestHeaders.Add("Authorization", $"bearer {Token}");

            var graphQLResponse = await graphQLClient.SendQueryAsync(getUserDataRequest);

            var Sucess = "";
            var Info = "";
            bool ErrorMessage;

            if (graphQLResponse.Data.getUserData != null)
            {
                Sucess = graphQLResponse.Data.getUserData.status.Value;
                ErrorMessage = false;
            }
            else
            {
                Info = graphQLResponse.Errors[0].Message;
                ErrorMessage = true;

            }

            return ErrorMessage == false ? Sucess : Info;

        }

        //CreatePost
        public async Task<PostModel> CreatePost(string photo,PostModel model) 
        {
            //Checks if the file was received
            if (photo == null)
            {
                await SecureStorage.SetAsync("ImageError", "No picture selected!");
                return null;
            }

            //Send the image file
            string Token = await SecureStorage.GetAsync("Auth_Token");

            var result = await CrossFileUploader.Current.UploadFileAsync("http://192.168.0.12:8080/post-image", new FilePathItem("image",photo), new Dictionary<string, string>()
                {
                   {"Authorization" , $"Bearer {Token}"}
                }
             );

            JObject resultmessage = JObject.Parse(result.Message);
            string filePath = (string)resultmessage["filePath"];

            //Create Post with the path and name image also the user id
            var CreatePost = new GraphQLRequest
            {
                Query = @"
	            mutation CreatePost($title:String!,$content:String!,$imageUrl:String!)
                {
                    createPost(postInput:{title:$title,content:$content,imageUrl:$imageUrl})
                    {
                         _id
                        title
                        content
                        imageUrl
                        creator
                        {
                            _id
                            name
                            email
                            password
                            status
                            posts
                            {
                                _id
                                title
                                content
                                imageUrl
                            }
                        }
                        createdAt
                        updatedAt
                    }
                }",
                OperationName = "CreatePost",
                Variables = new
                {
                    title=model.Title,
	                content=model.Content,
	                imageUrl= filePath
                }
            };


            GraphQLHttpClient graphQLClient = new GraphQLHttpClient("http://192.168.0.12:8080/graphql");
            graphQLClient.DefaultRequestHeaders.Add("Authorization", $"bearer {Token}");

            var graphQLResponse = await graphQLClient.SendMutationAsync(CreatePost);

            var Post = new PostModel();
            var Info = "";
            bool ErrorMessage;

            if (graphQLResponse.Data.createPost != null)
            {
                Post = graphQLResponse.GetDataFieldAs<PostModel>("createPost");
                ErrorMessage = false;
            }
            else
            {
                Info = graphQLResponse.Errors[0].Message;
                await SecureStorage.SetAsync("CreatePostError", Info);
                ErrorMessage = true;
            }

            return ErrorMessage == false ? Post: null;


        }
        
        //List All The Posts
        public async Task<PostModel> GetPosts()
        {
            var getPosts = new GraphQLRequest
            {
                Query = @"
	            query
                {
                    getPosts
                    {
                        posts
                        {
                            _id
                            title
                            imageUrl
                            creator
                            {
                                _id
                                name
                            }
                        }
                    }    
                }"

            };

            string Token = await SecureStorage.GetAsync("Auth_Token");

            GraphQLHttpClient graphQLClient = new GraphQLHttpClient("http://192.168.0.12:8080/graphql");
            graphQLClient.DefaultRequestHeaders.Add("Authorization", $"bearer {Token}");

            var graphQLResponse = await graphQLClient.SendQueryAsync(getPosts);

            var Sucess = new PostModel();
            var Info = "";
            bool ErrorMessage;

            if (graphQLResponse.Data.getPosts != null)
            {
                Sucess = graphQLResponse.GetDataFieldAs<PostModel>("getPosts");
                ErrorMessage = false;
            }
            else
            {
                Info = graphQLResponse.Errors[0].Message;
                ErrorMessage = true;

            }

            return ErrorMessage == false ? Sucess: null;

        }

        //List Single Post
        public async Task<PostModel> SinglePost(string id)
        {
            var getPost = new GraphQLRequest
            {
                Query = @"
	            query SinglePost($Id:ID)
                {
                    getPost(Id:$Id)
                    {
                        title
                        content
                        imageUrl
                    }    
                }",
                OperationName = "SinglePost",
                Variables = new
                {
                    Id = id
                }

            };

            string Token = await SecureStorage.GetAsync("Auth_Token");

            GraphQLHttpClient graphQLClient = new GraphQLHttpClient("http://192.168.0.12:8080/graphql");
            graphQLClient.DefaultRequestHeaders.Add("Authorization", $"bearer {Token}");
            graphQLClient.DefaultRequestHeaders.Add("Single", $"{id}");

            var graphQLResponse = await graphQLClient.SendQueryAsync(getPost);

            var Sucess = new PostModel();
            var Info = "";
            bool ErrorMessage;

            if (graphQLResponse.Data.getPost != null)
            {
                Sucess = graphQLResponse.GetDataFieldAs<PostModel>("getPost");
                ErrorMessage = false;
            }
            else
            {
                Info = graphQLResponse.Errors[0].Message;
                ErrorMessage = true;

            }

            return ErrorMessage == false ? Sucess : null;
        }

        //Delete Post
        public async Task<bool> DeletePost(string id)
        {
            var deletePost = new GraphQLRequest
            {
                Query = @"
	            mutation RemovePost($Id:ID)
                {
                    deletePost(Id:$Id)
                }",
                OperationName = "RemovePost",
                Variables = new
                {
                    Id = id
                }

            };

            string Token = await SecureStorage.GetAsync("Auth_Token");

            GraphQLHttpClient graphQLClient = new GraphQLHttpClient("http://192.168.0.12:8080/graphql");
            graphQLClient.DefaultRequestHeaders.Add("Authorization", $"bearer {Token}");
            graphQLClient.DefaultRequestHeaders.Add("Single", $"{id}");

            var graphQLResponse = await graphQLClient.SendMutationAsync(deletePost);

            bool ErrorMessage;

            if (graphQLResponse.Data.deletePost == true)
            {
                ErrorMessage = true;
            }
            else
            {
                ErrorMessage = false;
            }

            return ErrorMessage;
        }
    }
}
