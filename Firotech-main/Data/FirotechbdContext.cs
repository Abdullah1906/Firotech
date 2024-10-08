using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Firotechbd.Areas.Admin.Models;

namespace Firotechbd.Data;

public class FirotechbdContext : IdentityDbContext<IdentityUser>
{
    public FirotechbdContext(DbContextOptions<FirotechbdContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
       
    }
    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<BlogAuthor> BlogAuthors { get; set; }
    public DbSet<Comment> BlogComments { get; set; }
    public DbSet<Category> BlogCategories { get; set; }
    public DbSet<Tag> BlogTags { get; set; }
    public DbSet<Lookup> Lookup { get; set; }
    public DbSet<Firotechbd.Areas.Admin.Models.EmployeePubProfile>? EmployeePubProfile { get; set; }
}
