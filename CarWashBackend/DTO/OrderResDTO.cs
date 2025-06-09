public class OrderDto
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string WasherId { get; set; }
    public string WasherName { get; set; }
    public int CarId { get; set; }
    public int PackageId { get; set; }
    public int? PromoCodeId { get; set; }
    public DateTime ScheduledDate { get; set; }
    public string Status { get; set; }
    public decimal TotalAmount { get; set; }
}
