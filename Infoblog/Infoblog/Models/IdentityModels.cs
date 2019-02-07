using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Infoblog.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Meetings = new HashSet<Meeting>();
        }
        [Display (Name = "Förnamn")]
        public string FirstName { get; set; }
        [Display (Name = "Efternamn")]
        public string LastName { get; set; }
        public virtual ICollection<Meeting> Meetings{ get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<FormalPostModel> Post { get; set; }
        public DbSet<EducationPostModel> EducationPosts { get; set; }
        public DbSet<ScienceModel> SciencePost { get; set; }
        public DbSet<InformalPostModel> InformalPost { get; set; }
        
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingPoll> MeetingPolls { get; set; }
        public DbSet<CategoryModel> Category { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}