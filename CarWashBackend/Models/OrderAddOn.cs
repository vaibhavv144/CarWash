public class OrderAddOn
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int AddOnId { get; set; }
        public AddOn AddOn { get; set; }
    }