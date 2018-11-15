using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VirtualCalendarV01.Klassen;
using VirtualCalendarV01.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace VirtualCalendarV01.Pages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class ProfilBearbeiten : Page
    {
        //Hier werden die Daten der aktuellen Person angezeigt
        //hole den aktuellen Benutzer

        private DateTime datum ;
        private string email ;
        private string pass ;
        //Setze Flag, ob ein benutzer sich anmelden möchte
        private bool UserWantRegister = false;
        
        public ProfilBearbeiten()
        {
            this.InitializeComponent();
            Ellipse elipseFarbe = new Ellipse();
            elipseFarbe.Fill = new SolidColorBrush(Windows.UI.Colors.Black);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //prüfe dass das Element nicht leer ist
            if (Benutzer.MyCurrentUser.Str_Name != null)
            {
                //hole die Daten vom aktuellen Benutzer
                Tb_Name.Text = Benutzer.MyCurrentUser.Str_Name;
                Tb_Name.IsReadOnly = true;
                Tb_Email.IsReadOnly = true;
                dp_Geburtsdatum.Date = Benutzer.MyCurrentUser.Dt_GeburtsDatum;
                Tb_Email.Text = Benutzer.MyCurrentUser.Str_Email;
                passwordBox.Password = Benutzer.MyCurrentUser.Str_Password;
                Tb_Id.Text += Benutzer.MyCurrentUser.Id.ToString();

                UserWantRegister = false;
                passwordBox.IsEnabled = true;
                Deletebtn.Visibility = Visibility.Visible;
                addbtn.IsEnabled = true;

                datum = dp_Geburtsdatum.Date.DateTime;
                email = Tb_Email.Text;
                pass = passwordBox.Password;
            }
            else
            {
                //sonst sind die Textboxs frei 
                Tb_Name.Text = "";
                dp_Geburtsdatum.Date = DateTime.Now;
                Tb_Email.Text = "";
                passwordBox.Password = "";
                Tb_Id.Text = "";
                //Das Pasword wird vom Programm gesetzt
                passwordBox.IsEnabled = false;
                button.IsEnabled = false;
                UserWantRegister = true;
                Deletebtn.Visibility = Visibility.Collapsed;
            }           

        }

        private void AddBarButton_Click(object sender, RoutedEventArgs e)
        {
            if ((String.IsNullOrEmpty(Tb_Name.Text)) || (String.IsNullOrEmpty(Tb_Email.Text)))
            {                
                string MessageD = "Geben Sie bitte eine User ID und eine gültige E-Mail-Adresse ein! :)";
                Meldung(MessageD);
                return;
            }

            //Prüfe ob Daten wurden geändert
            bool datenok = false;
            if ((datum == dp_Geburtsdatum.Date.DateTime) && (email == Tb_Email.Text) && (pass == passwordBox.Password))
            {
                datenok = true;
                return;
            }
            

            if (UserWantRegister == false)
            {
                //Setze Passwordfeld als nicht modifizierbar
                passwordBox.IsEnabled = true;                         

                if (!datenok)
                {
                    Benutzer.MyCurrentUser.Dt_GeburtsDatum = dp_Geburtsdatum.Date.DateTime;
                    Benutzer.MyCurrentUser.Str_Email = Tb_Email.Text;
                    Benutzer.MyCurrentUser.Str_Password = passwordBox.Password;
                    //Hier wird die Info im DB updated!
                    string MessageD = "Ihre Daten wurden geändert!:)";
                    Meldung(MessageD);
                    Benutzer.MyCurrentUser.BenutzerUpdaten();

                    //Hier wir die aktuell angemeldete Person, zu dem aktuellen Benutzer
                    Benutzer.MyCurrentUser.RefreshCurrentUser();

                    Frame.GoBack();
                }
                
            }
            else
            {
                string MessageD;
                if (!(IsValidEmail(Tb_Email.Text)))
                {
                    MessageD = "Die angegebene E-Mail ist nicht gültig! :)";
                    Meldung(MessageD);
                    return;
                }
                Benutzer benutzer = new Klassen.Benutzer();

                benutzer.Str_Name = Tb_Name.Text;
                benutzer.Dt_GeburtsDatum = dp_Geburtsdatum.Date.DateTime;
                benutzer.Str_Email = Tb_Email.Text;
    
                //Setze als Gruppenname der eigeneName
                benutzer.Str_MitgliedVon = benutzer.Str_Name;
     
                //Hier wird die Info im DB aktualliziert!
                benutzer.BenutzerErzeugen();

                //Hier wir die aktuell angemeldete Person, zu dem aktuellen Benutzer
                Benutzer.MyCurrentUser = benutzer;
                
                //Meldung erzeugen!
                MessageD = "Sie haben sich erfolgreich bei VirtualCalendar registriert! \nGeben Sie bitte Ihren Benutzernamen und das per E-Mail gesendete Passwort beim Einloggen ein ! :)";
                Meldung(MessageD);

                UserWantRegister = true;
                //gehe wieder zurück
                Frame.GoBack();
            }
           
        }

        private async void DelBarButton_Click(object sender, RoutedEventArgs e)
        {
            
            //Warne den Benutzer 
            var message = new MessageDialog("Möchten Sie fortfahren?", "Ihr Profil bei Virtual Calender wird gelöscht! :)");
            message.Commands.Add(new Windows.UI.Popups.UICommand("Ja") { Id = 0 });
            message.Commands.Add(new Windows.UI.Popups.UICommand("Nein") { Id = 1 });

            message.DefaultCommandIndex = 0;
            message.CancelCommandIndex = 1;

            var result = await message.ShowAsync();
            if (result.Label == "Ja")
            {
                if (Benutzer.MyCurrentUser.Bool_IsAdmin)
                {
                    //Wenn der Benutzer auch ein Admin ist, dann lösche gleich die erstellte Gruppe
                    //Lösche Termine für den Benutzer
                    //Lösche Termine für den Benutzer
                    foreach (var item in Termin.TerminResponse)
                    {
                        if ((item.Int_BenutzerId == Benutzer.MyCurrentUser.Id) || (item.Str_ErzeugtFuerGruppe == Benutzer.MyCurrentUser.Str_Name))
                        {
                            item.TerminLoeschenSilently();
                        }
                    }
                    //Lösche den aktuellen Benutzer vom Datenbank
                    //Also setze die jeweilige GruppeDaten der Benutzer zurück
                    foreach (var item in Gruppe.MyCurrentGruppe.List_Benutzer)
                    {
                        item.DeleteGruppeMitgliedschafftVomBenutzer();
                        //Meldung an alle Mitgliedern schicken
                        string Betreff = "Die Gruppe: " + Gruppe.MyCurrentGruppe.Str_GruppenName + " wurde gelöscht.";
                        string Message = "Alle Gruppentermine und auch Ihre Mitgliedschaft wurden gelöscht.";
                        EmailVerarbeitung.EmailSendenGruppeGelöscht(item.Str_Email, Betreff, Message);
                    }
                }                

                //Lösche den aktuellen Benutzer vom Datenbank
                //Also setze die jeweilige GruppeDaten der Benutzer zurück
                Benutzer.MyCurrentUser.DeleteProfilVomBenutzer();

                //update Daten
                Benutzer.getBenutzerResponse();

                //setze Flag auf true
                UserWantRegister = true; 
                      
                //Meldung erzeugen!
                string MessageD = "Sie haben Ihr Profil erfolgreich gelöscht ! :)";
                Meldung(MessageD);
            }
            Frame.Navigate(typeof(PopUpAnmeldung));
        }

        private void BackBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //Auslogen
            //Setze die Daten weg
            Benutzer.MyCurrentUser = new Benutzer();
            //gehe zur anmeldeseite
            Frame.Navigate(typeof(PopUpAnmeldung));
        }

        private async void Meldung(string MessageD)
        {
            var dialog = new MessageDialog(MessageD);

            await dialog.ShowAsync();
        }       
        
        private void Tb_Name_LostFocus(object sender, RoutedEventArgs e)
        {
            //Nur den Fall wenn der Benutzer zum ersten Mal sich anmelden möchte
            if (UserWantRegister)
            {
                //prüfe ob den UserName verfügbar ist
                bool flag = false;
                Benutzer _benutzer = Benutzer.sucheVerantwoertlichMitName(Tb_Name.Text, ref flag);

                if (flag)
                {
                    //Gebe den benutzer die Meldung, dass der einen anderen Namen eingeben soll
                    string MessageD = "Der angegebene Name ist leider schon registriert, \nVersuchen Sie es erneut! :)";
                    Meldung(MessageD);
                    addbtn.IsEnabled = false;
                }
                else
                {
                    addbtn.IsEnabled = true;                   
                }
            }
        }

        public bool IsValidEmail(string email)
        {
            bool flag = false;
            if (email.Contains("@") && (Tb_Email.Text != "") && (email.Contains(".com") || email.Contains(".de")) )
            {
                return true;
            }
            return flag;
        }
       
    }
}
