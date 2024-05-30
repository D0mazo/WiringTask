using Microsoft.EntityFrameworkCore;

namespace Wiring

{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<HarnessDrawing> HarnessDrawings { get; set; }
        public DbSet<HarnessWires> HarnessWires { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new Wiring.Tables.HarnessWireConfiguration());
            modelBuilder.ApplyConfiguration(new Wiring.Tables.HarnessDrawingConfiguration());
        }
    }
}