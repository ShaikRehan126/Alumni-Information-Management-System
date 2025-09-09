using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMVCMappingDEMO.Data;
using MyMVCMappingDEMO.Models;
using OfficeOpenXml;
namespace MyMVCMappingDEMO.Controllers
{
    [Authorize]
    public class MechEmpController : Controller
    {
        private readonly MechEmpDbContext _context;
        public MechEmpController(MechEmpDbContext context)
        {
            _context = context;
        }
        //[Authorize]
        //public IActionResult Index()
        //{

        //    return View();
        //}
        public IActionResult Index()
        {
            ViewBag.TotalEmployees = _context.MechEmployees.Count();
            ViewBag.TotalDepartments = _context.MechEmployees.Select(e => e.MDepartment).Distinct().Count();
            ViewBag.Departments = _context.MechEmployees
                .Select(e => e.MDepartment)
                .Where(d => !string.IsNullOrEmpty(d))
                .Distinct()
                .OrderBy(d => d)
                .ToList();
            return View();
        }

        public IActionResult AddEmp()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> AddEmp([Bind("MId,MRollNo,MName,MEmail,MPhone,MDesignation,MDepartment,MCompany,MYearPassed,MAddress,MNotes")] MechEmployee mechemp, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    //To display image in view, we need to save it in wwwroot/images folder
                    var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    mechemp.ImagePath = "/images/" + fileName;
                }
                _context.Add(mechemp);
                await _context.SaveChangesAsync();
                return RedirectToAction("ViewEmp");
            }
            return View(mechemp);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _context.MechEmployees.FirstOrDefaultAsync(e=>e.MId==id);
            //if (employee == null)
            //{
            //    return NotFound();
            //}
            return View(employee);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("MId,MRollNo,MName,MEmail,MPhone,MDesignation,MDepartment,MCompany,MYearPassed,MAddress,ImagePath,MNotes")] MechEmployee mechemp, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                // Get the existing employee from the database (tracking enabled this time)
                var existingEmp = await _context.MechEmployees.FirstOrDefaultAsync(e => e.MId == id);
                if (existingEmp == null)
                {
                    return NotFound();
                }

                // Update scalar properties from the form (except ImagePath)
                existingEmp.MRollNo = mechemp.MRollNo;
                existingEmp.MName = mechemp.MName;
                existingEmp.MDesignation = mechemp.MDesignation;
                existingEmp.MCompany = mechemp.MCompany;
                existingEmp.MYearPassed = mechemp.MYearPassed;
                existingEmp.MAddress = mechemp.MAddress;
                existingEmp.MDepartment = mechemp.MDepartment;
                existingEmp.MPhone = mechemp.MPhone;
                existingEmp.MEmail = mechemp.MEmail;  
                existingEmp.MNotes = mechemp.MNotes;


                if (imageFile != null && imageFile.Length > 0)
                {
                    // Generate unique file name
                    var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    // Update the new image path
                    existingEmp.ImagePath = "/images/" + fileName;
                }
                // else keep existingEmp.ImagePath as is

                await _context.SaveChangesAsync();
                return RedirectToAction("ViewEmp");
            }
                return View(mechemp);
    
        }

        public async Task<IActionResult> Delete(int id)
        {
            var emp = await _context.MechEmployees.FirstOrDefaultAsync(e => e.MId == id);
            return View(emp);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id != null)
            {
                var emp = await _context.MechEmployees.FindAsync(id);
                if (emp == null)
                {
                    return NotFound();
                }
                _context.MechEmployees.Remove(emp);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("ViewEmp");

        }

        public async Task<IActionResult> ViewDetails(int id)
        {
            var emp = await _context.MechEmployees.FirstOrDefaultAsync(e => e.MId == id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }
        public async Task<IActionResult> ViewEmp(string? searchString, string? searchBy)
        {
            var employees = from e in _context.MechEmployees
                            select e;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                if (searchBy == "Name")
                {
                    employees = employees.Where(e => e.MName.Contains(searchString));
                }
                else if (searchBy == "Mobile")
                {
                    employees = employees.Where(e => e.MPhone.Contains(searchString));
                }
                else if(searchBy == "Department")
                {
                    employees = employees.Where(e => e.MDepartment.Contains(searchString));
                }
                else if (searchBy == "Email")
                {
                    employees = employees.Where(e => e.MEmail.Contains(searchString));
                }
                else if(searchBy == "YearPassed")
                {
                    employees = employees.Where(e => e.MYearPassed.ToString().Contains(searchString));
                }
            }

            ViewData["CurrentFilter"] = searchString;
            ViewData["SearchBy"] = searchBy;
            return View(await employees.ToListAsync());
        }
        [HttpGet]
        public IActionResult Upload()
        {
            return View(new FileUploadViewModel());
        }

    [HttpPost]
    public async Task<IActionResult> Upload(FileUploadViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.ExcelFile != null && model.ExcelFile.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await model.ExcelFile.CopyToAsync(stream);
                    stream.Position = 0; // reset stream

                    using (var workbook = new XLWorkbook(stream))
                    {
                        var worksheet = workbook.Worksheet(1); // first sheet
                        var rowCount = worksheet.LastRowUsed().RowNumber();

                        var employees = new List<MechEmployee>();

                        for (int row = 2; row <= rowCount; row++) // skip header row
                        {
                            var employee = new MechEmployee
                            {
                                MRollNo = worksheet.Cell(row, 1).GetString(),
                                MName = worksheet.Cell(row, 2).GetString(),
                                MAddress = worksheet.Cell(row, 3).GetString(),
                                MEmail = worksheet.Cell(row, 4).GetString(),
                                MPhone = worksheet.Cell(row, 5).GetString(),
                                MDesignation = worksheet.Cell(row, 6).GetString(),
                                MDepartment = worksheet.Cell(row, 7).GetString(),
                                MCompany = worksheet.Cell(row, 8).GetString(),
                                MYearPassed = worksheet.Cell(row, 9).GetValue<int>(),
                                MNotes = worksheet.Cell(row, 10).GetString(),
                                ImagePath = "/images/default.png"
                            };

                            employees.Add(employee);
                        }

                        _context.MechEmployees.AddRange(employees);
                        await _context.SaveChangesAsync();

                        // Pass preview info to view
                        ViewBag.TotalRows = rowCount;
                        ViewBag.DataRows = rowCount - 1;
                        ViewBag.MoreRows = employees.Count > 5 ? employees.Count - 5 : 0;

                        return View("UploadResult", employees.Take(5).ToList());
                    }
                }
            }
        }
        return View(model);
    }


    }
}
