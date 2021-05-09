using System.Threading.Tasks;
using QuickHelpers.Domain.Models.Models.Convertor;

namespace QuickHelpers.Business.ConvertorService.Strategies
{
    public interface IConvertWorker<out TRequest> where TRequest : IConvertRequest
    {
        TRequest Request { get; }

        IConvertResult Convert();
    }
}