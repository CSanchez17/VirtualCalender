using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VirtualCalendarV01.Klassen;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace VirtualCalendarV01.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PopUpAnmeldung : Page
    {

      
        public PopUpAnmeldung()
        {
          
            this.InitializeComponent();

        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            //Gehe zu Profil bearbeiten
            Frame.Navigate(typeof(ProfilBearbeiten));
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (Benutzer.Login(txtUser.Text, txtPassword.Password))
            {
                
                //Gehe zu Profil bearbeiten
                Frame.Navigate(typeof(MainPage));
            }
            else
            {
                var message = new MessageDialog("Einloggen fehlgeschlagen");
                await message.ShowAsync();
            }
        }

        private async void txtPassword_AccessKeyInvoked(UIElement sender, AccessKeyInvokedEventArgs args)
        {
            if (Benutzer.Login(txtUser.Text, txtPassword.Password))
            {
                //Gehe zu Profil bearbeiten
                Frame.Navigate(typeof(MainPage));
            }
            else
            {
                var message = new MessageDialog("Einloggen fehlgeschlagen");
                await message.ShowAsync();
            }
        }
    }
}
