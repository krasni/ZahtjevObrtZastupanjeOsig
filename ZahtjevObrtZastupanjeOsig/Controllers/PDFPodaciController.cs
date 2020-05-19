using ZahtjevObrtZastupanjeOsig.Clients;
using ZahtjevObrtZastupanjeOsig.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZahtjevObrtZastupanjeOsig.Mvc.Attributes;
using RestSharp;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.IO;
using Vereyon.Web;
using log4net;
using Attachment = ZahtjevObrtZastupanjeOsig.HelperClasses.Attachment;
using Spire.Doc;

namespace ZahtjevObrtZastupanjeOsig.Controllers
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

        [HttpGet]
        [ImportModelState]
        public ActionResult KreirajPDF()
        {   
            var PodaciPDF = new PodaciPDF();
            return View(PodaciPDF);
        }

        [HttpPost]
        [ExportModelState]
        public ActionResult KreirajPDF(PodaciPDF podaciPDF)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("KreirajPDF");
            }

            OsnivacObrt osnivacObrt = podaciPDF.OsnivacObrt;
            Obrt obrt = podaciPDF.Obrt;
            OvlasteniZastupnik ovlasteniZastupnik = podaciPDF.OvlasteniZastupnik;
            KontrolaPovezanosti kontrolaPovezanosti = podaciPDF.KontrolaPovezanosti;
            EmailPodaci emailPodaci = podaciPDF.EmailPodaci;
            DokumentPodaci dokumentPodaci = podaciPDF.DokumentPodaci;

            // otvori word document
            string filesFolder = System.Web.HttpContext.Current.Server.MapPath("~/Bin/Files/");
            string templateFolder = System.Web.HttpContext.Current.Server.MapPath("~/Bin/Template/");

            Document document = new Document();

            document.LoadFromFile(Path.Combine(templateFolder, "TemplateZastupanje.docx"));

            document.Replace("%ImeIPrezimeOsnivaca%", String.Format($"{osnivacObrt.Ime} {osnivacObrt.Prezime}"), true, false);

            // replace stringova
            document.Replace("%ImeIPrezimeOsnivaca%", String.Format($"{osnivacObrt.Ime} {osnivacObrt.Prezime}"), true, false);
            document.Replace("%AdresaPrebivalista%", String.Format($"{osnivacObrt.Mjesto}, {osnivacObrt.UlicaIKucniBroj}"), true, false);
            document.Replace("%OIBOsnivaca%", String.Format($"{osnivacObrt.OIB}"), true, false);
            document.Replace("%TvrtkaObrta%", String.Format($"{obrt.Naziv}, {obrt.Oznaka.ToLower()}, {obrt.ImeObrtnika} {obrt.PrezimeObrtnika}, {obrt.Mjesto}, {obrt.UlicaIKucniBroj}"), true, false);

            // zastupnik
            if (ovlasteniZastupnik.KontrolaOvlastenja == true)
            {
                document.Replace("%ImeIPrezimeOsnivacaZastupnik%", String.Format($"{osnivacObrt.Ime} {osnivacObrt.Prezime}"), true, false);
                document.Replace("%OvlasteniZastupnik%", String.Format($"{ovlasteniZastupnik.Ime} {ovlasteniZastupnik.Prezime}, " +
                    $"{ ovlasteniZastupnik.Mjesto}, { ovlasteniZastupnik.UlicaIKucniBroj}, OIB: {ovlasteniZastupnik.Oib}"), true, false);
                document.Replace("%RH%", ovlasteniZastupnik.BrojURegistruHanfe, true, false);

                document.Replace("%OdgovornaOsoba%", String.Format($"{ovlasteniZastupnik.Ime} {ovlasteniZastupnik.Prezime}"), true, false);
            }
            else
            {
                document.Replace("%ImeIPrezimeOsnivacaZastupnik%", "", true, false);
                document.Replace("%OvlasteniZastupnik%", "", true, false);
                document.Replace("%RH%", "", true, false);

                document.Replace("%OdgovornaOsoba%", String.Format($"{osnivacObrt.Ime} {osnivacObrt.Prezime}"), true, false);
            }

            // kontrola povezanosti
            if (kontrolaPovezanosti.KontrolaPovlastenosti == true)
            {
                document.Replace("%ImeIPrezimeOsnivacaPov%", String.Format($"{osnivacObrt.Ime} {osnivacObrt.Prezime}"), true, false);

                string povezanostText = "";

                if (kontrolaPovezanosti.FizickeOsobe != null && kontrolaPovezanosti.FizickeOsobe.Any())
                {
                    foreach (var fizickaOsoba in kontrolaPovezanosti.FizickeOsobe)
                    {
                        povezanostText += String.Format($"{fizickaOsoba.Ime} {fizickaOsoba.Prezime}, " +
                        $"{ fizickaOsoba.Mjesto}, { fizickaOsoba.UlicaIKucniBroj}, OIB: {fizickaOsoba.OIB}, ");
                    }
                }

                if (kontrolaPovezanosti.PravneOsobe != null && kontrolaPovezanosti.PravneOsobe.Any())
                {
                    foreach (var pravnaOsoba in kontrolaPovezanosti.PravneOsobe)
                    {
                        povezanostText += String.Format($"{pravnaOsoba.Naziv}, " +
                       $"{ pravnaOsoba.Mjesto}, { pravnaOsoba.UlicaIKucniBroj}, OIB: {pravnaOsoba.OIB}, ");
                    }
                }

                document.Replace("%Povezanost%", povezanostText.Remove(povezanostText.Length - 2), true, false);
            }
            else
            {
                document.Replace("%Povezanost%", "", true, false);
                document.Replace("%ImeIPrezimeOsnivacaPov%", "", true, false);
            }

            // fali podatke za mjesto
            document.Replace("%DatumMjesto%", String.Format($"U {dokumentPodaci.MjestoIzrade} (mjesto), dana {dokumentPodaci.DatumIzrade.ToString("dd.MM.yyyy")} (datum)."), true, false);

            // potpis
            document.Replace("%PodaciOsnivac%", String.Format($"{osnivacObrt.Ime} {osnivacObrt.Prezime}, {osnivacObrt.KontaktBroj}, {osnivacObrt.Email}"), true, false);

            var newFileNameWithoutExtension = String.Format($"Zastupanje_{DateTime.Now.ToString("yyyy-MM-dd_HH_ss")}_{Guid.NewGuid().ToString()}");
            var newPdfFileName = String.Format($"{newFileNameWithoutExtension}.pdf");

            document.SaveToFile(filesFolder + newPdfFileName, FileFormat.PDF);

            using (MemoryStream stream = new MemoryStream())
            {
                document.SaveToStream(stream, FileFormat.PDF);

                if (podaciPDF.EmailPodaci.PosaljiNaEmail)
                {
                    log.Info("PosaljiNaEmail:" + podaciPDF.EmailPodaci.PosaljiNaEmail);

                    emailService.SendEmail(
                       fromDisplayName: "",
                       fromEmailAddress: "noreply@hanfa.hr",
                       toName: "",
                       toEmailAddress: podaciPDF.EmailPodaci.EmailAdresaZaDostavu,
                       subject: "Generirani PDF dokument",
                       message: "Generirani PDF dokument",
                       attachments: new Attachment(newPdfFileName, stream.ToArray()));

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

                    log.Info($"Download PDF-a: {newPdfFileName}");

                    return File(stream.ToArray(), "application/pdf", newPdfFileName);
                }
            }
        }
    }
}
