using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using Plugin.FirebaseAuth;


namespace imPACt.ViewModels
{
    class EditProfileViewModel
    {
        public EditProfileViewModel()
        {
            try
            {
                Uid = CrossFirebaseAuth.Current.Instance.CurrentUser.Uid;
            }
            catch(FirebaseAuthException e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
           
        }
        private string uid;

        public string Uid
        {
            get { return uid; }
            set { uid = value; }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
            }
        }

        private string school;

        public string School
        {
            get { return school; }
            set { school = value; }
        }
        public Command UpdateCommand
        {
            get { return new Command(UpdatePassword); }
        }

        public Command DeleteCommand
        {
            get { return new Command(Delete); }
        }
        //For Logout
        public Command LogoutCommand
        {
            get
            {
                return new Command(() =>
                {
                    App.Current.MainPage.Navigation.PopAsync();
                });
            }
        }
        //Update user data
        private async void UpdatePassword()
        {
            if (!string.IsNullOrEmpty(Password))
            {
                try
                {
                    await CrossFirebaseAuth.Current.Instance.CurrentUser.UpdatePasswordAsync(Password);
                    await App.Current.MainPage.DisplayAlert("Update Success", "", "Ok");
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
            else
                await App.Current.MainPage.DisplayAlert("Password Require", "Please enter your new password", "Ok");
        }

        
        //Delete user data
        private async void Delete()
        {
            try
            {
                
                var isdelete = await FirebaseHelper.DeleteUser(Uid);
                if (isdelete)
                    await App.Current.MainPage.Navigation.PopAsync();
                else
                    await App.Current.MainPage.DisplayAlert("Error", "Record not delete", "Ok");
            }
            catch (Exception e)
            {

                Debug.WriteLine($"Error:{e}");
            }
        }
    }
}

