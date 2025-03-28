using System.ComponentModel.DataAnnotations;

namespace ClubMates.Web.Models
{
    public class PollResponse
    {
        [Key]
        public int ResponseId { get; set; }
        public Poll? Poll { get; set; }
        public ClubMatesUser? ClubmatesUser { get; set; }
        public List<PollOption>? PollOptions { get; set; }
    }
}
