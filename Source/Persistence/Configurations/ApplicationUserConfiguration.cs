namespace Erdmier.MediaOrganizer.Persistence.Configurations;

public sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable(name: "ApplicationUsers");

        builder.HasKey(au => au.Id);

        builder.HasIndex(au => au.NormalizedUserName)
               .HasDatabaseName(name: "UserNameIndex")
               .IsUnique();

        builder.HasIndex(au => au.NormalizedEmail)
               .HasDatabaseName(name: "EmailIndex")
               .IsUnique();

        builder.Property(au => au.ConcurrencyStamp)
               .IsConcurrencyToken();

        builder.Property(au => au.UserName)
               .IsRequired();

        builder.Property(au => au.Email)
               .IsRequired();

        builder.Property(au => au.UserName)
               .HasMaxLength(maxLength: 256);

        builder.Property(au => au.NormalizedUserName)
               .HasMaxLength(maxLength: 256);

        builder.Property(au => au.Email)
               .HasMaxLength(maxLength: 256);

        builder.Property(au => au.NormalizedEmail)
               .HasMaxLength(maxLength: 256);

        builder.HasMany(au => au.Claims)
               .WithOne(auc => auc.User)
               .HasForeignKey(auc => auc.UserId)
               .IsRequired();

        builder.HasMany(au => au.Logins)
               .WithOne(aul => aul.User)
               .HasForeignKey(aul => aul.UserId)
               .IsRequired();

        builder.HasMany(au => au.Tokens)
               .WithOne(aut => aut.User)
               .HasForeignKey(aut => aut.UserId)
               .IsRequired();

        builder.HasMany(au => au.UserRoles)
               .WithOne(aur => aur.User)
               .HasForeignKey(aur => aur.UserId)
               .IsRequired();
    }
}
