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
    public partial class SignUpPage : ContentPage
    {
        SignUpViewModel signUpVM;
        public SignUpPage()
        {
            InitializeComponent();
            signUpVM = new SignUpViewModel();
            //set binding    
            BindingContext = signUpVM;
        }
    }
}