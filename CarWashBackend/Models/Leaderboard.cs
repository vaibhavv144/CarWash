using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Leaderboard{

     [Key]
    public int LeaderboardId{get;set;}

    [Required]
     [ForeignKey("User")]
    public string UserID { get; set; }
    public ApplicationUser User { get; set; }
    public int WaterSaved{get;set;}
   
}