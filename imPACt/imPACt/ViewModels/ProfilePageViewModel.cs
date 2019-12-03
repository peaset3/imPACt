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
using System.Threading.Tasks;

namespace imPACt.ViewModels
{
    
    class ProfilePageViewModel
    {
        private User user;
        public User Profile
        {
            get { return user; }
            set { user = value; }
        }

        public ImageSource PhotoSource
        {
            get
            {
                ImageSource i = new Uri(user.PhotoUrl);
                return i;
            }
        }

        public ProfilePageViewModel()
        {
        }

        public async Task<User> getUid(string uid)
        {
            user = await FirebaseHelper.GetUserByUid(uid); //bug here
            return user;
        }

        public string AccountType
        {
            get
            {
                if (user.AccountType == 1)
                    return "Mentee";
                else if (user.AccountType == 2)
                    return "Mentor";
                else
                    return "ERROR";
            }
        }

        public string Surname
        {
            get { return user.Surname; }
        }

        public string Lastname
        {
            get { return user.Lastname; }
        }

        public string School
        {
            get { return user.School; }
        }

        public string Degree
        {
            get { return user.Degree; }
        }

        public string Email
        {
            get { return user.Email; }
        }

        public string Bio
        {
            get { return user.Bio; }
        }
    }
}
