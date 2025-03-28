using System.Security.Cryptography;
using ClubMates.Web.Areas.Club.Models;

namespace ClubMates.Web.Areas.Club.Services
{
    public interface IClubLayoutService
    {
        public Task<ClubLayout> PopulateClubLayout(string loggedInUserEmail, int clubId);
        public Task<bool> ValidateClub(int? clubId);
        public Task<bool> ValidateClubUser(string? loggedInUserEmail);
    }
}
