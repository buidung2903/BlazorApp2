using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MediatorPattern
{
    public class ConcreteMediator : IMediator
    {
        private readonly ILogger<ConcreteMediator> _logger;
        private static Logger logger = LogManager.GetLogger("ConcreteMediator");

        public ConcreteMediator(ILogger<ConcreteMediator> logger)
        {
            _logger = logger;
        }

        public void LogInformationAction(dynamic sender, string ev)
        {
            _logger.LogInformation($"Create {ev} with id {sender} successfully!");
        }
    }
}
