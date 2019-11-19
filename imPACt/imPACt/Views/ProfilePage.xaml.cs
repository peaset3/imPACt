using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using imPACt.ViewModels;
using Xamarin.Forms;
using imPACt.Models;
using Xamarin.Forms.Xaml;

namespace imPACt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        ProfilePageViewModel ppvm;
        public ProfilePage(User user)
        {
            ppvm = new ProfilePageViewModel();
            ppvm.Profile =  user;
            BindingContext = ppvm;
            InitializeComponent();
        }
    }
}