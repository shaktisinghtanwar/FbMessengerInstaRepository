using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Fb_InstaWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
			try 
			{
			this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            this.Exit += App_Exit;
			}
			 catch(Exception ex) 
			 {
				MessageBox.Show(ex.Message);
			
			}
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
			try
			 {
			 if  ( OnlineFetcher.chromeWebDriver != null )
					OnlineFetcher.chromeWebDriver.Quit();
			} 
			catch(Exception ex) 
			{
				MessageBox.Show(ex.Message);
			
			}
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
			try 
			{
			//MessageBox.Show("Some Error has occured. ");
				MessageBox.Show(e.Exception.ToString());
			
            e.Handled = true;

			} 
			catch(Exception ex) 
			{
				MessageBox.Show(ex.ToString());

			}
            
        }
    }
}
