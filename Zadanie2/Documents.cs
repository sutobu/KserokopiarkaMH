using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie2
{
    public interface IDocument
    {
        enum FormatType { TXT, PDF, JPG }

        string GetFileName();
        void SetFileName(string fileName);
    }

    public class PDFDocument : IDocument
    {
        private string fileName;

        public PDFDocument(string fileName)
        {
            this.fileName = fileName;
        }

        public string GetFileName() => fileName;
        public void SetFileName(string fileName) => this.fileName = fileName;
    }

    public class TextDocument : IDocument
    {
        private string fileName;

        public TextDocument(string fileName)
        {
            this.fileName = fileName;
        }

        public string GetFileName() => fileName;
        public void SetFileName(string fileName) => this.fileName = fileName;
    }

    public class ImageDocument : IDocument
    {
        private string fileName;

        public ImageDocument(string fileName)
        {
            this.fileName = fileName;
        }

        public string GetFileName() => fileName;
        public void SetFileName(string fileName) => this.fileName = fileName;
    }
}