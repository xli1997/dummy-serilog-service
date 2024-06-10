using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Models;
using System.Collections.Generic;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FutureMappingController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<FutureMapping>> GetFutureMappings()
        {
            var futureMappings = new List<FutureMapping>
            {
                new FutureMapping { Id = 1, Name = "Crude Oil", Contract = "CL", ExpiryDate = new DateTime(2024, 12, 31), Price = 70.00m },
                new FutureMapping { Id = 2, Name = "Gold", Contract = "GC", ExpiryDate = new DateTime(2024, 12, 31), Price = 1800.00m },
            };

            return Ok(futureMappings);
        }
    }
}
