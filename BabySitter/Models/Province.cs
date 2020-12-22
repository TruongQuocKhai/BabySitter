namespace BabySitter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Province")]
    public partial class Province
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Province()
        {
            InformationTravels = new HashSet<InformationTravel>();
        }

        [Key]
        public int id_province { get; set; }

        public int? id_areas { get; set; }

        [Required]
        [StringLength(50)]
        public string nameProvince { get; set; }

        public bool? hide { get; set; }

        public virtual Area Area { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InformationTravel> InformationTravels { get; set; }
    }
}
