using System;
using System.Web;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using TCI.TaskManager.Web.Core.Models;
using System.Data.Entity.Validation;

namespace TCI.TaskManager.Web.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public IDbSet<Task> Tasks { get; set; }

        public IDbSet<TaskType> TaskTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Task - TaskType relations
            modelBuilder.Entity<Task>()
                .HasRequired<TaskType>(t => t.TaskType)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Task>()
             .HasRequired<User>(t => t.ApplicationUser)
             .WithMany()
             .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaskType>()
              .HasRequired<User>(tt => tt.ApplicationUser)
              .WithMany()
              .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUserId = HttpContext.Current != null && HttpContext.Current.User != null
                ? HttpContext.Current.User.Identity.GetUserId()
                : "";

            foreach (var entity in entities)
            {
                ((BaseEntity)entity.Entity).LastUpdated = DateTime.Now;
                if (currentUserId != "")
                    ((BaseEntity)entity.Entity).LastUpdatedBy = currentUserId;            
            }
          
            return base.SaveChanges(); ;
        }
    }
}