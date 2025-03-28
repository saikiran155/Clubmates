using ClubMates.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ClubMates.Web.AppDbContext
{

    public class AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : IdentityDbContext<ClubMatesUser>(options)

    {
        public DbSet<Club> Clubs {  get; set; }
        public DbSet<ClubAccess> ClubsAccesses { get; set; }

        public DbSet<ClubEvent> ClubEvents { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollOption> PollOptions { get; set; }
        public DbSet<PollResponse> PollResponses { get; set; }
    }

}