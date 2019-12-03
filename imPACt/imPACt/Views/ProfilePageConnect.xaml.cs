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
    public partial class ProfilePageConnect : ContentPage
    {
        ProfilePageViewModel ppvm;
        public ProfilePageConnect(User user)
        {
            ppvm = new ProfilePageViewModel();
            ppvm.Profile =  user;
            BindingContext = ppvm;
            InitializeComponent();
        }
    }
}