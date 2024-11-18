using Microsoft.EntityFrameworkCore;
using StoredProcedureProject.DB;
using StoredProcedureProject.Domains;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(string category)
    {
        return await _context.Products
            .FromSqlInterpolated($"EXEC GetProductsByCategory {category}")
            .ToListAsync();
    }
}
