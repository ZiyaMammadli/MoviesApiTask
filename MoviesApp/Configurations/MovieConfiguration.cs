using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesApp.Entities;

namespace MoviesApp.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(m => m.Name).IsRequired().HasMaxLength(40);
            builder.Property(m => m.Desc).IsRequired().HasMaxLength(250);
            builder.Property(m => m.CostPrice).IsRequired();
            builder.Property(m=>m.SalePrice).IsRequired();
           
            builder.HasOne(x=>x.Genre).WithMany(x=>x.Movies).HasForeignKey(m=>m.GenreId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
