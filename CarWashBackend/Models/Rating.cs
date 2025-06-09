using System.ComponentModel.DataAnnotations;

public class Rating{
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string WasherId { get; set; }
        public ApplicationUser Washer { get; set; }

        public int RatingValue { get; set; }
    }