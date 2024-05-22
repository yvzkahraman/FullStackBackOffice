using BackOffice.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using Microsoft.VisualBasic;

namespace BackOffice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            //swagger request response mı bileyim. Cuma
            
            //İçerisine parametre olarak action alan bir method yav ve o metodu kullan.Cuma


            //burayı aynen yazıyorsun zipleyip teslim pazar()
            var result = DbData.UserList;

            return Ok(result);
        }


        // api/Users/{id}
        //api/Users?firstname = Yavuz  //query 
        //
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = DbData.UserList.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound("Böyle bir user bulunamadı, id =" + id);
            }

            return Ok(user);
        }

        //api/Users/GetByFirstName?firstname=Yavuz
        [HttpGet("GetByFirstName")]
        public IActionResult GetByFirstName(string firstname)
        {
            var user = DbData.UserList.FirstOrDefault(x => x.Firstname == firstname);
            if (user == null)
            {
                return NotFound("Böyle bir user bulunamadı, id =" + firstname);
            }

            return Ok(user);
        }

        //FROMBODY state management
        [HttpPost]
        public IActionResult Create(AppUser user)
        {

            DbData.UserList.Add(user);
            return Created(string.Empty, user);
        }

        [HttpPut]
        public IActionResult Put(AppUser user)
        {
            var updatedUser = DbData.UserList.FirstOrDefault(x => x.Id == user.Id);
            if (updatedUser == null)
                return NotFound("" + user.Id);
            updatedUser.Firstname = user.Firstname;
            updatedUser.Lastname = user.Lastname;

            return NoContent();
        }

        [HttpPatch]
        public IActionResult Patch(AppUser user)
        {
            var updatedUser = DbData.UserList.FirstOrDefault(x => x.Id == user.Id);

            if (updatedUser == null)
                return NotFound();

            updatedUser.Firstname = user.Firstname;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedUser = DbData.UserList.FirstOrDefault(x => x.Id == id);

            if (deletedUser == null)
                return NotFound();

            DbData.UserList.Remove(deletedUser);
            return NoContent();
        }

        // [HttpPost]
        // public IActionResult Create(){

        // }

        /* 
        
               var methodII = (AppUser x) =>
            {
                return true;
            };


            Action<AppUser> action = new Action<AppUser>(Method3);
            action+=Method4;

            result.ForEach(action);
        
           [HttpGet("3")]
        public void Method3(AppUser user)
        {
            System.Console.WriteLine(user.ToString());
        }

        [HttpGet("4")]
        public void Method4(AppUser user)
        {
            System.Console.WriteLine(user.Firstname);
        }
        */
    }
}