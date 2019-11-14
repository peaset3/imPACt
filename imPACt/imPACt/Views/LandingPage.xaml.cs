using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.FirebaseAuth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using imPACt.ViewModels;

namespace imPACt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPage : ContentPage
    {
        LandingPageViewModel welcomePageVM;
        public LandingPage()
        {
            InitializeComponent();
            welcomePageVM = new LandingPageViewModel();
            BindingContext = welcomePageVM;
        }
    }
}