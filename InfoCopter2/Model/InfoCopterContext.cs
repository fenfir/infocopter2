using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using InfoCopter2.Model;

namespace InfoCopter2.Model
{
    class InfoCopterContext : DbContext
    {
        public InfoCopterContext()
        {
            Database.SetInitializer<InfoCopterContext>(null);
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
