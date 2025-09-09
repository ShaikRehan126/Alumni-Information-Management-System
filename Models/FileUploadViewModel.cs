using System.ComponentModel.DataAnnotations;

namespace MyMVCMappingDEMO.Models;
public class FileUploadViewModel
{
    [Required]
    [Display(Name = "Excel File")]
    public IFormFile ExcelFile { get; set; }

    [Display(Name = "Employee Images (optional)")]
    public List<IFormFile>? ImageFiles { get; set; }
}
