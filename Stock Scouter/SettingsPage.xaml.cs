﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Stock_Scouter.Models;

namespace Stock_Scouter
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        private bool EnableAutoRefresh = false;

        public SettingsPage()
        {
            InitializeComponent();
        }

        private void SaveChanges(object sender, EventArgs e)
        {
            AppSettings.AutoRefreshInterval = Convert.ToInt32(AutoRefreshInterval_Input.Text);
            AppSettings.EnableAutoRefresh = EnableAutoRefresh;
            BackToPreviousPage();
        }

        private void DiscardChanges(object sender, EventArgs e)
        {
            BackToPreviousPage();
        }

        private void BackToPreviousPage()
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        private void EnableAutomaticRefresh_Checked(object sender, RoutedEventArgs e)
        {
            EnableAutoRefresh = true;
            AutoRefreshInterval_Input.IsEnabled = true;
        }

        private void EnableAutomaticRefresh_Unchecked(object sender, RoutedEventArgs e)
        {
            EnableAutoRefresh = false;
            AutoRefreshInterval_Input.IsEnabled = false;
        }

        private void AutoRefreshInterval_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("AutoRefreshInterval is changed to " + AutoRefreshInterval_Input.Text);
            try
            {
                int interval = Convert.ToInt32(AutoRefreshInterval_Input.Text);
                if (interval == 0)
                {
                    EnableAutoRefresh_Input.IsChecked = false;
                }
                else if (interval > 120 || interval < 0)
                {
                    string message = "Auto refresh interval should be a number from 1 to 120.";
                    string caption = "Error";
                    MessageBoxButton buttons = MessageBoxButton.OK;
                    MessageBoxResult result = MessageBox.Show(message, caption, buttons);
                    AutoRefreshInterval_Input.Text = AppSettings.AutoRefreshInterval.ToString();
                }
            }
            catch (Exception ex)
            {
                AutoRefreshInterval_Input.Text = AppSettings.AutoRefreshInterval.ToString();
            }
        }

        private void AutoRefreshInterval_TextChanged(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {

        }
    }
}