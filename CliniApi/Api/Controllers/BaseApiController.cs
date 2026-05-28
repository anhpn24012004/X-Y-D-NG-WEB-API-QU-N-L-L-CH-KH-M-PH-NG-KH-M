using CliniApi.Application.Common;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Reflection;

namespace CliniApi.Api.Controllers
{

    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected IActionResult HandleResult(Result result)
        {
            if (result.StatusCode == StatusCodes.Status204NoContent)
                return NoContent();

            return BuildResult(result, result.StatusCode);
        }

        protected IActionResult HandleResult<T>(Result<T> result)
        {
            if (result.StatusCode == StatusCodes.Status204NoContent)
                return NoContent();

            return BuildResult(result, result.StatusCode);
        }

        private IActionResult BuildResult(object result, int statusCode)
        {
            var accept = Request.Headers.Accept.ToString();

            if (string.IsNullOrWhiteSpace(accept) || accept.Contains("*/*"))
            {
                return new ObjectResult(result)
                {
                    StatusCode = statusCode
                };
            }

            var bestMediaType = GetBestAcceptedMediaType(accept);
            var data = GetData(result);

            if (bestMediaType == "application/xml" || bestMediaType == "text/csv")
            {
                return new ObjectResult(data ?? result)
                {
                    StatusCode = statusCode,
                    ContentTypes = { bestMediaType }
                };
            }

            return new ObjectResult(result)
            {
                StatusCode = statusCode,
                ContentTypes = { "application/json" }
            };
        }

        private static string GetBestAcceptedMediaType(string accept)
        {
            var supportedTypes = new[]
            {
            "application/json",
            "application/xml",
            "text/csv"
        };

            var best = accept
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(part =>
                {
                    var sections = part.Split(';', StringSplitOptions.RemoveEmptyEntries);

                    var mediaType = sections[0].Trim();

                    var q = 1.0;

                    foreach (var section in sections.Skip(1))
                    {
                        var item = section.Trim();

                        if (item.StartsWith("q=", StringComparison.OrdinalIgnoreCase))
                        {
                            var value = item.Substring(2);

                            if (double.TryParse(
                                value,
                                NumberStyles.Any,
                                CultureInfo.InvariantCulture,
                                out var parsedQ))
                            {
                                q = parsedQ;
                            }
                        }
                    }

                    return new
                    {
                        MediaType = mediaType,
                        Q = q
                    };
                })
                .Where(x => supportedTypes.Contains(x.MediaType) && x.Q > 0)
                .OrderByDescending(x => x.Q)
                .FirstOrDefault();

            return best?.MediaType ?? "application/json";
        }

        private static object? GetData(object result)
        {
            var dataProperty = result.GetType().GetProperty(
                "Data",
                BindingFlags.Public | BindingFlags.Instance
            );

            return dataProperty?.GetValue(result);
        }
    }
}