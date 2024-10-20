namespace DoAN.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Invoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Invoice()
        {
            Revenues = new HashSet<Revenue>();
        }

        public int InvoiceID { get; set; }

        public int? OrderID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? InvoiceDate { get; set; }

        public decimal? TotalAmount { get; set; }

        public virtual Order Order { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Revenue> Revenues { get; set; }
    }
}
