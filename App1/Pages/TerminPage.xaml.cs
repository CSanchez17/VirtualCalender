using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VirtualCalendarV01.Klassen;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class TerminPage : Page
    {
        //Erstelle eine Leere Instanz der Klasse Termin
        Termin termin = new Termin();

        //Finde wer der verantwortlich ist, die Funktion sucheVerantwoertlich gibt das Email der Person zurück (Erlaubt ohne Objektverweis, denn die Funktion ist static)
        Benutzer benutzer = new Benutzer();

        public TerminPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Hole die aktuelleste info aus dem DB
            App.updateInfo();
            //mit ItemsSource , kann ich eine Collection an meiner Liste Zuweisen
            TerminList.ItemsSource = Termin.getTerminResponse();
        }

        private void BackBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void AddBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddTermin));
        }

        private void BenutzersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var _termin = TerminList.SelectedItem as Termin;
            //gehe in die EditTermin Seite mit dem angegebenen Parameter _termin
            Frame.Navigate(typeof(EditTermin), _termin);
        }

        private void RefreshBarButton_Click(object sender, RoutedEventArgs e)
        {
            //Hole dieganze Termine aus dem DB   
            TerminList.ItemsSource = Termin.getTerminResponse();
            //Hole die aktuelleste info aus dem DB
            App.updateInfo();
        }
    }
}
