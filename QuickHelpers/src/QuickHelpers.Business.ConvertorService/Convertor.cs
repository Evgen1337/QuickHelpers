using QuickHelpers.Business.ConvertorService.Strategies;
using QuickHelpers.Domain.Models.Models.Convertor;

namespace QuickHelpers.Business.ConvertorService
{
    public class Convertor
    {
        public IConvertResult Convert(IConvertWorker<IConvertRequest> convertWorker) =>
            convertWorker.Convert();
    }
}