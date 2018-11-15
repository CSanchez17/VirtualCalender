using LightBuzz.SMTP;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualCalendarV01.Klassen;
using Windows.ApplicationModel.Email;
using Windows.UI.Popups;

namespace VirtualCalendarV01.Model
{
    public class ViewModel
    {
        //-----------------------------------------------------------------------------------------------//
        //Definiere eine globale Instanz der ViewModel
        private static ViewModel MyViewModel = new ViewModel();
        //-----------------------------------------------------------------------------------------------//
        //Definiere eine Globale Variable für die Verbindung mit dem Server:
        //MySqlConnection connection = new MySqlConnection("server=RFHINF528;user id=MyUser;password=1234;persistsecurityinfo=True;port=3306;database=virtual_calendar;SslMode=None")
        //MySqlConnection connection = new MySqlConnection("server=localhost;user id=Cristian;password=EstaEsMiPass123;persistsecurityinfo=True;port=3306;database=virtual_calendar;SslMode=None")
 //       private MySqlConnection MysqlConnectionVm = new MySqlConnection("server=RFHINF528;user id=MyUser;password=1234;persistsecurityinfo=True;port=3306;database=virtual_calendar;SslMode=None");
        private MySqlConnection MysqlConnectionLocal = new MySqlConnection("server=localhost;user id=Cristian;password=EstaEsMiPass123;persistsecurityinfo=True;port=3306;database=virtual_calendar;SslMode=None");

        //-----------------------------------------------------------------------------------------------//
        //Definiere eine Collection für die Termine, Benutzer und Gruppe
        private ObservableCollection<Termin> _alleTermine = new ObservableCollection<Termin>();
        private ObservableCollection<Benutzer> _alleBenutzer = new ObservableCollection<Benutzer>();
        private ObservableCollection<Gruppe> _alleGruppe = new ObservableCollection<Gruppe>();
        //-----------------------------------------------------------------------------------------------//
        //Die hier sind die Properties oder Eigenschaften (Ähnlich wie Methoden/Funktionen aber nicht ganz)
        //Definiere 3 Funktionen die jeweils meine Termine, Benutzern und Gruppe zurückgeben
        //Achtung, hier wird explizit nur die Bezeichnung des Termines zurückgegeben!
        public ObservableCollection<Termin> AlleTermine
        {
            get { return MyViewModel._alleTermine; }
        }
        public ObservableCollection<Benutzer> AlleBenutzer
        {
            get { return MyViewModel._alleBenutzer; }
        }
        public ObservableCollection<Gruppe> AlleGruppe
        {
            get { return MyViewModel._alleGruppe; }
        }

        //-----------------------------------------------------------------------------------------------//       
        //Definiere eine Enumerable Collection für die Gruppe
        public IEnumerable<Termin> GetTermine()
        {
            try
            {
                using (MysqlConnectionVm)
                {
                    MysqlConnectionVm.Open();
                    MySqlCommand getCommand = MysqlConnectionVm.CreateCommand();

                    //Hier wird die ganze Spalte Str_Bezeichnung ausgewählt
                    getCommand.CommandText = "SELECT * FROM virtual_calendar.termin";
                    using (MySqlDataReader reader = getCommand.ExecuteReader())
                    {
                        //Temporäre Collection Tmp_alleTermine, hier werden die Termine vom DB gesammelt
                        ObservableCollection<Termin> Tmp_alleTermine = new ObservableCollection<Termin>();
                        while (reader.Read())
                        {
                            Termin _termin = new Termin();
                            _termin.Int_BenutzerId = reader.GetInt32("Int_BenutzerId");
                            _termin.Dt_Beginn = reader.GetDateTime("Dt_Beginn");
                            _termin.Dt_Ende = reader.GetDateTime("Dt_Ende");
                            _termin.enum_KalenderAuswahl = findeKalenderAuswahl(reader.GetString("Enum_KalenderAuswahl"));
                            _termin.enum_WiederholungsZyklus = findeWiederhlZyklus(reader.GetString("Enum_WiederholungsZyklus"));
                            _termin.Str_TerminBeschreibung = reader.GetString("Str_TerminBeschreibung");
                            _termin.Str_Bezeichnung = reader.GetString("Str_Bezeichnung");
                            _termin.Id =reader.GetInt32("Id");
                            _termin.Str_BenutzerId = reader.GetString("Str_BenutzerId");
                            _termin.Str_ErzeugtFuerGruppe = reader.GetString("Str_ErzeugtFuerGruppe");
                            //Setzte Farbe
                            _termin.Str_Farbe = reader.GetString("Str_Farbe");
                            _termin.MyEllipse.Fill = Gruppe.setzeFarbeMitName(_termin.Str_Farbe);
                            Tmp_alleTermine.Add(_termin);
                        }
                        //Nachdem die Benutzern eingesammelt wurden, dann werden die in der globalen Variable _alleBenutzer gespeichert
                        MyViewModel._alleTermine = Tmp_alleTermine;
                    }
                }
            }
            catch (MySqlException ex)
            {
                showExeption(ex);
            }
            return MyViewModel.AlleTermine;
        }

