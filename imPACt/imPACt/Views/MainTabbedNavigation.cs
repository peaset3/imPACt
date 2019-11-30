using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace imPACt.Views
{
    public class MainTabbedNavigation : TabbedPage
    {
        public NavigationPage MessageTab { set; get; }
        public NavigationPage SettingsTab { set; get; }
        public NavigationPage ConnectionsTab { set; get; }
        public MainTabbedNavigation()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            MessageTab = new NavigationPage(new MessagesPage());
            MessageTab.IconImageSource = "mail.png";
            MessageTab.Title = "Messages";
            
            this.Children.Add(MessageTab);

            ConnectionsTab = new NavigationPage(new ConnectionsPage());
            ConnectionsTab.IconImageSource = "link.png";
            ConnectionsTab.Title = "Connections";
            this.Children.Add(ConnectionsTab);

            SettingsTab = new NavigationPage(new SettingsPage());
            SettingsTab.IconImageSource = "settings.png";
            SettingsTab.Title = "Settings";
            this.Children.Add(SettingsTab);

            (this as TabbedPage).CurrentPage = (this as TabbedPage).Children[1];
        }
    }
}