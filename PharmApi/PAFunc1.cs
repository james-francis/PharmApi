using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.Text;

namespace PharmApi
{
    public static class PAFunc1
    {
        [FunctionName("PAFunc1")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // parse query parameter
            string action = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "action", true) == 0)
                .Value;

            if (action == null)
            {
                // Get request body
                dynamic data = await req.Content.ReadAsAsync<object>();
                action = data?.action;
            }

            // all responses are canned due to lacking a real pharmacy api to talk to.
            switch (action)
            {
                // determines if a prescription can be filled.
                case "pharmFill":
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(new { message = "will be filled" }), Encoding.UTF8, "application/json")
                    };
                // determines where a prescription is.
                case "pharmFind":
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(new { message = "is delayed" }), Encoding.UTF8, "application/json")
                    };
                // determines when a prescription will be delivered.
                case "pharmWhen":
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(new { message = "will be ready by 4pm" }), Encoding.UTF8, "application/json")
                    };
                // calls a doctor.
                case "pharmCall":
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(new { message = "is called" }), Encoding.UTF8, "application/json")
                    };
                default:
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(new { message = "invalid call to pharmapi" }), Encoding.UTF8, "application/json")
                    };
            }
        }
    }
}
