//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarRentalEbertsonService
{
    using System;
    using System.Collections.Generic;
    
    public partial class Versicherungspaket
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Versicherungspaket()
        {
            this.Fahrzeug = new HashSet<Fahrzeug>();
        }
    
        public int VersicherungspaketID { get; set; }
        public string Bezeichnung { get; set; }
        public bool Insassenunfallschutzversicherung { get; set; }
        public bool Haftpflichtversicherung { get; set; }
        public bool Feuerversicherung { get; set; }
        public bool Haftungsbegrenzung { get; set; }
        public bool Diebstahlschutz { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fahrzeug> Fahrzeug { get; set; }
    }
}
