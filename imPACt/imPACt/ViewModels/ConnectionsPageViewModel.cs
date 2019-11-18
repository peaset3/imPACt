using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using imPACt.Views;
using Plugin.FirebaseAuth;
using imPACt.Models;
using imPACt.ViewModels;


namespace imPACt.ViewModels
{

    class ConnectionsPageViewModel
    {
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public Command AddConnectionCommand
        {
            get { return new Command(AddConnection); }
        }
        private async void AddConnection()
        {
            IUser requestor = CrossFirebaseAuth.Current.Instance.CurrentUser;
            var requestingTo = await FirebaseHelper.GetUserByEmail(Email);
            var requestorInfo = await FirebaseHelper.GetUserByUid(requestor.Uid);
            if (requestingTo != null)
            {
                if ((requestingTo.AccountType == 1 && requestorInfo.AccountType == 2) || (requestingTo.AccountType == 2 && requestorInfo.AccountType == 1))
                {
                    await FirebaseHelper.AddUserConnection(requestor.Uid, requestingTo.Uid);
                    await App.Current.MainPage.DisplayAlert("Success", "Accounts successfully linked.", "OK");
                }
                else
                {
                    if (requestorInfo.AccountType == 1)
                        await App.Current.MainPage.DisplayAlert("Error", "Mentee Accounts can only link with Mentor accounts.", "OK");
                    else
                        await App.Current.MainPage.DisplayAlert("Error", "Mentor Accounts can only link with Mentee accounts.", "OK");
                }
            }
            else
                await App.Current.MainPage.DisplayAlert("Error", "User not found. Please try again.", "OK");
        }

    }

    
}
