namespace BabySitter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InformationTravel")]
    public partial class InformationTravel
    {
        [Key]
        public int id_InformationTravel { get; set; }

        public int? id_province { get; set; }

        [Display(Name = "Loại Tin")]
        public int? id_category { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name ="Tiêu Đề")]
        public string title { get; set; }

        [StringLength(100)]
        [Display(Name = "Ảnh")]
        public string img { get; set; }

        [Display(Name = "Bài Viết")]
        public string description { get; set; }
        [Display(Name = " ")]
        public string detail { get; set; }

        [Required]
        [StringLength(300)]
        public string meta { get; set; }

        public bool? hide { get; set; }

        [Column(TypeName = "date")]
        public DateTime? datebegin { get; set; }

        public virtual Category Category { get; set; }

        public virtual Province Province { get; set; }
    }
}
