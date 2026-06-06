using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using CardExplorer.Client.Core;
using CardExplorer.Data.Collections;
using CardExplorer.Data.Explorations;

namespace CardExplorer.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser<UserId>
{
    public List<Exploration> Explorations { get; set; } = [];
    public List<CardCollection> CardCollections { get; set; } =  [];
}

internal sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasValueGenerator<UserIdValueGenerator>();
        
        builder.HasMany(x => x.Explorations)
            .WithOne()
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.CardCollections)
            .WithOne()
            .HasForeignKey(x => x.UserId);
    }
}

public sealed class UserIdValueGenerator : ValueGenerator<UserId>
{
    public override UserId Next(EntityEntry entry)
    {
        return UserId.FromNewVersion7Guid();
    }

    public override bool GeneratesTemporaryValues => false;
}