        public IEnumerable<Benutzer> GetBenutzer()
        {
            try
            {
                using (MysqlConnectionVm)
                {
                    MysqlConnectionVm.Open();
                    MySqlCommand getCommand = MysqlConnectionVm.CreateCommand();

                    //Hier wird die ganze Spalte Str_Bezeichnung ausgewählt
                    getCommand.CommandText = "SELECT * FROM virtual_calendar.benutzer";
                    using (MySqlDataReader reader = getCommand.ExecuteReader())
                    {
                        //Temporäre Collection Tmp_alleBenutzer, hier werden die Benutzern vom DB gesammelt
                        ObservableCollection<Benutzer> Tmp_alleBenutzer = new ObservableCollection<Benutzer>();
                        while (reader.Read())
                        {
                            Benutzer benutzer = new Benutzer();
                            benutzer.Str_Name = reader.GetString("Str_Name");
                            benutzer.Id = reader.GetInt32("Id");
                            benutzer.Str_Email = reader.GetString("Str_Email");
                            benutzer.Str_Password = reader.GetString("Str_Password");
                            benutzer.Bool_IsAdmin = reader.GetBoolean("Enum_IsAdmin");
                            benutzer.Bool_EinladungliegtVor = reader.GetBoolean("Enuml_EinladungliegtVor");
                            benutzer.Dt_GeburtsDatum = reader.GetDateTime("Dt_GeburtsDatum");
                            benutzer.Str_MitgliedVon = reader.GetString("Str_MitgliedVon");
                            //Setzte Farbe
                            benutzer.Str_Farbe = reader.GetString("Str_Farbe");
                            benutzer.MyEllipse.Fill = Gruppe.setzeFarbeMitName(benutzer.Str_Farbe);
                            Tmp_alleBenutzer.Add(benutzer);
                        }
                        //Nachdem die Benutzern eingesammelt wurden, dann werden die in der globalen Variable _alleBenutzer gespeichert
                        //damit wird es sichergestellt, dass keine Daten doppelt gelesen werden
                        MyViewModel._alleBenutzer = Tmp_alleBenutzer;
                    }
                }
            }
            catch (MySqlException ex)
            {
                showExeption(ex);
            }
            return MyViewModel.AlleBenutzer;
        }

