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
    
    public partial class Benutzer
    {
        public int BenutzerID { get; set; }
        public Nullable<int> AnmeldeID { get; set; }
        public Nullable<int> BenutzerartID { get; set; }
        public string Benutzernamen { get; set; }
    
        public virtual Anmeldung Anmeldung { get; set; }
        public virtual Benutzerart Benutzerart { get; set; }
    }
}
