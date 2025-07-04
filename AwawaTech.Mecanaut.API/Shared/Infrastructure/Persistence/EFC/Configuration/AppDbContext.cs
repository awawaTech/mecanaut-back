using AwawaTech.Mecanaut.API.IAM.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.IAM.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Multitenancy;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Competency.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.Competency.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.ValueObjects;
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

        // ------------------ AssetManagement ------------------
        builder.Entity<Plant>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Id)
             .ValueGeneratedOnAdd()
             .HasColumnName("plant_id");
            e.Property(p => p.Name).IsRequired();

            // Tenant filter (manual when querying)

            // Value Objects
            e.OwnsOne(p => p.Location, l =>
            {
                l.Property(v => v.Address).HasColumnName("address");
                l.Property(v => v.City).HasColumnName("city");
                l.Property(v => v.Country).HasColumnName("country");
            });
            e.OwnsOne(p => p.ContactInfo, c =>
            {
                c.Property(v => v.Phone).HasColumnName("phone");
                c.Property(v => v.Email).HasColumnName("email");
            });

            e.Property(p => p.TenantId)
             .HasConversion(v => v.Value,
                            v => new TenantId(v))
             .HasColumnName("tenant_id");
        });

        builder.Entity<ProductionLine>(e =>
        {
            e.HasKey(pl => pl.Id);
            e.Property(pl => pl.Id)
             .ValueGeneratedOnAdd()
             .HasColumnName("production_line_id");
            e.Property(pl => pl.Name).IsRequired();
            e.Property(pl => pl.Code).IsRequired();

            // Capacity value object
            e.OwnsOne(pl => pl.Capacity, c =>
            {
                c.Property(v => v.UnitsPerHour).HasColumnName("units_per_hour");
            });

            e.Property(pl => pl.TenantId)
             .HasConversion(v => v.Value,
                            v => new TenantId(v))
             .HasColumnName("tenant_id");
        });

        builder.Entity<Machine>(e =>
        {
            e.HasKey(m => m.Id);
            e.Property(m => m.Id)
             .ValueGeneratedOnAdd()
             .HasColumnName("machine_id");
            e.Property(m => m.SerialNumber).IsRequired();
            e.Property(m => m.Name).IsRequired();
            e.HasIndex(m => m.SerialNumber).IsUnique();

            e.OwnsOne(m => m.Specs, s =>
            {
                s.Property(v => v.Manufacturer).HasColumnName("manufacturer");
                s.Property(v => v.Model).HasColumnName("model");
                s.Property(v => v.Type).HasColumnName("type");
                s.Property(v => v.PowerConsumption).HasColumnName("power_consumption");
            });
            e.OwnsOne(m => m.MaintenanceInfo, mi =>
            {
                mi.Property(v => v.LastMaintenance).HasColumnName("last_maintenance");
                mi.Property(v => v.NextMaintenance).HasColumnName("next_maintenance");
            });

            e.Property(m => m.TenantId)
             .HasConversion(v => v.Value,
                            v => new TenantId(v))
             .HasColumnName("tenant_id");
        });

        // ------------------ Competency ------------------
        builder.Entity<Skill>(e =>
        {
            e.HasKey(s => s.Id);
            e.Property(s => s.Id)
             .ValueGeneratedOnAdd()
             .HasColumnName("skill_id");

            e.Property(s => s.Name).IsRequired().HasMaxLength(100);
            e.Property(s => s.Description).HasMaxLength(255);
            e.Property(s => s.Category).HasMaxLength(50);

            e.Property(s => s.Status)
             .HasConversion<string>()
             .HasColumnName("status");

            e.Property(s => s.TenantId)
             .HasConversion(v => v.Value, v => new TenantId(v))
             .HasColumnName("tenant_id");

            e.HasIndex(s => new { s.Name, s.TenantId }).IsUnique();
        });

        // ------------------ ConditionMonitoring ------------------
        builder.Entity<MetricDefinition>(e =>
        {
            e.HasKey(md => md.Id);
            e.Property(md => md.Id).ValueGeneratedOnAdd().HasColumnName("metric_definition_id");
            e.Property(md => md.Name).IsRequired().HasMaxLength(100);
            e.Property(md => md.Unit).IsRequired().HasMaxLength(20);
            e.HasIndex(md => md.Name).IsUnique();
        });

        builder.Entity<MachineMetrics>(e =>
        {
            e.HasKey(mm => mm.Id);
            e.Property(mm => mm.Id).ValueGeneratedOnAdd().HasColumnName("machine_metrics_id");
            e.Property(mm => mm.MachineId).HasColumnName("machine_id").IsRequired();
            e.Property(mm => mm.TenantId)
             .HasConversion(v => v.Value, v => new TenantId(v))
             .HasColumnName("tenant_id");
            e.HasIndex(mm => mm.MachineId).IsUnique();

            // CurrentReadings se calcula en dominio; no se persiste directamente
            e.Ignore(mm => mm.CurrentReadings);

            // readings 1-N
            e.HasMany(mm => mm.Readings).WithOne().HasForeignKey("machine_metrics_id");
        });

        builder.Entity<MetricReading>(e =>
        {
            e.HasKey(r => r.Id);
            e.Property(r => r.Id).ValueGeneratedOnAdd().HasColumnName("metric_reading_id");
            e.Property(r => r.MachineId).HasColumnName("machine_id");
            e.Property(r => r.MetricId).HasColumnName("metric_id");
            e.Property(r => r.Value).HasColumnName("value");
            e.Property(r => r.MeasuredAt).HasColumnName("measured_at");
            e.Property(r => r.TenantId).HasColumnName("tenant_id");
            e.HasIndex(r => new { r.MachineId, r.MetricId, r.MeasuredAt })
             .HasDatabaseName("idx_machine_metric_time");
        });

        // ------------------ InventoryManagement ------------------
        builder.Entity<InventoryPart>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Id)
             .ValueGeneratedOnAdd()
             .HasColumnName("inventory_part_id");
            
            e.Property(p => p.PartNumber).IsRequired();
            e.Property(p => p.Name).IsRequired();
            e.Property(p => p.Description);
            
            // Value Objects
            e.Property(p => p.CurrentStock).HasColumnName("current_stock");
            e.Property(p => p.MinimumStock).HasColumnName("min_stock");
            e.Property(p => p.PlantId).HasColumnName("plant_id");
            e.Property(p => p.UnitPrice)
                .HasConversion(
                    price => price.Amount,
                    amount => new Money(amount, "PEN"))
                .HasColumnName("unit_price");

            e.HasIndex(p => new { p.PartNumber }).IsUnique();
        });

        builder.Entity<PurchaseOrder>(e =>
        {
            e.HasKey(po => po.Id);
            e.Property(po => po.Id)
             .ValueGeneratedOnAdd()
             .HasColumnName("purchase_order_id");
            
            e.Property(po => po.OrderNumber).IsRequired();
            e.Property(po => po.Status)
             .HasConversion<string>()
             .HasColumnName("status");
            e.Property(po => po.OrderDate).HasColumnName("order_date");
            e.Property(po => po.DeliveryDate).HasColumnName("delivery_date");
            e.Property(po => po.TotalPrice)
                .HasConversion(
                    price => price.Amount,
                    amount => new Money(amount, "PEN"))
                .HasColumnName("total_price");
            e.Property(po => po.PlantId).HasColumnName("plant_id");
            
        });

        builder.UseSnakeCaseNamingConvention();
    }

   // DbSets for AssetManagement
   public DbSet<Plant> Plants { get; set; } = null!;
   public DbSet<ProductionLine> ProductionLines { get; set; } = null!;
   public DbSet<Machine> Machines { get; set; } = null!;
   public DbSet<Skill> Skills { get; set; } = null!;
   public DbSet<MachineMetrics> MachineMetrics { get; set; } = null!;
   public DbSet<MetricDefinition> MetricDefinitions { get; set; } = null!;
   public DbSet<MetricReading> MetricReadings { get; set; } = null!;
   public DbSet<InventoryPart> InventoryParts { get; set; } = null!;
   public DbSet<PurchaseOrder> PurchaseOrders { get; set; } = null!;
}