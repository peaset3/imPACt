using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using imPACt.Views;


namespace imPACt.ViewModels
{
    class LandingPageViewModel
    {
        public LandingPageViewModel(string name2)
        {
            Name = name2;
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
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

