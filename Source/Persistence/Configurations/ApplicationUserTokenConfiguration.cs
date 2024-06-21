namespace Erdmier.MediaOrganizer.Persistence.Configurations;

public sealed class ApplicationUserTokenConfiguration : IEntityTypeConfiguration<ApplicationUserToken>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ApplicationUserToken> builder)
    {
        builder.ToTable(name: "ApplicationUserTokens");

        builder.HasKey(aut => new { aut.UserId, aut.LoginProvider, aut.Name });

        builder.Property(aut => aut.LoginProvider)
               .HasMaxLength(maxLength: 128);

        builder.Property(aut => aut.Name)
               .HasMaxLength(maxLength: 128);
    }
}
