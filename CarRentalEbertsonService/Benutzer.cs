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
