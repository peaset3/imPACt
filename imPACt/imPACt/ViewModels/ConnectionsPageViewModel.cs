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
        public string CurrentUid
        {
            get { return user.Uid; }
        }



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
        public ConnectionsPageViewModel()
        {
            user = CrossFirebaseAuth.Current.Instance.CurrentUser;
        }

        public Command RemoveConnectionCommand
        {
            get { return new Command<string>((x) => RemoveConnection(x)); }
        }

        private async void RemoveConnection(string uid)
        {
            Bitmap v;
            var requestingTo = await FirebaseHelper.GetUserByUid(uid);
            var requestorInfo = await FirebaseHelper.GetUserByUid(this.CurrentUid);
            if (requestingTo != null)
            {
                if ((requestingTo.AccountType == 2 && requestorInfo.AccountType == 1))
                {
                    await FirebaseHelper.RemoveUserConnection(this.CurrentUid, requestingTo.Uid);
                    await App.Current.MainPage.DisplayAlert("Success", "Accounts successfully unlinked.", "OK");
                }
                else if (requestingTo.AccountType == 1 && requestorInfo.AccountType == 2)
                {
                    await FirebaseHelper.RemoveUserConnection(requestingTo.Uid, this.CurrentUid);
                    await App.Current.MainPage.DisplayAlert("Success", "Accounts successfully unlinked.", "OK");
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

            potentialConnections.Add(requestingTo);
            connections.Remove(connections.Where(i => i.Uid == requestingTo.Uid).FirstOrDefault());
            }

        public Command AddConnectionCommand
        {
            get { return new Command<string>((x) => AddConnection(x)); }
        }
        private async void AddConnection(string uid)
        {
            Bitmap v;
            var requestingTo = await FirebaseHelper.GetUserByUid(uid);
            var requestorInfo = await FirebaseHelper.GetUserByUid(this.CurrentUid);
            if (requestingTo != null)
            {
                if ((requestingTo.AccountType == 2 && requestorInfo.AccountType == 1))
                {
                    await FirebaseHelper.AddUserConnection(this.CurrentUid, requestingTo.Uid);
                    await App.Current.MainPage.DisplayAlert("Success", "Accounts successfully linked.", "OK");
                }
                else if (requestingTo.AccountType == 1 && requestorInfo.AccountType == 2)
                {
                    await FirebaseHelper.AddUserConnection(requestingTo.Uid, this.CurrentUid);
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
            connections.Add(requestingTo);
            potentialConnections.Remove(potentialConnections.Where(i => i.Uid == requestingTo.Uid).FirstOrDefault());
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

        public Command GotoConnectionCommand
        {
            get { return new Command<string>((x) => GotoConnectionProfile(x)); }
        }

        async void GotoConnectionProfile(string luid)
        {
            var user = await FirebaseHelper.GetUserByUid(luid);

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

        public Command GotoConnectionConnectCommand
        {
            get { return new Command<string>((x) => GotoConnectionProfilewConnect(x)); }
        }

        async void GotoConnectionProfilewConnect(string luid)
        {
            var user = await FirebaseHelper.GetUserByUid(luid);

            if (user != null)
            {

                try
                {
                    await App.Current.MainPage.Navigation.PushAsync(new ProfilePageConnect(user));
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }
            else
                await App.Current.MainPage.DisplayAlert("Error", "No profile could be found. Please try again.", "OK");
        }

        public async Task<ObservableCollection<User>> GetConnections()
        {
            var temp = await FirebaseHelper.GetAllConnections(this.CurrentUid);
            return temp;
        }
        
        
        private async Task<ObservableCollection<User>> GenerateConnections()
        {
            var firebase = new FirebaseClient("https://impact-de4e1.firebaseio.com/");
            var menteeUser = await FirebaseHelper.GetUserByUid(CrossFirebaseAuth.Current.Instance.CurrentUser.Uid);


            var allMentorsList = (await firebase.Child("Users").OnceAsync<User>()).Select(item =>
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
                    }).Where(item => item.Degree == menteeUser.Degree
                                  && item.AccountType != 1).ToList();

            var mentors = new List<User>(allMentorsList);
            foreach (User u in allMentorsList)
            {
                if (connections.Contains(connections.Where(i => i.Uid == u.Uid).FirstOrDefault()))
                {
                    mentors.Remove(mentors.Where(i => i.Uid == u.Uid).FirstOrDefault());
                }
            }
            return new ObservableCollection<User>(mentors);
        }
    }
}
