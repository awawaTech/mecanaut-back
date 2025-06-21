using AwawaTech.Mecanaut.API.IAM.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.IAM.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Multitenancy;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context for the Learning Center Platform
/// </summary>
/// <param name="options">
///     The options for the database context
/// </param>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
   /// <summary>
   ///     On configuring the database context
   /// </summary>
   /// <remarks>
   ///     This method is used to configure the database context.
   ///     It also adds the created and updated date interceptor to the database context.
   /// </remarks>
   /// <param name="builder">
   ///     The option builder for the database context
   /// </param>
   protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

   /// <summary>
   ///     On creating the database model
   /// </summary>
   /// <remarks>
   ///     This method is used to create the database model for the application.
   /// </remarks>
   /// <param name="builder">
   ///     The model builder for the database context
   /// </param>
   protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // -------------------- IAM Context --------------------

        // Roles
        builder.Entity<Role>(e =>
        {
            e.HasKey(r => r.Id);
            e.Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
            e.Property(r => r.Name).IsRequired();
        });

        // Tenants
        builder.Entity<Tenant>(e =>
        {
            e.HasKey(t => t.Id);
            e.Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            e.Property(t => t.Ruc).IsRequired();
            e.Property(t => t.LegalName).IsRequired();
            e.Property(t => t.Code).IsRequired();

            // Email (value object as string)
            e.Property(t => t.Email)
             .HasConversion(
                 v => v.HasValue ? v.Value.Value : null,
                 v => string.IsNullOrEmpty(v) ? null : new EmailAddress(v!)
             )
             .HasColumnName("email")
             .HasMaxLength(255);

            // PhoneNumber (value object as string)
            e.Property(t => t.PhoneNumber)
             .HasConversion(
                 v => v.HasValue ? v.Value.Value : null,
                 v => string.IsNullOrEmpty(v) ? null : new PhoneNumber(v!)
             )
             .HasColumnName("phone_number")
             .HasMaxLength(25);
        });

        // Users
        builder.Entity<User>(e =>
        {
            e.HasKey(u => u.Id);
            e.Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
            e.Property(u => u.Username).IsRequired();
            e.Property(u => u.PasswordHash).IsRequired();

            e.Property(u => u.Email)
             .HasConversion(
                 v => v.HasValue ? v.Value.Value : null,
                 v => string.IsNullOrEmpty(v) ? null : new EmailAddress(v!)
             )
             .HasColumnName("email")
             .HasMaxLength(255);

            e.HasMany(u => u.Roles)
             .WithMany()
             .UsingEntity(j => j.ToTable("user_roles"));

            e.HasQueryFilter(u => u.TenantId == TenantContext.CurrentTenantId);
        });

        // Global naming convention (snake_case + pluralization)
        builder.UseSnakeCaseNamingConvention();
    }
}