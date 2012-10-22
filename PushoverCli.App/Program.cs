using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using NLog;
using RestSharp;

namespace PushoverCli.App
{
    class Program
    {
        static void Main(string[] args)
        {
            //all api parameters can be used, but must be formatted exactly as they occur in the api.
            List<Parameter> parameters = args.Select(arg => arg.Split('='))
                .Select(keyValuePair => new Parameter
                                            {
                                                Name = keyValuePair[0],
                                                Value = keyValuePair[1],
                                                Type = ParameterType.GetOrPost
                                            }).ToList();
            
            //allow for override, but default to configuration
            if (!parameters.Any(x => x.Name == "user"))
                parameters.Add(new Parameter { Name = "user", Value = ConfigurationManager.AppSettings.Get("UserToken"), Type = ParameterType.GetOrPost });
            if (!parameters.Any(x => x.Name == "token"))
                parameters.Add(new Parameter { Name = "token", Value = ConfigurationManager.AppSettings.Get("ApplicationToken"), Type = ParameterType.GetOrPost });
            
            var restClient = new RestClient(string.Format("https://api.pushover.net/{0}/",ConfigurationManager.AppSettings.Get("ApiVersion")));
            var request = new RestRequest("messages.json", Method.POST);
            request.RequestFormat = DataFormat.Json;
            parameters.ForEach(x => request.Parameters.Add(x));
            
            var response = restClient.Execute<Dictionary<string,string>>(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                foreach (var error in response.Data)
                {
                    LogManager.GetLogger("PushoverCli").Error(error.Key + " : " + error.Value);
                }
            }
        }
    }
}
