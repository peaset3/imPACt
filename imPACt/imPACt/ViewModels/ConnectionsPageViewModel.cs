using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using imPACt.Views;
using Plugin.FirebaseAuth;
using imPACt.Models;
using imPACt.ViewModels;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Firebase.Database;
using System.Linq;

namespace imPACt.ViewModels
{

    class ConnectionsPageViewModel
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
        public string Uid
        {
            get { return user.Uid; }
        }

        
        /*private ObservableCollection<User> currentConnections;

        public ObservableCollection<User> CurrentConnections
        {
            get
            {
                if (currentConnections == null)
                {
                    currentConnections = Task.Run(() => this.FindCurrentConnections()).Result;
                }
                return currentConnections;
            }
        }

        private async Task<ObservableCollection<User>> FindCurrentConnections()
        {
            ObservableCollection<User> currConn = new ObservableCollection<User>();
            foreach(User u in FirebaseHelper.GetAllConnections(Uid).Result)
            {
                currConn.Add(u);
            }
            return currConn;
        }
        */
        ObservableCollection<User> potentialConnections;

        public ObservableCollection<User> PotentialConnections { 
            get {
                if (potentialConnections == null)
                {
                    potentialConnections = Task.Run(() => this.GenerateConnections()).Result;
                }
                return potentialConnections;
            }
        }

        private List<User> connections;
        public List<User> Connections
        {
            get
            {
                if (connections == null)
                {
                    connections = Task.Run(() => this.GetConnections()).Result;
                }
                return connections;
            }
                
                
            set { connections = value; }
        }
        public ConnectionsPageViewModel()
        {
            user = CrossFirebaseAuth.Current.Instance.CurrentUser;
        }

        public Command AddConnectionCommand
        {
            get { return new Command(AddConnection); }
        }
        private async void AddConnection()
        {

            var requestingTo = await FirebaseHelper.GetUserByEmail(RequestEmail);
            var requestorInfo = await FirebaseHelper.GetUserByUid(this.Uid);
            if (requestingTo != null)
            {
                if ((requestingTo.AccountType == 2 && requestorInfo.AccountType == 1))
                {
                    await FirebaseHelper.AddUserConnection(this.Uid, requestingTo.Uid);
                    await App.Current.MainPage.DisplayAlert("Success", "Accounts successfully linked.", "OK");
                }
                else if (requestingTo.AccountType == 1 && requestorInfo.AccountType == 2)
                {
                    await FirebaseHelper.AddUserConnection(requestingTo.Uid, this.Uid);
                    await App.Current.MainPage.DisplayAlert("Success", "Accounts successfully linked.", "OK");
                }
                else
                {
                    if (requestorInfo.AccountType == 1)
                        await App.Current.MainPage.DisplayAlert("Error", "Mentee Accounts can only link with Mentor accounts.", "OK");
                    else
                        await App.Current.MainPage.DisplayAlert("Error", "Mentor Accounts can only link with Mentee accounts.", "OK");
                }
            }
            else
                await App.Current.MainPage.DisplayAlert("Error", "User not found. Please try again.", "OK");
        }

        public Command GotoProfileCommand
        {
            get { return new Command(GotoProfile); }
        }
        private async void GotoProfile()
        {
            var user = await FirebaseHelper.GetUserByEmail(Query);
            
            if (user != null)
            {
                
                try
                {
                    await App.Current.MainPage.Navigation.PushAsync(new ProfilePage(user));
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }
            else
                await App.Current.MainPage.DisplayAlert("Error", "No profile could be found. Please try again.", "OK");
        }

        public Command GotoConnectionProfileCommand
        {
            get { return new Command<string>(GotoConnectionProfile); }
        }

        async void GotoConnectionProfile(string lemail)
        {
            var user = await FirebaseHelper.GetUserByEmail(lemail);

            if (user != null)
            {

                try
                {
                    await App.Current.MainPage.Navigation.PushAsync(new ProfilePage(user));
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }
            else
                await App.Current.MainPage.DisplayAlert("Error", "No profile could be found. Please try again.", "OK");
        }


        public async Task<List<User>> GetConnections()
        {
            var temp = await FirebaseHelper.GetAllConnections(this.Uid);
            return temp;
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
                        AccountType = item.Object.AccountType
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
                        AccountType = item.Object.AccountType
                    }).Where<User>(item => item.Degree == menteeUser.Degree
                                        && item.AccountType != 1).ToList();
                //If there are no local mentors, fetch ALL mentors with requisite degree
            }
            return new ObservableCollection<User>(localMentorsList);
        }
    }
}
