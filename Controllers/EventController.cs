using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.UI.Services;
using MyMVCMappingDEMO.Data;
using MyMVCMappingDEMO.Models;

public class EventController : Controller
{
    private readonly MechEmpDbContext _context;
    private readonly IEmailSender _emailSender;

    public EventController(MechEmpDbContext context, IEmailSender emailSender)
    {
        _context = context;
        _emailSender = emailSender;
    }

    [HttpGet]
    public IActionResult AddEvent()
    {
        // Get distinct years from alumni table
        var years = _context.MechEmployees
            .Select(e => e.MYearPassed)
            .Distinct()
            .OrderByDescending(y => y)
            .ToList();
        ViewBag.Years = years;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddEvent(Event eventModel)
    {
        // Get selected year from form
        var selectedYearStr = Request.Form["YearPassed"];
        if (!int.TryParse(selectedYearStr, out int selectedYear))
        {
            ModelState.AddModelError("YearPassed", "Please select a valid year.");
        }

        if (ModelState.IsValid)
        {
            _context.Events.Add(eventModel);
            await _context.SaveChangesAsync();

            // Get alumni emails for selected year
            var alumniEmails = _context.MechEmployees
                .Where(e => e.MYearPassed == selectedYear && !string.IsNullOrEmpty(e.MEmail))
                .Select(e => e.MEmail)
                .ToList();

            string subject = $"New Event: {eventModel.Title}";
            string message = $@"
                <h2>Dear Alumni,</h2>
                <p>We are excited to announce a new event:</p>
                <b>{eventModel.Title}</b><br/>
                {eventModel.Description}<br/>
                <b>Date:</b> {eventModel.Date:dd MMM yyyy}<br/>
                <b>Location:</b> {eventModel.Location}<br/><br/>
                <p>Best regards,<br/>Alumni Team</p>";

            foreach (var email in alumniEmails)
            {
                await _emailSender.SendEmailAsync(email, subject, message);
            }

            // Redirect to Event/Index after success
            return RedirectToAction("Index");
        }

        // If invalid, re-populate years and return view
        ViewBag.Years = _context.MechEmployees
            .Select(e => e.MYearPassed)
            .Distinct()
            .OrderByDescending(y => y)
            .ToList();
        return View(eventModel);
    }

    public IActionResult Index()
    {
        // Show success message
        return View();
    }
}