using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User;

namespace ResturantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserLogic userLogic;

        public UserController(IUserLogic userLogic)
        {
            this.userLogic = userLogic;
        }

        private static List<UserInfo> users = new List<UserInfo>
        {
            new UserInfo{UserName = "SampleA", Password = "PasswordA", IsAdmin = false},
            new UserInfo{UserName = "SampleB", Password = "PasswordB", IsAdmin = false},
            new UserInfo{UserName = "SampleC", Password = "PasswordC", IsAdmin = false}
        };
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<UserInfo>> Get()
        {
            return userLogic.GetAllUsers();
            //return Ok(users);
        }
        [HttpGet("name")]
        [ProducesResponseType(200, Type = typeof(UserInfo))]
        [ProducesResponseType(404)]
        public ActionResult<UserInfo> Get(string name)
        {
            var allUsers = userLogic.GetAllUsers();
            var answer = allUsers.Find(r => r.UserName.Contains(name));
            if (answer == null)
            {
                return NotFound($"User {name} not found in DB");
            }
            return Ok(answer);
        }
        
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult Post([FromBody] UserInfo user)
        {
            if (user == null)
                return BadRequest("Invalid User. Please try again with valid values");
            userLogic.addNewUser(user);
            return CreatedAtAction("Get", user);
        }
    }
}
