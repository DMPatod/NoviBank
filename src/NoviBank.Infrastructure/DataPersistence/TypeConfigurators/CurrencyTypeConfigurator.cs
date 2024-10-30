using DDD.Core.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoviBank.Domain.Currencies;

namespace ECB.Infrastructure.DataPersistence.TypeConfigurators;

public class CurrencyTypeConfigurator : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable("Currencies");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                val => DefaultGuidId.Create(val));

        builder.Property(c => c.Name)
            .IsRequired();

        builder.Property(c => c.Rate)
            .HasPrecision(18,5)
            .IsRequired();
    }
}