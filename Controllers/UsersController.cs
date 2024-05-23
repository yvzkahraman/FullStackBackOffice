using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BackOffice.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace BackOffice.Controllers
{

    //AuthController GetToken
    // User Login  

    [Authorize]
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

            // alışverişe çıktın eline bir sepet  => a markası süt 1 lt
            // eliine bir sepet => a markası süt 1lt 

            // senle bu adam yanı  ? 


            // AppUser user = new AppUser();

            // user.Username = "yavuz";
            // user.Password = "2";


            // AppUser user1 = new AppUser();
            // user1.Username = "yavuz";
            // user1.Password = "1";


            // var control = user.Equals(user1);

            //   System.Console.WriteLine("control"+control);


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

        [AllowAnonymous]
        //api/Users/Login POST
        [HttpPost("Login")]
        public IActionResult Login(UserLoginRequest request)
        {
            // request.Password => haslendikten => değer => verisetimi 

            var user = DbData.UserList.SingleOrDefault(x => x.Username == request.Username && x.Password == request.Password);
            if (user == null)
            {
                return BadRequest("Kullanıcı adı veya şifre hatalı");
            }


            // identity ile beraber token conf.
            // user => usertokens  token 
            // geleneksel 

            /// 30 =>  hesabını donduracağım. 10 saat  10 saat  datetime.now => türkiye saatine göre, 10 saat ileri  


            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YavuzyavuzyavuzA.YavuzyavuzyavuzAYavuzyavuzyavuzA.YavuzyavuzyavuzA")), SecurityAlgorithms.HmacSha256);

        


            var jwtToken = new JwtSecurityToken(issuer: "http://localhost", audience: "http://localhost", claims: null, notBefore: DateTime.UtcNow, expires: DateTime.UtcNow.AddDays(30), signingCredentials: signingCredentials);


            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtToken);




            return Created("", token);
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

        public record UserLoginRequest(string Username, string Password);
    }

}