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
    public partial class EditProfilePage : ContentPage
    {

        EditProfileViewModel EditProfilePageVM;
        public EditProfilePage()
        {
            InitializeComponent();
            EditProfilePageVM = new EditProfileViewModel();
            BindingContext = EditProfilePageVM;
        }
    }
}