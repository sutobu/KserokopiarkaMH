using System;

namespace Zadanie3
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

    public class Printer : IPrinter
    {
        private IDevice.State state = IDevice.State.off;
        private int printCounter = 0;

        public int PrintCounter => printCounter;

        public IDevice.State GetState() => state;

        public void PowerOn()
        {
            if (state == IDevice.State.off)
            {
                state = IDevice.State.on;
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
    }

    public class Scanner : IScanner
    {
        private IDevice.State state = IDevice.State.off;
        private int scanCounter = 0;

        public int ScanCounter => scanCounter;

        public IDevice.State GetState() => state;

        public void PowerOn()
        {
            if (state == IDevice.State.off)
            {
                state = IDevice.State.on;
            }
        }

        public void PowerOff()
        {
            if (state == IDevice.State.on)
            {
                state = IDevice.State.off;
            }
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
    }

    public class Fax : IFax
    {
        private IDevice.State state = IDevice.State.off;
        private int faxCounter = 0;

        public int FaxCounter => faxCounter;

        public IDevice.State GetState() => state;

        public void PowerOn()
        {
            if (state == IDevice.State.off)
            {
                state = IDevice.State.on;
            }
        }

        public void PowerOff()
        {
            if (state == IDevice.State.on)
            {
                state = IDevice.State.off;
            }
        }

        public void SendFax(in IDocument doc)
        {
            if (state != IDevice.State.on) return;

            faxCounter++;
            Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss} Fax: {doc.GetFileName()}");
        }
    }

    public class Copier : IPrinter, IScanner
    {
        private readonly Printer printer;
        private readonly Scanner scanner;
        private IDevice.State state = IDevice.State.off;
        private int counter = 0;

        public Copier()
        {
            printer = new Printer();
            scanner = new Scanner();
        }

        public int Counter => counter;
        public int PrintCounter => printer.PrintCounter;
        public int ScanCounter => scanner.ScanCounter;

        public IDevice.State GetState() => state;

        public void PowerOn()
        {
            if (state == IDevice.State.off)
            {
                state = IDevice.State.on;
                printer.PowerOn();
                scanner.PowerOn();
                counter++;
            }
        }

        public void PowerOff()
        {
            if (state == IDevice.State.on)
            {
                state = IDevice.State.off;
                printer.PowerOff();
                scanner.PowerOff();
            }
        }

        void IPrinter.Print(in IDocument doc) => printer.Print(doc);
        void IScanner.Scan(out IDocument doc, IDocument.FormatType format) => scanner.Scan(out doc, format);

        public void ScanAndPrint()
        {
            if (state != IDevice.State.on) return;

            IDocument doc;
            ((IScanner)this).Scan(out doc, IDocument.FormatType.JPG);
            if (doc != null)
            {
                ((IPrinter)this).Print(doc);
            }
        }
    }

    public class MultifunctionalDevice : IPrinter, IScanner, IFax
    {
        private readonly Printer printer;
        private readonly Scanner scanner;
        private readonly Fax fax;
        private IDevice.State state = IDevice.State.off;
        private int counter = 0;

        public MultifunctionalDevice()
        {
            printer = new Printer();
            scanner = new Scanner();
            fax = new Fax();
        }

        public int Counter => counter;
        public int PrintCounter => printer.PrintCounter;
        public int ScanCounter => scanner.ScanCounter;
        public int FaxCounter => fax.FaxCounter;

        public IDevice.State GetState() => state;

        public void PowerOn()
        {
            if (state == IDevice.State.off)
            {
                state = IDevice.State.on;
                printer.PowerOn();
                scanner.PowerOn();
                fax.PowerOn();
                counter++;
            }
        }

        public void PowerOff()
        {
            if (state == IDevice.State.on)
            {
                state = IDevice.State.off;
                printer.PowerOff();
                scanner.PowerOff();
                fax.PowerOff();
            }
        }

        void IPrinter.Print(in IDocument doc) => printer.Print(doc);
        void IScanner.Scan(out IDocument doc, IDocument.FormatType format) => scanner.Scan(out doc, format);
        void IFax.SendFax(in IDocument doc) => fax.SendFax(doc);

        public void ScanAndPrint()
        {
            if (state != IDevice.State.on) return;

            IDocument doc;
            ((IScanner)this).Scan(out doc, IDocument.FormatType.JPG);
            if (doc != null)
            {
                ((IPrinter)this).Print(doc);
            }
        }
    }
}