using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BotCore;
using BotLogger;

namespace Glitch_Bot
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Logger> _logger;

        public Worker(ILogger<Logger> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var bot = new Bot();
                await bot.RunAsync(stoppingToken, _logger);
                await Task.Delay(-1, stoppingToken);
            }
        }
    }
}
