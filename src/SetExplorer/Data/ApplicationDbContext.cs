using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SetExplorer.Client.Core;
using SetExplorer.Client.Core.Cards;

namespace SetExplorer.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, IdentityRole<UserId>, UserId>(options)
{
    public DbSet<Card> Cards { get; set; }
    
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.RegisterAllInVogenEfCoreConverter();
        base.ConfigureConventions(configurationBuilder);
    }
}
