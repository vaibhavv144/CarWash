using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class WashRequest
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string WasherId { get; set; }
        public ApplicationUser Washer { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }

        public int PackageId { get; set; }
        public ServicePackage Package { get; set; }

        public int? PromoCodeId { get; set; }
        public PromoCode PromoCode { get; set; }

        public DateTime ScheduledDate { get; set; }

        public string Status { get; set; } = "PENDING"; // PENDING, ACCEPTED, DECLINED

        public ICollection<OrderAddOn> OrderAddOns { get; set; }
    }