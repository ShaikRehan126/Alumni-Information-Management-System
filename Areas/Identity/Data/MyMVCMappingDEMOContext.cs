using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyMVCMappingDEMO.Areas.Identity.Data;

namespace MyMVCMappingDEMO.Data;

public class MyMVCMappingDEMOContext : IdentityDbContext<MyMVCMappingDEMOUser>
{
    public MyMVCMappingDEMOContext(DbContextOptions<MyMVCMappingDEMOContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
