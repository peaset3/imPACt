using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using imPACt.Models;
using System.Collections.ObjectModel;

namespace imPACt.ViewModels
{
    class FirebaseHelper
    {
        public static FirebaseClient firebase = new FirebaseClient("https://impact-de4e1.firebaseio.com/");

        //Read All    
        public static async Task<List<User>> GetAllUser()
        {
            try
            {
                var userlist = (await firebase
                .Child("Users")
                .OnceAsync<User>()).Select(item =>
                new User
                {
                    Uid = item.Object.Uid,
                    Email = item.Object.Email,
                    Surname = item.Object.Surname,
                    Lastname = item.Object.Lastname,
                    School = item.Object.School,
                    Degree = item.Object.Degree,
                    AccountType = item.Object.AccountType,
                    PhotoUrl = item.Object.PhotoUrl
                }).ToList();
                return userlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        public static async Task<ObservableCollection<User>> GetAllConnections(string uid) 
        {

            var users = await firebase
                .Child("Users")
                .OnceAsync<User>();
            var user = users.Where(a => a.Object.Uid == uid).FirstOrDefault();

            var key_check = (await firebase
                .Child("Users")
                .Child(user.Key)
                .Child("Active Connections")
                .OnceAsync<Key>());
            if (key_check.Count == 0)
                return new ObservableCollection<User>();
            var keys = key_check
            .Select(item =>
                new Key
                {
                    Value = item.Object.Value
                }).ToList();

            if (keys.Count == 0)
                return new ObservableCollection<User>();

            var connections = new ObservableCollection<Connection>();

            foreach (Key k in keys)
            {
                connections.Add(await firebase
                    .Child("Connections")
                    .Child(k.Value)
                    .OnceSingleAsync<Connection>());
            }

            ObservableCollection<User> connected_users = new ObservableCollection<User>();

            if (user.Object.AccountType == 1)
            {
                foreach (Connection c in connections)
                {
                    User u = await FirebaseHelper.GetUserByUid(c.MentorUid);
                    connected_users.Add(u);
                }
            }
            else
            {
                foreach (Connection c in connections)
                {
                    User u = await FirebaseHelper.GetUserByUid(c.MenteeUid);
                    connected_users.Add(u);
                }
            }

            return connected_users;
        }

        //Read     
        public static async Task<User> GetUserByEmail(string email)
        {
            try
            {
                var allUsers = await GetAllUser();
                await firebase
                .Child("Users")
                .OnceAsync<User>();
                return allUsers.Where(a => a.Email == email).FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        public static async Task<User> GetUserByUid(string Uid)
        {
            try
            {
                var allUsers = await GetAllUser();
                await firebase
                .Child("Users")
                .OnceAsync<User>();
                return allUsers.Where(a => a.Uid == Uid).FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Insert a user    
        public static async Task<bool> AddUser(string email, string surname, string lastname, string school, string degree, string uid, byte type)
        {
            try
            {


                await firebase
                .Child("Users")
                .PostAsync(new User() { Email = email, Surname = surname, Lastname = lastname,
                    School = school, Degree = degree, Uid = uid,
                    AccountType = type, PhotoUrl = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_640.png"
                });
                return true;
                
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        //Update     
        public static async Task<bool> UpdateUserEmail(string uid, string newEmail)
        {
            try
            {


                var toUpdateUser = (await firebase
                .Child("Users")
                .OnceAsync<User>()).Where(a => a.Object.Uid == uid).FirstOrDefault();
                await firebase
                .Child("Users")
                .Child(toUpdateUser.Key)
                .PutAsync(new User() { Email = newEmail });
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        public static async Task<bool> UpdateUserPhoto(string uid, string url)
        {
            try
            {


                var toUpdateUser = (await firebase
                .Child("Users")
                .OnceAsync<User>()).Where(a => a.Object.Uid == uid).FirstOrDefault();
                await firebase
                .Child("Users")
                .Child(toUpdateUser.Key)
                .PutAsync(new User() { PhotoUrl = url });
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        public static async Task<bool> UpdateUserSchool(string email, string school, string degree)
        {
            try
            {


                var toUpdateUser = (await firebase
                .Child("Users")
                .OnceAsync<User>()).Where(a => a.Object.Email == email).FirstOrDefault();
                await firebase
                .Child("Users")
                .Child(toUpdateUser.Key)
                .PutAsync(new User() { School = school, Degree = degree });
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        //Delete User    
        public static async Task<bool> DeleteUser(string email)
        {
            try
            {


                var toDeletePerson = (await firebase
                .Child("Users")
                .OnceAsync<User>()).Where(a => a.Object.Email == email).FirstOrDefault();
                await firebase.Child("Users").Child(toDeletePerson.Key).DeleteAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        //Add Message to Conversation
        public static async Task<bool> AddMessage(string message, Connection connection, string name)
        {
            try
            {
                //adds messsage and signature
                connection.Conversation.Add(message+" -"+name);

                //updates conversation itself
                var toUpdateConvo = (await firebase
                .Child("Connections")
                .OnceAsync<Connection>()).Where(a => a.Object.MenteeUid == connection.MenteeUid && a.Object.MentorUid == connection.MentorUid).FirstOrDefault();
                await firebase
                .Child("Connections")
                .Child(toUpdateConvo.Key)
                .PutAsync(obj: new Connection() { Conversation = connection.Conversation });
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        //Insert connection
        public static async Task<bool> AddUserConnection(string menteeuid, string mentorUid)
        {
            
            try
            {
                var post = await firebase
                    .Child("Connections")
                    .PostAsync(new Connection() { MentorUid = mentorUid, MenteeUid = menteeuid, Conversation= new List<string>() });

                var toUpdateUser = (await firebase
                    .Child("Users")
                    .OnceAsync<User>()).Where(a => a.Object.Uid == menteeuid).FirstOrDefault();

                await firebase
                    .Child("Users")
                    .Child(toUpdateUser.Key)
                    .Child("Active Connections")
                    .PostAsync(new Key() { Value = post.Key });

                toUpdateUser = (await firebase
                    .Child("Users")
                    .OnceAsync<User>()).Where(a => a.Object.Uid == mentorUid).FirstOrDefault();

                await firebase
                    .Child("Users")
                    .Child(toUpdateUser.Key)
                    .Child("Active Connections")
                    .PostAsync(new Key() { Value = post.Key });
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        public static async Task<Connection> GetSpecificConnection(string uidCurrent, string uidOther)
        {

            var users = await firebase
                .Child("Users")
                .OnceAsync<User>();
            var user = users.Where(a => a.Object.Uid == uidCurrent).FirstOrDefault();

            var key_check = (await firebase
                .Child("Users")
                .Child(user.Key)
                .Child("Active Connections")
                .OnceAsync<Key>());
            if (key_check.Count == 0)
                return new Connection();
            var keys = key_check
            .Select(item =>
                new Key
                {
                    Value = item.Object.Value
                }).ToList();

            if (keys.Count == 0)
                return new Connection();

            var connections = new ObservableCollection<Connection>();

            foreach (Key k in keys)
            {
                connections.Add(await firebase
                    .Child("Connections")
                    .Child(k.Value)
                    .OnceSingleAsync<Connection>());
            }

            foreach (Connection c in connections)
            {
                if ((c.MenteeUid == uidCurrent && c.MentorUid == uidOther) || (c.MenteeUid == uidCurrent && c.MenteeUid == uidOther))
                {
                    return c;
                }
            }
            return new Connection();
        }
    }
}    

