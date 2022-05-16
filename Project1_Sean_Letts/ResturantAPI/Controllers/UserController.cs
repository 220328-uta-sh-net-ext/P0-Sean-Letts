using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Memory;
using User;

namespace ResturantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserLogic userLogic;
        //private IMemoryCache memoryCache;

        public UserController(UserLogic userLogic)
        {
            this.userLogic = userLogic;
            //this.memoryCache = memoryCache;
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
            try
            {
                users = userLogic.GetAllUsers();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(users);
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
        [Route("Add New User")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult AddNewUser([FromQuery] UserInfo user)
        {
            if (user == null)
                return BadRequest("Invalid User. Please try again with valid values");
            userLogic.addNewUser(user);
            return CreatedAtAction("Get", user);
        }

        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult Login([FromQuery] UserInfo user)
        {
            //TODO Fix
            if (user == null)
                return BadRequest("Invalid User. Please try again with valid values");
            users = userLogic.GetAllUsers();
            if (users.Contains(user))
                return Ok(user);
            return BadRequest("Wrong Credentials.");
        }
        /*
        [HttpPost]
        public ActionResult Login([FromQuery] string UserName, [FromQuery] string Password)
        {
            UserInfo user = new UserInfo();
            user.UserName = UserName;
            user.Password = Password;
            if (user.UserName == "Admin")
                user.IsAdmin = true;
            else
                user.IsAdmin = false;
            users = userLogic.GetAllUsers();
            if (users.Contains(user))
                return Ok(user);
            return BadRequest("Wrong Credentials.");
        }*/
    }
}
