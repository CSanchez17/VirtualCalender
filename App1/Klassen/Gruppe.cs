using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;

namespace VirtualCalendarV01.Klassen
{
    public class Gruppe
    {
        //-------------------------------------------------------------------------------------------------------//
        //Erzeuge Collection für die ganze Gruppe im System (Datenbank)
        private static ObservableCollection<Gruppe> gruppeResponse = new ObservableCollection<Gruppe>();
        //hier ist die zugehörige Eigenschaft (Get und Set)
        //damit man Zugriff auf das Element hat
        public static ObservableCollection<Gruppe> GruppeResponse
        {
            get { return gruppeResponse; }
            set { gruppeResponse = value; }
        }
        //-------------------------------------------------------------------------------------------------------//
        //hier wird das aktuelle Benutzer gespeichert
        private static Gruppe myCurrentGruppe = new Gruppe();
        public static Gruppe MyCurrentGruppe
        {
            get { return myCurrentGruppe; }
            set { myCurrentGruppe = value; }
        }
        //Wie viele Leute:      //******
        //Diese Liste soll begrenzt werden zu Max_BenutzerAnzahl
        //Der erste Benutzer in der Liste ist der Admin
        private ObservableCollection<Benutzer> list_Benutzer = new ObservableCollection<Benutzer>();
        public ObservableCollection<Benutzer> List_Benutzer
        {
            get { return list_Benutzer; }
            set { list_Benutzer = value; }
        }
        //Aufgaben             //******
        private ObservableCollection<Termin> list_Termine = new ObservableCollection<Termin>();
        public ObservableCollection<Termin> List_Termine
        {
            get { return list_Termine; }
            set { list_Termine = value; }
        }

        private string str_GruppenName;
        public string Str_GruppenName
        {
            get { return str_GruppenName; }
            set { str_GruppenName = value; }
        }

        private string str_GruppenPassword;
        public string Str_GruppenPasword
        {
            get { return str_GruppenPassword; }
            set { str_GruppenPassword = value; }
        }

        public const int Max_BenutzerAnzahl = 8;

        private int int_AdminId;
        public int Int_AdminId
        {
            get { return int_AdminId; }
            set { int_AdminId = value; }
        }

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int int_MaxBenutzerAnzahl;
        public int Int_MaxBenutzerAnzahl
        {
            get { return int_MaxBenutzerAnzahl; }
            set { int_MaxBenutzerAnzahl = value; }
        }


        private static List<SolidColorBrush> meineFarben = new List<SolidColorBrush>();        
        public List<SolidColorBrush> MeineFarben
        {
            get { return meineFarben; }
            set { meineFarben = value; }
        }


        public Gruppe()
        {
          
        }