        public IEnumerable<Gruppe> GetGruppe()
        {
            try
            {
                using (MysqlConnectionVm)
                {
                    MysqlConnectionVm.Open();
                    MySqlCommand getCommand = MysqlConnectionVm.CreateCommand();

                    //Hier wird die ganze Spalte Str_Bezeichnung ausgewählt
                    getCommand.CommandText = "SELECT * FROM virtual_calendar.gruppe";
                    using (MySqlDataReader reader = getCommand.ExecuteReader())
                    {            
                        //Temporäre Collection Tmp_alleBenutzer, hier werden die Benutzern vom DB gesammelt
                        ObservableCollection<Gruppe> Tmp_alleGruppe = new ObservableCollection<Gruppe>();                       
                        while (reader.Read())
                        {
                            Gruppe gruppe = new Gruppe();
                            gruppe.Str_GruppenName = reader.GetString("Str_GruppenName");
                            gruppe.Str_GruppenPasword = reader.GetString("Str_GruppenPassword");
                            gruppe.Int_AdminId = reader.GetInt32("Int_AdminId");
                            gruppe.Id = reader.GetInt32("Id");
                            gruppe.Int_MaxBenutzerAnzahl = reader.GetInt32("Int_MaxBenutzerAnzahl");
                            //Suche die jeweilige Benutzer in der Gruppe
                            gruppe.SucheMitgliederDerGruppe();
                            gruppe.SucheTermineDerGruppe();
                            Tmp_alleGruppe.Add(gruppe);
                        }                        
                        //Setze meine 
                        MyViewModel._alleGruppe = Tmp_alleGruppe;
                    }
                }
            }
            catch (MySqlException ex)
            {
                showExeption(ex);
            }
            return MyViewModel.AlleGruppe;
        }

      

