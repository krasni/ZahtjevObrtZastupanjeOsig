using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintPDF
{
    class Program
    {
        static void Main(string[] args)
        {
            // generate a file name as the current date/time in unix timestamp format
            string file = (string)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString();

            // the directory to store the output.
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // initialize PrintDocument object
            PrintDocument doc = new PrintDocument()
            {
                PrinterSettings = new PrinterSettings()
                {
                    // set the printer to 'Microsoft Print to PDF'
                    PrinterName = "Microsoft Print to PDF",

                    // tell the object this document will print to file
                    PrintToFile = true,

                    // set the filename to whatever you like (full path)
                    PrintFileName = Path.Combine(directory, file + ".pdf"),
                }
            };

            doc.Print();
        }
    }
}
