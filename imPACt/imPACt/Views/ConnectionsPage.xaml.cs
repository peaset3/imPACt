using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using imPACt.ViewModels;
using Xamarin.Forms;
using imPACt.Models;
using Xamarin.Forms.Xaml;
using Firebase.Database;
using Plugin.FirebaseAuth;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace imPACt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConnectionsPage : ContentPage
    {
        ConnectionsPageViewModel cpvm;
        public ConnectionsPage()
        {
            
            NavigationPage.SetHasNavigationBar(this, false);
            cpvm = new ConnectionsPageViewModel();
            BindingContext = cpvm;            
            InitializeComponent();         

        }

    }
}