﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using imPACt.Views;
using Plugin.FirebaseAuth;
using Firebase;
using System.Linq;

namespace imPACt.ViewModels
{

    public class SignUpViewModel : INotifyPropertyChanged
    {
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

        public event PropertyChangedEventHandler PropertyChanged;

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
                surname = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Surname"));
            }
        }
        private string lastname;
        public string Lastname
        {
            get { return lastname; }
            set
            {
                lastname = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Lastname"));
            }
        }
        private string school;
        public string School
        {
            get { return school; }
            set
            {
                school = value;
                PropertyChanged(this, new PropertyChangedEventArgs("School"));
            }
        }

        private string degree;
        public string Degree
        {
            get { return degree; }
            set
            {
                degree = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Degree"));
            }
        }
        private string confirmpassword;
        public string ConfirmPassword
        {
            get { return confirmpassword; }
            set
            {
                confirmpassword = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ConfirmPassword"));
            }
        }
        public Command SignUpCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (Password == ConfirmPassword)
                        SignUp();
                    else
                        App.Current.MainPage.DisplayAlert("", "Password must be same as above!", "OK");
                });
            }
        }
        public Command SignInCommand
        {
            get { return new Command(DoLoginPage); }

        }
        private async void DoLoginPage()
        {
            await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
        private async void SignUp()
        {
            //null or empty field validation, check weather email and password is null or empty    

            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Surname)
                || string.IsNullOrEmpty(Lastname) || string.IsNullOrEmpty(School) || string.IsNullOrEmpty(Degree) )
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
            else if ((Email.Substring(Email.Length - 4)) != ".edu")
                await App.Current.MainPage.DisplayAlert("Invalid Email", "imPACt requires that you use your school's .edu email to register.", "OK");
            else
            {

                //call AddUser function which we define in Firebase helper class    
                try {
                    //create new user in Firestore
                    var result = await CrossFirebaseAuth.Current.Instance.CreateUserWithEmailAndPasswordAsync(Email, Password);

                    //create new User entry in Database
                    var table = await FirebaseHelper.AddUser(Email, Surname, Lastname, School, Degree, result.User.Uid);

                    //AddUser return true if data insert successfuly     
                    if (result != null && table)
                    {
                        await App.Current.MainPage.DisplayAlert("SignUp Success", "", "Ok");
                        //Navigate to Welcome page after successfuly SignUp    
                        //pass user email to welcom page    
                        await App.Current.MainPage.Navigation.PopToRootAsync();
                        await App.Current.MainPage.Navigation.PushAsync(new MainTabbedNavigation());
                    }
                    else
                        await App.Current.MainPage.DisplayAlert("Error", "SignUp Fail", "OK");

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
