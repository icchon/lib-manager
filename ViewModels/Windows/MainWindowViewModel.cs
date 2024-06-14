// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;
using LibManager.Views.Pages;
using LibManager.Models;
using System.Diagnostics;
using System.ComponentModel;

namespace LibManager.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly Wpf.Ui.INavigationService _navigationService = App.GetService<Wpf.Ui.INavigationService>();
        private readonly ObservalProps _props = App.GetService<ObservalProps>();


        [ObservableProperty]
        private string _applicationTitle = "LibManager";

        [ObservableProperty]
        private string _userName = ""; 

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Home",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home12 },
                TargetPageType = typeof(HomePage)
            },
   
            /*
            new NavigationViewItem()
            {
                Content = "Add",
                Icon = new SymbolIcon {Symbol = SymbolRegular.HandLeft16 },
                TargetPageType = typeof(AddBookPage),
            },

            new NavigationViewItem()
            {
                Content = "User",
                Icon = new SymbolIcon {Symbol = SymbolRegular.HandRight16 },
                TargetPageType = typeof(UserScanPage),
            },
            */
           

            new NavigationViewItem()
            {
                Content = "Books",
                Icon = new SymbolIcon {Symbol = SymbolRegular.Book24},
                TargetPageType = typeof(BooksPage),
            },
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Settings",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                //TargetPageType = typeof(RentalPage)
            },
            new NavigationViewItem()
            {
                Content = "Help",
                Icon = new SymbolIcon {Symbol = SymbolRegular.Question24},
                //TargetPageType = typeof(RentalPage)
            },
            new NavigationViewItem()
            {
                Content = "Notice",
                Icon = new SymbolIcon {Symbol=SymbolRegular.Info12},
                //TargetPageType = typeof (RentalPage),
            },
        };

        public void UpdateUserName()
        {
            UserName = _props.NowUser.Name;
        }
    }
}