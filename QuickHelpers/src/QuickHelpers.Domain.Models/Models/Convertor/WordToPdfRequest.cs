using System.IO;

namespace QuickHelpers.Domain.Models.Models.Convertor
{
    public class WordToPdfRequest : IConvertRequest
    {
        public string FileName { get; }

        public Stream Content { get; }

        public WordToPdfRequest(string fileName, Stream content)
        {
            FileName = fileName;
            Content = content;
        }
    }
}