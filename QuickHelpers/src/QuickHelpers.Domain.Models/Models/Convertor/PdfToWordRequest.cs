using System.IO;

namespace QuickHelpers.Domain.Models.Models.Convertor
{
    public class PdfToWordRequest : IConvertRequest
    {
        public string FileName { get; }

        public Stream Content { get; }

        public PdfToWordRequest(string fileName, Stream content)
        {
            FileName = fileName;
            Content = content;
        }
    }
}