using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VirtualCalendarV01.Klassen;
using VirtualCalendarV01.Model;
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
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class GruppeBearbeiten : Page
    {
        public GruppeBearbeiten()
        {
            this.InitializeComponent();
            //Hole die Leute, die in meiner Gruppe sind!
            BenutzerList.ItemsSource = Gruppe.MyCurrentGruppe.List_Benutzer;

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if ((Benutzer.MyCurrentUser.Bool_IsAdmin) && (Benutzer.MyCurrentUser.Str_MitgliedVon == Gruppe.MyCurrentGruppe.Str_GruppenName))
            {
                DeleteGruppeBarBtn.Visibility = Visibility.Visible;
            }
            else
            {
                DeleteGruppeBarBtn.Visibility = Visibility.Collapsed;
            }
        }

        private void BackBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void DeleteBarButton_Click(object sender, RoutedEventArgs e)
        {
            //Warne den Benutzer 
            var message = new MessageDialog("Möchten Sie Fortfahren?", "Ihre Mitgliedschaft wird gelöscht! ");
            message.Commands.Add(new Windows.UI.Popups.UICommand("Ja") { Id = 0 });
            message.Commands.Add(new Windows.UI.Popups.UICommand("Nein") { Id = 1 });

            message.DefaultCommandIndex = 0;
            message.CancelCommandIndex = 1;

            var result = await message.ShowAsync();
            if (result.Label == "Ja")
            {
                //Lösche Termine für den Benutzer in der Gruppe
                foreach (var item in Gruppe.MyCurrentGruppe.List_Termine)
                {
                    if ((item.Int_BenutzerId == Benutzer.MyCurrentUser.Id) && (item.Str_BenutzerId == Benutzer.MyCurrentUser.Str_Name))
                    {
                        item.TerminLoeschenSilently();
                    }
                }
                //Lösche Mitgliedschaft
                //Also setze die jeweilige GruppeDaten der Benutzer zurück
                Benutzer.MyCurrentUser.DeleteGruppeMitgliedschafftVomBenutzer();
                
                //aktualisiere den aktuellen Benutzer
                Benutzer.MyCurrentUser.RefreshCurrentUser();
            }
            Frame.GoBack();

        }

        private async void DeleteGruppeBarBtn_Click(object sender, RoutedEventArgs e)
        {
            //Warne den Benutzer 
            string grpe = Benutzer.MyCurrentUser.Str_MitgliedVon;
            var message = new MessageDialog("Möchten Sie fortfahren?", "Die Gruppe: "+ grpe + ", wird gelöscht! :)");
            message.Commands.Add(new Windows.UI.Popups.UICommand("Ja") { Id = 0 });
            message.Commands.Add(new Windows.UI.Popups.UICommand("Nein") { Id = 1 });

            message.DefaultCommandIndex = 0;
            message.CancelCommandIndex = 1;

            var result = await message.ShowAsync();
            if (result.Label == "Ja")
            {               
                //Lösche Termine für den Benutzer
                foreach (var item in Gruppe.MyCurrentGruppe.List_Termine)
                {
                    item.TerminLoeschenSilently();                    
                }
                //Lösche den aktuellen Benutzer vom Datenbank
                //Also setze die jeweilige GruppeDaten der Benutzer zurück
                foreach (var item in Gruppe.MyCurrentGruppe.List_Benutzer)
                {
                    item.DeleteGruppeMitgliedschafftVomBenutzer();
                    //Meldung an alle Mitgliedern schicken
                    string Betreff = "Die Gruppe: " + Gruppe.MyCurrentGruppe.Str_GruppenName + ", wurde gelöscht.";
                    string Message = "Alle Gruppentermine und auch Ihre Mitgliedschaft wurden gelöscht.";
                    EmailVerarbeitung.EmailSendenGruppeGelöscht(item.Str_Email, Betreff, Message);
                }

                //Setze Flag Admin als False für den Ersteller der Gruppe
                Benutzer.MyCurrentUser.Bool_IsAdmin = false;
                Benutzer.MyCurrentUser.UpdateAdminPowerVomBenutzer();

                //Delete Gruppe
                Gruppe.MyCurrentGruppe.DeleteGruppe();

                //aktualisiere den aktuellen Benutzer
                Benutzer.MyCurrentUser.RefreshCurrentUser();
            }
            Frame.GoBack();
        }

        private async void Meldung(string MessageD)
        {
            var dialog = new MessageDialog(MessageD);

            await dialog.ShowAsync();
        }
    }
}
