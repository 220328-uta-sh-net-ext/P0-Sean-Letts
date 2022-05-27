using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using ResturantAPI.Repository;
using User;

namespace ResturantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserLogic userLogic;
        private IMemoryCache memoryCache;
        private IJWTManagerRepo JWTrepo;

        private static List<UserInfo> users = new List<UserInfo>();
        /// <summary>
        /// Takes in the params to construct the User Controller
        /// </summary>
        /// <param name="userLogic"></param>
        /// <param name="memoryCache"></param>
        /// <param name="JWTrepo"></param>
        public UserController(UserLogic userLogic, IMemoryCache memoryCache, IJWTManagerRepo JWTrepo)
        {
            this.userLogic = userLogic;
            this.memoryCache = memoryCache;
            this.JWTrepo = JWTrepo;
        }
        /// <summary>
        /// Displays all Users. Must be an admin to see.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Get All Users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<UserInfo>> Get()
        {
            try
            {
                users = userLogic.GetAllUsers();
            }
            catch (Exception ex)
            {
                Log.Information("Bad Request exception in get user.");
                return BadRequest(ex.Message);
            }
            Log.Information("Good request at Get User.");
            return Ok(users);
        }

        /// <summary>
        /// Displays all Users with an asynchronous method. Must be an admin to see.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Async Get All Users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<UserInfo>>> GetAsync()
        {
            try
            {
                if (!memoryCache.TryGetValue("users", out users))
                {
                    users = await userLogic.GetAllUsersAsync();
                    memoryCache.Set("users", users, new TimeSpan(0, 1, 0));
                    Log.Information("Set users list in get user async.");
                }
            }
            catch (SqlException ex)
            {
                Log.Information("Not Found exception in get user async.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Information("Bad Request exception in get user async.");
                return BadRequest(ex.Message);
            }
            Log.Information("Success at get users async.");
            return Ok(users);
        }

        /// <summary>
        /// Displays all usernames with 'name' in it. Must be an admin to see.
        /// </summary>
        /// <param name="name">Takes in a username and checks to see if it's in the db</param>
        [Authorize(Roles = "Admin")]
        [HttpGet("name")]
        [ProducesResponseType(200, Type = typeof(UserInfo))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserInfo>> Get(string name)
        {
            try
            {
                if (!memoryCache.TryGetValue("users", out users))
                {
                    users = await userLogic.GetAllUsersAsync();
                    memoryCache.Set("users", users, new TimeSpan(0, 1, 0));
                    Log.Information("Set users in program.");
                }
            }
            catch (SqlException ex)
            {
                Log.Information("Not found exception in get user by name.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Information("Bad Request exception in get user by name.");
                return BadRequest(ex.Message);
            }
            var answer = users.Where(r => r.UserName.Contains(name)).ToList();
            if (answer == null || answer.Count < 1)
            {
                Log.Information($"Not Found {name} in get user by name.");
                return NotFound($"User {name} not found in DB");
            }
            Log.Information("Get username successful.");
            return Ok(answer);
        }
        /// <summary>
        /// Register a new user to the database
        /// </summary>
        /// <param name="user"></param>
        [HttpPost]
        [Route("Register New User")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult AddNewUser([FromQuery] UserInfo user)
        {
            if (user.UserName == String.Empty || user.Password == String.Empty)
            {
                Log.Information("Bad Request at add new user.");
                return BadRequest("Invalid User. Please try again with valid values");
            }
            if(user.UserName.ToLower() == "admin")
            {
                Log.Information("Illegal attempt at making admin account.");
                return BadRequest("Illegal attempt at making admin account");
            }
            try
            {
                userLogic.addNewUser(user);
            }
            catch(Exception ex)
            {
                Log.Information("Excetion occured in AddNewUser: " + ex);
            }
            Log.Information("New user created w/ username: " + user.UserName);
            return CreatedAtAction("Get", user);
        }
        /// <summary>
        /// Gets the authentication key needed to access other features in the program.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Authentication Key if you use a real username and password.</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult Authenticate([FromQuery] UserInfo user)
        {
            var token = JWTrepo.Authenticate(user);
            if(token == null)
            {
                Log.Information("Unauthorized user attempted access.");
                return Unauthorized("You do not have proper autherization to see this.");
            }
            Log.Information("Authorized user given token.");
            return Ok(token);
        }
    }
}
