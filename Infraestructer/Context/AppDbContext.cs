using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infraestructer.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    
    

    protected override void OnModelCreating(ModelBuilder builder)
    {
        
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        
        //padronixacao de tabela
    
        var entities = builder.Model.GetEntityTypes().ToList();
        {
            foreach (var entity in entities)
            {
                var tableName = entity.GetTableName();

                // Check if the entity represents a join (relationship) table.
                var isJoinTable = entity.FindPrimaryKey()?.Properties.All(p => p.IsForeignKey()) ?? false;

                // Apply naming prefixes to tables if they don't already have one.
                if (tableName != null && !tableName.StartsWith("tb_") && !tableName.StartsWith("rtb_"))
                    entity.SetTableName(isJoinTable ? $"rtb_{tableName}" : $"tb_{tableName}");

                var properties = entity.GetProperties().ToList();

                foreach (var property in properties)
                {
                    var clrType = property.ClrType;

                    // Determine prefix based on the CLR property type.
                    var prefix =
                        clrType == typeof(string) ? "s" :
                        clrType == typeof(DateTime) || clrType == typeof(DateTime?) ? "dt" :
                        clrType == typeof(DateTimeOffset) || clrType == typeof(DateTimeOffset?) ? "dt" :
                        clrType == typeof(DateOnly) || clrType == typeof(DateOnly?) ? "dt" :
                        clrType == typeof(int) || clrType == typeof(int?) ? "i" :
                        clrType == typeof(Guid) || clrType == typeof(Guid?) ? "s" :
                        clrType == typeof(bool) || clrType == typeof(bool?) ? "b" :
                        clrType == typeof(decimal) || clrType == typeof(decimal?) ? "d" :
                        clrType == typeof(double) || clrType == typeof(double?) ? "d" :
                        clrType == typeof(float) || clrType == typeof(float?) ? "d" :
                        clrType == typeof(long) || clrType == typeof(long?) ? "i" :
                        clrType.IsEnum ? "s" :
                        string.Empty;

                    // Get the current column name in the database.
                    var currentColumnName =
                        property.GetColumnName(StoreObjectIdentifier.Table(entity.GetTableName()!));

                    if (currentColumnName != null)
                    {
                        string newColumnName;

                        // Remove prefixes from Value Objects (use last segment if it has underscores).
                        if (currentColumnName.Contains('_'))
                        {
                            var cleanName = currentColumnName.Split('_').Last();
                            newColumnName = prefix + cleanName;
                        }
                        else if (string.Equals(currentColumnName, property.Name, StringComparison.Ordinal))
                        {
                            newColumnName = prefix + property.Name;
                        }
                        else
                        {
                            newColumnName = currentColumnName;
                        }

                        property.SetColumnName(newColumnName);
                    }

                    // Apply default max length to string properties without a defined limit.
                    if (clrType == typeof(string) && property.GetMaxLength() == null)
                        property.SetMaxLength(256);
                }
            }
        }
    }
}