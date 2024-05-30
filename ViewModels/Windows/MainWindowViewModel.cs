﻿// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;
using LibManager.Views.Pages;

namespace LibManager.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly Wpf.Ui.INavigationService _navigationService = App.GetService<Wpf.Ui.INavigationService>();

        [ObservableProperty]
        private string _applicationTitle = "LibManager";


        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Home",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home12 },
                TargetPageType = typeof(RentalPage)
            },
   
            new NavigationViewItem()
            {
                Content = "Rental",
                Icon = new SymbolIcon {Symbol = SymbolRegular.HandLeft16 },
                TargetPageType = typeof(WaitingRentalPage),
            },

            new NavigationViewItem()
            {
                Content = "Return",
                Icon = new SymbolIcon {Symbol = SymbolRegular.Send16 },
                TargetPageType = typeof(WaitingReturnPage),
            },

            new NavigationViewItem()
            {
                Content = "Books",
                Icon = new SymbolIcon {Symbol = SymbolRegular.Book16 },
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
       

        public MainWindowViewModel()
        {
            
        }
    }
}