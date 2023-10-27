using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MyDemo.Core.Data.Entity;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
    { }

    // private User _currentUser;

    // public ApplicationDbContext(IHttpContextAccessor httpContextAccessor)
    //   :base()
    // { 
    //     var user = httpContextAccessor.HttpContext?.User?.Identity;
    //     if (user != null)
    //     {
    //         _currentUser = this.Users.FirstOrDefault(u => u.Email == user.Name && !u.IsDeleted);
    //     }
    // }

    // public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
    //   :base(options)
    // { 
    //     var user = httpContextAccessor.HttpContext?.User?.Identity;
    //     if (user != null)
    //     {
    //         _currentUser = this.Users.FirstOrDefault(u => u.Email == user.Name && !u.IsDeleted);
    //     }
    // }

    //  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    //   :base(options)
    // { 

    // }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // add the EntityTypeConfiguration classes
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly
        );
    }
}
