using BackOffice.Dtos;
using BackOffice.Interfaces;
using BackOffice.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly ReactDbContext context;

        public ProductsController(ReactDbContext context)
        {
            this.context = context;
        }

        // IQUERYABLE (sorgulanabilir) IENUMERABLE(sıralanabilir)
        [HttpGet]
        public IActionResult Get()
        {

            var products = context.Products.AsNoTracking().ToList();

            foreach (var product in products)
            {
                product.Name = "Yavuz";

                System.Console.WriteLine($"state {product.Id} {product.Name} => {context.Products.Entry(product).State}");
            }
            context.SaveChanges();
            return Ok(context.Products.AsNoTracking().ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = context.Products.AsNoTracking().SingleOrDefault(x => x.Id == id);

            if (product == null)
                return NotFound();


            var result = new ProductListDto(product.Id, product.Name, product.Price);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(ProductCreateDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock
            };
            this.context.Products.Add(product);
            this.context.SaveChanges();

            var productListDto = new ProductListDto(product.Id, product.Name, product.Price);

            return Created("", productListDto);
        }

        [HttpPut]
        public IActionResult Put(ProductUpdateDto dto)
        {

            var updatedEntity = this.context.Products.SingleOrDefault(x => x.Id == dto.Id);

            if (updatedEntity == null)
                return NotFound();

            updatedEntity.Name = dto.Name;
            updatedEntity.Price = dto.Price;
            updatedEntity.Stock = dto.Stock;

            var state = this.context.Entry(updatedEntity).State;

            System.Console.WriteLine("state :" + state);


            this.context.SaveChanges();

            return NoContent();

        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedEntity = this.context.Products.SingleOrDefault(x => x.Id == id);
            if (deletedEntity == null)
                return NotFound();

            this.context.Products.Remove(deletedEntity);
            int affectedRows = this.context.SaveChanges();
            if (affectedRows > 0)
            {
                return NoContent();
            }
            else
            {
                return Problem("bir problem oluştu");
            }


        }
    }
}