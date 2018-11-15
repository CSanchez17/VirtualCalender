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
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace VirtualCalendarV01.Pages.Ansichten
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    /// 
   
    public sealed partial class Tagesansicht : Page
    {
        public Tagesansicht()
        {
            this.InitializeComponent();
            //Setze die Initials der Benutzern in den Elipsen
            List<string> GruppeinitialsBenutzer = Gruppe.findeCharachtersDerBenutzers();
            //B0.Text = GruppeinitialsBenutzer[0];
            //B1.Text = GruppeinitialsBenutzer[1];
            //B2.Text = GruppeinitialsBenutzer[2];
            //B3.Text = GruppeinitialsBenutzer[3];
            //B4.Text = GruppeinitialsBenutzer[4];
            //B5.Text = GruppeinitialsBenutzer[5];
            //B6.Text = GruppeinitialsBenutzer[6];
            //B7.Text = GruppeinitialsBenutzer[7];
        }

        private string str_Tag;
        private int _Tag;
        private string str_Jahr;
        private int _Jahr;
        private string str_Monat;
        private int _Monat;

        DateTime aktuellesDatum;

        private Termin akt_Termin = new Termin();

        private ObservableCollection<Termin> MeineTermineFürHeute = new ObservableCollection<Termin>();
        private ObservableCollection<Termin> _MeineTermineZuZeigen = new ObservableCollection<Termin>();
        private ObservableCollection<Termin> _MeineTermineZuZeigen_grup = new ObservableCollection<Termin>();
        private ObservableCollection<Termin> _MeineTermineZuZeigen_priv = new ObservableCollection<Termin>();
        private ObservableCollection<Grid> TermineAmTagGrid = new ObservableCollection<Grid>();
        private Gruppe meineAktuelleGruppe = new Gruppe();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            //Hier Funktion die berechnet wann ein Termin stattfindet und plaziert den in der Liste, an seiner 
            //richtigen Position
            //Hole den ausgewählten Tag (Aus der Monatsansicht)
            
            
            //Setze die Aktuelle Parameter für das Tagesansicht
            if (e.Parameter != null)
            {
                aktuellesDatum = Convert.ToDateTime(e.Parameter);
                str_Tag = aktuellesDatum.Day.ToString();
                _Tag = aktuellesDatum.Day;
                str_Jahr = aktuellesDatum.Year.ToString();
                _Jahr = aktuellesDatum.Year;
                str_Monat = DatumVerarbeitung.berechneMonatIndexZuName(aktuellesDatum.Month);
                _Monat = aktuellesDatum.Month;
                DatePicker_day.Date = Convert.ToDateTime(e.Parameter);
                //         TbDatumHeading.Text = _Tag + " " + str_Monat + " " + _Jahr;
            }
            else
            {
                aktuellesDatum = DateTime.Now;
                str_Tag = aktuellesDatum.Day.ToString();
                _Tag = aktuellesDatum.Day;
                str_Jahr = aktuellesDatum.Year.ToString();
                _Jahr = aktuellesDatum.Year;
                str_Monat = DatumVerarbeitung.berechneMonatIndexZuName(aktuellesDatum.Month);
                _Monat = aktuellesDatum.Month;
                DatePicker_day.Date = aktuellesDatum;
                //         TbDatumHeading.Text = _Tag + " " + str_Monat + " " + _Jahr;
            }

            //hole nur die Termine für diesen Tag für den angegebenen Benutzer(registrierte)
            Termin.findeDieAufgabenfürYMDH(_Jahr, _Monat, _Tag);
            MeineTermineFürHeute = Termin.get_myCurrentGroupTasksAtDay();
            fuelleTagesAnsicht_gesamt();

            //Fülle Elipsen mit den Farben und Buchstaben der Benutzern in der Gruppe
            

            //Setze Collection mit den Individuele Termine, zeige die an
            //Zeige die Elemente vom Indivudualansicht
            grdElipsenCommndbar.Visibility = Visibility.Visible;
            
           
        }
               
        private void fuelleTagesAnsicht_gesamt()
        {
            fuelleAngegebenenTagMitTermine(MeineTermineFürHeute);            
        }
        
        private void fuelleAngegebenenTagMitTermine(ObservableCollection<Termin> _termine)
        {
            int _row;
            int _column = 1;
            akt_Termin = new Termin();

            //Betrachte alle Termine in der Collection
            foreach (var item in _termine)
            {                
                //Finde Uhrzeit des Termines
                for (int i = 0; i < 24; i++)
                {
                    _row = i;
                    //finde row anhand des Uhrzeits
                    //Erzeuge Grid mit einem Gelben Background
                    Grid _myGrid = new Grid();
                    _myGrid.Name = "Grid_" + _column + " " + _row;
                    _myGrid.VerticalAlignment = VerticalAlignment.Stretch;
                    _myGrid.HorizontalAlignment = HorizontalAlignment.Stretch;

                    //Setze mein Grid in der jeweiligen Spalte und Zeile im WochenansichtGrid
                    //Erzeuge 2 columns 0: Uhrzeit, 1: Inhalt 
                    _myGrid.SetValue(Grid.ColumnProperty, _column);
                    _myGrid.SetValue(Grid.RowProperty, _row);

                    // handler using "-=" syntax rather than "+=".
                    akt_Termin = item;
                    
                    _myGrid.PointerPressed += new PointerEventHandler(geheZuTerminDurchGrid);

                    if (item.Dt_Beginn.Hour == i)
                    {
                        //Check ob der Termin Individull oder zu der Gruppenansicht gehört
                        if (item.enum_KalenderAuswahl == Termin.Enum_KalenderAuswahl.Gruppenansicht)
                        {
                            SolidColorBrush GruppeFarbeBrush = new SolidColorBrush();
                            GruppeFarbeBrush.Color = Colors.LightSalmon;
                            _myGrid.Background = GruppeFarbeBrush;

                        }
                        else
                        {
                            SolidColorBrush PrivatFarbeBrush = new SolidColorBrush();
                            PrivatFarbeBrush.Color = Colors.LightSkyBlue;
                            _myGrid.Background = PrivatFarbeBrush;
                        }

                        TextBlock txtBz = new TextBlock();
                        txtBz.FontSize = 24;
                        txtBz.Text = item.Str_Bezeichnung;
                        txtBz.Margin = new Thickness(5, 0, 0, 0);

                        txtBz.HorizontalAlignment = HorizontalAlignment.Left;
                        txtBz.VerticalAlignment = VerticalAlignment.Stretch;

                        Ellipse elipse = new Ellipse();
                        elipse.Fill = item.MyEllipse.Fill;
                        elipse.HorizontalAlignment = HorizontalAlignment.Right;
                        elipse.Height = 20;
                        elipse.Margin = new Thickness(0, 0, 20, 0);
                        elipse.VerticalAlignment = VerticalAlignment.Center;
                        elipse.Width = 20;

                        _myGrid.Children.Add(txtBz);
                        _myGrid.Children.Add(elipse);
                        //Add Grid zu meiner Liste von Grids, wo ein Termin vorkommt
                        TermineAmTagGrid.Add(_myGrid);
                    }
                    else
                    {
                        SolidColorBrush GruppeFarbeBrush = new SolidColorBrush();
                        GruppeFarbeBrush.Color = Colors.Transparent;
                        _myGrid.Background = GruppeFarbeBrush;
                    }              

                    //Add Termin als Kinder meiner GridTermine
                    GridTermine.Children.Add(_myGrid);
                }
            }
        }

    
        private ObservableCollection<Termin> sucheIndividuelleTermine()
        {
            ObservableCollection<Termin> IndTermine = new ObservableCollection<Termin>();
            //Nehme die Termine für den Tag;
            foreach (var item in MeineTermineFürHeute)
            {
                //Hole die um 0 stattfinden
                if (item.enum_KalenderAuswahl == Termin.Enum_KalenderAuswahl.Individualansicht)
                {
                    if (item.Str_BenutzerId == Benutzer.MyCurrentUser.Str_Name)
                    {
                        IndTermine.Add(item);
                    }      
                }
            }
            return IndTermine;
        }

        private ObservableCollection<Termin> sucheGruppenTermine()
        {
            ObservableCollection<Termin> GrpTermine = new ObservableCollection<Termin>();
            //Nehme die Termine für den Tag;
            foreach (var item in MeineTermineFürHeute)
            {
                //Hole die um 0 stattfinden
                if (item.enum_KalenderAuswahl == Termin.Enum_KalenderAuswahl.Gruppenansicht)
                {
                    GrpTermine.Add(item);
                }
            }
            return GrpTermine;
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
            //Zeige die Elemente vom Indivudualansicht
            grdElipsenCommndbar.Visibility = Visibility.Collapsed;
            foreach (var item in TermineAmTagGrid)
            {
                var background = item.Background as SolidColorBrush;
                var color = background.Color;

                if (Colors.LightSalmon.Equals(color))
                {
                    item.Visibility = Visibility.Collapsed;
                }
                if (Colors.LightSkyBlue.Equals(color))
                {
                    item.Visibility = Visibility.Visible;
                }
            }
        }

        private void GruppeBarButton_Click(object sender, RoutedEventArgs e)
        {
            //Zeige die Elemente vom Gruppenansicht
            grdElipsenCommndbar.Visibility = Visibility.Visible;
            ////Setze leeres Collection
            foreach (var item in TermineAmTagGrid)
            {
                var background = item.Background as SolidColorBrush;
                var color = background.Color;

                if (Colors.LightSalmon.Equals(color))
                {
                    item.Visibility = Visibility.Visible;
                }
                if (Colors.LightSkyBlue.Equals(color))
                {
                    item.Visibility = Visibility.Collapsed;
                }
            }
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

        private void geheZuTermin(string Bezeichnung)
        {                   
            foreach (var item in MeineTermineFürHeute)
            {
                if (item.Str_Bezeichnung == Bezeichnung)
                {
                    Frame.Navigate(typeof(EditTermin), item);
                }
            }

        }
        private void DatePicker_day_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            //Setze Collection auf null, hier waren die alte Grid Termine gespeichert
            GridTermine.Children.Clear();
            //hier wird die Funktion nochmal mit dem durch den DatePicker ausgewählten Datum aufgerufen

            // Der Heading wir mit dem durch den DatePicker ausgewählten Datum gleichgesetzt
            _Jahr = DatePicker_day.Date.Year;
            _Monat = DatePicker_day.Date.Month;
            _Tag = DatePicker_day.Date.Day;

            Termin.findeDieAufgabenfürYMDH(_Jahr, _Monat, _Tag);
            MeineTermineFürHeute = Termin.get_myCurrentGroupTasksAtDay();

            _MeineTermineZuZeigen = sucheIndividuelleTermine();
            fuelleTagesAnsicht_gesamt();

        }
        private void geheZuTerminDurchGrid(object sender, PointerRoutedEventArgs e)
        {
            var _mygrid = sender as Grid;
            string bezeichnung;
            if (_mygrid.Children.Count > 0)
            {
                TextBlock _textblock = _mygrid.Children.ElementAt(0) as TextBlock;
                bezeichnung = _textblock.Text;
                geheZuTermin(bezeichnung);
            }
            else
            {
                DateTime dt = new DateTime(_Jahr, _Monat, _Tag);
                Frame.Navigate(typeof(AddTermin), dt);
            }
        }

        private void rectangle1_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
                DateTime dt = new DateTime(_Jahr, _Monat, _Tag);
                TimeSpan uhrzeit1 = new TimeSpan(0,0,0);
                dt = dt.Date + uhrzeit1;
                
                Frame.Navigate(typeof(AddTermin), dt);
        }

        private void rectangle3_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime dt = new DateTime(_Jahr, _Monat, _Tag);
            TimeSpan uhrzeit1 = new TimeSpan(2, 0, 0);
            dt = dt.Date + uhrzeit1;
            Frame.Navigate(typeof(AddTermin), dt);
        }

        private void rectangle5_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime dt = new DateTime(_Jahr, _Monat, _Tag);
            TimeSpan uhrzeit1 = new TimeSpan(4, 0, 0);
            dt = dt.Date + uhrzeit1;
            Frame.Navigate(typeof(AddTermin), dt);
        }

        private void rectangle7_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime dt = new DateTime(_Jahr, _Monat, _Tag);
            TimeSpan uhrzeit1 = new TimeSpan(6, 0, 0);
            dt = dt.Date + uhrzeit1;
            Frame.Navigate(typeof(AddTermin), dt);
        }
        private void rectangle9_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime dt = new DateTime(_Jahr, _Monat, _Tag);
            TimeSpan uhrzeit1 = new TimeSpan(8, 0, 0);
            dt = dt.Date + uhrzeit1;
            Frame.Navigate(typeof(AddTermin), dt);
        }

        private void rectangle11_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime dt = new DateTime(_Jahr, _Monat, _Tag);
            TimeSpan uhrzeit1 = new TimeSpan(10, 0, 0);
            dt = dt.Date + uhrzeit1;
            Frame.Navigate(typeof(AddTermin), dt);
        }

        private void rectangle13_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime dt = new DateTime(_Jahr, _Monat, _Tag);
            TimeSpan uhrzeit1 = new TimeSpan(12, 0, 0);
            dt = dt.Date + uhrzeit1;
            Frame.Navigate(typeof(AddTermin), dt);
        }

        private void rectangle15_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime dt = new DateTime(_Jahr, _Monat, _Tag);
            TimeSpan uhrzeit1 = new TimeSpan(14, 0, 0);
            dt = dt.Date + uhrzeit1;
            Frame.Navigate(typeof(AddTermin), dt);
        }

        private void rectangle17_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime dt = new DateTime(_Jahr, _Monat, _Tag);
            TimeSpan uhrzeit1 = new TimeSpan(16, 0, 0);
            dt = dt.Date + uhrzeit1;
            Frame.Navigate(typeof(AddTermin), dt);
        }

        private void rectangle19_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            DateTime dt = new DateTime(_Jahr, _Monat, _Tag);
            TimeSpan uhrzeit1 = new TimeSpan(18, 0, 0);
            dt = dt.Date + uhrzeit1;
            Frame.Navigate(typeof(AddTermin), dt);
        }

        private void rectangle21_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime dt = new DateTime(_Jahr, _Monat, _Tag);
            TimeSpan uhrzeit1 = new TimeSpan(20, 0, 0);
            dt = dt.Date + uhrzeit1;
            Frame.Navigate(typeof(AddTermin), dt);
        }

        private void rectangle23_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime dt = new DateTime(_Jahr, _Monat, _Tag);
            TimeSpan uhrzeit1 = new TimeSpan(22, 0, 0);
            dt = dt.Date + uhrzeit1;
            Frame.Navigate(typeof(AddTermin), dt);
        }

        private void rectangle2_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime dt = new DateTime(_Jahr, _Monat, _Tag);
            TimeSpan uhrzeit1 = new TimeSpan(1, 0, 0);
            dt = dt.Date + uhrzeit1;
            Frame.Navigate(typeof(AddTermin), dt);
        }

        private void rectangle4_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime dt = new DateTime(_Jahr, _Monat, _Tag);
            TimeSpan uhrzeit1 = new TimeSpan(3, 0, 0);
            dt = dt.Date + uhrzeit1;
            Frame.Navigate(typeof(AddTermin), dt);
        }

        private void rectangle6_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime dt = new DateTime(_Jahr, _Monat, _Tag);
            TimeSpan uhrzeit1 = new TimeSpan(5, 0, 0);
            dt = dt.Date + uhrzeit1;
            Frame.Navigate(typeof(AddTermin), dt);
        }

        private void rectangle8_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime dt = new DateTime(_Jahr, _Monat, _Tag);
            TimeSpan uhrzeit1 = new TimeSpan(7, 0, 0);
            dt = dt.Date + uhrzeit1;
            Frame.Navigate(typeof(AddTermin), dt);
        }

        private void rectangle10_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime dt = new DateTime(_Jahr, _Monat, _Tag);
            TimeSpan uhrzeit1 = new TimeSpan(9, 0, 0);
            dt = dt.Date + uhrzeit1;
            Frame.Navigate(typeof(AddTermin), dt);
        }

        private void rectangle12_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime dt = new DateTime(_Jahr, _Monat, _Tag);
            TimeSpan uhrzeit1 = new TimeSpan(11, 0, 0);
            dt = dt.Date + uhrzeit1;
            Frame.Navigate(typeof(AddTermin), dt);
        }

        private void rectangle14_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime dt = new DateTime(_Jahr, _Monat, _Tag);
            TimeSpan uhrzeit1 = new TimeSpan(13, 0, 0);
            dt = dt.Date + uhrzeit1;
            Frame.Navigate(typeof(AddTermin), dt);
        }

        private void rectangle16_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime dt = new DateTime(_Jahr, _Monat, _Tag);
            TimeSpan uhrzeit1 = new TimeSpan(15, 0, 0);
            dt = dt.Date + uhrzeit1;
            Frame.Navigate(typeof(AddTermin), dt);
        }

        private void rectangle18_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime dt = new DateTime(_Jahr, _Monat, _Tag);
            TimeSpan uhrzeit1 = new TimeSpan(17, 0, 0);
            dt = dt.Date + uhrzeit1;
            Frame.Navigate(typeof(AddTermin), dt);
        }

        private void rectangle20_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime dt = new DateTime(_Jahr, _Monat, _Tag);
            TimeSpan uhrzeit1 = new TimeSpan(19, 0, 0);
            dt = dt.Date + uhrzeit1;
            Frame.Navigate(typeof(AddTermin), dt);
        }

        private void rectangle22_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime dt = new DateTime(_Jahr, _Monat, _Tag);
            TimeSpan uhrzeit1 = new TimeSpan(21, 0, 0);
            dt = dt.Date + uhrzeit1;
            Frame.Navigate(typeof(AddTermin), dt);
        }

        private void rectangle24_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime dt = new DateTime(_Jahr, _Monat, _Tag);
            TimeSpan uhrzeit1 = new TimeSpan(23, 0, 0);
            dt = dt.Date + uhrzeit1;
            Frame.Navigate(typeof(AddTermin), dt);
        }
    }
}
