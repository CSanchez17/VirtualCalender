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
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class AddGruppe : Page
    {
    
        private ObservableCollection<Benutzer> meineAuswahl = new ObservableCollection<Benutzer>();
        private Gruppe gruppe = new Gruppe();
        public AddGruppe()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            //-------------------------------------------------------------------------------------------------//
            //Hole die vorhandene Personen im DB
            CbPersonAuswahl.ItemsSource = Benutzer.getListeVonBenutzernNamenDieKeineGruppeHaben();
            //Den aktuellen Benutzer soll als Default im checkbox schon ausgewählt worden sein
            CbPersonAuswahl.SelectedItem = Benutzer.MyCurrentUser.Str_Name;

            //Ein neuer Benutzer wird als mitglied einer Gruppe mit deiner eigener Namen, deswegen prüfe ob das so ist
            //Wenn es auftritt, dann darf der Benutzer keine Gruppe erstellen
            if (Benutzer.MyCurrentUser.Str_MitgliedVon != Benutzer.MyCurrentUser.Str_Name)
            {              
                var dialog = new MessageDialog("Solange Sie Mitglied einer Gruppe sind, dürfen sie keine Gruppe erstellen. ");
                await dialog.ShowAsync();
                Frame.GoBack();
            }

        }

        //Das Ereignis CbPersonAuswahl_SelectionChanged tritt auf, wenn im ComboBOx CbPersonAuswahl
        //Ein anderes Item ausgewählt wird
        private void CbPersonAuswahl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Hole das selected Item (ausgewählten Benutzer
            string auswahlBenutzer = CbPersonAuswahl.SelectedItem.ToString();

            //Suche die Person nach dem Name
            //Setze den Flag auf false (Bei Erfolgreichen Suche, wird flag = true)
            bool flag = false;
            Benutzer _benutzerSelected = Benutzer.sucheVerantwoertlichMitName(auswahlBenutzer, ref flag);

            //Füge den ausgewählten Benutzer in meiner Collection hinzu, wenn der Benutzer gefunden wird
            meineAuswahl.Add(_benutzerSelected);
            if (flag)
            {
                PersonenList.ItemsSource = meineAuswahl;
            }
      
           //sonst wurde die Person nicht gefunden, was ehe nie geschieht, denn die mögliche Auswahl ist ja nur für schon geprüfte Elemente im DB
        }

        private void BackBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void AddBarButton_Click(object sender, RoutedEventArgs e)
        {
            
            //Setze dass diese Person gehört zu den angegebenen Gruppe
            //Hole Info von TExtboxs
            string Str_Gruppe = Tb_GRName.Text;
            string Str_GruppePassword = passwordBox.Password;

            //Setze den aktuellen Benutzer als Admin
            Benutzer.MyCurrentUser.Bool_IsAdmin = true;

            //Hier wird eine Gruppe erzeugt
            Gruppe _gruppe = new Gruppe(Str_Gruppe, Str_GruppePassword, meineAuswahl, Benutzer.MyCurrentUser.Id);

            Benutzer.MyCurrentUser.UpdateAdminPowerVomBenutzer();
            Benutzer.MyCurrentUser.RefreshCurrentUser();
            //Schicke die Daten zum Datenbank
            //Erzeuge Gruppe im DB
            _gruppe.GruppeErzeugen();

            Frame.GoBack();
        }

      
    }
}
