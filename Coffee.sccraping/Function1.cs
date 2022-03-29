using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using HtmlAgilityPack;
using System.Net;
using System.Linq;

namespace HAP
{
    public static class HAP
    {
        [FunctionName("HAP")]

        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "hap")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var url = "https://nomada.studio/";
            var web = new HtmlWeb();
            var doc = web.Load(url);

            //var scraping = doc.DocumentNode.SelectNodes("//div")
            //    .Where(imput => imput.HasClass("wpb_text_column")).ToList();

            string xpath = "/html[1]/body[1]/div[1]/div[1]/div[1]/div[2]/main[1]/div[1]/div[1]/div[3]/div[2]/div[1]/div[1]/div[3]";

            var scraping = doc.DocumentNode.SelectSingleNode("//html[1]/body[1]/div[1]/div[1]/div[1]/div[2]/main[1]/div[1]/div[1]/div[3]/div[2]/div[1]/div[1]/div[3]").InnerText.TextoGris();

           // var result = new { Message = scraping };

            return new OkObjectResult(scraping);
            //return new OkObjectResult(doc.Text);
        }
        internal static string TextoGris(this string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return texto;

            return texto.Replace("\n", "").Replace("\t", "").Trim();
        }
    }
}
