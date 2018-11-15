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
using VirtualCalendarV01.Model;

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
            int aktJahr = DatePicker_year.Date.DateTime.Year;
            DateTime aktMonat = DatumVerarbeitung.berechneStringtoMonat(jan.Text, aktJahr);
          //  string aktMonatUndAktJahr = aktMonat.Date.Month.ToString() + aktJahr;
            Frame.Navigate(typeof(Monatsansicht), aktMonat);
        }
       
        private void feb_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int aktJahr = DatePicker_year.Date.DateTime.Year;
            DateTime aktMonat = DatumVerarbeitung.berechneStringtoMonat(feb.Text, aktJahr);
            //  string aktMonatUndAktJahr = aktMonat.Date.Month.ToString() + aktJahr;
            Frame.Navigate(typeof(Monatsansicht), aktMonat);
        }

        private void mar_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int aktJahr = DatePicker_year.Date.DateTime.Year;
            DateTime aktMonat = DatumVerarbeitung.berechneStringtoMonat(mar.Text, aktJahr);
            //  string aktMonatUndAktJahr = aktMonat.Date.Month.ToString() + aktJahr;
            Frame.Navigate(typeof(Monatsansicht), aktMonat);
        }

        private void apr_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int aktJahr = DatePicker_year.Date.DateTime.Year;
            DateTime aktMonat = DatumVerarbeitung.berechneStringtoMonat(apr.Text, aktJahr);
            //  string aktMonatUndAktJahr = aktMonat.Date.Month.ToString() + aktJahr;
            Frame.Navigate(typeof(Monatsansicht), aktMonat);
        }

        private void mai_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int aktJahr = DatePicker_year.Date.DateTime.Year;
            DateTime aktMonat = DatumVerarbeitung.berechneStringtoMonat(mai.Text, aktJahr);
            //  string aktMonatUndAktJahr = aktMonat.Date.Month.ToString() + aktJahr;
            Frame.Navigate(typeof(Monatsansicht), aktMonat);
        }

        private void jun_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int aktJahr = DatePicker_year.Date.DateTime.Year;
            DateTime aktMonat = DatumVerarbeitung.berechneStringtoMonat(jun.Text, aktJahr);
            //  string aktMonatUndAktJahr = aktMonat.Date.Month.ToString() + aktJahr;
            Frame.Navigate(typeof(Monatsansicht), aktMonat);
        }

        private void jul_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int aktJahr = DatePicker_year.Date.DateTime.Year;
            DateTime aktMonat = DatumVerarbeitung.berechneStringtoMonat(jul.Text, aktJahr);
            //  string aktMonatUndAktJahr = aktMonat.Date.Month.ToString() + aktJahr;
            Frame.Navigate(typeof(Monatsansicht), aktMonat);
        }

        private void aug_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int aktJahr = DatePicker_year.Date.DateTime.Year;
            DateTime aktMonat = DatumVerarbeitung.berechneStringtoMonat(aug.Text, aktJahr);
            //  string aktMonatUndAktJahr = aktMonat.Date.Month.ToString() + aktJahr;
            Frame.Navigate(typeof(Monatsansicht), aktMonat);
        }

        private void sep_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int aktJahr = DatePicker_year.Date.DateTime.Year;
            DateTime aktMonat = DatumVerarbeitung.berechneStringtoMonat(sep.Text, aktJahr);
            //  string aktMonatUndAktJahr = aktMonat.Date.Month.ToString() + aktJahr;
            Frame.Navigate(typeof(Monatsansicht), aktMonat);
        }

        private void okt_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int aktJahr = DatePicker_year.Date.DateTime.Year;
            DateTime aktMonat = DatumVerarbeitung.berechneStringtoMonat(okt.Text, aktJahr);
            //  string aktMonatUndAktJahr = aktMonat.Date.Month.ToString() + aktJahr;
            Frame.Navigate(typeof(Monatsansicht), aktMonat);
        }

        private void nov_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int aktJahr = DatePicker_year.Date.DateTime.Year;
            DateTime aktMonat = DatumVerarbeitung.berechneStringtoMonat(nov.Text, aktJahr);
            //  string aktMonatUndAktJahr = aktMonat.Date.Month.ToString() + aktJahr;
            Frame.Navigate(typeof(Monatsansicht), aktMonat);
        }

        private void dez_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int aktJahr = DatePicker_year.Date.DateTime.Year;
            DateTime aktMonat = DatumVerarbeitung.berechneStringtoMonat(dez.Text, aktJahr);
            //  string aktMonatUndAktJahr = aktMonat.Date.Month.ToString() + aktJahr;
            Frame.Navigate(typeof(Monatsansicht), aktMonat);
        }

        //Ereignisshandler im Fall dass der Zeiger auf dem Button beweght wird.
        private void MenuButton1_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = true;
        }

        private void MySplitView_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = false;
        }
    }
}
