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
    }
}
