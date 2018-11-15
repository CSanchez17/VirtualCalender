using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VirtualCalendarV01.Klassen;
using VirtualCalendarV01.Pages;
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

namespace VirtualCalendarV01
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class GruppeAnmeldung : Page
    {
        public GruppeAnmeldung()
        {
            this.InitializeComponent();           
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            Gruppe meineGruppe = Gruppe.sucheGruppe(txtGruppeName.Text,ref flag);
            if (flag)
            {
                
                //Check der Daten
                if ((meineGruppe.Str_GruppenPasword == txtPassword.Password) && (Benutzer.MyCurrentUser.Bool_EinladungliegtVor))
                {
                    Gruppe.MyCurrentGruppe = meineGruppe;
                    Benutzer.MyCurrentUser.Str_MitgliedVon = Gruppe.MyCurrentGruppe.Str_GruppenName;
                    Benutzer.MyCurrentUser.InGruppeAnmelden();
                    var message = new MessageDialog("Sie haben sich erfolgreich angemeldet");
                    await message.ShowAsync();
                }
                else
                {
                    var message = new MessageDialog("Das Passwort ist falsch, oder Sie haben noch keine Einladung erhalten.");
                    await message.ShowAsync();
                }
                //Gehe zu Profil bearbeiten                             
            }
            else
            {
                var message = new MessageDialog("Einloggen fehlgeschlagen");
                await message.ShowAsync();
            }
            Frame.Navigate(typeof(MainPage));
        }

        private void BackBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

       
    }
}
