using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using imPACt.Views;
using imPACt.Models;
using imPACt.ViewModels;
using Plugin.FirebaseAuth;
using System.Linq;
using Firebase.Database.Query;

namespace imPACt.ViewModels
{
    class SettingsViewModel : TabbedPage
    {

        private User user;
        public User CurrentUser
        {
            get
            {
                if (user == null)
                    user = Task.Run(() => FirebaseHelper.GetUserByUid(CrossFirebaseAuth.Current.Instance.CurrentUser.Uid)).Result;
                return user;
            }
        }


        public ImageSource PhotoSource
        {
            get
            {
                ImageSource i = new Uri(CurrentUser.PhotoUrl);
                return i;
            }
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
            
            var toUpdateUser = (await FirebaseHelper.firebase
                .Child("Users")
                .OnceAsync<User>()).Where(a => a.Object.Uid == CurrentUser.Uid).FirstOrDefault();

            await FirebaseHelper.firebase
            .Child("Users")
            .Child(toUpdateUser.Key)
            .PutAsync(new User() { School = CurrentUser.School, Degree = CurrentUser.Degree, PhotoUrl = CurrentUser.PhotoUrl});
        }

        private void DoLogout()
        {
            App.Current.MainPage = new MainPage();
        }
    }
}

