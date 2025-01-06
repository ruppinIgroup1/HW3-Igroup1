using Microsoft.AspNetCore.Mvc;
using STEAM.Models;
using System.Xml.Linq;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace STEAM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // POST api/<UsersController>
        [HttpPost("AddNewUser")]
        public UserClass AddNewUser(string Name, string Email, string Password)
        {
            UserClass user = new UserClass();
            return user.AddNewUser(Name, Email, Password);
        }

        [HttpPost("LoginUser")]
        public UserClass LoginUser(string Email, string Password)
        {
            UserClass user = new UserClass();
            return user.LoginUser(Email, Password);
        }


        // GET: api/<UsersController>
        [HttpGet]
        public List<UserClass> MyUsers()
        {
            UserClass user = new UserClass();
            return user.MyUsers();
        }


        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }



        // PUT api/<UsersController>/5
        [HttpPut("updateUser/{id}")]
        public int UpdateUser(int id, [FromBody] UserClass user)
        {
            DBservices dbs = new DBservices();
            return dbs.UpdateUser(id, user.Email, user.Password, user.Name);
        }



        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
