using AutomatskiPDFMVC.Clients;
using AutomatskiPDFMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutomatskiPDFMVC.Mvc.Attributes;
using RestSharp;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.IO;
using Vereyon.Web;
using log4net;
using Attachment = AutomatskiPDFMVC.HelperClasses.Attachment;

namespace AutomatskiPDFMVC.Controllers
{
    public class PDFPodaciController : Controller
    {
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(PDFPodaciController));
        private readonly IEmailService emailService;

        public PDFPodaciController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        // GET: PDFPodaci
        [HttpPost]
        [ExportModelState]
        public ActionResult KreirajPDF(PodaciPDF podaciPDF)
        {
            PodaciPDFClient client = new PodaciPDFClient(ModelState);
            PathPDF pathToPDF = client.KreirajDokument(podaciPDF);

            if (!ModelState.IsValid)
            {
                return RedirectToAction("KreirajPDF");
            }

            PDFByteArray pdfBytes = client.GetPDFBytes(pathToPDF.PathToPDF);

            log.Info("PosaljiNaEmail:" + podaciPDF.EmailPodaci.PosaljiNaEmail);

            if (podaciPDF.EmailPodaci.PosaljiNaEmail)
            {
                emailService.SendEmail(
                   fromDisplayName: "",
                   fromEmailAddress: "noreply@hanfa.hr",
                   toName: "",
                   toEmailAddress: podaciPDF.EmailPodaci.EmailAdresaZaDostavu,
                   subject: "Generirani PDF dokument",
                   message: "Generirani PDF dokument",
                   attachments: new Attachment(pathToPDF.PathToPDF, pdfBytes.PDFBytes));

                FlashMessage.Confirmation("Dokument je poslan na email: " + podaciPDF.EmailPodaci.EmailAdresaZaDostavu);
                return RedirectToAction("KreirajPDF");
            }
            else
            {
                var downloadCookie = new System.Web.HttpCookie("fileDownloadToken", podaciPDF.DownloadToken)
                {
                    Path = "/ZahtjevObrtZastupanjeOsig"
                };

                Response.AppendCookie(downloadCookie);

                return File(pdfBytes.PDFBytes, "application/pdf", pathToPDF.PathToPDF);
            }
        }

        [HttpGet]
        [ImportModelState]
        public ActionResult KreirajPDF()
        {   
            var PodaciPDF = new PodaciPDF();
            return View(PodaciPDF);
        }
    }
}