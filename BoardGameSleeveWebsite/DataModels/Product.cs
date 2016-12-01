namespace BoardGameSleeveWebsite
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Sales = new HashSet<Sale>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        public decimal? Price { get; set; }

        public int? InStock { get; set; }

        public int? SleeveCountInProduct { get; set; }

        public int SizeID { get; set; }

        public virtual Size Size { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
