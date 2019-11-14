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
    class LandingPageViewModel
    {
        


        private string name;
        public string Name
        {
            get { return name; }
        }

        public LandingPageViewModel()
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
            await App.Current.MainPage.Navigation.PushAsync(new EditProfilePage());
        }

        private async void DoLogout()
        {
            //code logout function
        }
    }
}

