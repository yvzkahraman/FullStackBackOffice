using Microsoft.AspNetCore.Mvc;

namespace BackOffice.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase{

        [HttpGet]
        public IActionResult Get(){
            ReactDbContext context = new ReactDbContext();
            return Ok();
        }
    }
}