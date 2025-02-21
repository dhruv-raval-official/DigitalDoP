using System.ComponentModel.DataAnnotations;

public class UpdateServiceRequest
{
    [Required]
    public string Status { get; set; }

    public DateTime? ArrivalDate { get; set; }
    public DateTime? DispatchDate { get; set; }
    public DateTime? DeliveredDate { get; set; }
}
