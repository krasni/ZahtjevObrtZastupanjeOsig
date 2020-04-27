using AutomatskiPDFWebApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace AutomatskiPDFWebApi.Controllers
{
    public class DownloadController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(string fileName)
        {
            //Create HTTP Response.
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

            //Set the File Path.
            string filePath = HttpContext.Current.Server.MapPath("~/Bin/Files/") + fileName;

            //Check whether File exists.
            //if (!File.Exists(filePath))
            //{
            //    //Throw 404 (Not Found) exception if File not found.
            //    response.StatusCode = HttpStatusCode.NotFound;
            //    response.ReasonPhrase = string.Format("File not found: {0} .", fileName);
            //    throw new HttpResponseException(response);
            //}

            //Read the File into a Byte Array.
            byte[] bytes = File.ReadAllBytes(filePath);

            return Ok(new PDFByteArray { PDFBytes = bytes});
        }
    }
}
