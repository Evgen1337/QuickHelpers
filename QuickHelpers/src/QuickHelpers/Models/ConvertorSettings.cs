using System;
using System.Collections.Generic;

namespace QuickHelpers.Models
{
    public class ConvertorServiceSettings : IServiceSettings
    {
        public Dictionary<string, string> Modes { get; } = new Dictionary<string, string>
        {
            [ConvertMode.PdfToWord.ToString()] = "Pdf в Word",
            [ConvertMode.WordToPdf.ToString()] = "Word в Pdf"
        };

        public string ServiceName { get; } = "Convertors";
    }
}