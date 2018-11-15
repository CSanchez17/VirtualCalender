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
    public class Benutzer
    {
        //-------------------------------------------------------------------------------------------------------//
        //Erzeuge Collection für die ganze Benutzern im System (Datenbank)
        //Hier sind die ganze Termine enthalten
        private static ObservableCollection<Benutzer> benutzerResponse = new ObservableCollection<Benutzer>();
        //hier ist die zugehörige Eigenschaft (Get und Set)
        //damit man Zugriff auf das Element hat
        public static ObservableCollection<Benutzer> BenutzerResponse
        {
            get { return benutzerResponse; }
            set { benutzerResponse = value; }
        }
        //-------------------------------------------------------------------------------------------------------//
        //hier wird das aktuelle Benutzer gespeichert
        private static Benutzer myCurrentUser = new Benutzer();
        public static Benutzer MyCurrentUser
        {
            get { return myCurrentUser; }
            set { myCurrentUser = value; }
        }

        //------------------------------------------------------------------------------------------------------//
        //Ab hier kommen die ganze Elemente der Klasse: element mit den zugehörigen Eigenschaft get und set

        private string str_Name;
        public string Str_Name
        {
            get { return str_Name; }
            set { str_Name = value; }
        }

        private DateTime dt_GeburtsDatum;
        public DateTime Dt_GeburtsDatum
        {
            get { return dt_GeburtsDatum; }
            set { dt_GeburtsDatum = value; }
        }

        private string str_Email;
        public string Str_Email
        {
            get { return str_Email; }
            set { str_Email = value; }
        }

        private string str_Password;
        public string Str_Password
        {
            get { return str_Password; }
            set { str_Password = value; }
        }

       
        private string str_Farbe;
        public string Str_Farbe
        {
            get { return str_Farbe; }
            set { str_Farbe = value; }
        }

        private char char_Initials;
        public char Char_Initials
        {
            get { return char_Initials; }
            set { char_Initials = value; }
        }

        private Ellipse myEllipse = new Ellipse();
        public Ellipse MyEllipse
        {
            get { return myEllipse; }
            set { myEllipse = value; }
        }
        // Key
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        // 
        private bool bool_IsAdmin;
        public bool Bool_IsAdmin
        {
            get { return bool_IsAdmin; }
            set { bool_IsAdmin = value; }
        }
        // 
        private bool bool_EinladungliegtVor;
        public bool Bool_EinladungliegtVor
        {
            get { return bool_EinladungliegtVor; }
            set { bool_EinladungliegtVor = value; }
        }

        private int int_MitgliedVon;
        public int Int_MitgliedVon
        {
            get { return int_MitgliedVon; }
            set { int_MitgliedVon = value; }
        }

        private string str_MitgliedVon;
        public string Str_MitgliedVon
        {
            get { return str_MitgliedVon; }
            set { str_MitgliedVon = value; }
        }

        //-------------------------------------------------------------------------------------------------------//
        //Konstruktoren     
        public Benutzer(string Str_Name)
        {
            str_Password = SetzeZufallPassword();
        }

        public Benutzer()
        {
            //Im defekt ist nur Admin wenn der eine Gruppe erstellt
            bool_IsAdmin = false;
            str_Farbe = "Keine Farbe";
            int_MitgliedVon = 0;
            str_MitgliedVon = "Null";
            str_Password = SetzeZufallPassword();
            //Decode the Input String:

            //          
        }
               
        //Die Funktion sucheVerantwoertlich findet das Email eines Benutzers in einem Collection von Benutzern, anhand der Name
        public static Benutzer sucheVerantwoertlichMitName(string Str_BenutzerAuswahl, ref bool flag)
        {
            //Suche in jeder Benutzer des Arrays benutzerResponse
            //erzeuge mein gesuchtes Element benutzer
            Benutzer benutzer = new Benutzer();
            //foreach Schleife läuft durch jedes Element in der Collection
            //der benutzt item als ein Element der Collection, also item hier ist ein benutzer
            foreach (var item in benutzerResponse)
            {
                if (item.Str_Name == Str_BenutzerAuswahl)
                {
                    //da item auch ein benutzer ist, darf man das hier machen
                    benutzer = item;
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
            return benutzer;
        }

        //Die Funktion sucheVerantwoertlich findet das Email eines Benutzers in einem Collection von Benutzern, anhand der ID
        public static Benutzer sucheVerantwoertlichMitId(int Int_BenutzerID)
        {
            //Erzeige mein gesuchtes Element benutzer
            Benutzer benutzer = new Benutzer();
            foreach (var item in benutzerResponse)
            {
                if (item.Id == Int_BenutzerID)
                {
                    benutzer = item;
                    break;
                }
            }
            return benutzer;
        }

        public static List<string> getListeVonBenutzernNamenDieKeineGruppeHaben()
        {
            List<string> NamenVonBenutzern = new List<string>();
            //Hier suche den Name der schon registrierten Benutzern
            foreach (var item in BenutzerResponse)
            {
                //Begrenze die Liste auf nur die Leute die ín meiner Gruppe ist
                if (item.Str_MitgliedVon == item.Str_Name)
                {
                    NamenVonBenutzern.Add(item.Str_Name);
                }
            }
            return NamenVonBenutzern;
        }

        public static List<string> getListeVonMeinerMitbewohnern()
        {
            List<string> NamenVonBenutzern = new List<string>();
            //Hier suche den Name der schon registrierten Benutzern
            foreach (var item in BenutzerResponse)
            {
                //Begrenze die Liste auf nur die Leute die ín meiner Gruppe ist
                if (item.Str_MitgliedVon == MyCurrentUser.Str_MitgliedVon)
                {
                    NamenVonBenutzern.Add(item.Str_Name);
                }
            }
            return NamenVonBenutzern;
        }

        //Die Funktion getBenutzerResponse holt sich die ganze Benutzern die im Datenbank sind
        //mit hilfe der unter Funktion App.VIEW_MODEL.GetBenutzer()
        public static ObservableCollection<Benutzer> getBenutzerResponse()
        {
            //hole die Daten aus dem Datenbank
            benutzerResponse = new ObservableCollection<Benutzer>(App.VIEW_MODEL.GetBenutzer());
            //gebe die Collection zurück
            return BenutzerResponse;
        }

        public void BenutzerErzeugen()
        {
            //Erzeuge einen neuen Benutzer im DB
            App.VIEW_MODEL.InsertNewUser(this);
           
            //Check ob der Benutzer tatsächlich im DB angelegt wurde
            //Update Liste von Benutzern
            getBenutzerResponse();

            //Setze ein Falg : wird true, wenn der Benutzer gefunden wurde!
            bool flag = false;
            //Suche den Benutzer durch den angegebenen Namen
            myCurrentUser = sucheVerantwoertlichMitName(this.str_Name, ref flag);

            if (flag)
            {
                //Setze den Betreff
                string Betreff = "Sie haben sich bei VirtualCalender erfolgreich angemeldet!: ٩(●̮̃•)۶ \n";
                string Messag = "\n " + Betreff + "\nName: " + myCurrentUser.Str_Name + "\nPassword:  " + myCurrentUser.Str_Password + "\nFarbe: " + myCurrentUser.Str_Farbe + "\nId: " + myCurrentUser.Id + "\nMitglied von: " + myCurrentUser.Str_MitgliedVon + "\n\n";

                //schicke Email an den Benutzern mit dem neuen Daten
                nachrichtSchicken(myCurrentUser, Betreff, Messag);
            }     

        }

        private void nachrichtSchicken(Benutzer benutzer, string Betreff, string Message)
        {
            //Der Benutzer soll Meldung nach der Anmeldung            
            //schicke Email:
            Model.EmailVerarbeitung.EmailSendenBenutzerDaten(benutzer, Betreff, Message);
        }

        public void BenutzerUpdaten()
        {
            //Update Termin
            App.VIEW_MODEL.UpdateBenutzer_Eingaben(this, MyCurrentUser.Id);
            string Betreff = "Sie haben ihre Daten geändert! ٩(●̮̃•)۶: \n";
            string Messag = "\n " + Betreff + "\nName: " + this.Str_Name + "\nPassword:  " + this.Str_Password + "\nFarbe: " + this.Str_Farbe + "\nId: " + this.Id + "\nMitglied von: " + this.Str_MitgliedVon + "\n\n";
            

            //schicke Email an den verantwörtlichen
            nachrichtSchicken(this, Betreff, Messag);

        }

        public void UpdateGruppeMitgliedschafftVomBenutzer()
        {
            //Update BenutzerMitgliedschafft im DB
            App.VIEW_MODEL.UpdateGruppeMitgliedschafftVomBenutzer(this);

            //Update BenutzerMitgliedschafft Local
            getBenutzerResponse();

        }

        public void InGruppeAnmelden()
        {
            //Update BenutzerMitgliedschafft im DB
            App.VIEW_MODEL.GruppeDerBenutzerUpdaten(this);

            //Update BenutzerMitgliedschafft Local
            getBenutzerResponse();

        }

        public void UpdateAdminPowerVomBenutzer()
        {
            //Update BenutzerMitgliedschafft im DB
            App.VIEW_MODEL.UpdateAdminPowerVomBenutzer(this);

            //Update BenutzerMitgliedschafft Local
            getBenutzerResponse();

        }

        public void DeleteGruppeMitgliedschafftVomBenutzer()
        {
            //Update BenutzerMitgliedschafft im DB
            App.VIEW_MODEL.DeleteGruppeMitgliedschafftVomBenutzer(this);

            //Update BenutzerMitgliedschafft Local
            getBenutzerResponse();

        }

        public void DeleteProfilVomBenutzer()
        {
            //Update BenutzerMitgliedschafft im DB
            App.VIEW_MODEL.DeleteBenutzer(this.Id);

            //Update BenutzenLocal
            getBenutzerResponse();

        }

        public static bool Login(string User, string Password)
        {
            bool flag = false;
            //Suche den Benutzer durch den angegebenen Namen
            myCurrentUser = sucheVerantwoertlichMitName(User, ref flag);
 
            if (flag)
            {
                //Benutzer gefunden, check password
                if (myCurrentUser.Str_Password == Password)
                {
                    //Login Erfolgreich
                    flag = true;
                    return flag;
                }
                else
                {
                    //Login erfolglos
                    return false;
                }
            }
            else
            {
                return flag;
            }
        }

        public bool RefreshCurrentUser()
        {
            bool flag = false;
            //Suche den Benutzer durch den angegebenen Namen
            myCurrentUser = sucheVerantwoertlichMitName(this.Str_Name, ref flag);

            if (flag)
            {
                //Benutzer gefunden, check password
                if (myCurrentUser.Str_Password == this.Str_Password)
                {
                    //Login Erfolgreich
                    flag = true;
                    return flag;
                }
                else
                {
                    //Login erfolglos
                    return false;
                }
            }
            else
            {
                return flag;
            }
        }

        public static bool GruppeLogin(string Email,string User, string Password)
        {
            bool flag = false;
            //Suche die Gruppe durch den angegebenen Namen
            myCurrentUser = sucheVerantwoertlichMitName(User, ref flag);

            if (flag)
            {
                //Benutzer gefunden, check password
                if (myCurrentUser.Str_Password == Password)
                {
                    //Login Erfolgreich
                    flag = true;
                    return flag;
                }
                else
                {
                    //Login erfolglos
                    return false;
                }
            }
            else
            {
                return flag;
            }
        }

        private string SetzeZufallPassword()
        {
            string ret = string.Empty;
            System.Text.StringBuilder SB = new System.Text.StringBuilder();
            string Content = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvw!öäüÖÄÜß\"§$%&/()=?*#-";
            int Länge = 8;

            Random rnd = new Random();
            for (int i = 0; i < Länge; i++)
                SB.Append(Content[rnd.Next(Content.Length)]);
            return SB.ToString();
        }
    }
}
