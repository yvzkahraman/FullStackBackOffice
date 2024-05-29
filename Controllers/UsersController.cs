using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BackOffice.Data;
using BackOffice.Entities;
using BackOffice.Interfaces;
using BackOffice.Repos;
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


    /// <summary>
    ///  EF => ADONET GELİŞTİRMESİDİR.
    /// </summary>

    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        // DI 

        private readonly IRepo repo;
        public UsersController(IRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {

           


            var result = this.repo.GetAll();

            return Ok(result);
        }

        
        [HttpGet("GetWithEf")]
        public IActionResult GetWithEf()
        {
            var list = this.repo.GetAll();
            return Ok(list);
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
        public IActionResult GetByFirstName(string name)
        {
            var user = DbData.UserList.FirstOrDefault(x => x.Name == name);
            if (user == null)
            {
                return NotFound("Böyle bir user bulunamadı, id =" + name);
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
            updatedUser.Name = user.Name;
            updatedUser.Surname = user.Surname;

            return NoContent();
        }

        [HttpPatch]
        public IActionResult Patch(AppUser user)
        {
            var updatedUser = DbData.UserList.FirstOrDefault(x => x.Id == user.Id);

            if (updatedUser == null)
                return NotFound();

            updatedUser.Name = user.Name;
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