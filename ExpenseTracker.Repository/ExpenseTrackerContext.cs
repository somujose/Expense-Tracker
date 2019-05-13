using ExpenseTracker.Model.EF;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ExpenseTracker.Repository
{
    public class ExpenseTrackerContext : DbContext
    {
        public ExpenseTrackerContext()
            : base("ExpenseTrackerContext")
        {

        }

        public DbSet<ExpenseGroup> ExpenseGroups { get; set; }

        public DbSet<ExpenseUser> ExpenseUsers { get; set; }


        public DbSet<ExpenseItem> ExpenseItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpenseGroup>().Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ExpenseUser>().Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ExpenseItem>().Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
           
        }

        
    }

}
