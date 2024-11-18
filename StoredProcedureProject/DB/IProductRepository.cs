using StoredProcedureProject.Domains;

public interface IProductRepository
{
    Task<List<Product>> GetProductsByCategoryAsync(string category);
}
