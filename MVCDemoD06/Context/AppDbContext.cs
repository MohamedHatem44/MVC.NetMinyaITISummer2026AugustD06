using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCDemoD06.Models;

namespace MVCDemoD06.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        /*------------------------------------------------------------------*/
        public AppDbContext() : base()
        {
        }
        /*------------------------------------------------------------------*/
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        /*------------------------------------------------------------------*/
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Configuration

            // Keep
            base.OnModelCreating(builder);
        }
        /*------------------------------------------------------------------*/
    }
}