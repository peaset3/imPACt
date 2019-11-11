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
    public partial class LandingPage : ContentPage
    {
        LandingPageViewModel welcomePageVM;
        public LandingPage(string name)
        {
            InitializeComponent();
            welcomePageVM = new LandingPageViewModel(name);
            BindingContext = welcomePageVM;
        }
    }
}