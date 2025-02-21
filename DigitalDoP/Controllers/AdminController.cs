using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DigitalDoP.Data;
using DigitalDoP.Models;
using DigitalDoP.Models.DTOs;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Admin")]
[Route("api/admin")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    // 📌 Add a new staff member (Creates User + Links to Staff)
    [HttpPost("staff")]
    public async Task<IActionResult> AddStaff([FromBody] StaffRequest request)
    {
        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = "Staff",
            OfficeId = request.OfficeId
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var staff = new Staff
        {
            UserId = user.Id,
            OfficeId = request.OfficeId
        };

        _context.Staff.Add(staff);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStaff), new { id = staff.Id }, staff);
    }


    // 📌 Get all staff (Now includes Office)
    [HttpGet("staff")]
    public async Task<IActionResult> GetStaff()
    {
        var staff = await _context.Staff
            .Include(s => s.User) // ✅ Load User
            .Include(s => s.Office) // ✅ Load Office
            .ToListAsync();
        return Ok(staff);
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

    // 📌 Get all services
    [HttpGet("services")]
    public async Task<IActionResult> GetServices()
    {
        var services = await _context.Services.Include(s => s.Office).ToListAsync(); // ✅ Include Office
        return Ok(services);
    }

    // 📌 Dashboard Analytics
    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboardStats()
    {
        var totalServices = await _context.Services.CountAsync();
        var totalFeedbacks = await _context.Feedbacks.CountAsync();
        return Ok(new { totalServices, totalFeedbacks });
    }

    // 📌 Get all feedbacks (Now includes User & Office)
    [HttpGet("feedback")]
    public async Task<IActionResult> GetFeedback()
    {
        var feedbacks = await _context.Feedbacks
            .Include(f => f.Office) // ✅ Load Office details
            .ToListAsync();
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
