using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using imPACt.Views;
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
            await Navigation.PushAsync(new EditProfilePage());
        }

        private async void DoLogout()
        {
            App.Current.MainPage = new MainPage();
        }
    }
}

