using feednews.Code.Model;
using GraphQL.Client.Http;
using GraphQL.Common.Request;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace feednews.Code.Service
{
    class UserService
    {
 
        //Create User
        public async Task<string> CreateUser(UserModel model)
        {

            var userRequest = new GraphQLRequest
            {
                Query = @"
	            mutation createUser($email:String!,$password:String!,$name:String!)
                {
                  createUser(userInput:{email:$email,password:$password,name:$name})
                  {
                    name
                  }
                }",
                OperationName = "createUser",
                Variables = new
                {
                    email = model.Email,
                    password = model.Password,
                    name = model.Name
                }
            };

            GraphQLHttpClient graphQLClient = new GraphQLHttpClient("http://192.168.0.12:8080/graphql");
            
            var graphQLResponse = await graphQLClient.SendMutationAsync(userRequest);

            var Sucess = "";
            var Info = "";
            bool ErrorMessage; 

            if (graphQLResponse.Data.createUser != null)
            {
                Sucess = graphQLResponse.Data.createUser.name.Value;
                ErrorMessage = false;
            }
            else
            {
                Info = graphQLResponse.Errors[0].Message;
                ErrorMessage = true;
            }

            return ErrorMessage == false ? Sucess.Replace(graphQLResponse.Data.createUser.name.Value, "Your profile has been created: " + graphQLResponse.Data.createUser.name.Value) : Info;

        }

        //Login
        public async Task<string> Login(UserModel model) 
        {
            var LoginRequest = new GraphQLRequest
            {
                Query = @"
	            query SignIn($email:String!,$password:String!)
                {
                    login(email:$email,password:$password)
                    {
                        token
                        userId
                    }
                }",
                OperationName = "SignIn",
                Variables = new
                {
                    email = model.Email,
                    password = model.Password
                }
            };

            GraphQLHttpClient graphQLClient = new GraphQLHttpClient("http://192.168.0.12:8080/graphql");

            var graphQLResponse = await graphQLClient.SendQueryAsync(LoginRequest);

            var Sucess = "";
            var Info = "";
            bool ErrorMessage;

            if (graphQLResponse.Data.login != null)
            {
                var Person = graphQLResponse.GetDataFieldAs<AuthModel>("login"); //data->hero is casted as Person
                //MessagingCenter.Send(this, "Person", Person);

                //Storing in SecureStorage(This doesn't work in Lifecycle)
                await SecureStorage.SetAsync("Auth_userId", Person.UserId);
                await SecureStorage.SetAsync("Auth_Token", Person.Token);

                //For lifecycle we use Application.current.properties
                Application.Current.Properties["Auth_userId"] = Person.UserId;
                Application.Current.Properties["Auth_Token"] = Person.Token;
                ErrorMessage = false;
                Sucess = "Logged";
            }
            else
            {
                Info = graphQLResponse.Errors[0].Message;
                ErrorMessage = true;

            }

            return ErrorMessage == false ? Sucess : Info;



        }

    }
}