        private void ManageBenutzerEigenschaften(ObservableCollection<Benutzer> benutzer_)
        {

            // Create a SolidColorBrush with a red color to fill the             
            int FarbeIndex = 1;
            foreach (var item in benutzer_)
            {
                //Setze den Namen jeweils zu den Benutzern
                item.Str_MitgliedVon = this.Str_GruppenName;
                // Fill rectangle with blue color
                SolidColorBrush FarbeBrush = new SolidColorBrush();
                switch (FarbeIndex)
                {
                    case 1:
                        //Sezte Farbe im SolidColorBrush
                        FarbeBrush.Color = Colors.Blue;
                        //Setze Farbe in der Elipse
                        item.MyEllipse.Fill = FarbeBrush;
                        //Setze Farbe als String für den Benutzer
                        item.Str_Farbe = "Blue";
                        break;
                    case 2:
                        FarbeBrush.Color = Colors.Yellow;
                        item.MyEllipse.Fill = FarbeBrush;
                        item.Str_Farbe = "Yellow";
                        break;
                    case 3:
                        FarbeBrush.Color = Colors.Red;
                        item.MyEllipse.Fill = FarbeBrush;
                        item.Str_Farbe = "Red";
                        break;
                    case 4:
                        FarbeBrush.Color = Colors.LightGreen;
                        item.MyEllipse.Fill = FarbeBrush;
                        item.Str_Farbe = "LightGreen";
                        break;
                    case 5:
                        FarbeBrush.Color = Colors.Plum;
                        item.MyEllipse.Fill = FarbeBrush;
                        item.Str_Farbe = "Plum";
                        break;
                    case 6:
                        FarbeBrush.Color = Colors.Cyan;
                        item.MyEllipse.Fill = FarbeBrush;
                        item.Str_Farbe = "Cyan";
                        break;
                    case 7:
                        FarbeBrush.Color = Colors.Orange;
                        item.MyEllipse.Fill = FarbeBrush;
                        item.Str_Farbe = "Orange";
                        break;
                    case 8:
                        FarbeBrush.Color = Colors.Purple;
                        item.MyEllipse.Fill = FarbeBrush;
                        item.Str_Farbe = "Purple";
                        break;
                    default:
                        break;
                }
                //Update Mitgliedschaft im Datenbank
                item.UpdateGruppeMitgliedschafftVomBenutzer();
                //Schicke Email
                schickeMeldungPerEmail(item);
                FarbeIndex++;
            }
        }
        public static SolidColorBrush setzeFarbeMitName(string name)
       {
            SolidColorBrush FarbeBrush = new SolidColorBrush();
            FarbeBrush.Color = Colors.Black;
            switch (name)
            {
                case "Blue":
                    //Sezte Farbe im SolidColorBrush
                    FarbeBrush.Color = Colors.Blue;
                    return FarbeBrush;
                case "Purple":
                    //Sezte Farbe im SolidColorBrush
                    FarbeBrush.Color = Colors.Purple;
                    return FarbeBrush;
                case "Red":
                    //Sezte Farbe im SolidColorBrush
                    FarbeBrush.Color = Colors.Red;
                    return FarbeBrush;
                case "Yellow":
                    //Sezte Farbe im SolidColorBrush
                    FarbeBrush.Color = Colors.Yellow;
                    return FarbeBrush;
                case "LightGreen":
                    //Sezte Farbe im SolidColorBrush
                    FarbeBrush.Color = Colors.LightGreen;
                    return FarbeBrush;
                case "Plum":
                    //Sezte Farbe im SolidColorBrush
                    FarbeBrush.Color = Colors.Plum;
                    return FarbeBrush;
                case "Orange":
                    //Sezte Farbe im SolidColorBrush
                    FarbeBrush.Color = Colors.Orange;
                    return FarbeBrush;
                case "Cyan":
                    //Sezte Farbe im SolidColorBrush
                    FarbeBrush.Color = Colors.Cyan;
                    return FarbeBrush;
                default:
                    return FarbeBrush; 
            }
        }

        public Gruppe(string GruppenName, string GruppenPassword, ObservableCollection<Benutzer> Teilnehmer, int AdminId)
        {
            Str_GruppenName = GruppenName;
            Str_GruppenPasword = GruppenPassword;
            list_Benutzer = Teilnehmer;
            Int_AdminId = AdminId;
            //setze die Änderungen im Benutzer
            ManageBenutzerEigenschaften(this.list_Benutzer);

        }

        private void schickeMeldungPerEmail(Benutzer benutzer)
        {
            //Define Heading für das Email
            string Betreff = "Sie haben eine Einladung fuer eine Virtual Calendar Gruppe erhalten! ٩(●̮̃•)۶: \n";
            string Message = "\nVc Kalendergruppe: " + Str_GruppenName + "\nDas Gruppenpasswort lautet: " + str_GruppenPassword ;

            //schicke Email an den verantwörtlichen
            Model.EmailVerarbeitung.EmailSendenBenutzerDaten(benutzer, Betreff, Message);
        }

