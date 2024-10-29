using DDD.Core.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoviBank.Domain.Wallets;

namespace ECB.Infrastructure.DataPersistence.TypeConfigurators;

public class WalletTypeConfigurator : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable("Wallets");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                val => DefaultGuidId.Create(val));

        builder.HasOne(w => w.Currency)
            .WithMany()
            .IsRequired();

        builder.Property(w => w.Balance)
            .HasPrecision(5,5)
            .IsRequired();
        
        
    }
}