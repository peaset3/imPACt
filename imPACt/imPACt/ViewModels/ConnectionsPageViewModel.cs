﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using imPACt.Views;
using Plugin.FirebaseAuth;
using imPACt.Models;
using imPACt.ViewModels;
using System.Threading.Tasks;


namespace imPACt.ViewModels
{

    class ConnectionsPageViewModel
    {
        private string email;
        public string RequestEmail
        {
            get { return email; }
            set { email = value; }
        }
        private string query;
        public string Query
        {
            get { return query; }
            set { query = value; }
        }

        private IUser user;
        public string Uid
        {
            get { return user.Uid; }
        }

        private List<User> connections;
        public List<User> Connections
        {
            get
            {
                if (connections == null)
                {
                    connections = Task.Run(() => this.GetConnections()).Result;
                }
                return connections;
            }
                
                
            set { connections = value; }
        }
        public ConnectionsPageViewModel()
        {
            user = CrossFirebaseAuth.Current.Instance.CurrentUser;
            
        }



        public Command AddConnectionCommand
        {
            get { return new Command(AddConnection); }
        }
        private async void AddConnection()
        {

            var requestingTo = await FirebaseHelper.GetUserByEmail(RequestEmail);
            var requestorInfo = await FirebaseHelper.GetUserByUid(this.Uid);
            if (requestingTo != null)
            {
                if ((requestingTo.AccountType == 2 && requestorInfo.AccountType == 1))
                {
                    await FirebaseHelper.AddUserConnection(this.Uid, requestingTo.Uid);
                    await App.Current.MainPage.DisplayAlert("Success", "Accounts successfully linked.", "OK");
                }
                else if (requestingTo.AccountType == 1 && requestorInfo.AccountType == 2)
                {
                    await FirebaseHelper.AddUserConnection(requestingTo.Uid, this.Uid);
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

        public Command GotoProfileCommand
        {
            get { return new Command(GotoProfile); }
        }
        private async void GotoProfile()
        {
            var user = await FirebaseHelper.GetUserByEmail(Query);
            
            if (user != null)
            {
                
                try
                {
                    await App.Current.MainPage.Navigation.PushAsync(new ProfilePage(user));
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }
            else
                await App.Current.MainPage.DisplayAlert("Error", "No profile could be found. Please try again.", "OK");
        }

        public async Task<List<User>> GetConnections()
        {
            var temp = await FirebaseHelper.GetAllConnections(this.Uid);
            return temp;
        }
    }



}
