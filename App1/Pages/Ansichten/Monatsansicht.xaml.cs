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
    public sealed partial class Monatsansicht : Page
    {
        DateTime Dt_AktuellesDatum;
        DatumVerarbeitung Datumtmp = new DatumVerarbeitung();
        private ObservableCollection<Termin> MeineTermineFürDenMonat = new ObservableCollection<Termin>();
        private ObservableCollection<Grid> _Grd_MeineTermineZuZeigen_grup = new ObservableCollection<Grid>();
        private ObservableCollection<Grid> _Grd_MeineTermineZuZeigen_priv = new ObservableCollection<Grid>();
        private ObservableCollection<Grid> TermineAmMonatGrid = new ObservableCollection<Grid>();
     
        private int _Monat = DateTime.Now.Month;
        private string str_Monat = DatumVerarbeitung.berechneMonatIndexZuName(DateTime.Now.Month);
        private int _Jahr = DateTime.Now.Year;
        private string str_Jahr = DateTime.Now.Year.ToString();
        private int _Tag = DateTime.Now.Day;
        private string str_Tag = DateTime.Now.Day.ToString();
        
        
        public Monatsansicht()
        {
            this.InitializeComponent();
            //Setze die Initials der Benutzern in den Ellipsen            
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

            if (e.Parameter != null)
            {
                DatePicker_month.Date = Convert.ToDateTime(e.Parameter);
                Dt_AktuellesDatum = DatePicker_month.Date.DateTime;

                str_Monat = DatumVerarbeitung.berechneMonatIndexZuName(Dt_AktuellesDatum.Month);
                _Monat = Dt_AktuellesDatum.Month;
                str_Tag = Dt_AktuellesDatum.Day.ToString();
                _Tag = Dt_AktuellesDatum.Day;
                str_Jahr = Dt_AktuellesDatum.Year.ToString();
                _Jahr = Dt_AktuellesDatum.Year;

                MonatsHeading.Text = (str_Monat + " " + str_Jahr);
            }
            else
            {
                DatePicker_month.Date = DateTime.Now;
                Dt_AktuellesDatum = DatePicker_month.Date.DateTime;
                //Berehcne was für ein Monat es ist
                MonatsHeading.Text = str_Monat + " " + str_Jahr;
            }

            Termin.findeDieAufgabenfürYMDH(_Jahr, _Monat, _Tag);
            MeineTermineFürDenMonat = Termin.get_myCurrentGroupTasksAtMonth();

            //Hier nehme ich das Datum der DataPicker
            Dt_AktuellesDatum = DatePicker_month.Date.DateTime;

            //berechne die Tage im MonatsKalender
            fuelle_Monatsansicht(Dt_AktuellesDatum);

            //setze die Initials in den Elipsen
            List<string> initials = Gruppe.findeCharachtersDerBenutzers();

        }

        private bool CheckObEinTerminAnDiesemTagVorliegt(DateTime meinTag, ref bool IsGKalender, ref bool IsPKalender, ref Ellipse elipse)
        {
            bool flag = false;
            //Hole die Termine für diesen Monat 
            //         MeineTermineFürDenMonat.Clear();
            
            foreach (var item in MeineTermineFürDenMonat)
            {
                if ((item.Dt_Beginn.Day == meinTag.Day) && (item.Dt_Beginn.Month == meinTag.Month))
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
                    return true;                    
                }
            }
            return flag;
        }

        private void fuelle_Monatsansicht(DateTime Dt_AktuellesDatum)
        {
            //Hier möchte ich das Datum des DatePickers meiner Funktion zu übergeben

            //hole vom angegebenen Datum, den ersten Tag
            //           DateTime ersterTag = new DateTime(_Jahr, _Monat, 1);
            DateTime ersterTag = new DateTime(Dt_AktuellesDatum.Year, Dt_AktuellesDatum.Month, 1);
            int firstday = ersterTag.Day;
            DateTime letzterTag = ersterTag.AddMonths(1).AddDays(-1);
            int letzterTag_letzterMonat = ersterTag.AddDays(-1).Day;

            string firstDayofweek = ersterTag.DayOfWeek.ToString();

            //Bilde ein Array von TextBoxs für die IndexVariablen der Tagen im Monat         
            int MaximaleAnzahlDerTage = 42;

            DateTime TagIndex = ersterTag;
            int countColumn = 0;
            int countRow = 0;

            //lege die Koordinaten fest              
            if (firstDayofweek == "Tuesday")
            {
                TagIndex = TagIndex.AddDays(-1);
            }
            if (firstDayofweek == "Wednesday")
            {
                TagIndex = TagIndex.AddDays(-2);
            }
            if (firstDayofweek == "Thursday")
            {
                TagIndex = TagIndex.AddDays(-3);
            }
            if (firstDayofweek == "Friday")
            {
                TagIndex = TagIndex.AddDays(-4);
            }
            if (firstDayofweek == "Saturday")
            {
                TagIndex = TagIndex.AddDays(-5);
            }
            if (firstDayofweek == "Sunday")
            {
                TagIndex = TagIndex.AddDays(-6);
            }

            for (int count = 0; count < MaximaleAnzahlDerTage; count++)
            {
                if (((count % 7) == 0) && (count != 0))
                {
                    countRow++;
                    countColumn = 0;
                }

                //bilde der Name des TextBox
                string TbName = count.ToString();
                //Fülle Die Tage in der Zeile (Woche)
                                  
                //Erzeuge Temporäres Grid
                Grid _myGrid = new Grid();
                _myGrid.Name = "Grid_" + countColumn + " " + countRow;
                _myGrid.PointerPressed += new PointerEventHandler(geheZuTerminDurchGrid);
                
                _myGrid.SetValue(Grid.RowProperty, countRow);
                _myGrid.SetValue(Grid.ColumnProperty, countColumn);

                //Erzeuge die Textblocks für die Index im Monat
                TextBlock tmp_Day = new TextBlock();
                tmp_Day.Margin = new Thickness(50, 25, 25, 0);
                tmp_Day.FontSize = 20;
                //Setze Den Index als der Text der TextBoxes
                tmp_Day.Text = TagIndex.Day.ToString();

                if ((TagIndex.Month == ersterTag.AddMonths(1).Month) || (TagIndex.Month == ersterTag.AddMonths(-1).Month))
                {
                    tmp_Day.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);

                }

                TextBlock tmp_Month = new TextBlock();
                tmp_Month.Text = TagIndex.Month.ToString();
                tmp_Month.Visibility = Visibility.Collapsed;
                //Check ob der Termin Individull oder zu der Gruppenansicht gehört
                bool IsGKalender = false;
                bool IsPKalender = false;
                Ellipse _terminEllipse = new Ellipse();
                Ellipse elipse = new Ellipse();
                elipse.HorizontalAlignment = HorizontalAlignment.Right;
                elipse.Height = 20;
                elipse.Margin = new Thickness(50, 10, 20, 0);
                elipse.VerticalAlignment = VerticalAlignment.Bottom;
                elipse.Width = 20;

                //Add als Children meines gesamten Grids
                _myGrid.Children.Add(tmp_Day);
                _myGrid.Children.Add(tmp_Month);

                if (CheckObEinTerminAnDiesemTagVorliegt(TagIndex, ref IsGKalender, ref IsPKalender,ref _terminEllipse))
                {
                    _myGrid.Children.Add(elipse);
                    if (IsGKalender)
                    {
                        SolidColorBrush GruppeFarbeBrush = new SolidColorBrush();
                        GruppeFarbeBrush.Color = Colors.LightSalmon;
                        _myGrid.Background = GruppeFarbeBrush;
                        _Grd_MeineTermineZuZeigen_grup.Add(_myGrid);
                    }
                    else if (IsPKalender)
                    {
                        SolidColorBrush GruppeFarbeBrush = new SolidColorBrush();
                        GruppeFarbeBrush.Color = Colors.LightSkyBlue;
                        _myGrid.Background = GruppeFarbeBrush;
                        _Grd_MeineTermineZuZeigen_priv.Add(_myGrid);
                    }
                    elipse.Fill = _terminEllipse.Fill;
                }
                else
                {
                    SolidColorBrush PrivatFarbeBrush = new SolidColorBrush();
                    PrivatFarbeBrush.Color = Colors.Transparent;
                    _myGrid.Background = PrivatFarbeBrush;
                }                                                              
                               
                GridTermine.Children.Add(_myGrid);

                TermineAmMonatGrid.Add(_myGrid);
                TagIndex = TagIndex.AddDays(1);
                countColumn++;
            }                
        }
                
        private void geheZuTerminDurchGrid(object sender, PointerRoutedEventArgs e)
        {
            var _mygrid = sender as Grid;
            TextBlock _textblockDay = _mygrid.Children.ElementAt(0) as TextBlock;
            TextBlock _textblockMonth = _mygrid.Children.ElementAt(1) as TextBlock;
            int tagVomTextBlock = Convert.ToInt32(_textblockDay.Text);
            int yearVomTextBlock = Convert.ToInt32(_textblockMonth.Text);

            DateTime gebautesDatum = new DateTime(DatePicker_month.Date.Year, yearVomTextBlock, tagVomTextBlock);
            string bezeichnung = _textblockDay.Text;
            Frame.Navigate(typeof(Tagesansicht), gebautesDatum);
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            //HamburgerButton
        }

        private void MenuButton1_Click(object sender, RoutedEventArgs e)
        {
            //StartSeite Button
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
            foreach (var item in _Grd_MeineTermineZuZeigen_priv)
            {
                item.Background.Opacity = 100;
            }
           
            //Zeige die Elemente vom Individualansicht
            grdElipsenCommndbar.Visibility = Visibility.Collapsed;

            foreach (var item in _Grd_MeineTermineZuZeigen_grup)
            {
                var background = item.Background as SolidColorBrush;
                var color = background.Color;
 
                if (Colors.LightSalmon.Equals(color))
                {
                    item.Background.Opacity = 0;
                    item.Children.ElementAt(2).Visibility = Visibility.Collapsed;
                }
               
                

            }
            foreach (var item in _Grd_MeineTermineZuZeigen_priv)
            {
                var background = item.Background as SolidColorBrush;
                var color = background;
                 if (Colors.LightSkyBlue.Equals(color))             
                    item.Background.Opacity = 100;                              
            }
        }

        private void GruppeBarButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in _Grd_MeineTermineZuZeigen_grup)
            {
                item.Background.Opacity = 100;
                item.Children.ElementAt(2).Visibility = Visibility.Visible;
            }
            //Zeige die Elemente vom Gruppenansicht
            grdElipsenCommndbar.Visibility = Visibility.Visible;
            ////Setze leeres Collection          
            
            foreach (var item in _Grd_MeineTermineZuZeigen_grup)
            {
                var background = item.Background as SolidColorBrush;
                var color = background.Color;

                if (Colors.LightSalmon.Equals(color))
                    item.Background.Opacity = 100;
                
            }
            foreach (var item in _Grd_MeineTermineZuZeigen_priv)
            {
                var background = item.Background as SolidColorBrush;
                var color = background.Color;

                if (Colors.LightSkyBlue.Equals(color))
                     item.Background.Opacity = 0;                                     
            }
                
        }
        

        private void DatePicker_month_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            // Die Farbe aller Tageszahlen wird wieder auf schwarz gesetzt
            GridTermine.Children.Clear();
            TermineAmMonatGrid.Clear();
            //hier wird die Funktion nochmal mit dem durch den DatePicker ausgewählten Datum aufgerufen
            DateTime tmp = DatePicker_month.Date.DateTime;

            Termin.findeDieAufgabenfürYMDH(tmp.Year, tmp.Month, tmp.Day);
            MeineTermineFürDenMonat = Termin.get_myCurrentGroupTasksAtMonth();

            fuelle_Monatsansicht(tmp);

            // Der Heading wir mit dem durch den DatePicker ausgewählten Datum gleichgesetzt
            //MonatsHeading.Text = tmp.Month.ToString() + tmp.Year.ToString();
            MonatsHeading.Text = (DatumVerarbeitung.berechneMonatIndexZuName(tmp.Month) + " " + (tmp.Year.ToString()));
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
