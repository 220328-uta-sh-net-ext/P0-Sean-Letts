using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public ResturantController(ResturantLogic resLogic)
        {
            this.resLogic = resLogic;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ResturantInfo>> Get()
        {
            return resLogic.GetAllResturants();
        }

        [HttpGet("Name")]
        [ProducesResponseType(200, Type = typeof(ResturantInfo))]
        [ProducesResponseType(404)]
        public ActionResult<ResturantInfo> GetN(string name)
        {
            var allUsers = resLogic.GetAllResturants();
            var answer = allUsers.Find(r => r.name.Contains(name));
            if (answer == null)
            {
                return NotFound($"Resturant named {name} not found in DB");
            }
            return Ok(answer);
        }

        [HttpGet("Address")]
        [ProducesResponseType(200, Type = typeof(ResturantInfo))]
        [ProducesResponseType(404)]
        public ActionResult<ResturantInfo> GetA(string address)
        {
            var allUsers = resLogic.GetAllResturants();
            var answer = allUsers.Find(r => r.address.Contains(address));
            if (answer == null)
            {
                return NotFound($"Address {address} not found in DB");
            }
            return Ok(answer);
        }

        [HttpGet("Zipcode")]
        [ProducesResponseType(200, Type = typeof(ResturantInfo))]
        [ProducesResponseType(404)]
        public ActionResult<ResturantInfo> GetZ(int zipcode)
        {
            var allUsers = resLogic.GetAllResturants();
            var answer = allUsers.Find(r => r.zipcode.Equals(zipcode));
            if (answer == null)
            {
                return NotFound($"Zipcode {zipcode} not found in DB");
            }
            return Ok(answer);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult Post([FromBody] ResturantInfo res)
        {
            if (res == null)
                return BadRequest("Invalid Resturant. Please try again with valid values");
            resLogic.addNewResturant(res);
            return CreatedAtAction("Get", res);
        }
    }
}
