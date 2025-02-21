using Microsoft.AspNetCore.Mvc;
using DigitalDoP.Data;
using DigitalDoP.Models;
using Microsoft.EntityFrameworkCore;
using DigitalDoP.Models.DTOs;

[Route("api")]
[ApiController]
public class PublicController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PublicController(ApplicationDbContext context)
    {
        _context = context;
    }

    // 📌 Track service by tracking ID
    [HttpGet("services/track/{trackingId}")]
    public async Task<IActionResult> TrackService(string trackingId)
    {
        var service = await _context.Services.FirstOrDefaultAsync(s => s.TrackingId == trackingId);
        if (service == null) return NotFound("Tracking ID not found.");
        return Ok(service);
    }

    // 📌 Submit feedback (Users)
    [HttpPost("feedback")]
    public async Task<IActionResult> SubmitFeedback([FromBody] FeedbackRequest request)
    {
        var feedback = new Feedback
        {
            UserName = request.UserName,
            UserEmail = request.UserEmail,
            OfficeId = request.OfficeId,
            Rating = request.Rating,
            Comment = request.Comment
        };

        _context.Feedbacks.Add(feedback);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFeedbackById), new { id = feedback.Id }, feedback);
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
