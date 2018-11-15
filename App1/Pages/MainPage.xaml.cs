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
using VirtualCalendarV01.Pages;
using VirtualCalendarV01.Pages.Ansichten;
using VirtualCalendarV01.Klassen;
using System.Collections.ObjectModel;


// Die Vorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 dokumentiert.

namespace VirtualCalendarV01.Pages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //Erstelle eine Leere Instanz der Klasse Termin
        Termin termin = new Termin();

        //Finde wer der verantwortlich ist, die Funktion sucheVerantwoertlich gibt das Email der Person zurück (Erlaubt ohne Objektverweis, denn die Funktion ist static)
        Benutzer benutzer = new Benutzer();

        public MainPage()
        {
            this.InitializeComponent();
            App.updateInfo();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {            
            //Hole die aktuelleste info aus dem DB
            App.updateInfo();            

            //Die Banutzer und Termine meiner Gruppe werden angezeigt
            bool flag = false;
            Gruppe meineGruppe = Gruppe.sucheGruppe(Benutzer.MyCurrentUser.Str_MitgliedVon, ref flag);
            if (flag)
            {
            //    TerminList.ItemsSource = meineGruppe.List_Termine;
                //hole nur die Termine für diesen Tag für den angegebenen Benutzer(registrierte)
                Termin.findeDieAufgabenfürYMDH(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                ObservableCollection<Termin> valideTermine = Termin.get_myCurrentGroupTasksAtMonth();
                for (int index = 0; index < valideTermine.Count; index++)               
                {
                    if (valideTermine[index].Dt_Beginn < DateTime.Now)
                    {
                        //Lösche Termin zu zeigen, da der nicht mehr valid ist.
                        valideTermine.RemoveAt(index);
                    }
                }
                TerminList.ItemsSource = valideTermine;
                BenutzerList.ItemsSource = meineGruppe.List_Benutzer;
                Gruppe.MyCurrentGruppe = meineGruppe;
            }
            else
            {
                DateTime dt = DateTime.Now;
                //Zeige nur die Private Termine, wenn noch Keine Gruppe vorhanden!
                TerminList.ItemsSource = Termin.getPrivateTermineAmTag(dt.Year,dt.Month,dt.Day);
            }

            //hiermit wird den Namen und Id des aktuellen Benutzers angezeigt!
            if (Benutzer.MyCurrentUser.Str_Name != null)
            {
                textBlockCurrentUser.Text += Benutzer.MyCurrentUser.Str_Name;
                textBlockCurrentUser_Id.Text += Benutzer.MyCurrentUser.Id.ToString();
            }           
            
        }

        private void BackBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void AddBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddTermin));
        }

        private void BenutzersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var _termin = TerminList.SelectedItem as Termin;
            //gehe in die EditTermin Seite mit dem angegebenen Parameter _termin
            Frame.Navigate(typeof(EditTermin), _termin);
        }

        private void RefreshBarButton_Click(object sender, RoutedEventArgs e)
        {
            //Hole dieganze Termine aus dem DB   
            TerminList.ItemsSource = Termin.getTerminResponse();
            //Hole die aktuelleste info aus dem DB
            App.updateInfo();
        }

        private void MenuButton1_Click(object sender, RoutedEventArgs e)
        {
            //StartSeite Button
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
            //Jahresansicht Button
            Frame.Navigate(typeof(Jahresansicht));
        }
              
        private void MenuButton7_Click(object sender, RoutedEventArgs e)
        {
            //Info Button
            Frame.Navigate(typeof(InfoPage));
        }
            
        //Ereignisshandler im Fall dass der Zeiger auf dem Button bewegt wird.
        private void MenuButton1_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = true;
        }

        private void MySplitView_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = false;
        }       

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddTermin));
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddGruppe));
        }

        private void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            //Gehe in die Seite Profilbearbeiten
            Frame.Navigate(typeof(ProfilBearbeiten));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PopUpAnmeldung));
        }

        private void GruppeBarButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (Benutzer.MyCurrentUser.Str_MitgliedVon == Benutzer.MyCurrentUser.Str_Name)
            {
                Frame.Navigate(typeof(GruppeAnmeldung));
            }
            else
            {
                //Gehe zu GruppeBearbeiten
                Frame.Navigate(typeof(GruppeBearbeiten));
            }
        }
    }
}
