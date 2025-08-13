using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMVCMappingDEMO.Models
{
    public class MechEmployee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MId { get; set; }
        [Required]
        public string MRollNo { get; set; }
        public string? MName { get; set; }
        public string? MAddress { get; set; }
        public string? MEmail { get; set; }
        public string? MPhone { get; set; }
        public string? MDesignation { get; set; }
        public string? MDepartment { get; set; }
        public string? MCompany { get; set; }

        [Range(1900, 2100, ErrorMessage = "Please enter a valid year.")]
        public int MYearPassed { get; set; }
        public string? MNotes { get; set; }
        public string? ImagePath { get; set; }
    }
}
