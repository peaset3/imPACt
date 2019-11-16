using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace imPACt.Views
{
    public class MainTabbedNavigation : TabbedPage
    {
        public MainTabbedNavigation()
        {
            this.Children.Add(new NavigationPage(new MessagesPage()));
            this.Children.Add(new NavigationPage(new ConnectionsPage()));
            this.Children.Add(new NavigationPage(new SettingsPage()));
        }
    }
}