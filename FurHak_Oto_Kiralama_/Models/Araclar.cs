//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FurHak_Oto_Kiralama_.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Araclar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Araclar()
        {
            this.Sozlesmeler = new HashSet<Sozlesmeler>();
        }
    
        public byte Id { get; set; }
        public string Plaka { get; set; }
        public Nullable<byte> Marka { get; set; }
        public Nullable<byte> Seri { get; set; }
        public string Yil { get; set; }
        public string Motor { get; set; }
        public string KM { get; set; }
        public string Renk { get; set; }
        public string Yakittipi { get; set; }
        public string Vites { get; set; }
        public string Motorgucu { get; set; }
        public string Aciklama { get; set; }
        public Nullable<bool> Durum { get; set; }
        public string Foto { get; set; }
        public Nullable<short> Günlük { get; set; }
        public Nullable<short> Aylik { get; set; }
        public Nullable<short> Yillik { get; set; }
    
        public virtual Markalar Markalar { get; set; }
        public virtual Seriler Seriler { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sozlesmeler> Sozlesmeler { get; set; }
    }
}
