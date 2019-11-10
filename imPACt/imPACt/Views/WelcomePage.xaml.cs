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
    public partial class WelcomePage : ContentPage
    {

        WelcomePageViewModel welcomePageVM;
        public WelcomePage(string surname)
        {
            InitializeComponent();
            welcomePageVM = new WelcomePageViewModel(surname);
            BindingContext = welcomePageVM;
        }
    }
}