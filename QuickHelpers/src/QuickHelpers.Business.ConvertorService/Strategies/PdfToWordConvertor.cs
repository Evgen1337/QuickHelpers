using System;
using System.IO.Packaging;
using DocumentFormat.OpenXml.Packaging;
using OpenXmlPowerTools;
using QuickHelpers.Domain.Models.Models.Convertor;

namespace QuickHelpers.Business.ConvertorService.Strategies
{
    public class PdfToWordConvertor : IConvertWorker<PdfToWordRequest>
    {
        public PdfToWordRequest Request { get; }

        public PdfToWordConvertor(PdfToWordRequest request)
        {
            Request = request;
        }

        public IConvertResult Convert()
        {
            throw new NotImplementedException();
        }
    }
}