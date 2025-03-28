using System.Security.Claims;
using ClubMates.Web.AppDbContext;
using ClubMates.Web.Models;
using ClubMates.Web.Models.AdminViewModel;
using ClubMates.Web.Models.ClubsViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClubMates.Web.Controllers
{
    [Authorize("MustBeAGuest")]
    public class ClubsController(AppIdentityDbContext dbContext, UserManager<ClubMatesUser> userManager) : Controller
    {


        private readonly AppIdentityDbContext _dbContext = dbContext;
        private readonly UserManager<ClubMatesUser> _userManager = userManager;

        public async Task<IActionResult> Index()
        {
            var listOfClubs = await _dbContext
                .Clubs
                .Include(x => x.ClubManager)
                .ToListAsync();

            var listOfClubsViewModel = listOfClubs.Select(Club => new CustomerClubViewModel
            {
                ClubId = Club.ClubId,
                ClubName = Club.ClubName,
                ClubDescription = Club.ClubDescription,
                CLUBTYPE = Club.CLUBTYPE,
                ClubRules = Club.ClubRules,
                ClubManager = Club.ClubManager?.Email,
                ClubContactNumber = Club.ClubContactNumber,
                ClubEmail = Club.ClubEmail,
                ClubLogo = Club.ClubLogo,
                ClubBanner = Club.ClubBanner,
                ClubBackground = Club.ClubBackground
            }).ToList();
            return View(listOfClubsViewModel);
        }

        public async Task<IActionResult> ClubDetails(int clubId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var clubDetails = await _dbContext
                .Clubs
                .Include(x => x.ClubManager)
                .FirstOrDefaultAsync(x => x.ClubId == clubId);

            if (clubDetails == null)
            {
                return RedirectToAction("Index");
            }
            var clubDetailsViewModel = new CustomerClubViewModel()
            {
                ClubId = clubDetails.ClubId,
                ClubName = clubDetails.ClubName,
                ClubDescription = clubDetails.ClubDescription,
                CLUBCATEGORY = clubDetails.CLUBCATEGORY,
                CLUBTYPE = clubDetails.CLUBTYPE,
                ClubRules = clubDetails.ClubRules,
                ClubManager = clubDetails.ClubManager?.Email,
                ClubContactNumber = clubDetails.ClubContactNumber,
                ClubEmail = clubDetails.ClubEmail,
                ClubBackground = clubDetails.ClubBackground
            };
            return View(clubDetailsViewModel);
        }


        public IActionResult CreateClubForCustomer()
        {
            return View(new CustomerClubViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateClubForCustomer(
            CustomerClubViewModel customerClubViewModel,
            IFormFile clubLogo,
            IFormFile clubBanner,
            IFormFile clubBackground)
        {
            if (!ModelState.IsValid)
            {
                return View(customerClubViewModel);
            }
            var loggedInEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            if (loggedInEmail == null)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = "/Clubs/CreateClubForCustomer" });
            }

            var loggedInUser = await _userManager.FindByEmailAsync(loggedInEmail);
            if (customerClubViewModel != null && loggedInUser != null)
            {
                Club club = new()
                {
                    ClubName = customerClubViewModel.ClubName,
                    ClubDescription = customerClubViewModel.ClubDescription,
                    CLUBCATEGORY = customerClubViewModel.CLUBCATEGORY,
                    CLUBTYPE = customerClubViewModel.CLUBTYPE,
                    ClubRules = customerClubViewModel.ClubRules,
                    ClubManager = loggedInUser,
                    ClubContactNumber = customerClubViewModel.ClubContactNumber,
                    ClubEmail = customerClubViewModel.ClubEmail
                };

                if (clubLogo != null)
                {
                    using var memoryStream = new MemoryStream();
                    await clubLogo.CopyToAsync(memoryStream);
                    club.ClubLogo = memoryStream.ToArray();
                }

                if (clubBanner != null)
                {
                    using var memoryStream = new MemoryStream();
                    await clubBanner.CopyToAsync(memoryStream);
                    club.ClubBanner = memoryStream.ToArray();
                }

                if (clubBackground != null)
                {
                    using var memoryStream = new MemoryStream();
                    await clubBackground.CopyToAsync(memoryStream);
                    club.ClubBackground = memoryStream.ToArray();
                }

                var createdClubEntity = _dbContext.Clubs.Add(club);
                await _dbContext.SaveChangesAsync();

                if (createdClubEntity != null)
                {
                    var createdClub = await _dbContext.Clubs.FindAsync(createdClubEntity.Entity.ClubId);
                    if (createdClub != null)
                    {
                        bool isClubRoleAvailable = false;
                        if (await _userManager.GetClaimsAsync(loggedInUser) != null)
                        {
                            var userClaims = await _userManager.GetClaimsAsync(loggedInUser);
                            foreach (var claim in userClaims)
                            {
                                if (claim.Value == Enum.GetName(ClubMatesRole.ClubUser))
                                {
                                    isClubRoleAvailable = true;
                                }
                            }
                            if (!isClubRoleAvailable)
                            {
                                await _userManager
                                            .AddClaimAsync(loggedInUser,
                                            new(ClaimTypes.Role, Enum.GetName(ClubMatesRole.ClubUser) ?? ""));
                            }
                        }

                        loggedInUser.Role = ClubMatesRole.ClubUser;

                        await _userManager.UpdateAsync(loggedInUser);

                        _dbContext.ClubsAccesses.Add(new ClubAccess
                        {
                            CLub = createdClub,
                            ClubMatesUser = loggedInUser,
                            ClubAccessRole = ClubAccessRole.ClubManager
                        });
                        await _dbContext.SaveChangesAsync();
                    }

                }

                return RedirectToAction("Index");
            }
            return View(customerClubViewModel);
        }
    }
}
