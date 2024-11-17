using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StoredProcedureProject.DB;
using StoredProcedureProject.Domains;
using System.Data;

namespace StoredProcedureProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class StoredProcedureController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public StoredProcedureController(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// استفاده از 
        /// DbContext
        /// و اجرای 
        /// Stored Procedure
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get001")]
        public async Task<IActionResult> Get1()
        {
            var Result = string.Empty;

            var category = "Electronics"; // مقدار پارامتر
            var products = await _context.Products
                .FromSqlInterpolated($"EXEC GetProductsByCategory {category}")
                .ToListAsync();




            foreach (var product in products)
            {
                Result += $"Name: {product.Name}, Price: {product.Price}";
            }

            return Ok(Result);
        }




        /// <summary>
        ///  استفاده از 
        ///  ExecuteSqlInterpolated
        ///  برای
        ///  
        /// Stored Procedure
        /// هایی که دیتا برنمی‌گردانند
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get002")]
        public async Task<IActionResult> Get2()
        {
            var Result = string.Empty;

            await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC DeleteProduct");

            return Ok(Result);
        }




        /// <summary>
        /// تعریف 
        /// ViewModel 
        /// برای مقادیر برگردانده شده
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get003")]
        public async Task<IActionResult> Get3()
        {
            var Result = string.Empty;

            var products = await _context.ProductViewModels
                .FromSqlInterpolated($"EXEC GetProductSummary")
                .ToListAsync();

            foreach (var product in products)
            {
                Result += $"TotalProducts: {product.TotalProducts}, AveragePrice: {product.AveragePrice},MinPrice: {product.MinPrice}, MaxPrice: {product.MaxPrice}";
            }

            return Ok(Result);
        }


        /// <summary>
        /// اضافه کردن پارامترهای ورودی/خروجی
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get004/{productId}")]
        public async Task<IActionResult> Get4(int productId)
        {
            // تعریف پارامتر خروجی برای تعداد سفارشات
            var outputParam = new SqlParameter("@OrderCount", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            // فراخوانی استور پروسیجر و ارسال پارامتر ورودی و خروجی
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC GetOrderCount @ProductId, @OrderCount OUTPUT",
                new SqlParameter("@ProductId", productId),
                outputParam
            );

            // دریافت تعداد سفارشات از خروجی
            int orderCount = (int)outputParam.Value;

            // بازگشت تعداد سفارشات
            return Ok(orderCount);
        }




    }
}
