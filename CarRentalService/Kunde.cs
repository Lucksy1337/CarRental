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
    
    public partial class Kunde
    {
        public Kunde()
        {
            this.Auftrag = new HashSet<Auftrag>();
        }
    
        public int KundeID { get; set; }
        public int KontaktID { get; set; }
        public int AdresseID { get; set; }
        public string Vornamen { get; set; }
        public string Nachnamen { get; set; }
        public int Alter { get; set; }
        public string Geschlecht { get; set; }
        public string Kundennummer { get; set; }
    
        public virtual Adresse Adresse { get; set; }
        public virtual ICollection<Auftrag> Auftrag { get; set; }
        public virtual Kontakt Kontakt { get; set; }
    }
}
