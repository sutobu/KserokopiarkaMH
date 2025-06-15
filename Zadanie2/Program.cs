using System;

namespace Zadanie2
{
    class Program
    {
        static void Main()
        {
            var multiDevice = new MultifunctionalDevice();
            multiDevice.PowerOn();

            IDocument doc1 = new PDFDocument("doc1.pdf");
            multiDevice.Print(doc1);

            IDocument doc2;
            multiDevice.Scan(out doc2, IDocument.FormatType.PDF);

            multiDevice.ScanAndPrint();

            IDocument doc3 = new TextDocument("fax1.txt");
            multiDevice.SendFax(doc3);

            Console.WriteLine(multiDevice.Counter);
            Console.WriteLine(multiDevice.PrintCounter);
            Console.WriteLine(multiDevice.ScanCounter);
            Console.WriteLine(multiDevice.FaxCounter);
        }
    }
}