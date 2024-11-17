namespace StoredProcedureProject.Controllers
{
    public partial class StoredProcedureController
    {
        public class ProductViewModel
        {
            public int TotalProducts { get; set; }
            public decimal AveragePrice { get; set; }
            public decimal MinPrice { get; set; }
            public decimal MaxPrice { get; set; }
        }
    }
}
