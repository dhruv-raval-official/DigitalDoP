using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DigitalDoP.Data;
using DigitalDoP.Models;
using Microsoft.EntityFrameworkCore;
using DigitalDoP.Models.DTOs;

[Authorize(Roles = "Staff")]
[Route("api/staff")]
[ApiController]
public class StaffController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public StaffController(ApplicationDbContext context)
    {
        _context = context;
    }

    // 📌 Add a new service
    [HttpPost("services")]
    public async Task<IActionResult> AddService([FromBody] ServiceRequest request)
    {
        Console.WriteLine($"Received OfficeId: {request.OfficeId}"); // ✅ Debugging

        if (request.OfficeId == 0)
        {
            return BadRequest(new { message = "OfficeId is missing or invalid." });
        }

        var officeExists = await _context.Offices.AnyAsync(o => o.Id == request.OfficeId);
        if (!officeExists)
        {
            return BadRequest(new { message = "Invalid Office ID. Office does not exist." });
        }

        // ✅ Creating and saving new service
        var newService = new Service
        {
            TrackingId = request.TrackingId,
            UserName = request.UserName,
            UserEmail = request.UserEmail,
            ServiceType = request.ServiceType,
            ExpectedDeliveryDate = request.ExpectedDeliveryDate,
            Status = request.Status,
            ArrivalDate = request.ArrivalDate,
            DispatchDate = request.DispatchDate,
            DeliveredDate = request.DeliveredDate,
            OfficeId = request.OfficeId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Services.Add(newService);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetServiceById), new { id = newService.Id }, newService);
    }

    // 📌 Get Service by ID
    [HttpGet("services/{id}")]
    public async Task<IActionResult> GetServiceById(int id)
    {
        var service = await _context.Services.FindAsync(id);
        if (service == null)
        {
            return NotFound(new { message = "Service not found." });
        }
        return Ok(service);
    }

    // 📌 Update service status
    [HttpPut("services/{TrackingId}")]
    public async Task<IActionResult> UpdateServiceStatus(string TrackingId, [FromBody] UpdateServiceRequest request)
    {
        var service = await _context.Services.FirstOrDefaultAsync(s => s.TrackingId == TrackingId);
        if (service == null)
        {
            return NotFound(new { message = "Service not found." });
        }

        // ✅ Update only the provided fields, preserving existing values
        service.Status = request.Status ?? service.Status;
        service.ArrivalDate = request.ArrivalDate ?? service.ArrivalDate;
        service.DispatchDate = request.DispatchDate ?? service.DispatchDate;
        service.DeliveredDate = request.DeliveredDate ?? service.DeliveredDate;
        service.UpdatedAt = DateTime.UtcNow; // ✅ Update timestamp

        await _context.SaveChangesAsync();
        return Ok(service);
    }




    // 📌 Get all services
    [HttpGet("services")]
    public async Task<IActionResult> GetServices()
    {
        var services = await _context.Services.Include(s => s.Office).ToListAsync(); // ✅ Include Office
        return Ok(services);
    }

    // 📌 Get feedback by ID
    [HttpGet("feedback")]
    public async Task<IActionResult> GetFeedback()
    {
        var feedbacks = await _context.Feedbacks.ToListAsync();
        return Ok(feedbacks);
    }

    // 📌 Get feedback by ID
    [HttpGet("feedback/{id}")]
    public async Task<IActionResult> GetFeedbackById(int id)
    {
        var feedback = await _context.Feedbacks.FindAsync(id);
        if (feedback == null) return NotFound();
        return Ok(feedback);
    }
}
