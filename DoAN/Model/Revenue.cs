namespace DoAN.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Revenue")]
    public partial class Revenue
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RevenueID { get; set; }

        public int? InvoiceID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RevenueDate { get; set; }

        public decimal? RevenueAmount { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}
