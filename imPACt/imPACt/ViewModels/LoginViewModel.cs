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
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public LoginViewModel()
        {
        }

        private IUser user;
        public IUser User
        {
            get { return user; }
            set { user = value; }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }

        private string surname;
        public string Surname
        {
            get { return surname; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Surname"));
            }
        }
        public Command LoginCommand
        {
            get
            {
                return new Command(Login);
            }
        }

        public Command SignUpCommand 
        {
            get 
            { 
                return new Command(DoSignUpPage); 
            }
        }
        private async void DoSignUpPage()
        {
            await App.Current.MainPage.Navigation.PushAsync(new SignUpPage());
        }

        private async void Login()
        {
            //null or empty field validation, check weather email and password is null or empty    

            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
            else
            {
                try
                {

                    //call GetUser function which we define in Firebase helper class    
                    var result = await CrossFirebaseAuth.Current.Instance.SignInWithEmailAndPasswordAsync(Email, Password);
                    user = CrossFirebaseAuth.Current.Instance.CurrentUser;
                    App.Current.MainPage = new MainTabbedNavigation();
                }
                catch (FirebaseAuthException e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                    var message = e.Reason ?? e.Message;
                    await App.Current.MainPage.DisplayAlert("Error", message, "OK");
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }
        }

    }
}
