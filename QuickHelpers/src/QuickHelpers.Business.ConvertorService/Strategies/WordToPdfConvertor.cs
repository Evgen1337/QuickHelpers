using System;
using System.IO;
using System.IO.Packaging;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using OpenXmlPowerTools;
using QuickHelpers.Domain.Models.Models.Convertor;

namespace QuickHelpers.Business.ConvertorService.Strategies
{
    public class WordToPdfConvertor : IConvertWorker<WordToPdfRequest>
    {
        public WordToPdfRequest Request { get; }

        public WordToPdfConvertor(WordToPdfRequest request)
        {
            Request = request;
        }

        public IConvertResult Convert()
        {
            using var memoryContent = new MemoryStream();
            Request.Content.CopyTo(memoryContent);
            
            using var source = Package.Open(memoryContent, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            using var document = WordprocessingDocument.Open(source);
            
            var settings = new HtmlConverterSettings();
            var html = HtmlConverter.ConvertToHtml(document, settings);

            Console.WriteLine(html.ToString());
            return null;
        }
    }
}