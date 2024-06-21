namespace Erdmier.MediaOrganizer.Persistence.Configurations;

public sealed class ApplicationUserClaimConfiguration : IEntityTypeConfiguration<ApplicationUserClaim>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ApplicationUserClaim> builder)
    {
        builder.ToTable(name: "ApplicationUserClaims");

        builder.HasKey(auc => auc.Id);
    }
}
