using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zadanie3;

namespace Zadanie3UnitTests
{
    [TestClass]
    public class CopierTests
    {
        [TestMethod]
        public void Copier_PowerOn_ComponentsOn()
        {
            var copier = new Copier();
            copier.PowerOn();
            Assert.AreEqual(IDevice.State.on, copier.GetState());
        }

        [TestMethod]
        public void Copier_ScanAndPrintWhenOn_IncrementsCounters()
        {
            var copier = new Copier();
            copier.PowerOn();
            copier.ScanAndPrint();
            Assert.AreEqual(1, copier.ScanCounter);
            Assert.AreEqual(1, copier.PrintCounter);
        }
    }

    [TestClass]
    public class MultifunctionalDeviceTests
    {
        [TestMethod]
        public void MultifunctionalDevice_FaxWhenOn_IncrementsCounter()
        {
            var device = new MultifunctionalDevice();
            device.PowerOn();
            IDocument doc = new PDFDocument("test.pdf");
            ((IFax)device).SendFax(doc);
            Assert.AreEqual(1, device.FaxCounter);
        }
    }
}