using System;


namespace Zadanie2
{
    public interface IDevice
    {
        enum State { on, off }

        void PowerOn();
        void PowerOff();
        State GetState();
    }

    public interface IPrinter : IDevice
    {
        void Print(in IDocument doc);
        int PrintCounter { get; }
    }

    public interface IScanner : IDevice
    {
        void Scan(out IDocument doc, IDocument.FormatType format = IDocument.FormatType.JPG);
        int ScanCounter { get; }
    }

    public interface IFax : IDevice
    {
        void SendFax(in IDocument doc);
        int FaxCounter { get; }
    }

    public class Copier : IPrinter, IScanner
    {
        private IDevice.State state = IDevice.State.off;
        private int printCounter = 0;
        private int scanCounter = 0;
        private int counter = 0;

        public int Counter => counter;
        public int PrintCounter => printCounter;
        public int ScanCounter => scanCounter;

        public IDevice.State GetState() => state;

        public void PowerOn()
        {
            if (state == IDevice.State.off)
            {
                state = IDevice.State.on;
                counter++;
            }
        }

        public void PowerOff()
        {
            if (state == IDevice.State.on)
            {
                state = IDevice.State.off;
            }
        }

        public void Print(in IDocument doc)
        {
            if (state != IDevice.State.on) return;

            printCounter++;
            Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss} Print: {doc.GetFileName()}");
        }

        public void Scan(out IDocument doc, IDocument.FormatType format = IDocument.FormatType.JPG)
        {
            doc = null;
            if (state != IDevice.State.on) return;

            scanCounter++;
            string fileName;
            switch (format)
            {
                case IDocument.FormatType.PDF:
                    fileName = $"PDFScan{scanCounter:D4}.pdf";
                    doc = new PDFDocument(fileName);
                    break;
                case IDocument.FormatType.TXT:
                    fileName = $"TextScan{scanCounter:D4}.txt";
                    doc = new TextDocument(fileName);
                    break;
                case IDocument.FormatType.JPG:
                default:
                    fileName = $"ImageScan{scanCounter:D4}.jpg";
                    doc = new ImageDocument(fileName);
                    break;
            }
            Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss} Scan: {fileName}");
        }

        public void ScanAndPrint()
        {
            if (state != IDevice.State.on) return;

            IDocument doc;
            Scan(out doc, IDocument.FormatType.JPG);
            if (doc != null)
            {
                Print(doc);
            }
        }
    }

    public class MultifunctionalDevice : IPrinter, IScanner, IFax
    {
        private IDevice.State state = IDevice.State.off;
        private int printCounter = 0;
        private int scanCounter = 0;
        private int faxCounter = 0;
        private int counter = 0;

        public int Counter => counter;
        public int PrintCounter => printCounter;
        public int ScanCounter => scanCounter;
        public int FaxCounter => faxCounter;

        public IDevice.State GetState() => state;

        public void PowerOn()
        {
            if (state == IDevice.State.off)
            {
                state = IDevice.State.on;
                counter++;
            }
        }

        public void PowerOff()
        {
            if (state == IDevice.State.on)
            {
                state = IDevice.State.off;
            }
        }

        public void Print(in IDocument doc)
        {
            if (state != IDevice.State.on) return;

            printCounter++;
            Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss} Print: {doc.GetFileName()}");
        }

        public void Scan(out IDocument doc, IDocument.FormatType format = IDocument.FormatType.JPG)
        {
            doc = null;
            if (state != IDevice.State.on) return;

            scanCounter++;
            string fileName;
            switch (format)
            {
                case IDocument.FormatType.PDF:
                    fileName = $"PDFScan{scanCounter:D4}.pdf";
                    doc = new PDFDocument(fileName);
                    break;
                case IDocument.FormatType.TXT:
                    fileName = $"TextScan{scanCounter:D4}.txt";
                    doc = new TextDocument(fileName);
                    break;
                case IDocument.FormatType.JPG:
                default:
                    fileName = $"ImageScan{scanCounter:D4}.jpg";
                    doc = new ImageDocument(fileName);
                    break;
            }
            Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss} Scan: {fileName}");
        }

        public void ScanAndPrint()
        {
            if (state != IDevice.State.on) return;

            IDocument doc;
            Scan(out doc, IDocument.FormatType.JPG);
            if (doc != null)
            {
                Print(doc);
            }
        }

        public void SendFax(in IDocument doc)
        {
            if (state != IDevice.State.on) return;

            faxCounter++;
            Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss} Fax: {doc.GetFileName()}");
        }
    }
}