using System;

namespace Zadanie3
{
    class Program
    {
        static void Main()
        {
            var copier = new Copier();
            copier.PowerOn();

            IDocument doc1 = new PDFDocument("doc1.pdf");
            ((IPrinter)copier).Print(doc1);

            IDocument doc2;
            ((IScanner)copier).Scan(out doc2, IDocument.FormatType.PDF);

            copier.ScanAndPrint();

            Console.WriteLine(copier.Counter);
            Console.WriteLine(copier.PrintCounter);
            Console.WriteLine(copier.ScanCounter);

            var multiDevice = new MultifunctionalDevice();
            multiDevice.PowerOn();

            IDocument doc3 = new TextDocument("fax1.txt");
            ((IFax)multiDevice).SendFax(doc3);

            Console.WriteLine(multiDevice.Counter);
            Console.WriteLine(multiDevice.PrintCounter);
            Console.WriteLine(multiDevice.ScanCounter);
            Console.WriteLine(multiDevice.FaxCounter);
        }
    }
}