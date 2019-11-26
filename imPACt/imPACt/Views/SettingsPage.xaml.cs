using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Plugin.FirebaseAuth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using imPACt.ViewModels;

namespace imPACt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        SettingsViewModel settingsPageVM;
        public SettingsPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            settingsPageVM = new SettingsViewModel();
            BindingContext = settingsPageVM;
        }
    }
}