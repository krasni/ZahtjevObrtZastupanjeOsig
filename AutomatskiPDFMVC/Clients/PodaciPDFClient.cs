using AutomatskiPDFMVC.HelperClasses;
using AutomatskiPDFMVC.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AutomatskiPDFMVC.Clients
{
    public class PodaciPDFClient : ClientBase
    {
        public PodaciPDFClient(ModelStateDictionary modelstate) : base(modelstate)
        {
        }

        public PathPDF KreirajDokument(PodaciPDF podaciPDF)
        {
            RestRequest request = new RestRequest("api/Obrt/KreirajDokument", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            request.AddJsonBody(podaciPDF);
            return ExecuteValidation<PathPDF>(request);
        }

        public PDFByteArray GetPDFBytes(string pathToPDF)
        {
            RestRequest request = new RestRequest("api/Download", Method.GET);
            request.AddOrUpdateParameter("fileName", pathToPDF);
            return ExecuteBytes<PDFByteArray>(request);
        }
    }
}

