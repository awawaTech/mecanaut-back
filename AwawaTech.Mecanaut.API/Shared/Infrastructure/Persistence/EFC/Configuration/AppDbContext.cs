// … using actuales …
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Aggregates;          // ← nuevos usings
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.Enums;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;

using Microsoft.EntityFrameworkCore;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

namespace AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options, ITenantProvider tenantProvider)
    : DbContext(options)
{
    private readonly ITenantProvider _tenantProvider = tenantProvider;

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* ========== AssetManagement ========== */
        // Value-object converters
        var plantIdConv       = new ValueConverter<PlantId, Guid>(v => v.Value, v => new PlantId(v));
        var lineIdConv        = new ValueConverter<ProductionLineId, Guid>(v => v.Value, v => new ProductionLineId(v));
        var machineryIdConv   = new ValueConverter<MachineryId, Guid>(v => v.Value, v => new MachineryId(v));
        var tenantIdConv      = new ValueConverter<TenantId, Guid>(v => v.Value, v => new TenantId(v));

        /* ---- Plant ---- */
        builder.Entity<Plant>(cfg =>
        {
            cfg.HasKey(p => p.Id);
            cfg.Property(p => p.Id).HasConversion(plantIdConv);
            cfg.Property(p => p.Tenant).HasConversion(tenantIdConv);
            cfg.Property(p => p.Status).HasConversion<int>();

            cfg.Property(p => p.Name).IsRequired().HasMaxLength(80);
            cfg.Property(p => p.Location).IsRequired().HasMaxLength(120);

            cfg.HasMany(p => p.Lines)
               .WithOne()
               .HasForeignKey(l => l.PlantId)
               .OnDelete(DeleteBehavior.Cascade);

            // Filtro multi-tenant
            cfg.HasQueryFilter(p => p.Tenant == _tenantProvider.Current);
        });

        /* ---- ProductionLine ---- */
        builder.Entity<ProductionLine>(cfg =>
        {
            cfg.HasKey(l => l.Id);
            cfg.Property(l => l.Id).HasConversion(lineIdConv);
            cfg.Property(l => l.Tenant).HasConversion(tenantIdConv);
            cfg.Property(l => l.PlantId).HasConversion(plantIdConv);
            cfg.Property(l => l.Status).HasConversion<int>();

            cfg.Property(l => l.Name).IsRequired().HasMaxLength(60);

            cfg.HasMany(l => l.Machinery)
               .WithOne()
               .HasForeignKey(m => m.LineId)
               .OnDelete(DeleteBehavior.Cascade);

            cfg.HasQueryFilter(l => l.Tenant == _tenantProvider.Current);
        });

        /* ---- Machinery ---- */
        builder.Entity<Machinery>(cfg =>
        {
            cfg.HasKey(m => m.Id);
            cfg.Property(m => m.Id).HasConversion(machineryIdConv);
            cfg.Property(m => m.Tenant).HasConversion(tenantIdConv);
            cfg.Property(m => m.LineId).HasConversion(lineIdConv);
            cfg.Property(m => m.Status).HasConversion<int>();

            cfg.Property(m => m.Model).IsRequired().HasMaxLength(60);
            cfg.Property(m => m.Brand).IsRequired().HasMaxLength(60);

            cfg.HasQueryFilter(m => m.Tenant == _tenantProvider.Current);
        });

        /* … mapeos ya existentes (Publishing, Profiles, IAM) … */

        builder.UseSnakeCaseNamingConvention();
    }

    /* DbSet propiedades opcionales para LINQ */
    public DbSet<Plant> Plants             => Set<Plant>();
    public DbSet<ProductionLine> Lines     => Set<ProductionLine>();
    public DbSet<Machinery> Machineries    => Set<Machinery>();
}
