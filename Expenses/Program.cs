using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.IO;

namespace Expenses
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read in file
            string inputFile = ProcessPDF("C:/Users/DEV.KyraT/Desktop/Statement.pdf");

            string[] lines = inputFile.Split('|');

            // Remove unwanted lines
            var elines = lines.Select(x => x.Trim())
                              .Where(x => !(x.Contains("Orig date") && x.Length == 37))
                              .Where(x => x != "")
                              .Where(x => x.Contains('.'))
                              .Where(x => !x.Contains("Opening balance"))
                              .Where(x => !x.Contains("Balance brought forward from previous page"))
                              .Where(x => !x.Contains("Account type"))
                              .Where(x => !x.Contains("Totals at end of page"))
                              .Where(x => !x.Contains("anz.co.nz"))
                              .Where(x => !x.Contains("Go"))
                              .Where(x => !x.Contains("International Money Machine"))
                              .Where(x => !x.Contains("Currency Conversion Charge"));
            

            File.WriteAllLines("C:/Users/DEV.KyraT/Desktop/Output-Statement.txt", elines);
            

            //File.WriteAllText("C:/Users/DEV.KyraT/Desktop/Output-Statement.txt", inputFile);

            // Parse out purchases and save as Purchase items

            // Create item categories

            // Identify common purchases like Supermarkets, Restaurants etc

            // Call Zomato API?

            // Call Splitwise API?

            // Categorise purchase items



            // UI - display expenses by category

            // User input for uncategorised?

            // Display via date range?

        }
        public static string ProcessPDF(string path)
        {
            string input = "";
            PdfReader reader = new PdfReader(path);
            LocationTextExtractionStrategy strategy = new LocationTextExtractionStrategy();

            for (int p = 1; p < reader.NumberOfPages; p++)
            {
                string page = PdfTextExtractor.GetTextFromPage(reader, p, strategy);

                string[] lines = page.Split('\n');

                foreach (string line in lines)
                {
                    input += "|" + line;
                }
            }

            reader.Close();
            return input;
        }
    }
}
