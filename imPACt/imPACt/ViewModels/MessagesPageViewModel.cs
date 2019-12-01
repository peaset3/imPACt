using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using imPACt.Views;
using Plugin.FirebaseAuth;
using imPACt.Models;
using System.Drawing;
using Xamarin.Forms.Core;
using imPACt.ViewModels;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Firebase.Database;
using System.Linq;
namespace imPACt.ViewModels
{
    class MessagesPageViewModel
    {
        private string email;
        public string RequestEmail
        {
            get { return email; }
            set { email = value; }
        }
        private string query;
        public string Query
        {
            get { return query; }
            set { query = value; }
        }

        private IUser user;
        public string CurrentUid
        {
            get { return user.Uid; }
        }

        ObservableCollection<User> potentialConnections;

        public ObservableCollection<User> PotentialConnections
        {
            get
            {
                if (potentialConnections == null)
                {
                    potentialConnections = Task.Run(() => this.GenerateConnections()).Result;
                }
                return potentialConnections;
            }
        }

        private ObservableCollection<User> connections;
        public ObservableCollection<User> Connections
        {
            get
            {
                if (connections == null)
                {
                    var conn = Task.Run(() => this.GetConnections()).Result;
                    if (conn == null)
                        connections = new ObservableCollection<User>();
                    else
                        connections = conn;
                }
                return connections;
            }


            set { connections = value; }
        }

        public ImageSource getImg(User u)
        {
            ImageSource i = new Uri(u.PhotoUrl);
            return i;
        }
        public MessagesPageViewModel()
        {
            user = CrossFirebaseAuth.Current.Instance.CurrentUser;
        }

        public async Task<ObservableCollection<User>> GetConnections()
        {
            var temp = await FirebaseHelper.GetAllConnections(this.CurrentUid);
            return temp;
        }

        public Command GotoConversationCommand
        {
            get { return new Command<string>((x) => GotoConversation(x)); } //change to get current connection
        }

        async void GotoConversation(string luid)
        {
            var otherUser = await FirebaseHelper.GetUserByUid(luid);

            if (user != null)
            {

                try
                {
                    await App.Current.MainPage.Navigation.PushAsync(new ConversationPage(otherUser)); //change to get current connection
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }
            else
                await App.Current.MainPage.DisplayAlert("Error", "No profile could be found. Please try again.", "OK");
        }

        private async Task<ObservableCollection<User>> GenerateConnections()
        {
            var firebase = new FirebaseClient("https://impact-de4e1.firebaseio.com/");
            var menteeUser = await FirebaseHelper.GetUserByUid(CrossFirebaseAuth.Current.Instance.CurrentUser.Uid);


            var localMentorsList = (await firebase.Child("Users").OnceAsync<User>()).Select(item =>
                    new User
                    {
                        Uid = item.Object.Uid,
                        Email = item.Object.Email,
                        Surname = item.Object.Surname,
                        Lastname = item.Object.Lastname,
                        School = item.Object.School,
                        Degree = item.Object.Degree,
                        AccountType = item.Object.AccountType,
                        PhotoUrl = item.Object.PhotoUrl
                    }).Where(item => item.School == menteeUser.School
                                  && item.Degree == menteeUser.Degree
                                  && item.AccountType != 1).ToList();


            //Check first if there are any local mentors incase the mentee's school does not have mentors of requisite degree
            var localMentorCount = localMentorsList.Count();
            if (localMentorCount < 1)
            {
                localMentorsList = (await firebase.Child("Users").OnceAsync<User>()).Select(item =>
                    new User
                    {
                        Uid = item.Object.Uid,
                        Email = item.Object.Email,
                        Surname = item.Object.Surname,
                        Lastname = item.Object.Lastname,
                        School = item.Object.School,
                        Degree = item.Object.Degree,
                        AccountType = item.Object.AccountType,
                        PhotoUrl = item.Object.PhotoUrl
                    }).Where<User>(item => item.Degree == menteeUser.Degree
                                        && item.AccountType != 1).ToList();
                //If there are no local mentors, fetch ALL mentors with requisite degree
            }
            return new ObservableCollection<User>(localMentorsList);
        }
    }
}