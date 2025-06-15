using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zadanie2;

namespace Zadanie2UnitTest
{
    [TestClass]
    public class Zadanie2UnitTest
    {
        [TestMethod]
        public void MultifunctionalDevice_GetState_OffByDefault()
        {
            var device = new MultifunctionalDevice();
            Assert.AreEqual(IDevice.State.off, device.GetState());
        }

        [TestMethod]
        public void MultifunctionalDevice_PowerOn_StateOn()
        {
            var device = new MultifunctionalDevice();
            device.PowerOn();
            Assert.AreEqual(IDevice.State.on, device.GetState());
        }

        [TestMethod]
        public void MultifunctionalDevice_FaxWhenOff_NoEffect()
        {
            var device = new MultifunctionalDevice();
            IDocument doc = new PDFDocument("test.pdf");
            device.SendFax(doc);
            Assert.AreEqual(0, device.FaxCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_FaxWhenOn_IncrementsCounter()
        {
            var device = new MultifunctionalDevice();
            device.PowerOn();
            IDocument doc = new PDFDocument("test.pdf");
            device.SendFax(doc);
            Assert.AreEqual(1, device.FaxCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_ScanWhenOn_IncrementsCounter()
        {
            var device = new MultifunctionalDevice();
            device.PowerOn();
            IDocument doc;
            device.Scan(out doc, IDocument.FormatType.PDF);
            Assert.AreEqual(1, device.ScanCounter);
            Assert.AreEqual("PDFScan0001.pdf", doc.GetFileName());
        }

        [TestMethod]
        public void MultifunctionalDevice_PrintWhenOn_IncrementsCounter()
        {
            var device = new MultifunctionalDevice();
            device.PowerOn();
            IDocument doc = new PDFDocument("test.pdf");
            device.Print(doc);
            Assert.AreEqual(1, device.PrintCounter);
        }
    }
}