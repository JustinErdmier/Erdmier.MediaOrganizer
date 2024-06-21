namespace Erdmier.MediaOrganizer.Persistence.Configurations;

public sealed class ApplicationUserLoginConfiguration : IEntityTypeConfiguration<ApplicationUserLogin>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ApplicationUserLogin> builder)
    {
        builder.ToTable(name: "ApplicationUserLogins");

        builder.HasKey(aul => new { aul.LoginProvider, aul.ProviderKey });

        builder.Property(aul => aul.LoginProvider)
               .HasMaxLength(maxLength: 128);

        builder.Property(aul => aul.ProviderKey)
               .HasMaxLength(maxLength: 128);
    }
}
