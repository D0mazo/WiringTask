using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wiring.Tables
{
    public class HarnessWireConfiguration : IEntityTypeConfiguration<HarnessWires>
    {
        public void Configure(EntityTypeBuilder<HarnessWires> builder)
        {
            builder.ToTable("HarnessWires");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Harness_ID).IsRequired();
            builder.Property(x => x.Length).IsRequired();
            builder.Property(x => x.Color).HasMaxLength(30);
            builder.Property(x => x.Housing_1).HasMaxLength(30);
            builder.Property(x => x.Housing_2).HasMaxLength(30);

            builder.HasOne(x => x.HarnessDrawing).WithMany(x => x.HarnessWires).HasForeignKey(x => x.Harness_ID).OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class HarnessDrawingConfiguration : IEntityTypeConfiguration<HarnessDrawing>
    {
        public void Configure(EntityTypeBuilder<HarnessDrawing> builder)
        {
            builder.ToTable("HarnessDrawings");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Harness).HasMaxLength(30);
            builder.Property(x => x.Harness_version).HasMaxLength(30);
            builder.Property(x => x.Drawing).HasMaxLength(30);
            builder.Property(x => x.Drawing_version).HasMaxLength(30);
        }
    }
}