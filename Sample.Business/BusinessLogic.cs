using System;
using Microsoft.Extensions.Logging;
namespace Sample.Business
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly ILogger<BusinessLogic> _logger;

        public BusinessLogic(ILogger<BusinessLogic> logger)
        {
            this._logger = logger;
        }
        public string[] Summaries()
        {
            var user = "TestUser";
            _logger.LogInformation("Inside the BusinessLogic: Summaries ");
            _logger.LogDebug("Logging by {User}", user);
            return new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };
        }
    }
}
