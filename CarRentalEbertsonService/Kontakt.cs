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
    
    public partial class Kontakt
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kontakt()
        {
            this.Kunde = new HashSet<Kunde>();
        }
    
        public int KontaktID { get; set; }
        public string E_Mail { get; set; }
        public string Telefonnummer { get; set; }
        public string Mobilnummer { get; set; }
        public string Faxnummer { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kunde> Kunde { get; set; }
    }
}
