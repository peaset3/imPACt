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

            /*int i = 0;
            string[] mentorNames = new string[cpvm.PotentialConnections.Count];
            foreach (User mentor in cpvm.PotentialConnections)
            {
                mentorNames[i] = mentor.Fullname;
                i++;
            }
            MyButtons.Children.Clear();
            foreach (var item in cpvm.PotentialConnections)
            {
                var btn = new Button()
                {
                    Text = item.Fullname,
                    StyleId = item.Uid
                    };
                btn.Clicked += OnDynamicBtnClicked;
                MyButtons.Children.Add(btn);
            }*/
            

        }

        private void OnDynamicBtnClicked(object sender, EventArgs e)
        {
            var myBtn = sender as Button;
            var uId = myBtn.StyleId;

            cpvm.Query = uId;

        }
    }
}