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
            // ef core 'un o l
            // query ile işini tolist
            // var products = this.context.Products.AsQueryable();

            // var control = products is IEnumerable<Product>;
            // System.Console.WriteLine("control" + control);

            // var products2 = products.Where(x => x.Name.Contains("Mackbook"));

            // products2= products2.Where(x=>x.Name.Contains("a"));
            // return Ok(products2);

            //connected entitiy
            var products = context.Products.AsNoTracking().ToList();

            foreach (var product in products){
                product.Name="Yavuz";

                System.Console.WriteLine($"state {product.Id} {product.Name} => {context.Products.Entry(product).State}" );
            }

            context.SaveChanges();

            //TRACKING 

            //Disconnected
            // var product = new Product();

            // product.Name="Iphone 14";
            // product.Price =100000;
            // product.Stock = 3;

            // context.Products.Add(product);

            // var state = context.Products.Entry(product).State;

            // context.Products.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.


            // System.Console.WriteLine("state : "+state);


            

            return Ok(context.Products.ToList());


        }

        [HttpPost]
        public IActionResult Post(Product product)
        {
            this.context.Products.Add(product);
            this.context.SaveChanges();
            return Created("", product);
        }


    }
}