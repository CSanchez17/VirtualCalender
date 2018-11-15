using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VirtualCalendarV01.Klassen;
using Windows.ApplicationModel.Email;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net;
using LightBuzz.SMTP;
using Windows.UI.Popups;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace VirtualCalendarV01.Pages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class AddTermin : Page
    {

        //Erstelle eine Leere Instanz der Klasse Termin
        Termin termin = new Termin();

        //Finde wer der verantwortlich ist, die Funktion sucheVerantwoertlich gibt das Email der Person zurück (Erlaubt ohne Objektverweis, denn die Funktion ist static)
        Benutzer verantwoertlicherBenutzer = Benutzer.MyCurrentUser;

        public AddTermin()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //-------------------------------------------------------------------------------------------------//
            //Initialisiere die ComboBoxs mit Default Anträge:
            //CbKalenderAuswahl : "Individualansicht" 
            CbKalenderAuswahl.SelectedItem = CbKalenderAuswahl.Items.ElementAt(0);
            //WiederholungAuswahl : "Keine"
            CbWiederholungAuswahl.SelectedItem = CbWiederholungAuswahl.Items.ElementAt(0);
            //-------------------------------------------------------------------------------------------------//
            CbPersonAuswahl.ItemsSource = Benutzer.getListeVonMeinerMitbewohnern();

            //Hiermit festgelegt, dass dem ersten gezeigten Benutzer die Aufgabe zugewissen bekommt
            //Als Defaultwert wird "Keine" als Wiederholung angezeigt

            //Im Prinzip ist man selber der verantwortlich für die erstellte Aufgabe
            //keine Auswahl möglich in Verantw Person
            CbPersonAuswahl.Visibility = Visibility.Collapsed;
            termin.Str_BenutzerId = Benutzer.MyCurrentUser.Str_Name;

            if ((e.Parameter != null))
            {
                DateTime meindatumuebergabe = Convert.ToDateTime(e.Parameter);
                StartCalendarDatePicker.Date = meindatumuebergabe;
                StartCalendarDatePicker.Date = meindatumuebergabe;
                EndCalendarDatePicker.MinDate = StartCalendarDatePicker.MinDate;
                EndCalendarDatePicker.Date = StartCalendarDatePicker.Date;

                TpVon.Time = meindatumuebergabe.TimeOfDay;
            }
            else
            {
                //Hier werden die default Werte der Comboboxen angezeigt
                StartCalendarDatePicker.MinDate = DateTime.Now;
                StartCalendarDatePicker.Date = DateTime.Now;
                EndCalendarDatePicker.MinDate = DateTime.Now;
                EndCalendarDatePicker.Date = DateTime.Now;

            }
        }

        private void BackBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void AddBarButton_Click(object sender, RoutedEventArgs e)
        {
            // Hiermit wird die angegebene Info in einem neuen Termin gespeichert
            // Die eingegebene Info wird für einen neuen Benutzer angelegt
            // Speichere den Namen der Benutzer (Auswahl im ComboBox)

            //Rufe den Konstruktor der Klasse Termin auf, mit dem Namen der Benutzer als Parameter
            //also hier bilde eine Instanz der Klasse Termin.

            //Prüfe dass eine Bezeichnung angegeben wurde:
            if (String.IsNullOrEmpty(Tb_Bezeichnung.Text))
            {
              
                string MessageD = "Geben sie bitte eine Bezeichnung für die Aufgabe ein! :)";
                Meldung(MessageD);
                return;
            }

            //Setzte die Farbe des zug. Benutzern
            termin.Str_Farbe = verantwoertlicherBenutzer.Str_Farbe;

            //Setze den Kalenderauswahl
            termin.enum_KalenderAuswahl = App.VIEW_MODEL.ConvertKalAuswIndexToEnum(CbKalenderAuswahl.SelectedIndex.ToString());
            //Setze den WiederholungsZyklus
            termin.enum_WiederholungsZyklus = App.VIEW_MODEL.ConvertWiedZyklIndexToEnum(CbWiederholungAuswahl.SelectedIndex.ToString());
            //Setze die Bezeichnng
            termin.Str_Bezeichnung = Tb_Bezeichnung.Text;
            //Setze die Beschreibung
            termin.Str_TerminBeschreibung = Tb_TerminBeschreibung.Text;

            //erzeuge Termin für die Gruppe des aktuellen Benutzers
            termin.Str_ErzeugtFuerGruppe = verantwoertlicherBenutzer.Str_MitgliedVon;                     

            //Setze das Beginn
            string TimeVon = TpVon.Time.ToString();
            string DatumVon = StartCalendarDatePicker.Date.Value.DateTime.ToString("yyyy-MM-dd");
            DatumVon += " " + TimeVon;
            termin.Dt_Beginn = Convert.ToDateTime(DatumVon);

            //Setze End datum
            string TimeBis = TpBis.Time.ToString();
            string DatumBis = StartCalendarDatePicker.Date.Value.DateTime.ToString("yyyy-MM-dd");
            DatumBis += " " + TimeBis;
            termin.Dt_Ende = Convert.ToDateTime(DatumBis);

            //Erzeuge das Termin
            termin.TerminErzeugen(verantwoertlicherBenutzer);

            Frame.GoBack();
        }

        private async void Meldung(string MessageD)
        {
            var dialog = new MessageDialog(MessageD);

            await dialog.ShowAsync();
        }

        private void CbKalenderAuswahl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Wenn der Benutzer IndvAnsicht auswählt, idt der gleich der verantwortliche für die Aufgabe            
            if (CbKalenderAuswahl.SelectedIndex == 0)
            {
                //keine Auswahl möglich in Verantw Person
                CbPersonAuswahl.Visibility = Visibility.Collapsed;
                verantwoertlicherBenutzer = Benutzer.MyCurrentUser;
                termin.Str_BenutzerId = Benutzer.MyCurrentUser.Str_Name;
            }
            else
            {
                CbPersonAuswahl.SelectedItem = Benutzer.MyCurrentUser.Str_Name;
                //Auswahl möglich in Verantw Person
                CbPersonAuswahl.Visibility = Visibility.Visible;
            }
        }

        private void CbPersonAuswahl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Finde wer der verantwortlich ist, die Funktion sucheVerantwoertlich gibt das Email der Person zurück (Erlaubt ohne Objektverweis, denn die Funktion ist static)
            bool flag = true;
            verantwoertlicherBenutzer = Benutzer.sucheVerantwoertlichMitName(CbPersonAuswahl.SelectedItem.ToString(), ref flag);
            termin.Str_BenutzerId = CbPersonAuswahl.SelectedItem.ToString();
        }              

        private void CbWiederholungAuswahl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
              
        //Hier wird das Enddatum abhängig vom Startdatum umgesetzt
        private void StartCalendarDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            //Setze das End Datum als mindestens den gleichen Tag
            EndCalendarDatePicker.MinDate = StartCalendarDatePicker.Date.Value.DateTime;
            EndCalendarDatePicker.Date = StartCalendarDatePicker.Date.Value.DateTime;
        }

        private void TpVon_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            TpBis.Time = TpVon.Time;
        }
    }
}
