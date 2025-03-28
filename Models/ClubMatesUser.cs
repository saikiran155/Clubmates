using Microsoft.AspNetCore.Identity;

namespace ClubMates.Web.Models
{
    public class ClubMatesUser : IdentityUser
    {
        public ClubMatesRole Role { get; set; }
        public ClubMatesProficiency ClubMatesProficiency { get; set; }
        public string? AddressLine1 { get; set; } = string.Empty;
        public string? AddressLine2 { get; set; } = string.Empty;
        public string? AddressLine3 { get; set; } = string.Empty;
        public string? AddressLine4 { get; set; } = string.Empty;
    }
    public enum ClubMatesRole
    {
        User,
        Guest,
        ClubUser,
        SuperAdmin
    }

    public enum ClubMatesProficiency
    {
        Beginner,
        Intermediate,
        Advance,
        Expert
    }
}
