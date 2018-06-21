using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Fb_InstaWpf.Helper;
using Fb_InstaWpf.Model;
using Fb_InstaWpf.ViewModel;
using OpenQA.Selenium.Chrome;

namespace Fb_InstaWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ChatMessenger : Window
    {
        public ChatMessenger()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel();
        }
            
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void TabLeftItemMessenger_GotFocus(object sender, RoutedEventArgs e)
        {
			try {
			TabRightItemInsta.Visibility = Visibility.Hidden;
            TabRightItemFacebook.Visibility = Visibility.Hidden;
            TabRightItemMessenger.IsSelected = true;

			 TabRightItemMessenger.GotFocus += TabRightItemMessenger_GotFocus;
			} 
			catch(Exception ex) 
			{
				MessageBox.Show(ex.ToString());
			
			}
        }

		private void TabRightItemMessenger_GotFocus(object sender,RoutedEventArgs e)
		{
			try {
			TabRightItemMessenger.IsSelected = true;
			}
			 catch(Exception ex) 
			{
				MessageBox.Show(ex.ToString());
			
			}
		}
		private void TabRightFacebookItem_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }
        private void TabLeftFacebookItem_GotFocus(object sender, RoutedEventArgs e)
        {
			try
			 {
			 TabRightItemMessenger.Visibility = Visibility.Hidden;
            TabRightItemInsta.Visibility = Visibility.Hidden;
            TabRightItemFacebook.Visibility = Visibility.Visible;
            TabRightItemFacebook.IsSelected = true;
			} 
			catch(Exception ex) 
			{
				MessageBox.Show(ex.ToString());
				
			}
        }

        private void TabLeftItemInsta_GotFocus(object sender, RoutedEventArgs e)
        {
			try {
			TabRightItemMessenger.Visibility = Visibility.Hidden;
            TabRightItemFacebook.Visibility = Visibility.Hidden;
            TabRightItemInsta.Visibility = Visibility.Visible;
            TabRightItemInsta.IsSelected = true;
			}
			 catch(Exception ex) 
			 {
				
				MessageBox.Show(ex.ToString());
			}
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
			try {
			TabRightItemInsta.Visibility = Visibility.Hidden;
            TabRightItemFacebook.Visibility = Visibility.Hidden;
            ImageProgressbar.Visibility = Visibility.Hidden;
			} 
			catch(Exception ex) 
			 {
				
				MessageBox.Show(ex.ToString());
			}
        }

        private void btnUserLogins_Click(object sender, RoutedEventArgs e)
        {
			
        }

		

       

    }
}
