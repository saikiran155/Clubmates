﻿using System.Security.Claims;
using ClubMates.Web.AppDbContext;
using ClubMates.Web.Areas.Club.Controllers;
using ClubMates.Web.Areas.Club.Models;
using ClubMates.Web.Areas.Club.Services;
using ClubMates.Web.Models;
using ClubMates.Web.Models.ClubsViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClubMates.Web.Areas.Club.Controllers
{
    public class HomeController(
                    AppIdentityDbContext dbContext,
                    IClubLayoutService clubLayoutService) : ClubBaseController
    {
        private readonly AppIdentityDbContext _dbContext = dbContext;
        private readonly IClubLayoutService _clubLayoutService = clubLayoutService;
        public async Task<IActionResult> Index(int? clubId = 0)
        {
            if (!ModelState.IsValid)
                return Redirect("/Clubs/Index");
            var club = await _dbContext
                            .Clubs
                            .Include(x => x.ClubManager)
                            .FirstOrDefaultAsync(x => x.ClubId == clubId);

            var loggedInUserEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            if (loggedInUserEmail == null)
                return Redirect("/Account/Login");
            if (await _clubLayoutService.ValidateClubUser(loggedInUserEmail))
                return Redirect("/Account/Login");

            var clubLayout = await _clubLayoutService.PopulateClubLayout(loggedInUserEmail, clubId ?? 0);

            ViewBag.MainMenuItems = clubLayout.MainMenus;
            ViewBag.ImgSrc = clubLayout.Logo;
            ViewBag.ClubName = clubLayout.ClubName;
            var clubViewModel = new CustomerClubViewModel();
            if (club != null)
            {
                clubViewModel.ClubId = club.ClubId;
                clubViewModel.ClubName = club.ClubName;
                clubViewModel.ClubContactNumber = club.ClubContactNumber;
                clubViewModel.ClubManager = club.ClubManager?.Email;
                clubViewModel.ClubLogo = club.ClubLogo;
                clubViewModel.ClubBanner = club.ClubBanner;
                clubViewModel.ClubBackground = club.ClubBackground;
                clubViewModel.CLUBCATEGORY = club.CLUBCATEGORY;
                clubViewModel.CLUBTYPE = club.CLUBTYPE;
            }
            return View(clubViewModel);
        }
    }
}
