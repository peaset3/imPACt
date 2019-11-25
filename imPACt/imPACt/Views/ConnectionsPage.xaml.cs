using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using imPACt.ViewModels;
using Xamarin.Forms;
using imPACt.Models;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;
using Firebase.Database;
using Plugin.FirebaseAuth;
using System.Collections.ObjectModel;

namespace imPACt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConnectionsPage : ContentPage
    {
        ConnectionsPageViewModel cpvm;
        public ConnectionsPage()
        {
            GenerateConnections();
            //ConnectionView.ItemsSource = potentialConnections;
            NavigationPage.SetHasNavigationBar(this, false);
            cpvm = new ConnectionsPageViewModel();
            BindingContext = cpvm;
            InitializeComponent();

        }

        ObservableCollection<User> potentialConnections = new ObservableCollection<User>();
        public ObservableCollection<User> PotentialConnections { get { return potentialConnections; } }
        private async void GenerateConnections()
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
                    }).Where(item => item.School == menteeUser.School && item.Degree == menteeUser.Degree).ToList();


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
                    }).Where<User>(item => item.Degree == menteeUser.Degree).ToList();
                //If there are no local mentors, fetch ALL mentors with requisite degree
            }
            potentialConnections = new ObservableCollection<User>(localMentorsList);
        }

        async void GotoProfileClicked(object sender, EventArgs a)
        {
            var u = await FirebaseHelper.GetUserByEmail(viewprofile.Text);
            if (u != null)
                await Navigation.PushAsync(new ProfilePage(u));
            else
                await App.Current.MainPage.DisplayAlert("Error", "No profile could be found. Please try again.", "OK");
        }
    }
}