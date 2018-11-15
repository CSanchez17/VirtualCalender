using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace VirtualCalendarV01.Klassen
{
    public class Termin
    {
        //-------------------------------------------------------------------------------------------------------//
        //Erzeuge Collection für die ganze Benutzern im System (Datenbank)
        //Hier sind die ganze Termine enthalten
        private static ObservableCollection<Termin> terminResponse = new ObservableCollection<Termin>();
        //hier ist die zugehörige Eigenschaft (Get und Set)
        //damit man Zugriff auf das Element hat
        public static ObservableCollection<Termin> TerminResponse
        {
            get { return terminResponse; }
            set { terminResponse = value; }
        }
        //-------------------------------------------------------------------------------------------------------//
        //-------------------------------------------------------------------------------------------------------//
        //hier werden die aktuelle Aufgabe(auf diesem Tag) angezeigt---------------------------------------------//
        private static ObservableCollection<Termin> myCurrentGroupTasksAtMonth = new ObservableCollection<Termin>();
        public static ObservableCollection<Termin> MyCurrentGroupTasksAtMonth
        {
            get { return myCurrentGroupTasksAtMonth; }
            set { myCurrentGroupTasksAtMonth = value; }
        }
        //------------------------------------------------------------------------------------------------------//
        //hier werden die aktuelle Aufgabe(auf diesem Tag) angezeigt---------------------------------------------//
        private static ObservableCollection<Termin> myCurrentGroupTasksAtDay = new ObservableCollection<Termin>();
        public static ObservableCollection<Termin> MyCurrentGroupTasksAtDay
        {
            get { return myCurrentGroupTasksAtDay; }
            set { myCurrentGroupTasksAtDay = value; }
        }
        //------------------------------------------------------------------------------------------------------//
        //hier werden die aktuelle Aufgabe(auf diesem Tag) angezeigt---------------------------------------------//
        private static ObservableCollection<Termin> myCurrentGroupTasksAtHour = new ObservableCollection<Termin>();
        public static ObservableCollection<Termin> MyCurrentGroupTasksAtHour
        {
            get { return myCurrentGroupTasksAtHour; }
            set { myCurrentGroupTasksAtHour = value; }
        }
        //------------------------------------------------------------------------------------------------------//
        //Ab hier kommen die ganze Elemente der Klasse: element mit den zugehörigen Eigenschaft get und set
        //Hier ist gemeint: der Termin soll in welcher Ansicht sichtbar sein
        public enum Enum_KalenderAuswahl { Individualansicht = 0,  Gruppenansicht = 1} 
        private Enum_KalenderAuswahl _enum_KalenderAuswahl;
        //hier ist die zugehörige Eigenschaft (Get und Set)
        //damit man Zugriff auf das Element hat
        public Enum_KalenderAuswahl enum_KalenderAuswahl
        {
            get { return _enum_KalenderAuswahl; }
            set { _enum_KalenderAuswahl = value; }
        }

        //Auswahl des Wiederholungszyklus
        public enum Enum_WiederholungsZyklus { Keine = 0, Jaehrlich = 1, Monatlich = 2, Woechentlich = 3, Taeglich = 4 };
        private Enum_WiederholungsZyklus _enum_WiederholungsZyklus;
        //hier ist die zugehörige Eigenschaft (Get und Set)
        //damit man Zugriff auf das Element hat
        public Enum_WiederholungsZyklus enum_WiederholungsZyklus
        {
            get { return _enum_WiederholungsZyklus; }
            set { _enum_WiederholungsZyklus = value; }
        }

        private string str_Farbe;
        public string Str_Farbe
        {
            get { return str_Farbe; }
            set { str_Farbe = value; }
        }

        private Ellipse myEllipse = new Ellipse();
        public Ellipse MyEllipse
        {
            get { return myEllipse; }
            set { myEllipse = value; }
        }


        //hier ist die zugehörige Eigenschaft (Get und Set)
        //damit man Zugriff auf das Element hat   
        private string str_Bezeichnung;
        public string Str_Bezeichnung
        {
            get { return str_Bezeichnung; }
            set { str_Bezeichnung = value; }
        }

        private DateTime dt_Beginn;
        public DateTime Dt_Beginn
        {
            get { return dt_Beginn; }
            set { dt_Beginn = value; }
        }
        
        private string str_Dt_Beginn;
        public string Str_Dt_Beginn
        {
            get { return str_Dt_Beginn; }
            set { str_Dt_Beginn = value; }
        }
        private DateTime dt_Ende;
        public DateTime Dt_Ende
        {
            get { return dt_Ende; }
            set { dt_Ende = value; }
        }
        private string str_dt_Ende;
        public string Str_dt_Ende
        {
            get { return str_dt_Ende; }
            set { str_dt_Ende = value; }
        }
        //Referenz zu dem Benutzer (DB Reelevant), 
        private int int_BenutzerId;
        public int Int_BenutzerId
        {
            get { return int_BenutzerId; }
            set { int_BenutzerId = value; }
        }

        private string str_BenutzerId;
        public string Str_BenutzerId
        {
            get { return str_BenutzerId; }
            set { str_BenutzerId = value; }
        }

        //Kommentar wo, Beschreibung
        private string str_TerminBeschreibung;
        public string Str_TerminBeschreibung
        {
            get { return str_TerminBeschreibung; }
            set { str_TerminBeschreibung = value; }
        }

        // Key
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        //zur welchen Gruppe gehört die Aufgabe
        private string str_ErzeugtFuerGruppe;
        public string Str_ErzeugtFuerGruppe
        {
            get { return str_ErzeugtFuerGruppe; }
            set { str_ErzeugtFuerGruppe = value; }
        }

        private string uhrzeitHeading;
        public string UhrzeitHeading
        {
            get { return uhrzeitHeading; }
            set { uhrzeitHeading = value; }
        }
        //hier ist unser Konstruktor
        public Termin()
        {
            
        }

        public Termin(string name)
        {
            this.str_Bezeichnung = name;

        }

        public Termin(Termin other)
        {
            //      this.Benutzer = other.Benutzer;
            this.Int_BenutzerId = other.Int_BenutzerId;
            this.Dt_Beginn = other.Dt_Beginn;
            this.Dt_Ende = other.Dt_Ende;
            this.enum_KalenderAuswahl = other.enum_KalenderAuswahl;
            this.enum_WiederholungsZyklus = other.enum_WiederholungsZyklus;
            this.Str_TerminBeschreibung = other.Str_TerminBeschreibung;
            this.Id = other.Id;
        }

        //hier übergebe welche Benutzer dafür zuständig ist
        public void TerminLoeschen(Benutzer benutzer)
        {            
            //Delete Termin
            App.VIEW_MODEL.DeleteTermin(this.Id);
            string Betreff = "Ein Termin wurde gelöscht! ٩(●̮̃•)۶ : \n";

            //schicke Email an den verantwörtlichen
            nachrichtSchicken(this, benutzer.Str_Email, Betreff);

        }

        public void TerminLoeschenSilently()
        {
            //Delete Termin
            App.VIEW_MODEL.DeleteTermin(this.Id);              

        }

        public void TerminUpdaten(Benutzer benutzer)
        {
            //Update Termin
            App.VIEW_MODEL.UpdateTermin(this);
            string Betreff = "Ein Termin wurde geändert! ٩(●̮̃•)۶: \n";

            //schicke Email an den verantwörtlichen
            nachrichtSchicken(this, benutzer.Str_Email, Betreff);

        }

        public void TerminErzeugen(Benutzer benutzer)
        {
            //setze Farbe des Termines
            myEllipse.Fill = benutzer.MyEllipse.Fill;

            //Update Termin
            App.VIEW_MODEL.InsertNewTermin(this);

            getTerminResponse();

            //Setze ein Falg : wird true, wenn der Benutzer gefunden wurde!
            bool flag = false;
            //Suche den Benutzer durch den angegebenen Namen
            Termin _termin = sucheErzeugterTermin(this, ref flag);
                        
            if (flag)
            {
                //Setze den Betreff
                string Betreff = "Ein neuer Termin wurde erstellt: ٩(●̮̃•)۶ \n";
                
                //schicke Email an den verantwörtlichen
                nachrichtSchicken(this, benutzer.Str_Email, Betreff);
            }
        }

        private Termin sucheErzeugterTermin(Termin termin, ref bool flag)
        {
            //Suche in jeder Benutzer des Arrays benutzerResponse
            //erzeuge mein gesuchtes Element benutzer
            Termin _Termin = new Termin();
            //foreach Schleife läuft durch jedes Element in der Collection
            //der benutzt item als ein Element der Collection, also item hier ist ein benutzer
            foreach (var item in TerminResponse)
            {
                if ((item.str_Bezeichnung == termin.str_Bezeichnung) && (item.str_BenutzerId == termin.str_BenutzerId))
                {
                    //da item auch ein benutzer ist, darf man das hier machen
                    _Termin = item;
                    flag = true;
                    break;
                }
                else
                {
                    //Benutzer nicht gefunden
                    //Setze flag auf false
                    //damit Ankunft, dass der Benutzer nicht da ist.
                    flag = false;
                }
            }
            return _Termin;
        }
           
        private void nachrichtSchicken(Termin termin, string BenutzerEmail, string Betreff)
        {
            //Der Benutzer soll Meldung bei Anlegen und Bearbeiten eines Termins bekommen            
            //schicke Email:
            Model.EmailVerarbeitung.EmailSenden(termin, BenutzerEmail, Betreff);
        }

        //Funktion getTerminResponse(): gibt eine ObservableCollection<Termin> zurück
        //hier wird die Unter Funktion App.VIEW_MODEL.GetTermine() aufgerufen,
        //diese holt sich die ganze Termine aus dem Datenbank und gibt eine ObservableCollection<Termin> zurück, 
        //wo die ganze Termine enthalten sind
        //also die ganze Termine werden jetzt in der Collection terminResponse gespeichert
        //Eine Collection ist einfach ein Vektor in C++, und ein Vektor ist ähnlich ein Array in C (nun viel besser, und dinamisch)
        public static ObservableCollection<Termin> getTerminResponse(int year = 0, int month = 0, int day = 0)
        {
            //Hole die Termine aus dem Datenbank
            terminResponse = new ObservableCollection<Termin>(App.VIEW_MODEL.GetTermine());
            //gebe die Collection terminResponse zurück
            //TerminResponse mit T, weil ich benutze die Eigenschaft, also get und set (Data Hidding)
            return TerminResponse;
        }

        

        public static void findeDieAufgabenfürYMDH(int year, int month, int day)
        {
            //Aktualiziere die Info
            App.updateInfo();

            ObservableCollection<Termin> atHour = new ObservableCollection<Termin>();
            ObservableCollection<Termin> atDay = new ObservableCollection<Termin>();
            ObservableCollection<Termin> atMonth = new ObservableCollection<Termin>();
            //hier suche ich alle die Termine die Anfangdatum als mein eingegebenes Datum haben!
            foreach (var item in terminResponse)
            {
                //Suche nur die Termine die für meine Gruppe markiert sind oder mir zugewissen wurden
                if ((item.Str_ErzeugtFuerGruppe == Benutzer.MyCurrentUser.Str_MitgliedVon) || (item.Str_ErzeugtFuerGruppe == Benutzer.MyCurrentUser.Str_Name))
                {
                    if (year == item.dt_Beginn.Year)
                    {
                        if (month == item.Dt_Beginn.Month)
                        {
                            if (day == item.Dt_Beginn.Day)
                            {
                                //if (hour == item.Dt_Beginn.Hour)
                                //{
                                //    //Speichere die Termine auf dieser Zeituhr
                                //    atHour.Add(item);
                                //}
                                //Speichere die Termine auf diesen Tag
                                atDay.Add(item);
                            }
                            //Speichere die Termine auf diesen Monat
                            atMonth.Add(item);
                        }
                    }
                }                   
            }
            myCurrentGroupTasksAtHour = atHour;
            myCurrentGroupTasksAtDay = atDay;
            myCurrentGroupTasksAtMonth = atMonth;
        }

        public static ObservableCollection<Termin> get_myCurrentGroupTasksAtMonth()
        {          
            return myCurrentGroupTasksAtMonth;
        }

        public static ObservableCollection<Termin> get_myCurrentGroupTasksAtDay()
        {      
            return myCurrentGroupTasksAtDay;
        }

        //Hier hole die Termine meiner Gruppe
        public static ObservableCollection<Termin> getGruppeTermineAmTag(int year, int month, int day)
        {
            findeDieAufgabenfürYMDH(year,month, day);
            ObservableCollection<Termin> grpKalenderTermineAmTag = new ObservableCollection<Termin>();
            foreach (var item in myCurrentGroupTasksAtDay)
            {
                if (item.enum_KalenderAuswahl == Termin.Enum_KalenderAuswahl.Gruppenansicht)
                {
                    grpKalenderTermineAmTag.Add(item);
                }
            }
            return grpKalenderTermineAmTag;
        }

        public static ObservableCollection<Termin> getPrivateTermineAmTag(int year, int month, int day)
        {
            findeDieAufgabenfürYMDH(year, month, day);
            ObservableCollection<Termin> indvKalenderTermineAmTag = new ObservableCollection<Termin>();
            foreach (var item in myCurrentGroupTasksAtDay)
            {
                if ((item.enum_KalenderAuswahl == Termin.Enum_KalenderAuswahl.Individualansicht) && (item.Str_BenutzerId == Benutzer.MyCurrentUser.Str_Name))
                {
                    indvKalenderTermineAmTag.Add(item);
                }
            }
            return indvKalenderTermineAmTag;
        }        


    }

}
