using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resturant;
using Resturant.Resturant;

namespace ResturantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private ReviewLogic revLogic;

        public ReviewController(ReviewLogic resLogic)
        {
            this.revLogic = revLogic;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ReviewsInfo>> Get()
        {
            return revLogic.GetAllReviews();
        }
    }
}
