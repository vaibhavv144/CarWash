using System;
using System.Collections.Generic;




     public class CreateOrderDto
    {
       // public string UserId { get; set; }
        public int CarId { get; set; }
        public int PackageId { get; set; }
        public List<int> AddOnIds { get; set; } // This will be linked to OrderAddOns
        public int? PromoCodeId { get; set; }
        public DateTime ScheduledDate { get; set; }
        public decimal TotalAmount { get; set; }
    }