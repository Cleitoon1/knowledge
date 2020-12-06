using System.Security.Claims;
using Domain.Models.Base;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public class KnowledgeContext : DbContext
    {
        // private readonly IHttpContextAccessor _httpContextAccessor;

        // public KnowledgeContext(DbContextOptions<KnowledgeContext> options, 
        //     IHttpContextAccessor httpContextAccessor)
        // : base(options)
        // {
        //     _httpContextAccessor = httpContextAccessor;
        // }

        public KnowledgeContext(DbContextOptions<KnowledgeContext> options)
        : base(options)
        {
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
                                break;
                        }
                    }
                }
            }
        }

        private long? GetCurrentUser()
        {
            long? id = (long?)null;            
            // if(this._httpContextAccessor != null && this._httpContextAccessor.HttpContext)
            // {
            //     HttpContext httpContext = this._httpContextAccessor.HttpContext;
            //     if(httpContext.User != null)
            //     {
            //         Claim claim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            //         id = claim != null ? long.Parse(claim.Value) : (long?)null;
            //     }
            // }            
            return id; 
        }
    }
}
