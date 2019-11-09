using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using imPACt.Views;

namespace imPACt.ViewModels
{
    class MainPageViewModel
    {


        public Command LoginCommand
        {
            get { return new Command(DoLoginPage); }

        }

        public Command SignUpCommand
        {
            get { return new Command(DoSignUpPage); }
        }

        private async void DoLoginPage()
        {
            await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }

        private async void DoSignUpPage()
        {
            await App.Current.MainPage.Navigation.PushAsync(new SignUpPage());
        }
    }
}
