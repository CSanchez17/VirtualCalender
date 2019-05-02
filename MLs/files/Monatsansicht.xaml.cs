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
using Windows.UI.Xaml.Shapes;
using VirtualCalendarV01.Klassen;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace VirtualCalendarV01.Pages.Ansichten
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    /// 
    
    public sealed partial class Monatsansicht : Page
    {
        DateTime Dt_AktuellesDatum = DateTime.Now;
        DatumVerarbeitung Datumtmp = new DatumVerarbeitung();

        private string Monat = DateTime.Now.Month.ToString();
        private string Jahr = DateTime.Now.Year.ToString();

        public Monatsansicht()
        {
            this.InitializeComponent();       
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (e.Parameter != null)
            {
                DatePicker_month.Date = Convert.ToDateTime(e.Parameter);                
            }
            else
            {
                DatePicker_month.Date = DateTime.Now;
                
            }

            //Berehcne was für ein Monat es ist
            Monat = DatumVerarbeitung.berechneMonatIndexZuName(Monat);

            MonatsHeading.Text = Monat + " " + Jahr;
            //Hier nehme ich das Datum der DataPicker
            DateTime dtDatePickerDatum = DatePicker_month.Date.DateTime;

            //berechne die Tage im MonatsKalender
            fuelle_Monatsansicht(dtDatePickerDatum);

        }

        private void fuelle_Monatsansicht(DateTime Dt_AktuellesDatum)
        {
            //Hier möchte ich das Datum des DatePickers meiner Funktion zu übergeben

            //DateTime date = DatePicker_month.Date.DateTime;
            //DateTime date = DateTime.Now;
            DateTime date = Dt_AktuellesDatum;

            var ersterTag = new DateTime(date.Year, date.Month, 1);
            int firstday = (int)ersterTag.Day;
            var letzterTag = ersterTag.AddMonths(1).AddDays(-1);
            int lastday = (int)letzterTag.Day;
            var letzterTag_letzterMonat = ersterTag.AddDays(-1);
            int lastday_lastmonth = (int)letzterTag_letzterMonat.Day;


            string dayofweek = ersterTag.DayOfWeek.ToString();


            // Die Berechnung der Tage, falls der erste Tag im Monat ein Montag ist
            if (dayofweek == "Monday")
            {

                mo1.Text = firstday.ToString();
                mo2.Text = (firstday + 1 * 7).ToString();
                mo3.Text = (firstday + 2 * 7).ToString();
                mo4.Text = (firstday + 3 * 7).ToString();
                mo5.Text = (firstday + 4 * 7).ToString();
                if ((firstday + 5 * 7) > lastday)
                {
                    var delta = ersterTag.AddDays(35) - letzterTag;
                    int days = (int)delta.Days;
                    mo6.Text = days.ToString();
                    mo6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    di6.Text = (days + 1).ToString();
                    di6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    mi6.Text = (days + 2).ToString();
                    mi6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    do6.Text = (days + 3).ToString();
                    do6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    fr6.Text = (days + 4).ToString();
                    fr6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    sa6.Text = (days + 5).ToString();
                    sa6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    so6.Text = (days + 6).ToString();
                    so6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }
                else
                    mo6.Text = (firstday + 5 * 7).ToString();

                di1.Text = (firstday + 1).ToString();
                di2.Text = (firstday + 1 + 1 * 7).ToString();
                di3.Text = (firstday + 1 + 2 * 7).ToString();
                di4.Text = (firstday + 1 + 3 * 7).ToString();

                if ((firstday + 1 + 4 * 7) > lastday)
                {
                    //int delta = lastday - (firstday + 1 + 4 * 7);
                    //di5.Text = delta.ToString();
                    di5.Text = ((firstday + 1 + 4 * 7) % lastday).ToString();
                    di5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }
                else
                    di5.Text = (firstday + 1 + 4 * 7).ToString();

                mi1.Text = (firstday + 2).ToString();
                mi2.Text = (firstday + 2 + 1 * 7).ToString();
                mi3.Text = (firstday + 2 + 2 * 7).ToString();
                mi4.Text = (firstday + 2 + 3 * 7).ToString();

                if ((firstday + 2 + 4 * 7) > lastday)
                {
                    mi5.Text = ((firstday + 2 + 4 * 7) % lastday).ToString();
                    mi5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }
                else
                    mi5.Text = (firstday + 2 + 4 * 7).ToString();


                do1.Text = (firstday + 3).ToString();
                do2.Text = (firstday + 3 + 1 * 7).ToString();
                do3.Text = (firstday + 3 + 2 * 7).ToString();
                do4.Text = (firstday + 3 + 3 * 7).ToString();
                if ((firstday + 3 + 4 * 7) > lastday)
                {
                    do5.Text = ((firstday + 3 + 4 * 7) % lastday).ToString();
                    do5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    do5.Text = (firstday + 3 + 4 * 7).ToString();


                fr1.Text = (firstday + 4).ToString();
                fr2.Text = (firstday + 4 + 1 * 7).ToString();
                fr3.Text = (firstday + 4 + 2 * 7).ToString();
                fr4.Text = (firstday + 4 + 3 * 7).ToString();
                if ((firstday + 4 + 4 * 7) > lastday)
                {
                    fr5.Text = ((firstday + 4 + 4 * 7) % lastday).ToString();
                    fr5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    fr5.Text = (firstday + 4 + 4 * 7).ToString();

                sa1.Text = (firstday + 5).ToString();
                sa2.Text = (firstday + 5 + 1 * 7).ToString();
                sa3.Text = (firstday + 5 + 2 * 7).ToString();
                sa4.Text = (firstday + 5 + 3 * 7).ToString();
                if ((firstday + 5 + 4 * 7) > lastday)
                {
                    sa5.Text = ((firstday + 5 + 4 * 7) % lastday).ToString();
                    sa5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    sa5.Text = (firstday + 5 + 4 * 7).ToString();

                so1.Text = (firstday + 6).ToString();
                so2.Text = (firstday + 6 + 1 * 7).ToString();
                so3.Text = (firstday + 6 + 2 * 7).ToString();
                so4.Text = (firstday + 6 + 3 * 7).ToString();
                if ((firstday + 5 + 4 * 7) > lastday)
                {
                    so5.Text = ((firstday + 6 + 4 * 7) % lastday).ToString();
                    so5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    so5.Text = (firstday + 6 + 4 * 7).ToString();

                return;
            }
            // Die Berechnung der Tage, falls der erste Tag im Monat ein Dienstag ist
            if (dayofweek == "Tuesday")
            {
                mo1.Text = (lastday_lastmonth).ToString();
                mo1.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);

                mo2.Text = (firstday + 6).ToString();
                mo3.Text = (firstday + 6 + 1 * 7).ToString();
                mo4.Text = (firstday + 6 + 2 * 7).ToString();
                mo5.Text = (firstday + 6 + 3 * 7).ToString();
                if ((firstday + 6 + 4 * 7) > lastday)
                {


                    mo6.Text = ((firstday + 6 + 4 * 7) % lastday).ToString();
                    mo6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    di6.Text = ((firstday + 6 + 4 * 7) % lastday + 1).ToString();
                    di6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    mi6.Text = ((firstday + 6 + 4 * 7) % lastday + 2).ToString();
                    mi6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    do6.Text = ((firstday + 6 + 4 * 7) % lastday + 3).ToString();
                    do6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    fr6.Text = ((firstday + 6 + 4 * 7) % lastday + 4).ToString();
                    fr6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    sa6.Text = ((firstday + 6 + 4 * 7) % lastday + 5).ToString();
                    sa6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    so6.Text = ((firstday + 6 + 4 * 7) % lastday + 6).ToString();
                    so6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }
                else
                    mo6.Text = (firstday + 6 + 4 * 7).ToString();

                di1.Text = firstday.ToString();
                di2.Text = (firstday + 1 * 7).ToString();
                di3.Text = (firstday + 2 * 7).ToString();
                di4.Text = (firstday + 3 * 7).ToString();
                if ((firstday + 4 * 7) > lastday)
                {
                    di5.Text = ((firstday + 4 * 7) % lastday).ToString();
                    di5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    di5.Text = (firstday + 4 * 7).ToString();


                mi1.Text = (firstday + 1).ToString();
                mi2.Text = (firstday + 1 + 1 * 7).ToString();
                mi3.Text = (firstday + 1 + 2 * 7).ToString();
                mi4.Text = (firstday + 1 + 3 * 7).ToString();

                if ((firstday + 1 + 4 * 7) > lastday)
                {
                    mi5.Text = ((firstday + 1 + 4 * 7) % lastday).ToString();
                    mi5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    mi5.Text = (firstday + 1 + 4 * 7).ToString();

                do1.Text = (firstday + 2).ToString();
                do2.Text = (firstday + 2 + 1 * 7).ToString();
                do3.Text = (firstday + 2 + 2 * 7).ToString();
                do4.Text = (firstday + 2 + 3 * 7).ToString();

                if ((firstday + 2 + 4 * 7) > lastday)
                {
                    do5.Text = ((firstday + 2 + 4 * 7) % lastday).ToString();
                    do5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    do5.Text = (firstday + 2 + 4 * 7).ToString();


                fr1.Text = (firstday + 3).ToString();
                fr2.Text = (firstday + 3 + 1 * 7).ToString();
                fr3.Text = (firstday + 3 + 2 * 7).ToString();
                fr4.Text = (firstday + 3 + 3 * 7).ToString();
                if ((firstday + 3 + 4 * 7) > lastday)
                {
                    fr5.Text = ((firstday + 3 + 4 * 7) % lastday).ToString();
                    fr5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }
                else
                    fr5.Text = (firstday + 3 + 4 * 7).ToString();


                sa1.Text = (firstday + 4).ToString();
                sa2.Text = (firstday + 4 + 1 * 7).ToString();
                sa3.Text = (firstday + 4 + 2 * 7).ToString();
                sa4.Text = (firstday + 4 + 3 * 7).ToString();
                if ((firstday + 4 + 4 * 7) > lastday)
                {
                    sa5.Text = ((firstday + 4 + 4 * 7) % lastday).ToString();
                    sa5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    sa5.Text = (firstday + 4 + 4 * 7).ToString();

                so1.Text = (firstday + 5).ToString();
                so2.Text = (firstday + 5 + 1 * 7).ToString();
                so3.Text = (firstday + 5 + 2 * 7).ToString();
                so4.Text = (firstday + 5 + 3 * 7).ToString();
                if ((firstday + 5 + 4 * 7) > lastday)
                {
                    so5.Text = ((firstday + 5 + 4 * 7) % lastday).ToString();
                    so5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    so5.Text = (firstday + 5 + 4 * 7).ToString();

                return;
            }
            // Die Berechnung der Tage, falls der erste Tag im Monat ein Mittwoch ist
            if (dayofweek == "Wednesday")
            {
                mo1.Text = (lastday_lastmonth - 1).ToString();
                mo1.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                mo2.Text = (firstday + 5).ToString();
                mo3.Text = (firstday + 5 + 1 * 7).ToString();
                mo4.Text = (firstday + 5 + 2 * 7).ToString();
                mo5.Text = (firstday + 5 + 3 * 7).ToString();
                if ((firstday + 5 + 4 * 7) > lastday)
                {
                    mo6.Text = ((firstday + 5 + 4 * 7) % lastday).ToString();
                    mo6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    di6.Text = ((firstday + 5 + 4 * 7) % lastday + 1).ToString();
                    di6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    mi6.Text = ((firstday + 5 + 4 * 7) % lastday + 2).ToString();
                    mi6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    do6.Text = ((firstday + 5 + 4 * 7) % lastday + 3).ToString();
                    do6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    fr6.Text = ((firstday + 5 + 4 * 7) % lastday + 4).ToString();
                    fr6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    sa6.Text = ((firstday + 5 + 4 * 7) % lastday + 5).ToString();
                    sa6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    so6.Text = ((firstday + 5 + 4 * 7) % lastday + 6).ToString();
                    so6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }
                else
                    mo6.Text = (firstday + 6 + 4 * 7).ToString();

                di1.Text = lastday_lastmonth.ToString();
                di1.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                di2.Text = (firstday + 6).ToString();
                di3.Text = (firstday + 6 + 1 * 7).ToString();
                di4.Text = (firstday + 6 + 2 * 7).ToString();
                if ((firstday + 6 + 3 * 7) > lastday)
                {
                    di5.Text = ((firstday + 6 + 3 * 7) % lastday).ToString();
                    di5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    di5.Text = (firstday + 6 + 3 * 7).ToString();


                mi1.Text = (firstday).ToString();
                mi2.Text = (firstday + 1 * 7).ToString();
                mi3.Text = (firstday + 2 * 7).ToString();
                mi4.Text = (firstday + 3 * 7).ToString();

                if ((firstday + 4 * 7) > lastday)
                {
                    mi5.Text = ((firstday + 4 * 7) % lastday).ToString();
                    mi5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    mi5.Text = (firstday + 4 * 7).ToString();

                do1.Text = (firstday + 1).ToString();
                do2.Text = (firstday + 1 + 1 * 7).ToString();
                do3.Text = (firstday + 1 + 2 * 7).ToString();
                do4.Text = (firstday + 1 + 3 * 7).ToString();

                if ((firstday + 1 + 4 * 7) > lastday)
                {
                    do5.Text = ((firstday + 1 + 4 * 7) % lastday).ToString();
                    do5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    do5.Text = (firstday + 1 + 4 * 7).ToString();


                fr1.Text = (firstday + 2).ToString();
                fr2.Text = (firstday + 2 + 1 * 7).ToString();
                fr3.Text = (firstday + 2 + 2 * 7).ToString();
                fr4.Text = (firstday + 2 + 3 * 7).ToString();
                if ((firstday + 2 + 4 * 7) > lastday)
                {
                    fr5.Text = ((firstday + 2 + 4 * 7) % lastday).ToString();
                    fr5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    fr5.Text = (firstday + 2 + 4 * 7).ToString();


                sa1.Text = (firstday + 3).ToString();
                sa2.Text = (firstday + 3 + 1 * 7).ToString();
                sa3.Text = (firstday + 3 + 2 * 7).ToString();
                sa4.Text = (firstday + 3 + 3 * 7).ToString();
                if ((firstday + 3 + 4 * 7) > lastday)
                {
                    sa5.Text = ((firstday + 3 + 4 * 7) % lastday).ToString();
                    sa5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    sa5.Text = (firstday + 3 + 4 * 7).ToString();

                so1.Text = (firstday + 4).ToString();
                so2.Text = (firstday + 4 + 1 * 7).ToString();
                so3.Text = (firstday + 4 + 2 * 7).ToString();
                so4.Text = (firstday + 4 + 3 * 7).ToString();
                if ((firstday + 4 + 4 * 7) > lastday)
                {
                    so5.Text = ((firstday + 4 + 4 * 7) % lastday).ToString();
                    so5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    so5.Text = (firstday + 4 + 4 * 7).ToString();

                return;
            }
            // Die Berechnung der Tage, falls der erste Tag im Monat ein Donnerstag ist
            if (dayofweek == "Thursday")
            {
                mo1.Text = (lastday_lastmonth - 2).ToString();
                mo1.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                mo2.Text = (firstday + 4).ToString();
                mo3.Text = (firstday + 4 + 1 * 7).ToString();
                mo4.Text = (firstday + 4 + 2 * 7).ToString();
                mo5.Text = (firstday + 4 + 3 * 7).ToString();
                if ((firstday + 4 + 4 * 7) > lastday)
                {
                    mo6.Text = ((firstday + 4 + 4 * 7) % lastday).ToString();
                    mo6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    di6.Text = ((firstday + 4 + 4 * 7) % lastday + 1).ToString();
                    di6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    mi6.Text = ((firstday + 4 + 4 * 7) % lastday + 2).ToString();
                    mi6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    do6.Text = ((firstday + 4 + 4 * 7) % lastday + 3).ToString();
                    do6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    fr6.Text = ((firstday + 4 + 4 * 7) % lastday + 4).ToString();
                    fr6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    sa6.Text = ((firstday + 4 + 4 * 7) % lastday + 5).ToString();
                    sa6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    so6.Text = ((firstday + 4 + 4 * 7) % lastday + 6).ToString();
                    so6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }
                else
                    mo6.Text = (firstday + 4 + 4 * 7).ToString();

                di1.Text = (lastday_lastmonth - 1).ToString();
                di1.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                di2.Text = (firstday + 5).ToString();
                di3.Text = (firstday + 5 + 1 * 7).ToString();
                di4.Text = (firstday + 5 + 2 * 7).ToString();
                if ((firstday + 5 + 3 * 7) > lastday)
                {
                    di5.Text = ((firstday + 5 + 3 * 7) % lastday).ToString();
                    di5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    di5.Text = (firstday + 5 + 3 * 7).ToString();


                mi1.Text = (lastday_lastmonth).ToString();
                mi1.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                mi2.Text = (firstday + 6).ToString();
                mi3.Text = (firstday + 6 + 1 * 7).ToString();
                mi4.Text = (firstday + 6 + 2 * 7).ToString();

                if ((firstday + 6 + 3 * 7) > lastday)
                {
                    mi5.Text = ((firstday + 6 + 3 * 7) % lastday).ToString();
                    mi5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    mi5.Text = (firstday + 6 + 3 * 7).ToString();

                do1.Text = (firstday).ToString();
                do2.Text = (firstday + 1 * 7).ToString();
                do3.Text = (firstday + 2 * 7).ToString();
                do4.Text = (firstday + 3 * 7).ToString();

                if ((firstday + 4 * 7) > lastday)
                {
                    do5.Text = ((firstday + 4 * 7) % lastday).ToString();
                    do5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    do5.Text = (firstday + 4 * 7).ToString();


                fr1.Text = (firstday + 1).ToString();
                fr2.Text = (firstday + 1 + 1 * 7).ToString();
                fr3.Text = (firstday + 1 + 2 * 7).ToString();
                fr4.Text = (firstday + 1 + 3 * 7).ToString();
                if ((firstday + 1 + 4 * 7) > lastday)
                {
                    fr5.Text = ((firstday + 1 + 4 * 7) % lastday).ToString();
                    fr5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    fr5.Text = (firstday + 1 + 4 * 7).ToString();


                sa1.Text = (firstday + 2).ToString();
                sa2.Text = (firstday + 2 + 1 * 7).ToString();
                sa3.Text = (firstday + 2 + 2 * 7).ToString();
                sa4.Text = (firstday + 2 + 3 * 7).ToString();
                if ((firstday + 2 + 4 * 7) > lastday)
                {
                    sa5.Text = ((firstday + 2 + 4 * 7) % lastday).ToString();
                    sa5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    sa5.Text = (firstday + 2 + 4 * 7).ToString();

                so1.Text = (firstday + 3).ToString();
                so2.Text = (firstday + 3 + 1 * 7).ToString();
                so3.Text = (firstday + 3 + 2 * 7).ToString();
                so4.Text = (firstday + 3 + 3 * 7).ToString();
                if ((firstday + 3 + 4 * 7) > lastday)
                {
                    so5.Text = ((firstday + 3 + 4 * 7) % lastday).ToString();
                    so5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    so5.Text = (firstday + 3 + 4 * 7).ToString();

                return;

            }
            // Die Berechnung der Tage, falls der erste Tag im Monat ein Freitag ist
            if (dayofweek == "Friday")
            {
                mo1.Text = (lastday_lastmonth - 3).ToString();
                mo1.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                mo2.Text = (firstday + 3).ToString();
                mo3.Text = (firstday + 3 + 1 * 7).ToString();
                mo4.Text = (firstday + 3 + 2 * 7).ToString();
                mo5.Text = (firstday + 3 + 3 * 7).ToString();
                if ((firstday + 3 + 4 * 7) > lastday)
                {
                    mo6.Text = ((firstday + 3 + 4 * 7) % lastday).ToString();
                    mo6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    di6.Text = ((firstday + 3 + 4 * 7) % lastday + 1).ToString();
                    di6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    mi6.Text = ((firstday + 3 + 4 * 7) % lastday + 2).ToString();
                    mi6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    do6.Text = ((firstday + 3 + 4 * 7) % lastday + 3).ToString();
                    do6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    fr6.Text = ((firstday + 3 + 4 * 7) % lastday + 4).ToString();
                    fr6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    sa6.Text = ((firstday + 3 + 4 * 7) % lastday + 5).ToString();
                    sa6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    so6.Text = ((firstday + 3 + 4 * 7) % lastday + 6).ToString();
                    so6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }
                else
                    mo6.Text = (firstday + 3 + 4 * 7).ToString();

                di1.Text = (lastday_lastmonth - 2).ToString();
                di1.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                di2.Text = (firstday + 4).ToString();
                di3.Text = (firstday + 4 + 1 * 7).ToString();
                di4.Text = (firstday + 4 + 2 * 7).ToString();
                if ((firstday + 4 + 3 * 7) > lastday)
                {
                    di5.Text = ((firstday + 4 + 3 * 7) % lastday).ToString();
                    di5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    di5.Text = (firstday + 4 + 3 * 7).ToString();


                mi1.Text = (lastday_lastmonth - 1).ToString();
                mi1.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                mi2.Text = (firstday + 5).ToString();
                mi3.Text = (firstday + 5 + 1 * 7).ToString();
                mi4.Text = (firstday + 5 + 2 * 7).ToString();

                if ((firstday + 5 + 3 * 7) > lastday)
                {
                    mi5.Text = ((firstday + 5 + 3 * 7) % lastday).ToString();
                    mi5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    mi5.Text = (firstday + 5 + 3 * 7).ToString();

                do1.Text = (lastday_lastmonth).ToString();
                do1.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                do2.Text = (firstday + 6).ToString();
                do3.Text = (firstday + 6 + 1 * 7).ToString();
                do4.Text = (firstday + 6 + 2 * 7).ToString();

                if ((firstday + 6 + 3 * 7) > lastday)
                {
                    do5.Text = ((firstday + 6 + 3 * 7) % lastday).ToString();
                    do5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    do5.Text = (firstday + 6 + 3 * 7).ToString();


                fr1.Text = (firstday).ToString();
                fr2.Text = (firstday + 1 * 7).ToString();
                fr3.Text = (firstday + 2 * 7).ToString();
                fr4.Text = (firstday + 3 * 7).ToString();
                if ((firstday + 4 * 7) > lastday)
                {
                    fr5.Text = ((firstday + 4 * 7) % lastday).ToString();
                    fr5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    fr5.Text = (firstday + 4 * 7).ToString();


                sa1.Text = (firstday + 1).ToString();
                sa2.Text = (firstday + 1 + 1 * 7).ToString();
                sa3.Text = (firstday + 1 + 2 * 7).ToString();
                sa4.Text = (firstday + 1 + 3 * 7).ToString();
                if ((firstday + 1 + 4 * 7) > lastday)
                {
                    sa5.Text = ((firstday + 1 + 4 * 7) % lastday).ToString();
                    sa5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    sa5.Text = (firstday + 1 + 4 * 7).ToString();

                so1.Text = (firstday + 2).ToString();
                so2.Text = (firstday + 2 + 1 * 7).ToString();
                so3.Text = (firstday + 2 + 2 * 7).ToString();
                so4.Text = (firstday + 2 + 3 * 7).ToString();
                if ((firstday + 2 + 4 * 7) > lastday)
                {
                    so5.Text = ((firstday + 2 + 4 * 7) % lastday).ToString();
                    so5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    so5.Text = (firstday + 2 + 4 * 7).ToString();

                return;
            }
            // Die Berechnung der Tage, falls der erste Tag im Monat ein Samstag ist
            if (dayofweek == "Saturday")
            {
                mo1.Text = (lastday_lastmonth - 4).ToString();
                mo1.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                mo2.Text = (firstday + 2).ToString();
                mo3.Text = (firstday + 2 + 1 * 7).ToString();
                mo4.Text = (firstday + 2 + 2 * 7).ToString();
                mo5.Text = (firstday + 2 + 3 * 7).ToString();


                if ((firstday + 2 + 4 * 7) > (lastday))
                {
                    mo6.Text = ((firstday + 2 + 4 * 7) % lastday).ToString();
                    mo6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    di6.Text = ((firstday + 2 + 4 * 7) % lastday + 1).ToString();
                    di6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    mi6.Text = ((firstday + 2 + 4 * 7) % lastday + 2).ToString();
                    mi6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    do6.Text = ((firstday + 2 + 4 * 7) % lastday + 3).ToString();
                    do6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    fr6.Text = ((firstday + 2 + 4 * 7) % lastday + 4).ToString();
                    fr6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    sa6.Text = ((firstday + 2 + 4 * 7) % lastday + 5).ToString();
                    sa6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    so6.Text = ((firstday + 2 + 4 * 7) % lastday + 6).ToString();
                    so6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }
                //Die else-Schleife wird betreten, wenn der Monat 31 Tage hat und an einem Samstag anfängt.
                else
                {
                    mo6.Text = (firstday + 2 + 4 * 7).ToString();
                    di6.Text = ((firstday + 2 + 4 * 7) % lastday + 1).ToString();
                    di6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    mi6.Text = ((firstday + 2 + 4 * 7) % lastday + 2).ToString();
                    mi6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    do6.Text = ((firstday + 2 + 4 * 7) % lastday + 3).ToString();
                    do6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    fr6.Text = ((firstday + 2 + 4 * 7) % lastday + 4).ToString();
                    fr6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    sa6.Text = ((firstday + 2 + 4 * 7) % lastday + 5).ToString();
                    sa6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    so6.Text = ((firstday + 2 + 4 * 7) % lastday + 6).ToString();
                    so6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);

                }


                di1.Text = (lastday_lastmonth - 3).ToString();
                di1.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                di2.Text = (firstday + 3).ToString();
                di3.Text = (firstday + 3 + 1 * 7).ToString();
                di4.Text = (firstday + 3 + 2 * 7).ToString();
                if ((firstday + 3 + 3 * 7) > lastday)
                {
                    di5.Text = ((firstday + 3 + 3 * 7) % lastday).ToString();
                    di5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    di5.Text = (firstday + 3 + 3 * 7).ToString();


                mi1.Text = (lastday_lastmonth - 2).ToString();
                mi1.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                mi2.Text = (firstday + 4).ToString();
                mi3.Text = (firstday + 4 + 1 * 7).ToString();
                mi4.Text = (firstday + 4 + 2 * 7).ToString();

                if ((firstday + 4 + 3 * 7) > lastday)
                {
                    mi5.Text = ((firstday + 4 + 3 * 7) % lastday).ToString();
                    mi5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    mi5.Text = (firstday + 4 + 3 * 7).ToString();

                do1.Text = (lastday_lastmonth - 1).ToString();
                do1.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                do2.Text = (firstday + 5).ToString();
                do3.Text = (firstday + 5 + 1 * 7).ToString();
                do4.Text = (firstday + 5 + 2 * 7).ToString();

                if ((firstday + 5 + 3 * 7) > lastday)
                {
                    do5.Text = ((firstday + 5 + 3 * 7) % lastday).ToString();
                    do5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    do5.Text = (firstday + 5 + 3 * 7).ToString();


                fr1.Text = (lastday_lastmonth).ToString();
                fr1.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                fr2.Text = (firstday + 6).ToString();
                fr3.Text = (firstday + 6 + 1 * 7).ToString();
                fr4.Text = (firstday + 6 + 2 * 7).ToString();
                if ((firstday + 6 + 3 * 7) > lastday)
                {
                    fr5.Text = ((firstday + 6 + 3 * 7) % lastday).ToString();
                    fr5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    fr5.Text = (firstday + 6 + 3 * 7).ToString();


                sa1.Text = (firstday).ToString();
                sa2.Text = (firstday + 1 * 7).ToString();
                sa3.Text = (firstday + 2 * 7).ToString();
                sa4.Text = (firstday + 3 * 7).ToString();
                if ((firstday + 4 * 7) > lastday)
                {
                    sa5.Text = ((firstday + 4 * 7) % lastday).ToString();
                    sa5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    sa5.Text = (firstday + 4 * 7).ToString();

                so1.Text = (firstday + 1).ToString();
                so2.Text = (firstday + 1 + 1 * 7).ToString();
                so3.Text = (firstday + 1 + 2 * 7).ToString();
                so4.Text = (firstday + 1 + 3 * 7).ToString();
                if ((firstday + 1 + 4 * 7) > lastday)
                {
                    so5.Text = ((firstday + 1 + 4 * 7) % lastday).ToString();
                    so5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    so5.Text = (firstday + 1 + 4 * 7).ToString();

                return;
            }
            // Die Berechnung der Tage, falls der erste Tag im Monat ein Sonntag ist
            if (dayofweek == "Sunday")
            {
                mo1.Text = (lastday_lastmonth - 5).ToString();
                mo1.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                mo2.Text = (firstday + 1).ToString();
                mo3.Text = (firstday + 1 + 1 * 7).ToString();
                mo4.Text = (firstday + 1 + 2 * 7).ToString();
                mo5.Text = (firstday + 1 + 3 * 7).ToString();
                // In diese Schleife wird reingegangen nur wenn der Monat Februar an einem Sonntag anfängt, da dieser nur 28 oder 29 Tage haben kann.
                if ((firstday + 1 + 4 * 7) > lastday)
                {
                    mo6.Text = ((firstday + 1 + 4 * 7) % lastday).ToString();
                    mo6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    di6.Text = ((firstday + 1 + 4 * 7) % lastday + 1).ToString();
                    di6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    mi6.Text = ((firstday + 1 + 4 * 7) % lastday + 2).ToString();
                    mi6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    do6.Text = ((firstday + 1 + 4 * 7) % lastday + 3).ToString();
                    do6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    fr6.Text = ((firstday + 1 + 4 * 7) % lastday + 4).ToString();
                    fr6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    sa6.Text = ((firstday + 1 + 4 * 7) % lastday + 5).ToString();
                    sa6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    so6.Text = ((firstday + 1 + 4 * 7) % lastday + 6).ToString();
                    so6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }
                else
                {
                    mo6.Text = (firstday + 1 + 4 * 7).ToString();
                    if (lastday == 30)
                    {
                        di6.Text = ((firstday + 1 + 4 * 7) % (lastday) + 1).ToString();
                        di6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                        mi6.Text = ((firstday + 1 + 4 * 7) % (lastday) + 2).ToString();
                        mi6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                        do6.Text = ((firstday + 1 + 4 * 7) % (lastday) + 3).ToString();
                        do6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                        fr6.Text = ((firstday + 1 + 4 * 7) % (lastday) + 4).ToString();
                        fr6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                        sa6.Text = ((firstday + 1 + 4 * 7) % (lastday) + 5).ToString();
                        sa6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                        so6.Text = ((firstday + 1 + 4 * 7) % (lastday) + 6).ToString();
                        so6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    }
                    else if (lastday == 31)
                    {
                        di6.Text = "31";
                        mi6.Text = "1";
                        mi6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                        do6.Text = "2";
                        do6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                        fr6.Text = "3";
                        fr6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                        sa6.Text = "4";
                        sa6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                        so6.Text = "5";
                        so6.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    }


                }


                di1.Text = (lastday_lastmonth - 4).ToString();
                di1.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                di2.Text = (firstday + 2).ToString();
                di3.Text = (firstday + 2 + 1 * 7).ToString();
                di4.Text = (firstday + 2 + 2 * 7).ToString();
                if ((firstday + 2 + 3 * 7) > lastday)
                {
                    di5.Text = ((firstday + 2 + 3 * 7) % lastday).ToString();
                    di5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    di5.Text = (firstday + 2 + 3 * 7).ToString();


                mi1.Text = (lastday_lastmonth - 3).ToString();
                mi1.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                mi2.Text = (firstday + 3).ToString();
                mi3.Text = (firstday + 3 + 1 * 7).ToString();
                mi4.Text = (firstday + 3 + 2 * 7).ToString();

                if ((firstday + 3 + 3 * 7) > lastday)
                {
                    mi5.Text = ((firstday + 3 + 3 * 7) % lastday).ToString();
                    mi5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    mi5.Text = (firstday + 3 + 3 * 7).ToString();

                do1.Text = (lastday_lastmonth - 2).ToString();
                do1.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                do2.Text = (firstday + 4).ToString();
                do3.Text = (firstday + 4 + 1 * 7).ToString();
                do4.Text = (firstday + 4 + 2 * 7).ToString();

                if ((firstday + 4 + 3 * 7) > lastday)
                {
                    do5.Text = ((firstday + 4 + 3 * 7) % lastday).ToString();
                    do5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    do5.Text = (firstday + 4 + 3 * 7).ToString();


                fr1.Text = (lastday_lastmonth - 1).ToString();
                fr1.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                fr2.Text = (firstday + 5).ToString();
                fr3.Text = (firstday + 5 + 1 * 7).ToString();
                fr4.Text = (firstday + 5 + 2 * 7).ToString();
                if ((firstday + 5 + 3 * 7) > lastday)
                {
                    fr5.Text = ((firstday + 5 + 3 * 7) % lastday).ToString();
                    fr5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    fr5.Text = (firstday + 5 + 3 * 7).ToString();


                sa1.Text = (lastday_lastmonth).ToString();
                sa1.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                sa2.Text = (firstday + 6).ToString();
                sa3.Text = (firstday + 6 + 1 * 7).ToString();
                sa4.Text = (firstday + 6 + 2 * 7).ToString();
                if ((firstday + 6 + 3 * 7) > lastday)
                {
                    sa5.Text = ((firstday + 6 + 3 * 7) % lastday).ToString();
                    sa5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    sa5.Text = (firstday + 6 + 3 * 7).ToString();

                so1.Text = (firstday).ToString();
                so2.Text = (firstday + 1 * 7).ToString();
                so3.Text = (firstday + 2 * 7).ToString();
                so4.Text = (firstday + 3 * 7).ToString();
                if ((firstday + 4 * 7) > lastday)
                {
                    so5.Text = ((firstday + 4 * 7) % lastday).ToString();
                    so5.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGray);
                }

                else
                    so5.Text = (firstday + 4 * 7).ToString();

                return;
            }
        }
        private void set_MonatsansichtFarbe_ToDefault()
        {
            mo1.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            mo2.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            mo3.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            mo4.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            mo5.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            mo6.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);

            di1.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            di2.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            di3.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            di4.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            di5.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            di6.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);

            mi1.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            mi2.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            mi3.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            mi4.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            mi5.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            mi6.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);

            do1.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            do2.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            do3.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            di4.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            do5.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            do6.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);

            fr1.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            fr2.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            fr3.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            fr4.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            fr5.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            fr6.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);

            sa1.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            sa2.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            sa3.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            sa4.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            sa5.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            sa6.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);

            so1.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            so2.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            so3.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            so4.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            so5.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            so6.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);

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
            //           tbKalenderAuswahlbttn.Text = "Individualansicht";
            //Zeige die Elemente vom Indivudualansicht
            //   Frame.Navigate(typeof(IndividualansichtPage));
            TagElipsen_20.Visibility = Visibility.Collapsed;
            grdElipsenCommndbar.Visibility = Visibility.Collapsed;
        }

        private void GruppeBarButton_Click(object sender, RoutedEventArgs e)
        {
            //          tbKalenderAuswahlbttn.Text = "Gruppenansicht";
            //Zeige die Elemente vom Gruppenansicht
            //   Frame.Navigate(typeof(GruppenansichtPage));
            TagElipsen_20.Visibility = Visibility.Visible;
            grdElipsenCommndbar.Visibility = Visibility.Visible;
        }

        private void grdElipsesEinzelneZelle20_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = mo1.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }
        private void grdElipsesEinzelneZelle21_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = di1.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle23_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = do1.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle24_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = fr1.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle25_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = sa1.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle26_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = so1.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle30_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = mo2.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle32_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = mi2.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle33_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = do2.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle34_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = fr2.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle35_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = sa2.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle36_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = so2.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle40_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = mo3.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle41_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = di3.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle42_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = mi3.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle43_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = do3.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle44_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = fr3.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle45_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = sa3.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle46_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = so3.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle50_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = mo4.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle51_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = di4.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle52_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = mi4.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle53_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = do4.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle54_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = fr4.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle55_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = sa4.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle56_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = so4.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle60_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = mo5.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle61_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = di5.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle62_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = mi5.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle63_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = do5.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle64_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = fr5.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle65_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = sa5.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle66_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = so5.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle70_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = mo6.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle71_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = di6.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle72_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = mi6.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle73_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = do6.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle74_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = fr6.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle75_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = sa6.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle76_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = so6.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle31_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = di2.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

        private void grdElipsesEinzelneZelle22_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //Sende den aktuellen Monat
            string Str_AktuellerMonat = DatePicker_month.Date.DateTime.Month.ToString();
            string Str_AktuellesJahr = DatePicker_month.Date.DateTime.Year.ToString();
            string str_aktMonatUndTag = mi1.Text + " " + DatumVerarbeitung.berechneMonatIndexZuName(Str_AktuellerMonat) + " " + Str_AktuellesJahr;
            Frame.Navigate(typeof(Tagesansicht), str_aktMonatUndTag);
        }

      
        private void DatePicker_month_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            // Die Farbe aller Tageszahlen wird wieder auf schwarz gesetzt
            set_MonatsansichtFarbe_ToDefault();
            //hier wird die Funktion nochmal mit dem durch den DatePicker ausgewählten Datum aufgerufen
            DateTime tmp = DatePicker_month.Date.DateTime;
            fuelle_Monatsansicht(tmp);

            // Der Heading wir mit dem durch den DatePicker ausgewählten Datum gleichgesetzt
            //MonatsHeading.Text = tmp.Month.ToString() + tmp.Year.ToString();
            MonatsHeading.Text = (DatumVerarbeitung.berechneMonatIndexZuName(tmp.Month.ToString()) + " " + (tmp.Year.ToString()));
        }

     }

    
}
