using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using Resturant;

namespace ResturantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResturantController : ControllerBase
    {
        private const string filepath = "C:/Users/Owner/Desktop/Revature/Sean-Letts/Project0_Sean_Letts/User/UserDatabase/";
        private readonly string connectionString;

        private ResturantLogic resLogic;
        private IMemoryCache memoryCache;

        private static List<ResturantInfo> rests = new List<ResturantInfo>();
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="resLogic"></param>
        /// <param name="memoryCache"></param>
        public ResturantController(ResturantLogic resLogic, IMemoryCache memoryCache)
        {
            this.resLogic = resLogic;
            this.memoryCache = memoryCache;
        }
        /// <summary>
        /// Returns all Resturants
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get All Resturants")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ResturantInfo>> Get()
        {
            try
            {
                rests = resLogic.GetAllResturants();
            }
            catch (Exception ex)
            {
                Log.Information("Exception occured in Resturant controller @ get. Exception:" + ex);
                return BadRequest(ex.Message);
            }
            Log.Information("Entered and exited get resturants with no troubles.");
            return Ok(rests);
        }
        /// <summary>
        /// Returns all resturants w/ an asynchronous method
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Async Get All Resturants")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ResturantInfo>>> GetAsync()
        {
            try
            {
                if (!memoryCache.TryGetValue("resturant", out rests))
                {
                    rests = await resLogic.GetAllResturantsAsync();
                    memoryCache.Set("resturant", rests, new TimeSpan(0, 1, 0));
                    Log.Information("Resturant memory cache reset.");
                }
            }
            catch (SqlException ex)
            {
                Log.Information("NotFound occured in Resturant controller @ get async. Exception:" + ex);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Information("BadRequest occured in Resturant controller @ get async. Exception:" + ex);
                return BadRequest(ex.Message);
            }
            Log.Information("GetAsync Resturant was a success");
            return Ok(rests);
        }
        /// <summary>
        /// Find a resturant by Name. Displays all resturants w/ matching name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("Search By Name")]
        [ProducesResponseType(200, Type = typeof(ResturantInfo))]
        [ProducesResponseType(404)]
        public ActionResult<ResturantInfo> GetN(string name)
        {
            var allUsers = resLogic.GetAllResturants();
            var answer = allUsers.Find(r => r.name.Contains(name));
            if (answer == null)
            {
                Log.Information($"{name} not found in Search by Name in resturant controller");
                return NotFound($"Resturant named {name} not found in DB");
            }
            Log.Information("Search Resturant by name was a success");
            return Ok(answer);
        }
        /// <summary>
        /// Find a resturant by Address. Displays all resturants w/ matching address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpGet("Search By Address")]
        [ProducesResponseType(200, Type = typeof(ResturantInfo))]
        [ProducesResponseType(404)]
        public ActionResult<ResturantInfo> GetA(string address)
        {
            var allUsers = resLogic.GetAllResturants();
            var answer = allUsers.Find(r => r.address.Contains(address));
            if (answer == null)
            {
                Log.Information($"{address} not found in Search by Address in resturant controller");
                return NotFound($"Address {address} not found in DB");
            }
            Log.Information("Search Resturant by Address was a success");
            return Ok(answer);
        }
        /// <summary>
        /// Find a resturant by Zipcode. Displays all resturants w/ matching zipcode
        /// </summary>
        /// <param name="zipcode"></param>
        /// <returns></returns>
        [HttpGet("Search By Zipcode")]
        [ProducesResponseType(200, Type = typeof(ResturantInfo))]
        [ProducesResponseType(404)]
        public ActionResult<ResturantInfo> GetZ(int zipcode)
        {
            var allUsers = resLogic.GetAllResturants();
            var answer = allUsers.Find(r => r.zipcode.Equals(zipcode));
            if (answer == null)
            {
                Log.Information($"{zipcode} not found in Search by Zipcode in resturant controller");
                return NotFound($"Zipcode {zipcode} not found in DB");
            }
            Log.Information("Search Resturant by Zipcode was a success");
            return Ok(answer);
        }
        /// <summary>
        /// Add a new Resturant to the db. Requires logging in.
        /// </summary>
        /// <param name="res"></param>
        /// <returns> 201 if created, 400 if bad request</returns>
        [Authorize]
        [HttpPost]
        [HttpGet("Add new Resturant")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult Post([FromBody] ResturantInfo res)
        {
            if (res == null)
            {
                Log.Information("Bad Request to add a new resturant.");
                return BadRequest("Invalid Resturant. Please try again with valid values");
            }
            Log.Information("Added new resturant.");
            resLogic.addNewResturant(res);
            return CreatedAtAction("Get", res);
        }
    }
}