        public bool InsertNewTermin(Termin NewTermin)
        {
            //Termin newTermin = new Termin(NewTermin);
            // Insert to the collection and update DB
            //NewTermin.Benutzer;
            try
            {
                //using (MySqlConnection connection = new MySqlConnection("server=RFHINF528;user id=MyUser;password=1234;persistsecurityinfo=True;port=3306;database=virtual_calendar;SslMode=None"))
                using (MysqlConnectionVm)
                {
                    MysqlConnectionVm.Open();
                    string Str_InserIntoString = "INSERT INTO virtual_calendar.termin (Enum_KalenderAuswahl,Str_Bezeichnung, Dt_Beginn, Dt_Ende, Str_TerminBeschreibung, Enum_WiederholungsZyklus, Int_BenutzerId, Str_BenutzerId, Str_Farbe, Str_ErzeugtFuerGruppe)";
                    string Str_ValuesString = " VALUES (@Enum_KalenderAuswahl,@Str_Bezeichnung, @Dt_Beginn, @Dt_Ende, @Str_TerminBeschreibung, @Enum_WiederholungsZyklus, @Int_BenutzerId, @Str_BenutzerId, @Str_Farbe, @Str_ErzeugtFuerGruppe)";

                    MySqlCommand insertCommand = MysqlConnectionVm.CreateCommand();
                    insertCommand.CommandText = Str_InserIntoString + Str_ValuesString;
                    insertCommand.Parameters.AddWithValue("@Enum_KalenderAuswahl", strVonEnumKalenderAuswahl(NewTermin.enum_KalenderAuswahl));
                    insertCommand.Parameters.AddWithValue("@Str_Bezeichnung", NewTermin.Str_Bezeichnung);
                    insertCommand.Parameters.AddWithValue("@Dt_Beginn", NewTermin.Dt_Beginn);
                    insertCommand.Parameters.AddWithValue("@Dt_Ende", NewTermin.Dt_Ende);
                    insertCommand.Parameters.AddWithValue("@Str_TerminBeschreibung", NewTermin.Str_TerminBeschreibung);
                    insertCommand.Parameters.AddWithValue("@Enum_WiederholungsZyklus", strVonEnumWiederhlZyklus(NewTermin.enum_WiederholungsZyklus));
                    insertCommand.Parameters.AddWithValue("@Int_BenutzerId", NewTermin.Int_BenutzerId);
                    insertCommand.Parameters.AddWithValue("@Str_BenutzerId", NewTermin.Str_BenutzerId);
                    insertCommand.Parameters.AddWithValue("@Str_Farbe", NewTermin.Str_Farbe);
                    if (NewTermin.enum_KalenderAuswahl == Termin.Enum_KalenderAuswahl.Individualansicht)
                    {
                        //Wenn der Termin privat ist, dann erzeuge es in meiner Name als Gruppe
                        insertCommand.Parameters.AddWithValue("@Str_ErzeugtFuerGruppe", NewTermin.Str_BenutzerId);
                    }
                    else if (NewTermin.enum_KalenderAuswahl == Termin.Enum_KalenderAuswahl.Gruppenansicht)
                    {
                        insertCommand.Parameters.AddWithValue("@Str_ErzeugtFuerGruppe", NewTermin.Str_ErzeugtFuerGruppe);
                    }
                    insertCommand.ExecuteNonQuery();
                    MyViewModel._alleTermine.Add(NewTermin);
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                showExeption(ex);
                return false;
            }
        }

        public bool DeleteTermin(int Int_IdTermin)
        {
            try
            {
                using (MysqlConnectionVm)
                {
                    MysqlConnectionVm.Open();
                    MySqlCommand getCommand = MysqlConnectionVm.CreateCommand();

                    string Str_Command = "DELETE FROM virtual_calendar.termin";
                    string Str_CommandValue = " Where Id =" + Int_IdTermin;
                    getCommand.CommandText = Str_Command + Str_CommandValue;
                    using (MySqlDataReader reader = getCommand.ExecuteReader())
                    {
                        reader.Read();
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Behandlung
                showExeption(ex);
                return false;
            }
            return true;
        }

        public bool UpdateTermin(Termin UpToDateTermin)
        {
            // Insert to the collection and update DB
            try
            {
                using (MysqlConnectionVm)
                {
                    MysqlConnectionVm.Open();
                    string StrVal1 = Convert.ToString(UpToDateTermin.enum_KalenderAuswahl);
                    string StrVal2 = UpToDateTermin.Str_Bezeichnung;
                    string StrVal3 = UpToDateTermin.Dt_Beginn.ToString("yyyy-MM-dd HH:mm:ss");
                    string StrVal4 = UpToDateTermin.Dt_Ende.ToString("yyyy-MM-dd HH:mm:ss");
                    string StrVal5 = UpToDateTermin.Str_TerminBeschreibung;
                    string StrVal6 = Convert.ToString(UpToDateTermin.enum_WiederholungsZyklus);
                    string StrVal18 = UpToDateTermin.Str_ErzeugtFuerGruppe;
                    int StrVal7 = UpToDateTermin.Int_BenutzerId;
                    int StrVal8 = UpToDateTermin.Id;


                    string Str_InserIntoString = "UPDATE virtual_calendar.termin";
                    string Str_ValuesString = " SET Enum_KalenderAuswahl ='" + StrVal1 + "', Str_Bezeichnung = '" + StrVal2 + "', Dt_Beginn = '" + StrVal3 + "', Dt_Ende = '" + StrVal4 + "', Str_TerminBeschreibung = '" + StrVal5 + "', Enum_WiederholungsZyklus = '" + StrVal6 + "', Int_BenutzerId = '" + StrVal7 + "', Str_ErzeugtFuerGruppe = '" + StrVal18+ "'";

                    string Str_WhereString = " WHERE Id =" + StrVal8;

                    MySqlCommand insertCommand = MysqlConnectionVm.CreateCommand();
                    insertCommand.CommandText = Str_InserIntoString + Str_ValuesString + Str_WhereString;

                    insertCommand.ExecuteNonQuery();
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                // Don't forget to handle it
                showExeption(ex);
                return false;
            }
        }

        public bool InsertNewUser(Benutzer benutzer)
        {
            // Insert to the collection and update DB
            try
            {
                using (MysqlConnectionVm)
                {
                    MysqlConnectionVm.Open();
                    string Str_InserIntoString = "INSERT INTO virtual_calendar.benutzer (Str_Name,Dt_GeburtsDatum, Str_Email, Str_Password, Enum_IsAdmin, Int_MitgliedVon, Str_MitgliedVon)";
                    string Str_ValuesString = " VALUES (@Str_Name,@Dt_GeburtsDatum, @Str_Email, @Str_Password, @Enum_IsAdmin, @Int_MitgliedVon, @Str_MitgliedVon)";
                    string admin = "False";
                    MySqlCommand insertCommand = MysqlConnectionVm.CreateCommand();
                    insertCommand.CommandText = Str_InserIntoString + Str_ValuesString;
                    insertCommand.Parameters.AddWithValue("@Str_Name", benutzer.Str_Name);
                    insertCommand.Parameters.AddWithValue("@Dt_GeburtsDatum", benutzer.Dt_GeburtsDatum);
                    insertCommand.Parameters.AddWithValue("@Str_Email", benutzer.Str_Email);
                    insertCommand.Parameters.AddWithValue("@Str_Password", benutzer.Str_Password);
                    insertCommand.Parameters.AddWithValue("@Enum_IsAdmin", admin);
                    insertCommand.Parameters.AddWithValue("@Int_MitgliedVon", benutzer.Int_MitgliedVon);
                    insertCommand.Parameters.AddWithValue("@Str_MitgliedVon", benutzer.Str_MitgliedVon);
                    insertCommand.ExecuteNonQuery();
                    //Update meine Collection mit dem neuen Benutzer
                    MyViewModel._alleBenutzer.Add(benutzer);
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                showExeption(ex);
                return false;
            }
        }

        public bool DeleteBenutzer(int Int_IBenutzer)
        {
            try
            {
                using (MysqlConnectionVm)
                {
                    MysqlConnectionVm.Open();
                    MySqlCommand getCommand = MysqlConnectionVm.CreateCommand();

                    string Str_Command = "DELETE FROM virtual_calendar.benutzer";
                    string Str_CommandValue = " Where Id =" + Int_IBenutzer;
                    //Hier wird die ganze Spalte Str_Bezeichnung ausgewählt
                    getCommand.CommandText = Str_Command + Str_CommandValue;
                    using (MySqlDataReader reader = getCommand.ExecuteReader())
                    {
                        reader.Read();
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Handle it 
                showExeption(ex);
                return false;
            }
            return true;
        }

        public bool UpdateBenutzer(Benutzer UpToDateBenutzer)
        {
            //Termin newTermin = new Termin(NewTermin);
            // Insert to the collection and update DB
            //NewTermin.Benutzer;
            try
            {
                using (MysqlConnectionVm)
                {
                    MysqlConnectionVm.Open();
                    string StrVal1 = UpToDateBenutzer.Str_Name;
                    string StrVal2 = UpToDateBenutzer.Dt_GeburtsDatum.ToString("yyyy-MM-dd");
                    string StrVal3 = UpToDateBenutzer.Str_Email;
                    string StrVal4 = UpToDateBenutzer.Str_Password;
                    string StrVal6 = UpToDateBenutzer.Bool_IsAdmin.ToString();
                    int intVal7 = UpToDateBenutzer.Int_MitgliedVon;
                    string StrVa8 = UpToDateBenutzer.Str_Farbe;
                    string StrVal9 = UpToDateBenutzer.Str_MitgliedVon;
                    int Id = UpToDateBenutzer.Id;

                    string Str_InserIntoString = "UPDATE virtual_calendar.benutzer";
                    string Str_ValuesString = " SET Str_Name ='" + StrVal1 + "', Dt_GeburtsDatum = '" + StrVal2 + "', Str_Email = '" + StrVal3 + "', Str_Password = '" + StrVal4 + "', Int_MitgliedVon = '" + intVal7 + "', Str_MitgliedVon = '" + StrVal9 + "', Str_Farbe = '"+ StrVa8 + "'";

                    string Str_WhereString = " WHERE Id =" + Id;

                    MySqlCommand insertCommand = MysqlConnectionVm.CreateCommand();
                    insertCommand.CommandText = Str_InserIntoString + Str_ValuesString + Str_WhereString;

                    insertCommand.ExecuteNonQuery();
                    return true;
                }
            }        
            catch (MySqlException ex)
            {
                // Don't forget to handle it
                showExeption(ex);
                return false;
            }
        }

        public bool UpdateBenutzer_Eingaben(Benutzer UpToDateBenutzer, int CurrentId)
        {
            // Insert to the collection and update DB
            try
            {
                using (MysqlConnectionVm)
                {
                    MysqlConnectionVm.Open();
                    string StrVal2 = UpToDateBenutzer.Dt_GeburtsDatum.ToString("yyyy-MM-dd");
                    string StrVal3 = UpToDateBenutzer.Str_Email;
                    string StrVal4 = UpToDateBenutzer.Str_Password;
                    int Id = CurrentId;

                    string Str_InserIntoString = "UPDATE virtual_calendar.benutzer";
                    string Str_ValuesString = " SET Dt_GeburtsDatum = '" + StrVal2 + "', Str_Email = '"
                                                + StrVal3 + "', Str_Password = '" + StrVal4 + "'";
                    string Str_WhereString = " WHERE Id =" + Id;

                    MySqlCommand insertCommand = MysqlConnectionVm.CreateCommand();
                    insertCommand.CommandText = Str_InserIntoString + Str_ValuesString + Str_WhereString;

                    insertCommand.ExecuteNonQuery();
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                // Don't forget to handle it
                showExeption(ex);
                return false;
            }
        }

        public bool InsertNewGruppe(Gruppe NewGruppe)
        {
            // Insert to the collection and update DB
            try
            {
                //using (MySqlConnection connection = new MySqlConnection("server=RFHINF528;user id=MyUser;password=1234;persistsecurityinfo=True;port=3306;database=virtual_calendar;SslMode=None"))
                using (MysqlConnectionVm)
                {
                    MysqlConnectionVm.Open();
                    string Str_InserIntoString = "INSERT INTO virtual_calendar.gruppe (Str_GruppenName,Str_GruppenPassword, Int_MaxBenutzerAnzahl, Int_AdminId)";
                    string Str_ValuesString = " VALUES (@Str_GruppenName,@Str_GruppenPassword, @Int_MaxBenutzerAnzahl, @Int_AdminId)";

                    MySqlCommand insertCommand = MysqlConnectionVm.CreateCommand();
                    insertCommand.CommandText = Str_InserIntoString + Str_ValuesString;
                    insertCommand.Parameters.AddWithValue("@Str_GruppenName", NewGruppe.Str_GruppenName);
                    insertCommand.Parameters.AddWithValue("@Str_GruppenPassword", NewGruppe.Str_GruppenPasword);
                    insertCommand.Parameters.AddWithValue("@Int_MaxBenutzerAnzahl", NewGruppe.Int_MaxBenutzerAnzahl);
                    insertCommand.Parameters.AddWithValue("@Int_AdminId", NewGruppe.Int_AdminId);
                    insertCommand.ExecuteNonQuery();
                    MyViewModel._alleGruppe.Add(NewGruppe);
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                showExeption(ex);
                return false;
            }
        }

        public bool DeleteGruppe(int Int_GruppeId)
        {
            try
            {
                using (MysqlConnectionVm)
                {
                    MysqlConnectionVm.Open();
                    MySqlCommand getCommand = MysqlConnectionVm.CreateCommand();

                    string Str_Command = "DELETE FROM virtual_calendar.gruppe";
                    string Str_CommandValue = " Where Id =" + Int_GruppeId;
                    //Hier wird die ganze Spalte Str_Bezeichnung ausgewählt
                    getCommand.CommandText = Str_Command + Str_CommandValue;
                    using (MySqlDataReader reader = getCommand.ExecuteReader())
                    {
                        reader.Read();

                    }
                }
            }
            catch (MySqlException ex)
            {
                // Handle it 
                showExeption(ex);
                return false;
            }
            return true;
        }

       

        public bool UpdateGruppe(Gruppe UpToDateGruppe)
        {
            // Insert to the collection and update DB
            try
            {
                using (MysqlConnectionVm)
                {
                    MysqlConnectionVm.Open();
                    string StrVal1 = UpToDateGruppe.Str_GruppenName;
                    string StrVal2 = UpToDateGruppe.Str_GruppenPasword;
                    int Id = UpToDateGruppe.Id;

                    string Str_InserIntoString = "UPDATE virtual_calendar.gruppe";
                    string Str_ValuesString = " SET Str_GruppenName ='" + StrVal1 + "', Str_GruppenPassword = '" + StrVal2 + "'";

                    string Str_WhereString = " WHERE Id =" + Id;

                    MySqlCommand insertCommand = MysqlConnectionVm.CreateCommand();
                    insertCommand.CommandText = Str_InserIntoString + Str_ValuesString + Str_WhereString;

                    insertCommand.ExecuteNonQuery();
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                // Don't forget to handle it
                showExeption(ex);
                return false;
            }
        }


        public bool UpdateGruppeMitgliedschafftVomBenutzer(Benutzer UpToDateBenutzer)
        {
            // Insert to the collection and update DB
            try
            {
                using (MysqlConnectionVm)
                {
                    MysqlConnectionVm.Open();
                    
                    int Id = UpToDateBenutzer.Id;

                    string Str_InserIntoString = "UPDATE virtual_calendar.benutzer";
                    bool Bool_EinladungliegtVor = true;
                    string Str_ValuesString = " SET Str_Farbe = '" + UpToDateBenutzer.Str_Farbe + "' , Enuml_EinladungliegtVor = '" + Bool_EinladungliegtVor + "'";
                
                    string Str_WhereString = " WHERE Id =" + Id;

                    MySqlCommand insertCommand = MysqlConnectionVm.CreateCommand();
                    insertCommand.CommandText = Str_InserIntoString + Str_ValuesString + Str_WhereString;

                    insertCommand.ExecuteNonQuery();
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                // Don't forget to handle it
                showExeption(ex);
                return false;
            }
        }

        public bool GruppeDerBenutzerUpdaten(Benutzer UpToDateBenutzer)
        {
            // Insert to the collection and update DB
            try
            {
                using (MysqlConnectionVm)
                {
                    MysqlConnectionVm.Open();

                    int Id = UpToDateBenutzer.Id;

                    string Str_InserIntoString = "UPDATE virtual_calendar.benutzer";
                    string Str_ValuesString = " SET Str_MitgliedVon ='" + UpToDateBenutzer.Str_MitgliedVon + "'";

                    string Str_WhereString = " WHERE Id =" + Id;

                    MySqlCommand insertCommand = MysqlConnectionVm.CreateCommand();
                    insertCommand.CommandText = Str_InserIntoString + Str_ValuesString + Str_WhereString;

                    insertCommand.ExecuteNonQuery();
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                // Don't forget to handle it
                showExeption(ex);
                return false;
            }
        }


        public bool UpdateAdminPowerVomBenutzer(Benutzer UpToDateBenutzer)
        {
            // Insert to the collection and update DB
            try
            {
                using (MysqlConnectionVm)
                {
                    MysqlConnectionVm.Open();

                    int Id = UpToDateBenutzer.Id;

                    string Str_InserIntoString = "UPDATE virtual_calendar.benutzer";
                    string Str_ValuesString = " SET Enum_IsAdmin = '" + UpToDateBenutzer.Bool_IsAdmin + "'";

                    string Str_WhereString = " WHERE Id =" + Id;

                    MySqlCommand insertCommand = MysqlConnectionVm.CreateCommand();
                    insertCommand.CommandText = Str_InserIntoString + Str_ValuesString + Str_WhereString;

                    insertCommand.ExecuteNonQuery();
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                // Don't forget to handle it
                showExeption(ex);
                return false;
            }
        }

        public bool DeleteGruppeMitgliedschafftVomBenutzer(Benutzer UpToDateBenutzer)
        {
            // Insert to the collection and update 
            try
            {
                using (MysqlConnectionVm)
                {
                    MysqlConnectionVm.Open();

                    int Id = UpToDateBenutzer.Id;
                    string Str_InserIntoString = "UPDATE virtual_calendar.benutzer";
                    bool Bool_EinladungliegtVor = false;
                    string Str_ValuesString = " SET Str_MitgliedVon = '" + UpToDateBenutzer.Str_Name +  "' , Str_Farbe = " + "''" + " , Enuml_EinladungliegtVor = '" + Bool_EinladungliegtVor+"'";

                    string Str_WhereString = " WHERE Id =" + Id;

                    MySqlCommand insertCommand = MysqlConnectionVm.CreateCommand();
                    insertCommand.CommandText = Str_InserIntoString + Str_ValuesString + Str_WhereString;

                    insertCommand.ExecuteNonQuery();
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                // Don't forget to handle it
                showExeption(ex);
                return false;
            }
        }

        public ViewModel()
        {

        }

        // Hier sind unabhängige Funktionen: 
        // Hiermit soll aus einem String ein Enum umgewandelt werden,
        // da der Benutzer hat nur eine Eingabe als String
        public Termin.Enum_KalenderAuswahl findeKalenderAuswahl(string str_Auswahl)
        {
            if (str_Auswahl == "Gruppenansicht")
                return Termin.Enum_KalenderAuswahl.Gruppenansicht;
            else
                return Termin.Enum_KalenderAuswahl.Individualansicht;
        }

        public Termin.Enum_WiederholungsZyklus findeWiederhlZyklus(string Str_Auswahl)
        {
            switch (Str_Auswahl)
            {
                case "Jaehrlich":
                    return Termin.Enum_WiederholungsZyklus.Jaehrlich;
                case "Monatlich":
                    return Termin.Enum_WiederholungsZyklus.Monatlich;
                case "Woechentlich":
                    return Termin.Enum_WiederholungsZyklus.Woechentlich;
                case "Taeglich":
                    return Termin.Enum_WiederholungsZyklus.Taeglich;
                default:
                    return Termin.Enum_WiederholungsZyklus.Keine;
            }
        }

        public string strVonEnumKalenderAuswahl(Termin.Enum_KalenderAuswahl En_Auswahl)
        {
            if (En_Auswahl == Termin.Enum_KalenderAuswahl.Gruppenansicht)
                return "Gruppenansicht";
            else
                return "Individualansicht";
        }

        public string strVonEnumWiederhlZyklus(Termin.Enum_WiederholungsZyklus En_Auswahl)
        {
            switch (En_Auswahl)
            {
                case Termin.Enum_WiederholungsZyklus.Jaehrlich:
                    return "Jaehrlich";
                case Termin.Enum_WiederholungsZyklus.Monatlich:
                    return "Monatlich";
                case Termin.Enum_WiederholungsZyklus.Woechentlich:
                    return "Woechentlich";
                case Termin.Enum_WiederholungsZyklus.Taeglich:
                    return "Taeglich";
                default:
                    return "Keine";
            }
        }

        public int ConvertEnumKAToIndex(Termin.Enum_KalenderAuswahl En_Auswahl)
        {
            if (En_Auswahl == Termin.Enum_KalenderAuswahl.Gruppenansicht)
                return 1;
            else
                return 0;
        }

        public int ConvertEnumWZToIndex(Termin.Enum_WiederholungsZyklus En_Auswahl)
        {
            switch (En_Auswahl)
            {
                case Termin.Enum_WiederholungsZyklus.Jaehrlich:
                    return 1;
                case Termin.Enum_WiederholungsZyklus.Monatlich:
                    return 2;
                case Termin.Enum_WiederholungsZyklus.Woechentlich:
                    return 3;
                case Termin.Enum_WiederholungsZyklus.Taeglich:
                    return 4;
                default:
                    return 0;
            }
        }

        public Termin.Enum_KalenderAuswahl ConvertKalAuswIndexToEnum(string Index)
        {
            if (Index == "1")
                return Termin.Enum_KalenderAuswahl.Gruppenansicht;
            else
                return Termin.Enum_KalenderAuswahl.Individualansicht;
        }

        public Termin.Enum_WiederholungsZyklus ConvertWiedZyklIndexToEnum(string Index)
        {
            switch (Index)
            {
                case "1":
                    return Termin.Enum_WiederholungsZyklus.Jaehrlich;
                case "2":
                    return Termin.Enum_WiederholungsZyklus.Monatlich;
                case "3":
                    return Termin.Enum_WiederholungsZyklus.Woechentlich;
                case "4":
                    return Termin.Enum_WiederholungsZyklus.Taeglich;
                default:
                    return Termin.Enum_WiederholungsZyklus.Keine;
            }
        }

        private async void showExeption(Exception ex)
        {
            var dialog = new MessageDialog("Eine Exeption wurde ausgelöst, \nKontaktieren Sie das Support-Team");

            await dialog.ShowAsync();
        }

    }
}
