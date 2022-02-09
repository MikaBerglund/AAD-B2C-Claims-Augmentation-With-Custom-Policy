using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;

namespace ClaimsAugmentor
{
    public class ClaimsFunctions
    {
        [FunctionName(nameof(GetClaims))]
        public Task<HttpResponseMessage> GetClaims([HttpTrigger(AuthorizationLevel.Function, "POST")] HttpRequestMessage request, ILogger log)
        {

            var claims = new Dictionary<string, object>
            {
                { "role", new string[]{ "administrator", "user", "your-custom-role" } },
                { "upn", "firstname.lastname@company.com" }
            };

            var json = System.Text.Json.JsonSerializer.Serialize(claims);

            return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            });
        }
    }
}
