using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VirtualCalendarV01.Klassen;
using VirtualCalendarV01.Pages.Ansichten;
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
    public sealed partial class EditTermin : Page
    {
        //Initializiere ComboBox CbPersonAuswahl mit den aktuellen Benutzern im BD System:
        //Definiere und initializiere Collection benutzerResponse
        //Hier sind die ganze Benutzern vom DB enthalten

        Termin termin = new Termin();
        //Suche den Ausgewählten Benutzer und gebe ID zurück unten
        Benutzer benutzer = new Benutzer();

        public EditTermin()
        {
            this.InitializeComponent();
        }

        //Hiermit werden die Daten vom DB eines bestimmtes Termines angezeigt, den der Benutzer angecklick hat!
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Parameter kommt aus der TerminPage
            //hier bekommt der globalen Termin, den Wert aus der Funktion in eine andere Seite
            termin = e.Parameter as Termin;
            bool flag = true;
            benutzer = Benutzer.sucheVerantwoertlichMitName(termin.Str_BenutzerId, ref flag);

            //Fulle TextBoxs mit der angegebene Info
            //App.VIEW_MODEL.ConvertEnumKAToIndex(...) findet ein passendes Index für das eingegebene string
            //hier muss man das so machen, denn die Elemente im ComboBox sind als string, aber werden durch Index erreicht
            // App.VIEW_MODEL.ConvertEnumKAToIndex(termin.enum_KalenderAuswahl) gibt den Index, passend zu diesem Enum
            //mit CbKalenderAuswahl.Items.ElementAt() habe ich zugriff auf ein Element im Combobox
            CbKalenderAuswahl.SelectedItem = CbKalenderAuswahl.Items.ElementAt(App.VIEW_MODEL.ConvertEnumKAToIndex(termin.enum_KalenderAuswahl));
            CbWiederholungAuswahl.SelectedItem = CbWiederholungAuswahl.Items.ElementAt(App.VIEW_MODEL.ConvertEnumWZToIndex(termin.enum_WiederholungsZyklus)); // CbWiederholungAuswahl.Items.ElementAt(App.VIEW_MODEL.ConvertEnumWZToIndex(termin.enum_WiederholungsZyklus));

            //hier fülle ich die PersonAuswahl mit den Namen der Personen im Db Sysem (siehe Kommentare in Benutzer.getListeVonBenutzernNamen())
            CbPersonAuswahl.ItemsSource = Benutzer.getListeVonMeinerMitbewohnern();
            //Nur den Namen wird ausgewählt
            CbPersonAuswahl.SelectedItem = benutzer.Str_Name;

            //hier übergebe den Wert der Variable an die jeweilige 'Textbox
            Tb_Bezeichnung.Text = termin.Str_Bezeichnung;
            Tb_TerminBeschreibung.Text = termin.Str_TerminBeschreibung;
            StartCalendarDatePicker.Date = termin.Dt_Beginn;
            EndCalendarDatePicker.Date = termin.Dt_Ende;


            TpVon.Time = termin.Dt_Beginn.TimeOfDay;
            TpBis.Time = termin.Dt_Ende.TimeOfDay;

        }

        private void BackBarButton_Click(object sender, RoutedEventArgs e)
        {
            //Info Button     
            DateTime gebautesDatum = new DateTime(termin.Dt_Beginn.Year, termin.Dt_Beginn.Month, termin.Dt_Beginn.Day);
          
            Frame.Navigate(typeof(Tagesansicht), gebautesDatum);
        }

        //Hier Update mit der neuen Info den angeclickten Termin
        private void AcceptBarButton_Click(object sender, RoutedEventArgs e)
        {
            //Das sollte noch implementiert werden :-------------------
            //Hier muss das Programm erkennen ob Änderungen gemacht wurden und wenn schon 
            //dann weiter machen, sonst überspringen.
            //---------------------------------------------------------
            
            //Finde den passenden Enum zu einem Auswahl vom Benutzer
            termin.enum_KalenderAuswahl = App.VIEW_MODEL.ConvertKalAuswIndexToEnum(CbKalenderAuswahl.SelectedIndex.ToString());
            termin.enum_WiederholungsZyklus = App.VIEW_MODEL.ConvertWiedZyklIndexToEnum(CbWiederholungAuswahl.SelectedIndex.ToString()); 

            //Speichere die neue Info:
            termin.Int_BenutzerId = benutzer.Id;
            termin.Str_BenutzerId = benutzer.Str_Name;
            termin.Str_Bezeichnung = Tb_Bezeichnung.Text;
            termin.Str_TerminBeschreibung = Tb_TerminBeschreibung.Text;
      

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

            termin.Id = termin.Id;

            //hier update Termin
            termin.TerminUpdaten(benutzer);
                      
            Frame.GoBack();
        }
        //Delete Element aus dem DBSystem
        private void DeleteBarButton_Click(object sender, RoutedEventArgs e)
        {
            //Finde Id des Elements
            //Delete Termin
            //übergebe den Benutzer der für den zuständig war
            termin.TerminLoeschen(benutzer);
            //gehe zurück
            Frame.GoBack();
        }

        private void CbKalenderAuswahl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }

        private void CbWiederholungAuswahl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CbPersonAuswahl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }
}
