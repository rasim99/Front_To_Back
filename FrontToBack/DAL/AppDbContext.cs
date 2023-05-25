using FrontToBack.Models;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderContent> SliderContents { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<ExpertContent> ExpertContents { get; set; }
        public DbSet<Say> Says { get; set; }
        public DbSet<Instagram> Instagrams { get; set; }

    }
}
