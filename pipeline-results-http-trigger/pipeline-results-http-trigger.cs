using System;
using System.IO;
using AzureFunctions.Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace pipelineresultshttptrigger
{
	[DependencyInjectionConfig(typeof(AutofacConfig))]
    public staticass pipeline_results_http_trigger
    {
        [FunctionName("pipeline_results_http_trigger")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req,
		                                TraceWriter log, [Inject] ServiceOne serviceOne)
        {         
			string accountSid = Environment.GetEnvironmentVariable("TwilioAccountSid");
			string authToken = Environment.GetEnvironmentVariable("TwilioAuthToken");

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
				body: serviceOne.Execute(),
				from: new PhoneNumber(Environment.GetEnvironmentVariable("FromPhoneNumber")),
				                      to: new PhoneNumber(Environment.GetEnvironmentVariable("ToPhoneNumber")));

            Console.WriteLine(message.Sid);

			return (ActionResult) new OkObjectResult("Did the message send?");
        }
    }
}
