using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using imPACt.Views;
using imPACt.Models;
using Plugin.FirebaseAuth;


namespace imPACt.ViewModels
{
    class SettingsViewModel : TabbedPage
    {


        private string name;
        public string Name
        {
            get { return name; }
        }

        public SettingsViewModel()
        {
        }
        public Command EditProfileCommand
        {
            get { return new Command(DoEditProfile); }

        }

        public Command LogoutCommand
        {
            get { return new Command(DoLogout); }
        }

        private async void DoEditProfile()
        {
            var user = await FirebaseHelper.GetUserByUid(CrossFirebaseAuth.Current.Instance.CurrentUser.Uid);
            await App.Current.MainPage.Navigation.PushAsync(new ProfilePage(user));
        }

        private void DoLogout()
        {
            App.Current.MainPage = new MainPage();
        }
    }
}

