using BusinessLayer.managers.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private IChaffeurManager _manager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IChaffeurManager man)
        {
            _logger = logger;
            _manager = man;

        }

        [HttpGet]
        public string Get()
        {
            return  _manager.GetChaffeurById(1).City;
        }
    }
}
