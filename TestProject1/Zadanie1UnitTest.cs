using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zadanie1;

namespace Zadanie1UnitTest
{
    [TestClass]
    public class Zadanie1UnitTest
    {
        [TestMethod]
        public void Copier_GetState_OffByDefault()
        {
            var copier = new Copier();
            Assert.AreEqual(IDevice.State.off, copier.GetState());
        }

        [TestMethod]
        public void Copier_PowerOn_StateOn()
        {
            var copier = new Copier();
            copier.PowerOn();
            Assert.AreEqual(IDevice.State.on, copier.GetState());
        }

        [TestMethod]
        public void Copier_PowerOff_StateOff()
        {
            var copier = new Copier();
            copier.PowerOn();
            copier.PowerOff();
            Assert.AreEqual(IDevice.State.off, copier.GetState());
        }

        [TestMethod]
        public void Copier_PowerOnWhenOn_NoEffect()
        {
            var copier = new Copier();
            copier.PowerOn();
            copier.PowerOn();
            Assert.AreEqual(IDevice.State.on, copier.GetState());
            Assert.AreEqual(1, copier.Counter);
        }

        [TestMethod]
        public void Copier_PowerOffWhenOff_NoEffect()
        {
            var copier = new Copier();
            copier.PowerOff();
            Assert.AreEqual(IDevice.State.off, copier.GetState());
        }

        [TestMethod]
        public void Copier_PrintWhenOff_NoEffect()
        {
            var copier = new Copier();
            IDocument doc = new PDFDocument("test.pdf");
            copier.Print(doc);
            Assert.AreEqual(0, copier.PrintCounter);
        }

        [TestMethod]
        public void Copier_ScanWhenOff_NoEffect()
        {
            var copier = new Copier();
            IDocument doc;
            copier.Scan(out doc);
            Assert.IsNull(doc);
            Assert.AreEqual(0, copier.ScanCounter);
        }

        [TestMethod]
        public void Copier_ScanAndPrintWhenOff_NoEffect()
        {
            var copier = new Copier();
            copier.ScanAndPrint();
            Assert.AreEqual(0, copier.ScanCounter);
            Assert.AreEqual(0, copier.PrintCounter);
        }

        [TestMethod]
        public void Copier_PrintWhenOn_IncrementsCounter()
        {
            var copier = new Copier();
            copier.PowerOn();
            IDocument doc = new PDFDocument("test.pdf");
            copier.Print(doc);
            Assert.AreEqual(1, copier.PrintCounter);
        }

        [TestMethod]
        public void Copier_ScanWhenOn_IncrementsCounter()
        {
            var copier = new Copier();
            copier.PowerOn();
            IDocument doc;
            copier.Scan(out doc);
            Assert.AreEqual(1, copier.ScanCounter);
            Assert.IsNotNull(doc);
            Assert.AreEqual("ImageScan0001.jpg", doc.GetFileName());
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
}

