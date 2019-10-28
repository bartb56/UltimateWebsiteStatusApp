using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace UltimateWebsiteStatus
{
    public class Worker : BackgroundService
    {
        private HttpClient client;

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            client = new HttpClient();
            return base.StartAsync(cancellationToken);
        }

        protected static List<string> WebsiteUrls = new List<string>() { "", "", "" }; //Enter URL's in this list

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                foreach(string url in WebsiteUrls)
                {
                    var result = await client.GetAsync(url);
                    if (!result.IsSuccessStatusCode)
                    {
                        string errorMessage = String.Format("The website {1} is down with errror code: {0}", result.StatusCode, url);
                        var sender = new MailSender();
                        await sender.SendMail(errorMessage);
                    }
                }
                await Task.Delay(300 * 1000, stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            client.Dispose();
            return base.StopAsync(cancellationToken);
        }
    }
}
