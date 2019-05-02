using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using VirtualCalendarV01.Klassen;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace VirtualCalendarV01.Pages.Ansichten
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class Jahresansicht : Page
    {
        public Jahresansicht()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            //HamburgerButton
        }

        private void MenuButton1_Click(object sender, RoutedEventArgs e)
        {
            //StartZeite Button
            Frame.Navigate(typeof(MainPage));
        }

        private void MenuButton2_Click(object sender, RoutedEventArgs e)
        {
            //Tagesansicht Button
            //Frame.Navigate(typeof(TerminPage));
            Frame.Navigate(typeof(Tagesansicht));
        }

        private void MenuButton3_Click(object sender, RoutedEventArgs e)
        {
            //Wochenansicht Button
            Frame.Navigate(typeof(Wochenansicht));
        }

        private void MenuButton4_Click(object sender, RoutedEventArgs e)
        {
            //Monatsansicht Button
            Frame.Navigate(typeof(Monatsansicht));
        }

        private void MenuButton5_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Jahresansicht));
        }

        private void MenuButton6_Click(object sender, RoutedEventArgs e)
        {
            //Info Button
            Frame.Navigate(typeof(EinstellungenPage));
        }

        private void MenuButton7_Click(object sender, RoutedEventArgs e)
        {
            //Info Button
            Frame.Navigate(typeof(InfoPage));
        }

        private void IndvBarButton_Click(object sender, RoutedEventArgs e)
        {
     //       tbKalenderAuswahlbttn.Text = "Individualansicht";
            //Zeige die Elemente vom Indivudualansicht
            //   Frame.Navigate(typeof(IndividualansichtPage));
        }

        private void GruppeBarButton_Click(object sender, RoutedEventArgs e)
        {
        //    tbKalenderAuswahlbttn.Text = "Gruppenansicht";
            //Zeige die Elemente vom Gruppenansicht
            //   Frame.Navigate(typeof(GruppenansichtPage));
        }

        private void jan_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DateTime aktJahr = DatePicker_year.Date.DateTime;
            DateTime aktMonat = DatumVerarbeitung.berechneStringtoMonat(jan.Text);
            string aktMonatUndAktJahr = aktMonat.Date.Month.ToString() + aktJahr.Date.Year;
            Frame.Navigate(typeof(Monatsansicht), aktMonatUndAktJahr);
        }

    }
}
