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

namespace imPACt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConnectionsPage : ContentPage
    {
        ConnectionsPageViewModel cpvm;
        public ConnectionsPage()
        {
            
            NavigationPage.SetHasNavigationBar(this, false);
            cpvm = new ConnectionsPageViewModel();
            BindingContext = cpvm;
            InitializeComponent();

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