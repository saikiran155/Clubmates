using System.ComponentModel.DataAnnotations;

namespace ClubMates.Web.Models
{
    public class Club
    {
        [Key]
        public int ClubId { get; set; }
        [Required]
        public string? ClubName { get; set; }
        public string? ClubDescription { get; set; }
        public ClubCategory CLUBCATEGORY { get; set; }
        public ClubType CLUBTYPE { get; set; }
        public string? ClubRules { get; set; }
        public ClubMatesUser? ClubManager { get; set; }
        public string? ClubContactNumber { get; set; }
        public string? ClubEmail { get; set; }
        public byte[]? ClubLogo { get; set; } = [];
        public byte[]? ClubBanner { get; set; } = [];
        public byte[]? ClubBackground { get; set; } = [];

    }
    public enum ClubCategory
    {
        Sports,
        Leisure,
        Entertainment,
        Educational,
        Research,
        Travel,
        Government
    }

    public enum ClubType
    {
        Public,
        Private
    }
}
