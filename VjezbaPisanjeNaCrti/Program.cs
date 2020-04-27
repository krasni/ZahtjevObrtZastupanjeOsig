using Microsoft.Office.Interop.Word;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;

namespace VjezbaPisanjeNaCrti
{
    class Program
    {
        static void Main(string[] args)
        {
            //string filesFolder = @"D:\Projects\HANFAapp\AutomatskiPDF\";

            //var newFileNameWithoutExtension = String.Format($"Zastupanje_{DateTime.Now.ToString("yyyy-MM-dd_HH_ss")}_{Guid.NewGuid().ToString()}");
            //var newWordFileName = String.Format($"{newFileNameWithoutExtension}.docx");

            //Application word = new Application();
            //Microsoft.Office.Interop.Word.Document doc = word.Documents.Open(filesFolder + "VjezbaWord.docx");
            //doc.Activate();

            ////object control = 1;
            ////doc.SelectContentControlsByTag("Ime").get_Item(ref control).Range.Text = "Saša";
            //Microsoft.Office.Interop.Word.Find fnd = word.ActiveWindow.Selection.Find;

            //fnd.ClearFormatting();
            //fnd.Replacement.ClearFormatting();
            //fnd.Forward = true;

            //fnd.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;

            //fnd.Text = "%Ime%";
            //fnd.Replacement.Text = "Borota Igor, Lastovska 2, odjebite svi.";
            //fnd.Execute(Replace: WdReplace.wdReplaceAll);

            //doc.SaveAs(filesFolder + newWordFileName);
            //doc.Close();

            //doc = null;
            //word.Quit(false);
            //Marshal.ReleaseComObject(word);
            //word = null;

            var pdfFile = @"D:\Projects\HANFAapp\AutomatskiPDF\AutomatskiPDFWebApi\Files\Zastupanje_2020-02-10_15_37_0d77d892-ad4b-4725-8cb3-6f90995c82d6.pdf";



            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            SmtpClient SmtpServer = new SmtpClient("mail2.hanfa.hr");
            mail.From = new MailAddress("noreply@hanfa.hr");
            mail.To.Add("igor.borota@hanfa.hr");
            mail.Subject = "Test Mail - 1";
            mail.Body = "mail with attachment";

            byte[] bytes = System.IO.File.ReadAllBytes(pdfFile);
            Attachment att = new Attachment(new MemoryStream(bytes), "pero.pdf");
            mail.Attachments.Add(att);

            SmtpServer.Send(mail);
        }
    }
}
