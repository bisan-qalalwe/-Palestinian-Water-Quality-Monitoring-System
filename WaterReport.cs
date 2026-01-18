using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaterQualityMonitoring.Models
{
    public class WaterReport
    {
        [Key]
        public int ReportID { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserID { get; set; }
        public virtual User User { get; set; }

        [Required(ErrorMessage = "Location required")]
        [StringLength(100)]
        public string Location { get; set; }

        public DateTime ReportDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Type of pollution required")]
        [StringLength(50)]
        public string PollutionType { get; set; } // مثل: "كيميائي", "بكتيري", "نفايات صلبة"

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "Pending"; // Pending, Confirmed, False

        [StringLength(50)]
        public string SourceType { get; set; } // مثل: "طبيعي", "مستوطنات", "مصانع محلية"


    }
}
