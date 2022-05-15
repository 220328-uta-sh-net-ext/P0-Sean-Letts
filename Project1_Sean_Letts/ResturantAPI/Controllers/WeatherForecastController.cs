using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Resturant;
using Resturant.Resturant;
using User;
namespace ResturantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        [HttpGet(Name = "Get Weather Forcasts")]
        [ProducesResponseType(200, Type = typeof(List<WeatherForecast>))]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        //private UserLogic ulogic;
        //private ResturantLogic reslogic;
        //private ReviewLogic revLogic;
        //private IMemoryCache memoryCache;
        /*
        public WeatherForecastController(UserLogic ulogic, ResturantLogic reslogic, ReviewLogic revLogic, IMemoryCache memoryCache)//Constructor dependency
        {
            this.ulogic = ulogic;
            this.reslogic = reslogic;
            this.revLogic = revLogic;
            this.memoryCache = memoryCache;
        }
        */
        /*
        [HttpGet(Name = "Get Resturants")]
        [ProducesResponseType(200, Type = typeof(List<ResturantInfo>))]
        public IEnumerable<ResturantInfo> Get()
        {
            List<ResturantInfo> result = reslogic.GetAllResturants();
            memoryCache.Set("resturants", result, new TimeSpan(0, 1, 0));
            return (IEnumerable<ResturantInfo>)Ok(result);
        }
        */
        /*
        [HttpGet(Name = "GetAllUsers")]
        public IEnumerable<UserInfo> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }*/
    }
}