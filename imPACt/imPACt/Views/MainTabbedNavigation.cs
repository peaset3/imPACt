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
            var navigationPage = new NavigationPage(new MessagesPage());
            navigationPage.IconImageSource = "mail.png";
            navigationPage.Title = "Messages";
            this.Children.Add(navigationPage);

            navigationPage = new NavigationPage(new ConnectionsPage());
            navigationPage.IconImageSource = "link.png";
            navigationPage.Title = "Connections";
            this.Children.Add(navigationPage);

            navigationPage = new NavigationPage(new SettingsPage());
            navigationPage.IconImageSource = "settings.png";
            navigationPage.Title = "Settings";
            this.Children.Add(navigationPage);
        }
    }
}