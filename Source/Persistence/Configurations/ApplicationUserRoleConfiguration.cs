namespace Erdmier.MediaOrganizer.Persistence.Configurations;

public sealed class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
    {
        builder.ToTable(name: "ApplicationUserRoles");

        builder.HasKey(aur => new { aur.UserId, aur.RoleId });
    }
}
