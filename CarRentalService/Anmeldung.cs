//------------------------------------------------------------------------------
// <auto-generated>
//    Dieser Code wurde aus einer Vorlage generiert.
//
//    Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten Ihrer Anwendung.
//    Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarRentalService
{
    using System;
    using System.Collections.Generic;
    
    public partial class Anmeldung
    {
        public Anmeldung()
        {
            this.Benutzer = new HashSet<Benutzer>();
        }
    
        public int AnmeldungID { get; set; }
        public string Benutzername { get; set; }
        public string Passwort { get; set; }
    
        public virtual ICollection<Benutzer> Benutzer { get; set; }
    }
}
