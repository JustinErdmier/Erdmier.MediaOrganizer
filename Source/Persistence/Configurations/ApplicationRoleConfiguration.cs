namespace Erdmier.MediaOrganizer.Persistence.Configurations;

public sealed class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.ToTable(name: "ApplicationRoles");

        builder.HasIndex(ar => ar.Id);

        builder.HasIndex(ar => ar.NormalizedName)
               .HasDatabaseName(name: "RoleNameIndex")
               .IsUnique();

        builder.Property(ar => ar.ConcurrencyStamp)
               .IsConcurrencyToken();

        builder.Property(ar => ar.Name)
               .HasMaxLength(maxLength: 256);

        builder.Property(ar => ar.NormalizedName)
               .HasMaxLength(maxLength: 256);

        builder.HasMany(ar => ar.UserRoles)
               .WithOne(aur => aur.Role)
               .HasForeignKey(aur => aur.RoleId)
               .IsRequired();

        builder.HasMany(ar => ar.RoleClaims)
               .WithOne(arc => arc.Role)
               .HasForeignKey(arc => arc.RoleId)
               .IsRequired();
    }
}
