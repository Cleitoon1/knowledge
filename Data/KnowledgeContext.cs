using System.Security.Claims;
using Domain.Models.Base;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.DTOs;

namespace Data
{
    public class KnowledgeContext : DbContext
    {
        private readonly LoggedUserDTO _loggedUserData;
        public KnowledgeContext(DbContextOptions<KnowledgeContext> options, LoggedUserDTO loggedUserData)
        : base(options)
        {
            _loggedUserData = loggedUserData;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Cascade;
            }
            modelBuilder.Ignore<Notification>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(KnowledgeContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is BaseEntity trackable)
                {
                    var now = DateTime.UtcNow;
                    var user = GetCurrentUser();
                    if(user != null)
                    {
                        switch (entry.State)
                        {
                            case EntityState.Modified:
                                trackable.SetModifiedBy(user);
                                break;

                            case EntityState.Added:
                                trackable.SetCreatedBy(user);
                                trackable.SetModifiedBy(user);
                                break;
                        }
                    }
                }
            }
        }

        private long? GetCurrentUser()
        {
            long? id = (long?)null;            
            if(_loggedUserData != null && _loggedUserData.Id > 0)
                id = _loggedUserData.Id;         
            return id; 
        }
    }
}