        //Die Funktion getBenutzerResponse holt sich die ganze Benutzern die im Datenbank sind
        //mit hilfe der unter Funktion App.VIEW_MODEL.GetBenutzer()
        public static ObservableCollection<Gruppe> getGruppeResponse()
        {
            //hole die Daten aus dem Datenbank
            gruppeResponse = new ObservableCollection<Gruppe>(App.VIEW_MODEL.GetGruppe());
            //gebe die Collection zurück
            return GruppeResponse;
        }

        public void GruppeErzeugen()
        {
            //Erzeuge einen neuen Benutzer im DB
            App.VIEW_MODEL.InsertNewGruppe(this);

            //Check ob der Benutzer tatsächlich im DB angelegt wurde
            //Update Liste von Gruppe
            getGruppeResponse();

            //Setze ein Falg : wird true, wenn der Benutzer gefunden wurde!
            bool flag = false;
            //Suche den Benutzer durch den angegebenen Namen
            myCurrentGruppe = sucheGruppe(this.str_GruppenName, ref flag);

            //Trifft zu nur, wenn die Gruppe erzeugt wurde!
            if (flag)
            {
                //Setze den Betreff
                string Betreff = "Sie haben die Gruppenkalender: "+ myCurrentGruppe.str_GruppenName +" erfolgreich erstellt!: ٩(●̮̃•)۶ \n";

                //schicke Email an den Benutzern mit dem neuen Daten
                //nachrichtSchicken(Banutzer.myCurrentUser, Betreff);

                //Erzeuge Meldung:
                MeldungErzeugen(Betreff);
            }

        }

     
        public static Gruppe sucheGruppe(string _Str_GruppenName, ref bool flag)
        {
            //Suche in jeder Benutzer des Arrays benutzerResponse
            //erzeuge mein gesuchtes Element benutzer
            Gruppe gruppe = new Gruppe();
            //foreach Schleife läuft durch jedes Element in der Collection
            //der benutzt item als ein Element der Collection, also item hier ist ein benutzer
            foreach (var item in gruppeResponse)
            {
                if (item.str_GruppenName == _Str_GruppenName)
                {
                    //da item auch eine Gruppe ist, darf man das hier machen
                    gruppe = item;
                    flag = true;
                    break;
                }
                else
                {
                    //Gruppe nicht gefunden
                    //Setze flag auf false
                    //damit Ankunft, dass der Benutzer nicht da ist.
                    flag = false;
                }
            }
            return gruppe;
        }

        public void SucheMitgliederDerGruppe()
        {            
            foreach (var item in Benutzer.BenutzerResponse)
            {
                if (this.Str_GruppenName == item.Str_MitgliedVon)
                {
                    this.list_Benutzer.Add(item);
                }
            }
        }

        public void SucheTermineDerGruppe()
        {           
            foreach (var item1 in Termin.TerminResponse)
            {
                foreach (var item2 in this.List_Benutzer)
                {
                    if (item1.Str_BenutzerId == item2.Str_Name)
                    {
                        if (item1.enum_KalenderAuswahl == Termin.Enum_KalenderAuswahl.Gruppenansicht)
                        {
                            this.List_Termine.Add(item1);
                        }
                    }
                }

            }
        }

      
        public static async void MeldungErzeugen(string Nachricht)
        {            
            var message = new MessageDialog(Nachricht);
            await message.ShowAsync();            
        }

        public static List<string> findeCharachtersDerBenutzers()
        {
            List<string> CharachtersDerMitgliedern = new List<string>();
            for (int i = 0; i < 8; i++)
            {
                string initials;
                if (i >= MyCurrentGruppe.List_Benutzer.Count)
                {
                    //hole die erste 2 Strings
                    initials = "";
                }
                else
                {
                    initials = MyCurrentGruppe.List_Benutzer[i].Str_Name.Substring(0, 2); 
                }
                CharachtersDerMitgliedern.Add(initials);
            }
            return CharachtersDerMitgliedern;
        }

        public void DeleteGruppe()
        {
            //Update BenutzerMitgliedschafft im DB
            App.VIEW_MODEL.DeleteGruppe(this.Id);

            //Update BenutzenLocal
            Gruppe.getGruppeResponse();

        }
    }
}
