using CliniApi.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace CliniApi.Api.Controllers
{

    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected IActionResult HandleResult(Result result)
        {
            if (result.StatusCode == StatusCodes.Status204NoContent)
                return NoContent();

            return StatusCode(result.StatusCode, result);
        }

        protected IActionResult HandleResult<T>(Result<T> result)
        {
            return StatusCode(result.StatusCode, result);
        }
    }
}