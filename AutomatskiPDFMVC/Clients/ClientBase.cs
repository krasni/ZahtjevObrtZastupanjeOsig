using AutomatskiPDFMVC.Extensions;
using AutomatskiPDFMVC.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AutomatskiPDFMVC.Clients
{
    public class ClientBase : RestClient
{
    private ModelStateDictionary _modelstate;

    public ClientBase(ModelStateDictionary modelstate) : base(Constants.ApiUrl)
    {
        _modelstate = modelstate;
    }

    public new void Execute(IRestRequest request)
    {
        var response = base.Execute(request);
        var parsedObj = JObject.Parse(response.Content);
        var apiResponse = JsonConvert.DeserializeObject<ResponsePackage>(parsedObj.ToString());
        if (apiResponse.HasErrors)
        {
            AddErrors(apiResponse);
        }
    }

    public  T ExecuteValidation<T>(IRestRequest request) where T : new()
    {
        var response = base.Execute(request);
        var parsedObj = JObject.Parse(response.Content);
        var apiResponse = JsonConvert.DeserializeObject<ResponsePackage>(parsedObj.ToString());
        if (apiResponse.HasErrors)
        {
            AddErrors(apiResponse);
            return default(T);
        }
        return response.Extract<T>();
    }

    public T ExecuteBytes<T>(IRestRequest request) where T : new()
    {
        var response = base.Execute(request);
        return response.Extract<T>();
    }

        private void AddErrors(ResponsePackage response)
    {
        List<string> listMessagesAdded = new List<string>();
        for (int i = 0; i < response.Errors.Count; i++)
        {
            if (listMessagesAdded.Contains(response.Errors[i])) continue;
            _modelstate.AddModelError("error" + i.ToString(), response.Errors[i]);
            listMessagesAdded.Add(response.Errors[i]);
        }
    }
}
}
