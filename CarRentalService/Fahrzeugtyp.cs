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
    
    public partial class Fahrzeugtyp
    {
        public Fahrzeugtyp()
        {
            this.Fahrzeug = new HashSet<Fahrzeug>();
        }
    
        public int FahrzeugtypID { get; set; }
        public string Bezeichnung { get; set; }
    
        public virtual ICollection<Fahrzeug> Fahrzeug { get; set; }
    }
}
