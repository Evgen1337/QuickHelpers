using System;
using System.IO;
using QuickHelpers.Business.ConvertorService.Strategies;
using QuickHelpers.Domain.Models.Models.Convertor;
using Xunit;

namespace QuickHelpers.Business.Convertors.UnitTests
{
    public class WordToPdfConvertorTest
    {
        [Fact]
        public void Test1()
        {
            using var fileStream = new FileStream("TestFiles/Template.docx", FileMode.Open, FileAccess.Read);
            var wordToPdfConvertor = new WordToPdfConvertor(new WordToPdfRequest("test.docx", fileStream));
            wordToPdfConvertor.Convert();
        }
    }
}