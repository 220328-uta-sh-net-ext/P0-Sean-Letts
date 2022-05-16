using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using Resturant;
using Resturant.Resturant;

namespace ResturantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private ReviewLogic revLogic;
        private IMemoryCache memoryCache;

        private static List<ReviewsInfo> revs = new List<ReviewsInfo>();

        public ReviewController(ReviewLogic revLogic, IMemoryCache memoryCache)
        {
            this.revLogic = revLogic;
            this.memoryCache = memoryCache;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ReviewsInfo>> Get()
        {
            try
            {
                revs = revLogic.GetAllReviews();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(revs);
        }

        [HttpGet]
        [Route("Async Get All Resturants")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ResturantInfo>>> GetAsync()
        {
            try
            {
                if (!memoryCache.TryGetValue("review", out revs))
                {
                    revs = await revLogic.GetAllReviewsAsync();
                    memoryCache.Set("review", revs, new TimeSpan(0, 1, 0));
                }
            }
            catch (SqlException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(revs);
        }
    }
}
