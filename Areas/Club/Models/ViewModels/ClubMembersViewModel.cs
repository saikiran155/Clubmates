using System.Reflection.Metadata.Ecma335;

namespace ClubMates.Web.Areas.Club.Models.ViewModels
{
    public class ClubMembersViewModel
    {
        public int ClubMemberId { get; set; }
        public int? ClubId { get; set; }
        public string? ClubName { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
