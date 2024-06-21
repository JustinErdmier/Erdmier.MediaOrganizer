namespace Erdmier.MediaOrganizer.Persistence.Configurations;

public sealed class ApplicationRoleClaimConfiguration : IEntityTypeConfiguration<ApplicationRoleClaim>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ApplicationRoleClaim> builder)
    {
        builder.ToTable(name: "ApplicationRoleClaims");

        builder.HasKey(arc => arc.Id);
    }
}
