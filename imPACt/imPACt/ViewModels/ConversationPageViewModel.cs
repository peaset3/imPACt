using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using imPACt.Views;
using Plugin.FirebaseAuth;
using imPACt.Models;
using System.Drawing;
using Xamarin.Forms.Core;
using imPACt.ViewModels;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Firebase.Database;
using System.Linq;


namespace imPACt.ViewModels
{
    class ConversationPageViewModel
    {
        private User user;
        public User Profile
        {
            get { return user; }
            set { user = value; }
        }

        private IUser userCurrent;
        public string CurrentUid
        {
            get { return userCurrent.Uid; }
        }

        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
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

        private Connection connection;

        public Connection Connection
        {
            get { return connection; }
            set { connection = this.GetConnection().Result; }
        }

        private List<string> conversation;

        public List<string> Conversation
        {
            get { return conversation; }
            set { conversation = Connection.Conversation; }
        }

        public async Task<ObservableCollection<User>> GetConnections()
        {
            var temp = await FirebaseHelper.GetAllConnections(this.CurrentUid); //make new method in firebase helper
            return temp;
        }

        //private ObservableCollection<User> connections;
        //public ObservableCollection<User> Connections
        //{
        //    get
        //    {
        //        if (connections == null)
        //        {
        //            var conn = Task.Run(() => this.GetConnections()).Result;
        //            if (conn == null)
        //                connections = new ObservableCollection<User>();
        //            else
        //                connections = conn;
        //        }
        //        return connections;
        //    }


        //    set { connections = value; }
        //}

        public async Task<Connection> GetConnection()
        {
            var connect = await FirebaseHelper.GetSpecificConnection(this.CurrentUid, Profile.Uid);
            return connect;
        }

        public Command AddMessageCommand
        {
            get { return new Command(() => AddMessageToConvo()); }
        }

        public async void AddMessageToConvo()
        {
            var name = await FirebaseHelper.GetUserByUid(this.CurrentUid);
            await FirebaseHelper.AddMessage(Message, Connection, name.Fullname);
        }
    }
}