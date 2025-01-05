using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly BookStoreContext db;
        public static int PAGE_SIZE { get; set; } = 4;

        public HomeController(BookStoreContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult GetAll(string? search, double? from, double? to, string? sortBy, int page=1)
        {
            try
            {
                var sp = db.Books.AsQueryable();
                #region Filtering
                if (search != null)
                {
                    sp = sp.Where(p => p.Title.Contains(search));
                }

                if (from.HasValue)
                {
                    sp = sp.Where(p => p.Price >= from);
                }

                if (to.HasValue)
                {
                    sp = sp.Where(p => p.Price <= to);
                }
                #endregion

                #region Sorting
                if (!string.IsNullOrEmpty(sortBy))
                {
                    switch (sortBy)
                    {
                        case "ten_asc": sp = sp.OrderBy(p => p.Title); break;
                        case "ten_desc": sp = sp.OrderByDescending(p => p.Title); break;
                        case "gia_asc": sp = sp.OrderBy(sp => sp.Price); break;
                        case "gia_desc": sp = sp.OrderByDescending(sp => sp.Price); break;
                        default: sp = sp.OrderBy(p => p.Title); break;
                    }
                }
                #endregion

                #region Paging
                sp = sp.Skip((page-1)*PAGE_SIZE).Take(PAGE_SIZE);
                #endregion
                return Ok(sp);
            }
            catch
            {
                return BadRequest("We can't get the product");
            }
            
        }
    }
}
