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
    
    public partial class Kontakt
    {
        public Kontakt()
        {
            this.Kunde = new HashSet<Kunde>();
        }
    
        public int KontaktID { get; set; }
        public string E_Mail { get; set; }
        public string Telefonnummer { get; set; }
        public string Mobilnummer { get; set; }
        public string Faxnummer { get; set; }
    
        public virtual ICollection<Kunde> Kunde { get; set; }
    }
}
