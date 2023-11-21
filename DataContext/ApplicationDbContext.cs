using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Emit;
using TasksControllerApp.Entities;
using TasksControllerApp.Models;

namespace TasksControllerApp.DataContext
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IServiceProvider services) : base(options)
        {
            Services = services;
        }

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Audit> AuditLogs { get; set; }

        public IServiceProvider Services { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration<IdentityRole>(new RoleConfiguration(Services));
        }
        public virtual async Task<int> SaveChangesAsync(string userId, string userName)
        {
            OnBeforeSaveChanges(userId, userName);
            var result = await base.SaveChangesAsync();
            return result;
        }
        private void OnBeforeSaveChanges(string userId, string userName)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;

                auditEntry.UserName = userName;
                auditEntry.UserId = userId;

                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue!;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue!;
                            break;

                        case EntityState.Deleted:
                            auditEntry.AuditType =AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue!;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType =AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue!;
                                if (property.CurrentValue != null)
                                    auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries)
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }
        }

    }
}
