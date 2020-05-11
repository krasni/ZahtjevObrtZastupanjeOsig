using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Http;
using AutomatskiPDFWebApi.Models;
using log4net;
using Microsoft.Office.Interop.Word;
using Xceed.Words.NET;
using Spire.Doc;

namespace AutomatskiPDFWebApi.Controllers
{
    public class ObrtController : ApiController
    {
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(ObrtController));

        [HttpPost]
        [ActionName("KreirajDokument")]
        public IHttpActionResult KreirajDokument(PodaciPDF podaciPDF)
        {
            OsnivacObrt osnivacObrt = podaciPDF.OsnivacObrt;
            Obrt obrt = podaciPDF.Obrt;
            OvlasteniZastupnik ovlasteniZastupnik = podaciPDF.OvlasteniZastupnik;
            KontrolaPovezanosti kontrolaPovezanosti = podaciPDF.KontrolaPovezanosti;
            EmailPodaci emailPodaci = podaciPDF.EmailPodaci;
            DokumentPodaci dokumentPodaci = podaciPDF.DokumentPodaci;

            log.Info("posalji na email:" + podaciPDF.EmailPodaci.PosaljiNaEmail);

            // otvori word document
            string filesFolder = HttpContext.Current.Server.MapPath("~/Bin/Files/");
            string templateFolder = HttpContext.Current.Server.MapPath("~/Bin/Template/");

            string wordTemplateFilePath = templateFolder + "TemplateZastupanje.docx";
            DocX letter = DocX.Load(wordTemplateFilePath);

            // replace stringova
            letter.ReplaceText("%ImeIPrezimeOsnivaca%", String.Format($"{osnivacObrt.Ime} {osnivacObrt.Prezime}"));
            letter.ReplaceText("%AdresaPrebivalista%", String.Format($"{osnivacObrt.Mjesto}, {osnivacObrt.UlicaIKucniBroj}"));
            letter.ReplaceText("%OIBOsnivaca%", String.Format($"{osnivacObrt.OIB}"));
            letter.ReplaceText("%TvrtkaObrta%", String.Format($"{obrt.Naziv}, {obrt.Oznaka.ToLower()}, {obrt.ImeObrtnika} {obrt.PrezimeObrtnika}, {obrt.Mjesto}, {obrt.UlicaIKucniBroj}"));

            // zastupnik
            if (ovlasteniZastupnik.KontrolaOvlastenja == true)
            {
                letter.ReplaceText("%ImeIPrezimeOsnivacaZastupnik%", String.Format($"{osnivacObrt.Ime} {osnivacObrt.Prezime}"));
                letter.ReplaceText("%OvlasteniZastupnik%", String.Format($"{ovlasteniZastupnik.Ime} {ovlasteniZastupnik.Prezime}, " +
                    $"{ ovlasteniZastupnik.Mjesto}, { ovlasteniZastupnik.UlicaIKucniBroj}, OIB: {ovlasteniZastupnik.Oib}"));
                letter.ReplaceText("%RH%", ovlasteniZastupnik.BrojURegistruHanfe);

                letter.ReplaceText("%OdgovornaOsoba%", String.Format($"{ovlasteniZastupnik.Ime} {ovlasteniZastupnik.Prezime}"));
            }
            else
            {
                letter.ReplaceText("%ImeIPrezimeOsnivacaZastupnik%", "");
                letter.ReplaceText("%OvlasteniZastupnik%","");
                letter.ReplaceText("%RH%", "");

                letter.ReplaceText("%OdgovornaOsoba%", String.Format($"{osnivacObrt.Ime} {osnivacObrt.Prezime}"));
            }

            // kontrola povezanosti
            if (kontrolaPovezanosti.KontrolaPovlastenosti == true)
            {
                letter.ReplaceText("%ImeIPrezimeOsnivacaPov%", String.Format($"{osnivacObrt.Ime} {osnivacObrt.Prezime}"));

                string povezanostText = "";

                if (kontrolaPovezanosti.FizickeOsobe.Any())
                {
                    foreach(var fizickaOsoba in kontrolaPovezanosti.FizickeOsobe)
                    {
                        povezanostText += String.Format($"{fizickaOsoba.Ime} {fizickaOsoba.Prezime}, " +
                        $"{ fizickaOsoba.Mjesto}, { fizickaOsoba.UlicaIKucniBroj}, OIB: {fizickaOsoba.OIB}, ");
                    }
                }

                if (kontrolaPovezanosti.PravneOsobe.Any())
                {
                    foreach (var pravnaOsoba in kontrolaPovezanosti.PravneOsobe)
                    {
                        povezanostText += String.Format($"{pravnaOsoba.Naziv}, " +
                       $"{ pravnaOsoba.Mjesto}, { pravnaOsoba.UlicaIKucniBroj}, OIB: {pravnaOsoba.OIB}, ");
                    }
                }

                letter.ReplaceText("%Povezanost%", povezanostText.Remove(povezanostText.Length - 2));
            }
            else
            {
                letter.ReplaceText("%Povezanost%", "");
                letter.ReplaceText("%ImeIPrezimeOsnivacaPov%", "");
            }

            // fali podatke za mjesto
            letter.ReplaceText("%DatumMjesto%", String.Format($"U {dokumentPodaci.MjestoIzrade} (mjesto), dana {dokumentPodaci.DatumIzrade.ToString("dd.MM.yyyy")} (datum)."));

            // potpis
            letter.ReplaceText("%PodaciOsnivac%", String.Format($"{osnivacObrt.Ime} {osnivacObrt.Prezime}, {osnivacObrt.KontaktBroj}, {osnivacObrt.Email}"));

            //snimi word pod drugim imenom sa datumom današnjim i guidom novim
            var newFileNameWithoutExtension = String.Format($"Zastupanje_{DateTime.Now.ToString("yyyy-MM-dd_HH_ss")}_{Guid.NewGuid().ToString()}");
            var newWordFileName = String.Format($"{newFileNameWithoutExtension}.docx");
            var newPdfFileName = String.Format($"{newFileNameWithoutExtension}.pdf");

            letter.SaveAs(filesFolder + newWordFileName);

            // otvori novi word sa word interop i snimi ga kao pdf i zapamti file name od pdf-a
            log.Info("Prije izrade nove word applikacije");

            Spire.Doc.Document doc = new Spire.Doc.Document();
            doc.LoadFromFile(filesFolder + newWordFileName);
            doc.SaveToFile(filesFolder + newPdfFileName, FileFormat.PDF);

            //Application word = new Application
            //{
            //    DisplayAlerts = WdAlertLevel.wdAlertsNone
            //};

            log.Info("Nakon stvaranja word instance");

            //Document doc = word.Documents.Open(filesFolder + newWordFileName);
            //doc.Activate();
            //doc.SaveAs2(filesFolder + newPdfFileName, WdSaveFormat.wdFormatPDF);
            //doc.Close();

            //doc = null;
            //word.Quit(false);
            //Marshal.ReleaseComObject(word);
            //word = null;

            return Ok(new PathPDF { PathToPDF = newPdfFileName });
        }
    }
}
