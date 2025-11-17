using Microsoft.EntityFrameworkCore.Storage;
using Concesionario.Exceptions;

namespace Concesionario.WebApi.Configurations
{
	public class ExceptionsMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionsMiddleware> _logger;

		public ExceptionsMiddleware(RequestDelegate next, ILogger<ExceptionsMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}
		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			_logger.LogError(ex, "Error capturado en middlware");
			context.Response.ContentType = "application/json";
			var statusCode = ex switch
			{
				BussinesException => StatusCodes.Status400BadRequest,
				ValidationException => StatusCodes.Status400BadRequest,
				NotFoundException => StatusCodes.Status404NotFound,
				InternalServerException => StatusCodes.Status500InternalServerError
			};
		}
	}
}
