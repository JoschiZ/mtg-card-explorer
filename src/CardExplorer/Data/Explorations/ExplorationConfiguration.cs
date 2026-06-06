using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CardExplorer.Data.Explorations;

public class ExplorationConfiguration : IEntityTypeConfiguration<Exploration>
{
    public void Configure(EntityTypeBuilder<Exploration> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        builder.Property(x => x.SearchString).HasMaxLength(1000).IsRequired();
        builder.HasMany(x => x.SeenCards).WithMany(x => x.Explorations);
    }
}