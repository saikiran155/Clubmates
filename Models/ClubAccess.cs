using System.ComponentModel.DataAnnotations;

namespace ClubMates.Web.Models
{
    public class ClubAccess
    {
        [Key]
        public int ClubAccessId { get; set; }
        public Club? CLub { get; set; }
        public ClubMatesUser? ClubMatesUser { get; set; }
        public ClubAccessRole? ClubAccessRole { get; set; }

    }

    public enum ClubAccessRole
    {
        ClubMember,
        ClubManager,
        ClubAdmin
    }
}
