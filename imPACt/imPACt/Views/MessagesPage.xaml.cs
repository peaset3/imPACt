using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using imPACt.ViewModels;

namespace imPACt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagesPage : ContentPage
    {
        MessagesPageViewModel mpvm;
        public MessagesPage()
        {
            mpvm = new MessagesPageViewModel();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = mpvm;
            InitializeComponent();
        }
    }
}