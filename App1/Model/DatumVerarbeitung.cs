using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualCalendarV01.Pages.Ansichten;

namespace VirtualCalendarV01.Model
{
    class DatumVerarbeitung
    {

        //List<int> TagederWoche = new List<int>();

        DateTime Dt_AktuellerDatum = DateTime.Now;
        public string Str_aktTag { get; set; }
        public string Str_aktMonat { get; set; }
        public string Str_aktJahr { get; set; }
        public string Str_aktTime { get; set; }
        public DayOfWeek DayofWeek{ get; set; }

        public DatumVerarbeitung()
        {
            Str_aktTag = Dt_AktuellerDatum.Day.ToString();
            Str_aktMonat = berechneMonatIndexZuName(Dt_AktuellerDatum.Month);
            Str_aktJahr = Dt_AktuellerDatum.Year.ToString();
            Str_aktTime = Dt_AktuellerDatum.Hour.ToString() + " " + Dt_AktuellerDatum.Minute.ToString();
        }

        public DatumVerarbeitung(int jahr, int monath, int tag)
        {
            Str_aktTag = Dt_AktuellerDatum.Day.ToString();
            Str_aktMonat = berechneMonatIndexZuName(Dt_AktuellerDatum.Month);
            Str_aktJahr = Dt_AktuellerDatum.Year.ToString();
            Str_aktTime = Dt_AktuellerDatum.Hour.ToString() + " " + Dt_AktuellerDatum.Minute.ToString();
        }

        public static string berechneTagIndexZuName(int Index)
        {
            switch (Index)
            {
                case 1:
                    return "Montag";
                case 2:
                    return "Dienstag";
                case 3:
                    return "Mittwoch";
                case 4:
                    return "Donnerstag";
                case 5:
                    return "Freitag";
                case 6:
                    return "Samstag";
                case 7:
                    return "Sonntag";
                default:
                    return "NoTag";
            }
        }

        public static string berechneMonatIndexZuName(int Index)
        {
            switch (Index)
            {
                case 1:
                    return "Januar";
                case 2:
                    return "Februar";
                case 3:
                    return "März";
                case 4:
                    return "April";
                case 5:
                    return "Mai";
                case 6:
                    return "Juni";
                case 7:
                    return "Juli";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "Oktober";
                case 11:
                    return "November";
                case 12:
                    return "Dezember";
                default:
                    return "NoMonth";
            }
        }

       
        internal static DateTime berechneStringtoMonat(string EingegebenesMonat, int Jahr)
        {
            DateTime dt;
            switch (EingegebenesMonat)
            {
                case "Januar":
                    dt = new DateTime(Jahr, 1, 1);
                    return dt;
                case "Februar":
                    dt = new DateTime(Jahr, 2, 1);
                    return dt;
                case "März":
                    dt = new DateTime(Jahr, 3, 1);
                    return dt;
                case "April":
                    dt = new DateTime(Jahr, 4, 1);
                    return dt;
                case "Mai":
                    dt = new DateTime(Jahr, 5, 1);
                    return dt;
                case "Juni":
                    dt = new DateTime(Jahr, 6, 1);
                    return dt;
                case "Juli":
                    dt = new DateTime(Jahr, 7, 1);
                    return dt;
                case "August":
                    dt = new DateTime(Jahr, 8, 1);
                    return dt;
                case "September":
                    dt = new DateTime(Jahr, 9, 1);
                    return dt;
                case "Oktober":
                    dt = new DateTime(Jahr, 10, 1);
                    return dt;
                case "November":
                    dt = new DateTime(Jahr, 11, 1);
                    return dt;
                case "Dezember":
                    dt = new DateTime(Jahr, 12, 1);
                    return dt;
                default:
                    dt = new DateTime(9999,1, 1);
                    return dt;
            }
        }
    }
}
