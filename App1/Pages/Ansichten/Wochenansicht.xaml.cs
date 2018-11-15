using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
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
using System.Collections.ObjectModel;
using Windows.UI;
using Windows.UI.Xaml.Shapes;
using VirtualCalendarV01.Model;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace VirtualCalendarV01.Pages.Ansichten
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class Wochenansicht : Page
    {
        private string str_Tag = DateTime.Now.Day.ToString();
        private int _Tag = DateTime.Now.Day;
        private int _Monat = DateTime.Now.Month;
        private string str_Jahr = DateTime.Now.Year.ToString();
        private int _Jahr = DateTime.Now.Year;
        private string str_Monat = DatumVerarbeitung.berechneMonatIndexZuName(DateTime.Now.Month);

        private DateTime aktuellesDatum = DateTime.Now;
        private Termin akt_Termin = new Termin();
        private List<DateTime> valideTageInDerWoche = new List<DateTime>();

        ObservableCollection<Termin> TermineAmAngegebenenTag = new ObservableCollection<Termin>();
        
        private Gruppe meineAktuelleGruppe = new Gruppe();

        private List<Termin> MeineTermineFürDieWoche = new List<Termin>();
        private ObservableCollection<Termin> _MeineTermineZuZeigen = new ObservableCollection<Termin>();
        private ObservableCollection<Termin> _MeineTermineZuZeigen_grup = new ObservableCollection<Termin>();
        private ObservableCollection<Termin> _MeineTermineZuZeigen_priv = new ObservableCollection<Termin>();

        ObservableCollection<Termin> MeineTermineFürHeuteCollection = new ObservableCollection<Termin>();


        private ObservableCollection<Grid> TermineAmTagGrid = new ObservableCollection<Grid>();

        public Wochenansicht()
        {
            this.InitializeComponent();
            DatePicker_month.Date = DateTime.Now;
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Hole die Termine aus dem DatePicker
            str_Monat = DatePicker_month.Date.Month.ToString();
            _Monat = DatePicker_month.Date.Month;
            str_Jahr = DatePicker_month.Date.Year.ToString();
            _Jahr = DatePicker_month.Date.Year;
            str_Tag = DatePicker_month.Date.Day.ToString();
            _Tag = DatePicker_month.Date.Day;
            //Hole die Termine für den aktuellen Benutzer und von der gesamten Gruppe
            holeTageVonWoche(_Monat, _Jahr,_Tag);

            fuelleWochenansicht_gesamt();
        }
    
        private void holeTageVonWoche(int Monat, int Jahr, int Tag)
        {
            //Berechnung: Wir holen die Wochentage anhand der Monate
            DateTime dt = new DateTime(Jahr,Monat,Tag);
            List<DateTime> _valideTageInDerWoche = new List<DateTime>();
            string str_WeekDay = dt.DayOfWeek.ToString();

            //Anhand des aktuellen Datums werden die anderen Tage berechnet
            switch (str_WeekDay)
            {
                case "Monday":
                    //speicher das Datum in Montag ein und gehe +1;
                    Datum_Mo.Text = dt.Date.ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.Date);
                    Datum_Di.Text = dt.AddDays(1).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(1));
                    Datum_Mi.Text = dt.AddDays(2).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(2));
                    Datum_Do.Text = dt.AddDays(3).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(3));
                    Datum_Fr.Text = dt.AddDays(4).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(4));
                    Datum_Sa.Text = dt.AddDays(5).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(5));
                    Datum_So.Text = dt.AddDays(6).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(6));
                    break;
                case "Tuesday":
                    Datum_Mo.Text = dt.AddDays(-1).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(-1));
                    Datum_Di.Text = dt.Date.ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.Date);
                    Datum_Mi.Text = dt.AddDays(1).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(1));
                    Datum_Do.Text = dt.AddDays(2).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(2));
                    Datum_Fr.Text = dt.AddDays(3).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(3));
                    Datum_Sa.Text = dt.AddDays(4).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(4));
                    Datum_So.Text = dt.AddDays(5).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(5));
                    break;
                case "Wednesday":
                    Datum_Mo.Text = dt.AddDays(-2).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(-2));
                    Datum_Di.Text = dt.AddDays(-1).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(-1));
                    Datum_Mi.Text = dt.Date.ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.Date);
                    Datum_Do.Text = dt.AddDays(1).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(1));
                    Datum_Fr.Text = dt.AddDays(2).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(2));
                    Datum_Sa.Text = dt.AddDays(3).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(3));
                    Datum_So.Text = dt.AddDays(4).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(4));
                    break;
                case "Thursday":
                    Datum_Mo.Text = dt.AddDays(-3).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(-3));
                    Datum_Di.Text = dt.AddDays(-2).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(-2));
                    Datum_Mi.Text = dt.AddDays(-1).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(-1));
                    Datum_Do.Text = dt.Date.ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.Date);
                    Datum_Fr.Text = dt.AddDays(1).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(1));
                    Datum_Sa.Text = dt.AddDays(2).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(2));
                    Datum_So.Text = dt.AddDays(3).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(3));
                    break;
                case "Friday":
                    Datum_Mo.Text = dt.AddDays(-4).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(-4));
                    Datum_Di.Text = dt.AddDays(-3).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(-3));
                    Datum_Mi.Text = dt.AddDays(-2).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(-2));
                    Datum_Do.Text = dt.AddDays(-1).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(-1));
                    Datum_Fr.Text = dt.Date.ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.Date);
                    Datum_Sa.Text = dt.AddDays(1).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(1));
                    Datum_So.Text = dt.AddDays(2).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(2));
                    break;
                case "Saturday":
                    Datum_Mo.Text = dt.AddDays(-5).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(-5));
                    Datum_Di.Text = dt.AddDays(-4).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(-4));
                    Datum_Mi.Text = dt.AddDays(-3).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(-3));
                    Datum_Do.Text = dt.AddDays(-2).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(-2));
                    Datum_Fr.Text = dt.AddDays(-1).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(-1));
                    Datum_Sa.Text = dt.Date.ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.Date);
                    Datum_So.Text = dt.AddDays(1).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(1));
                    break;
                case "Sunday":
                    Datum_Mo.Text = dt.AddDays(-6).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(-6));
                    Datum_Di.Text = dt.AddDays(-5).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(-5));
                    Datum_Mi.Text = dt.AddDays(-4).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(-4));
                    Datum_Do.Text = dt.AddDays(-3).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(-3));
                    Datum_Fr.Text = dt.AddDays(-2).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(-2));
                    Datum_Sa.Text = dt.AddDays(-1).ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.AddDays(-1));
                    Datum_So.Text = dt.Date.ToString("dd/MM/yyyy");
                    _valideTageInDerWoche.Add(dt.Date);
                    break;
                default:
                    break;
            }
            aktuellesDatum = dt;
            valideTageInDerWoche = _valideTageInDerWoche;
       
        }

        private void fuelleWochenansicht_gesamt()
        {
    //        ObservableCollection<Termin> MeineTermineFürHeuteCollection = new ObservableCollection<Termin>();
            //Laufe über die Tage der Woche
            for (int i = 0; i < 7; i++)
            {
                int validerTag = valideTageInDerWoche.ElementAt(i).Day;
                int validesMonat = valideTageInDerWoche.ElementAt(i).Month;
                int validesJahr = valideTageInDerWoche.ElementAt(i).Year;
                int _column = i;
                
                //hole die Termine am diesem Tag
                //hole die aktuellste Termine
                Termin.findeDieAufgabenfürYMDH(validesJahr, validesMonat, validerTag);
                MeineTermineFürHeuteCollection = Termin.get_myCurrentGroupTasksAtDay();
                //Setzte diese Termine in die Collection mit meine gesamte Termine in der Woche
                MeineTermineFürDieWoche.AddRange(MeineTermineFürHeuteCollection);
                fuelleTagesAnsicht_mitAufgaben(MeineTermineFürHeuteCollection, _column, valideTageInDerWoche.ElementAt(i));
            }
        }

        private bool CheckObEinTerminAnDiesemTagVorliegt(DateTime meinTag, ref bool IsGKalender, ref bool IsPKalender, ref Ellipse elipse, int Hour_, ref Termin _termin)
        {
            bool flag = false;
            //Hole die Termine für diesen Monat 
            //         MeineTermineFürDenMonat.Clear();

            foreach (var item in MeineTermineFürHeuteCollection)
            {
                if ((item.Dt_Beginn.Day == meinTag.Day) && (item.Dt_Beginn.Month == meinTag.Month) && (item.Dt_Beginn.Hour == Hour_))
                {
                    if (item.enum_KalenderAuswahl == Termin.Enum_KalenderAuswahl.Gruppenansicht)
                    {
                        IsGKalender = true;
                        elipse = item.MyEllipse;
                    }
                    if (item.enum_KalenderAuswahl == Termin.Enum_KalenderAuswahl.Individualansicht)
                    {
                        IsPKalender = true;
                    }
                    _termin = item;
                    return true;
                }
            }
            return flag;
        }

        private void fuelleTagesAnsicht_mitAufgaben(ObservableCollection<Termin> MeineTermineFürHeuteCollection, int _column, DateTime validerTag)
        {
            //hole die Termine am diesem Tag
            //Setze im Grid die Termine am diesem Tag    
            akt_Termin = new Termin();
          
            //Finde Uhrzeit des Termines
            for (int countHour = 0; countHour < 24; countHour++)
            {
                //Erzeuge Grid mit einem Gelben Background
                Grid _myGrid = new Grid();
                _myGrid.Name = "Grid_" + _column + " " + countHour;

                //Setze mein Grid in der jeweiligen Spalte und Zeile im WochenansichtGrid
                _myGrid.SetValue(Grid.RowProperty, countHour);
                _myGrid.SetValue(Grid.ColumnProperty, _column);

                _myGrid.PointerPressed += new PointerEventHandler(geheZuTerminDurchGrid);

                //Check ob der Termin Individull oder zu der Gruppenansicht gehört
                bool IsGKalender = false;
                bool IsPKalender = false;
                Ellipse _terminEllipse = new Ellipse();
                Termin _Termin = new Termin();

                if (CheckObEinTerminAnDiesemTagVorliegt(validerTag, ref IsGKalender, ref IsPKalender, ref _terminEllipse, countHour, ref _Termin))
                {
                    if (IsGKalender)
                    {
                        SolidColorBrush GruppeFarbeBrush = new SolidColorBrush();
                        GruppeFarbeBrush.Color = Colors.LightSalmon;
                        _myGrid.Background = GruppeFarbeBrush;
                    }
                    else if (IsPKalender)
                    {
                        SolidColorBrush GruppeFarbeBrush = new SolidColorBrush();
                        GruppeFarbeBrush.Color = Colors.LightSkyBlue;
                        _myGrid.Background = GruppeFarbeBrush;
                    }
                    //Erzeuge TextBox mit der Beschreibung der Aufgabe
                    TextBlock txttmp = new TextBlock();
                    txttmp.Text = _Termin.Str_Bezeichnung;
                    txttmp.FontFamily = new FontFamily("Century Gothic");
                    txttmp.HorizontalAlignment = HorizontalAlignment.Left;
                    txttmp.FontWeight = Windows.UI.Text.FontWeights.Bold;

                    Ellipse elipse = new Ellipse();
                    elipse.Fill = _Termin.MyEllipse.Fill;
                    elipse.HorizontalAlignment = HorizontalAlignment.Right;
                    elipse.Height = 20;
                    elipse.Margin = new Thickness(0, 0, 20, 0);
                    elipse.VerticalAlignment = VerticalAlignment.Center;
                    elipse.Width = 20;

                    _myGrid.Children.Add(txttmp);
                    _myGrid.Children.Add(elipse);
                }
                else
                {
                    SolidColorBrush PrivatFarbeBrush = new SolidColorBrush();
                    PrivatFarbeBrush.Color = Colors.Transparent;
                    _myGrid.Background = PrivatFarbeBrush;
                }
               
                //Add Grid zu meiner Liste von Grids, wo ein Termin vorkommt
                TermineAmTagGrid.Add(_myGrid);
                //Add Termin als Kinder meiner GridTermine
                GridTermine.Children.Add(_myGrid);

            }
          
        }
        
        private void geheZuTermin(string Bezeichnung)
        {
            foreach (var item in MeineTermineFürDieWoche)
            {
                if (item.Str_Bezeichnung == Bezeichnung)
                {
                    Frame.Navigate(typeof(EditTermin), item);
                    break;
                }
            }

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
                DateTime dt = new DateTime(_Jahr,_Monat,_Tag);
                Frame.Navigate(typeof(AddTermin), dt);
            }                        
        }
               
        private void MenuButton1_Click(object sender, RoutedEventArgs e)
        {
            //StartZeite Button
            Frame.Navigate(typeof(MainPage));
        }

        private void MenuButton2_Click(object sender, RoutedEventArgs e)
        {
            //Tagesansicht Button       
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
            
            //Setze leeres Collection           
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
            //Setze leeres Collection
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

        private void DatePicker_month_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            //hier wird die Funktion nochmal mit dem durch den DatePicker ausgewählten Datum aufgerufen
            DateTime tmp = DatePicker_month.Date.DateTime;

            //Setze Collection auf null, hier waren die alte valide Tage gespeichert
            valideTageInDerWoche.Clear();
            MeineTermineFürDieWoche.Clear();         

            //Setze Collection auf null, hier waren die alte Grid Termine gespeichert
            GridTermine.Children.Clear();
            //hier finde die passende Tage
            _Monat = tmp.Month;
            _Jahr = tmp.Year;
            _Tag = tmp.Day;

            holeTageVonWoche(_Monat, _Jahr, _Tag);

            fuelleWochenansicht_gesamt();

        }

        private void Datum_Mo_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime mo = valideTageInDerWoche.ElementAt(0);
            Frame.Navigate(typeof(Tagesansicht), mo);
        }
        private void Datum_Di_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime di = valideTageInDerWoche.ElementAt(1);
            Frame.Navigate(typeof(Tagesansicht), di);
        }
        private void Datum_Mi_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime mi = valideTageInDerWoche.ElementAt(2);
            Frame.Navigate(typeof(Tagesansicht), mi);
        }
        private void Datum_Do_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime donn = valideTageInDerWoche.ElementAt(3);
            Frame.Navigate(typeof(Tagesansicht), donn);
        }

        private void Datum_Fr_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime fr = valideTageInDerWoche.ElementAt(4);
            Frame.Navigate(typeof(Tagesansicht), fr);
        }
        private void Datum_Sa_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime sa = valideTageInDerWoche.ElementAt(5);
            Frame.Navigate(typeof(Tagesansicht), sa);
        }
        private void Datum_So_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            DateTime so = valideTageInDerWoche.ElementAt(6);
            Frame.Navigate(typeof(Tagesansicht), so);
        }
    }
}
