using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WatchPortalFunction {
    public static class Function1 {

        [FunctionName("Function1")]
        public static IActionResult Run(
                [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest request,
                ILogger logger
            ) {
            logger.LogInformation("C# HTTP trigger function processed a request.");

            string requestBodyReader = new StreamReader(request.Body)
                .ReadToEnd();

            string name = (string)request.Query["name"]
                ?? ((dynamic)JsonConvert.DeserializeObject(requestBodyReader))?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
