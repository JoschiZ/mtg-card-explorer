using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SetExplorer.Client.Core.Explorations;

namespace SetExplorer.Data.Explorations;

public class ExplorationConfiguration : IEntityTypeConfiguration<Exploration>
{
    public void Configure(EntityTypeBuilder<Exploration> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        builder.Property(x => x.SearchString).HasMaxLength(100).IsRequired();
        builder.HasMany(x => x.SeenCards).WithMany(x => x.Explorations);
    }
}