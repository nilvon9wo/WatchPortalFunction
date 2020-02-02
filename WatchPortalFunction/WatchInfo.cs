using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WatchPortalFunction {
    public static class WatchInfo {
        [FunctionName("WatchInfo")]
        public static async Task<IActionResult> Run(
               [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest request,
                ILogger logger
            ) {
            logger.LogInformation("C# HTTP trigger function processed a request.");

            string modelId = request?.Query["model"];

            return (modelId != null)
                ? (ActionResult)new OkObjectResult($"Watch Details: " + Stringify(CreateDummyWatch()))
                : new BadRequestObjectResult("Please provide a watch model in the query string");
        }

        private static dynamic CreateDummyWatch() {
            return new {
                Manufacturer = "Abc",
                CaseType = "Solid",
                Bezel = "Titanium",
                Dial = "Roman",
                CaseFinish = "Silver",
                Jewels = 15
            };
        }

        private static string Stringify(dynamic watchInfo) {
            return $"{watchInfo.Manufacturer}, " +
                            $"{watchInfo.CaseType}, " +
                            $"{watchInfo.Bezel}, " +
                            $"{watchInfo.Dial}, " +
                            $"{watchInfo.CaseFinish}, " +
                            $"{watchInfo.Jewels}";
        }
    }
}