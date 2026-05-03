using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SetExplorer.Client.Core.Collections;

namespace SetExplorer.Data.Collections;

public class CardCollectionConfiguration : IEntityTypeConfiguration<CardCollection>
{
    public void Configure(EntityTypeBuilder<CardCollection> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(50);
        
        builder.HasMany(x => x.Cards).WithMany(x => x.Collections);
        builder.HasMany(x => x.Explorations).WithMany(x => x.CardCollections);
    }
}