//------------------------------------------------------------------------------
// <auto-generated>
//    Dieser Code wurde aus einer Vorlage generiert.
//
//    Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten Ihrer Anwendung.
//    Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarRentalSchoolService
{
    using System;
    using System.Collections.Generic;
    
    public partial class Auftrag
    {
        public int AuftragID { get; set; }
        public int KundeID { get; set; }
        public int FahrzeugID { get; set; }
        public System.DateTime Auftragsdatum { get; set; }
        public System.DateTime Rückgabedatum { get; set; }
        public double Gesamtpreis { get; set; }
    
        public virtual Fahrzeug Fahrzeug { get; set; }
        public virtual Kunde Kunde { get; set; }
    }
}
