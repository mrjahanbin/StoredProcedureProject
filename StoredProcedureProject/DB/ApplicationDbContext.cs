using Microsoft.EntityFrameworkCore;
using StoredProcedureProject.Domains;
using static StoredProcedureProject.Controllers.StoredProcedureController;

namespace StoredProcedureProject.DB
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductViewModel> ProductViewModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductViewModel>().HasNoKey(); // اینجا کلید اصلی برای ProductViewModel غیرفعال می‌شود
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }



}
