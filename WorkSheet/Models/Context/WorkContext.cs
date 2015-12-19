using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WorkSheet.Models.Entity;

namespace WorkSheet.Models.Context
{
    public class WorkContext : DbContext
    {

        public DbSet<Task> Task { get; set; }
        public DbSet<Job> Job { get; set; }

        public DbSet<User> User { get; set; }

        public System.Data.Entity.DbSet<WorkSheet.Models.Entity.LoginModel> LoginModels { get; set; }

        public DbSet<Menu> Menu { get; set; }

        public DbSet<JobGroup> JobGroup { get; set; }

        public DbSet<JobGroupUser> JobGroupUser { get; set; }

        public DbSet<Rule> Rule { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<RoleRule> RoleRule { get; set; }

        public DbSet<Product> Product { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<WorkContext>(null);
            //todo:prod da kapalı kalsın*******
            //Database.SetInitializer<WorkContext>(new DropCreateDatabaseIfModelChanges<WorkContext>());
            
            //tabloları -s takısı ile oluşturmasını önlemek için . Bunun yerine entity de table ismi verildi 
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Configure domain classes using Fluent API here
            //base.OnModelCreating(modelBuilder);
 
        }

        

    }
}