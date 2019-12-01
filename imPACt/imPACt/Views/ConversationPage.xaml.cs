using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using imPACt.ViewModels;
using Xamarin.Forms;
using imPACt.Models;
using Xamarin.Forms.Xaml;


namespace imPACt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConversationPage : ContentPage
    {
        ConversationPageViewModel cpvm;
        public ConversationPage(User user)
        {
            cpvm = new ConversationPageViewModel();
            cpvm.Profile = user;
            BindingContext = cpvm;
            InitializeComponent();
        }
    }
}