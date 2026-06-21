using Microsoft.AspNetCore.Mvc;

namespace iap.API.Common
{
    public static class ResultExtensions
    {
        // For Result<T> — operations that return data
        public static IActionResult ToActionResult<T>(
            this Result<T> result, ControllerBase controller)
        {
            return result.ResultType switch
            {
                ResultType.Success =>
                    controller.Ok(result.Value),
                ResultType.NotFound =>
                    controller.NotFound(new { Message = result.Error }),
                ResultType.Conflict =>
                    controller.Conflict(new { Message = result.Error }),
                ResultType.ValidationError =>
                    controller.BadRequest(new { Message = result.Error }),
                ResultType.Unauthorized =>
                    controller.Unauthorized(new { Message = result.Error }),
                ResultType.Forbidden =>
                    controller.StatusCode(403, new { Message = result.Error }),
                _ =>
                    controller.StatusCode(500, 
                        new { Message = "An unexpected error occurred." })
            };
        }

        // For Result — operations that dont return data (delete etc)
        public static IActionResult ToActionResult(
            this Result result, ControllerBase controller)
        {
            return result.ResultType switch
            {
                ResultType.Success =>
                    controller.NoContent(),
                ResultType.NotFound =>
                    controller.NotFound(new { Message = result.Error }),
                ResultType.Conflict =>
                    controller.Conflict(new { Message = result.Error }),
                ResultType.ValidationError =>
                    controller.BadRequest(new { Message = result.Error }),
                ResultType.Unauthorized =>
                    controller.Unauthorized(new { Message = result.Error }),
                ResultType.Forbidden =>
                    controller.StatusCode(403, new { Message = result.Error }),
                _ =>
                    controller.StatusCode(500,
                        new { Message = "An unexpected error occurred." })
            };
        }
    }
}