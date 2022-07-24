using Microsoft.AspNetCore.Mvc;
using WebApiUnitTesting.Models;

namespace WebApiUnitTesting.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DataController : ControllerBase
{
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Data))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    public ActionResult<Data> Get(int id)
    {
        if (id <= 0)
        {
            return this.BadRequest(new ErrorDetails("invalid.id", "Id must be non-negative."));
        }

        if (id == 1)
        {
            return this.Ok(new Data(1, "one"));
        }
        else
        {
            return this.NotFound(new ErrorDetails("not.found", $"No data found for id {id}."));
        }
    }
}
