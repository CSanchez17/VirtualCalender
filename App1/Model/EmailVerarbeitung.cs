using LightBuzz.SMTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualCalendarV01.Klassen;
using Windows.ApplicationModel.Email;
using Windows.UI.Popups;

namespace VirtualCalendarV01.Model
{
    class EmailVerarbeitung
    {
        private static string smtp_server = "smtp-mail.outlook.com";
        private static int port = 587;
        private static bool ssl = false;
        private static string emailAdresse = "Virtual.Calendar@outlook.de";
        private static string password = "Team2SWP";
        public EmailVerarbeitung()
        {
            
        }

        public static async void EmailSenden(Termin termin, string PersonEmail, string Betreff)
        {
            //Try Catch
            try
            {
                using (SmtpClient client = new SmtpClient(smtp_server, port, ssl, emailAdresse, password)) //Funktion 
                {
                    EmailMessage emailMessage = new EmailMessage();

                    emailMessage.To.Add(new EmailRecipient(PersonEmail));
                    //           emailMessage.CC.Add(new EmailRecipient("CC_EMAIL..@.."));
                    //           emailMessage.Bcc.Add(new EmailRecipient("CC_EMAIL..@.."));
                    emailMessage.Subject = Betreff;
                    string Messag = "\n" + Betreff + termin.Str_Bezeichnung + "\nFindet am " + termin.Dt_Beginn + " bis: " + termin.Dt_Ende + " statt. \nVerantwortliche Person: " + termin.Str_BenutzerId;
                    Messag += "\n\n\nDas VirtualCalendar-Team wuenscht Ihnen einen guten Tag!  :)\n\n";
                    emailMessage.Body = Messag;

                    await client.SendMailAsync(emailMessage);
                }
            }
            catch (Exception ex)
            {
                showExeption(ex);
            }

        }

        public static async void EmailSendenBenutzerDaten(Benutzer benutzer, string Betreff, string Messag)
        {
            //Try Catch
            try
            {
                using (SmtpClient client = new SmtpClient(smtp_server, port, ssl, emailAdresse, password)) //Funktion 
                {
                    EmailMessage emailMessage = new EmailMessage();

                    emailMessage.To.Add(new EmailRecipient(benutzer.Str_Email));
                    //emailMessage.CC.Add(new EmailRecipient("CC_EMAIL..@.."));
                    //     emailMessage.Bcc.Add(new EmailRecipient("CC_EMAIL..@.."));
                    emailMessage.Subject = Betreff;
                 
                    Messag += "\n\n\nDas VirtualCalendar-Team wuenscht Ihnen einen guten Tag!  :)\n\n";
                    emailMessage.Body = Messag;

                    await client.SendMailAsync(emailMessage);
                }
            }
            catch (Exception ex)
            {
                showExeption(ex);
            }

        }

        public static async void EmailSendenGruppeGelöscht(string PersonEmail, string Betreff, string Messag)
        {
            //Try Catch
            try
            {
                using (SmtpClient client = new SmtpClient(smtp_server, port, ssl, emailAdresse, password)) //Funktion 
                {
                    EmailMessage emailMessage = new EmailMessage();

                    emailMessage.To.Add(new EmailRecipient(PersonEmail));
                    //           emailMessage.CC.Add(new EmailRecipient("CC_EMAIL..@.."));
                    //           emailMessage.Bcc.Add(new EmailRecipient("CC_EMAIL..@.."));
                    emailMessage.Subject = Betreff;
                    Messag += "\n\n\nDas VirtualCalendar-Team wuenscht Ihnen einen guten Tag!  :)\n\n";
                    emailMessage.Body = Messag;

                    await client.SendMailAsync(emailMessage);
                }
            }
            catch (Exception ex)
            {
                showExeption(ex);
            }

        }

        private static async void showExeption(Exception ex)
        {
            var dialog = new MessageDialog("Eine Exeption wurde ausgelöst, \nKontaktieren Sie das Support-Team");

            await dialog.ShowAsync();
        }
    }
}
