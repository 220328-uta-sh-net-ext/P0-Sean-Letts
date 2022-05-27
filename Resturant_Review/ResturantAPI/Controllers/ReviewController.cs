using Microsoft.AspNetCore.Authorization;
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
        /// <summary>
        /// Displays all reviews
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get All Reviews")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ReviewsInfo>> Get()
        {
            try
            {
                revs = revLogic.GetAllReviews();
            }
            catch (Exception ex)
            {
                Log.Information("Bad request, get all reviews.");
                return BadRequest(ex.Message);
            }
            Log.Information("Good request, get all reviews.");
            return Ok(revs);
        }
        /// <summary>
        /// Displays all reviews w/ an asynchronous method
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Async Get All Reviews")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ResturantInfo>>> GetAsync()
        {
            try
            {
                if (!memoryCache.TryGetValue("review", out revs))
                {
                    revs = await revLogic.GetAllReviewsAsync();
                    memoryCache.Set("review", revs, new TimeSpan(0, 1, 0));
                    Log.Information("Set review memory.");
                }
            }
            catch (SqlException ex)
            {
                Log.Information("Not found, get all reviews async.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Information("Bad Request, get all reviews async.");
                return BadRequest(ex.Message);
            }
            Log.Information("Good request at get all reviews async.");
            return Ok(revs);
        }
        /// <summary>
        /// Add a new review. Must be logged in.
        /// </summary>
        /// <param name="ResturantName"></param>
        /// <param name="rating"></param>
        /// <param name="reviewtext"></param>
        /// <returns>Returns the review</returns>
        [Authorize]
        [HttpPost]
        [Route("Add New Review")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Post([FromQuery] string ResturantName, decimal rating, string reviewtext)
        {
            ReviewsInfo temp = new ReviewsInfo();
            try
            {
                temp = await revLogic.AddNewReviewAPI(ResturantName, rating, reviewtext);
            }
            catch(Exception ex)
            {
                Log.Information("Bad request, add new review.");
                return BadRequest(ex.Message);
            }
            Log.Information("Added new review.");
            return Ok(temp);
        }
    }
}
