﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using imPACt.ViewModels;

namespace imPACt
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        MainPageViewModel MainPageVM;
        public MainPage()
        {
            InitializeComponent();
            MainPageVM = new MainPageViewModel();
            BindingContext = MainPageVM;
        }
    }
}
