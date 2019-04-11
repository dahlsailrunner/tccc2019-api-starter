using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Tccc2019.ApiStarter.Models;

namespace Tccc2019.ApiStarter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThingiesController : ControllerBase
    {
        private readonly List<Thingy> _thingies = new List<Thingy>
        {
            new Thingy {Name = "Thing 1", Description = "Mayhem maker extraordinaire"},
            new Thingy {Name = "Thing 2", Description = "Partner in crime" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Thingy>> Get()
        {
            return _thingies;
        }

        [HttpGet("{id}")]
        public ActionResult<Thingy> GetWithId(int id)
        {
            return _thingies[id];  // not very safe
        }        
    }
}
