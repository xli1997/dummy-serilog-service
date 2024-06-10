using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Models;
using System.Collections.Generic;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquityMappingController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<EquityMapping>> GetEquityMappings()
        {
            var equityMappings = new List<EquityMapping>
            {
                new EquityMapping { Id = 1, Name = "Apple Inc.", Symbol = "AAPL", Price = 150.00m },
                new EquityMapping { Id = 2, Name = "Microsoft Corp.", Symbol = "MSFT", Price = 250.00m },
            };

            return Ok(equityMappings);
        }
    }
}
