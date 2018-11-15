using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualCalendarV01.Klassen
{
    public class Admin : Benutzer
    {
        public Admin(Benutzer benutzer)
        {
            benutzer.Bool_IsAdmin = true;
        }
        private void gruppeErstellen() { } //Name Password für die Gruppe festlegen
        private void gruppeLoeschen() { }
        private void benutzerHinzufuegen() { }
        
        //Aus der Gruppe entfernen
        private void benutzerEntfernen() { }
    }
}
